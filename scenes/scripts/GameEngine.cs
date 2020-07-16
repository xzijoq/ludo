using Godot;
using System;
using Godot.Collections;


public class GameEngine : Node2D
{

    [Signal]
    public delegate void MovePiece(int Pl, int Pi, int Sq);
    struct PackedPiece
    {
        public int Player;
        public int Piece;
    }

    struct P_Info
    {
        public int PlCount;
        public PackedPiece[] Pie;


    }
    const int LpL = Global_I.MAX_PLAYERS + 1;
    const int LpE = Global_I.MAX_PIECE + 1;



    const int Sp = Global_I.START_POSI;

    P_Info[] B_I = new P_Info[74];

    int Turn = 0;
    int[,] Players_GI =
        new int[4, 4]{ {Sp,Sp,Sp,Sp },
                       {Sp,Sp,Sp,Sp },
                       {Sp,Sp,Sp,Sp },
                       {Sp,Sp,Sp,Sp } };              // 4 players with 4 pieces
                                                      // public int[] InternalLudoBoard_GI = new int[72];  // outer upto 51
    Random DiceR = new Random();


    ////////////////-ready------------------------------------
    public override void _Ready()
    {
        CellD.Resize(B_I.Length);
        G2 = (Global_I)GetNode("/root/GlobalI");
        for (int i = 0; i < B_I.Length; i++)
        {

            B_I[i].Pie = new PackedPiece[Global_I.MAX_PIECE];
            B_I[i].PlCount = 0;
        }

        B_I[72].PlCount = 16;
        for (int i = 0, k = 0; i < 4; i++)
        {

            for (int j = 0; j < 4; j++, k++)
            {
                B_I[72].Pie[k].Player = i;
                B_I[72].Pie[k].Piece = j;
                //string lol = B_I[72].Pie[k].Player + " " + B_I[72].Pie[k].Piece;
                //GD.Print(lol);
            }
        }




        DebugBoard.Resize(15 * 15 + 4);
        Draw_DebugBoard();
        DebugTheBoard();

    }
    int InputSelected(int player, int piece)
    {

        if (Players_GI[player, piece] == Global_I.START_POSI)
        {
            //   Players_GI[player, piece] = 72;


            MovePlayer(player, piece, Global_I.StartSquare[player]);

            return Global_I.StartSquare[player];
        }

        else
        {
            int From = Players_GI[player, piece];

            int To = (From + 1) % 71;
            MovePlayer(player, piece, To);
            return To;
        }
    }

    int GCount(int sq)//GetCount
    {
        return B_I[sq].PlCount;
    }
    PackedPiece GetPP(int sq, int index)
    {
        return B_I[sq].Pie[index];
    }



    int lol_Recur=0;
    void MovePlayer(int player, int piece, int to)
    {

        PackedPiece PP;
        PP.Player = -22;
        PP.Piece = -11;
        int IndexInSafe = -123;
        int From = Players_GI[player, piece];
        int To = to;
        bool IsFromSafe = false;
        bool IsToSafe = false;
        for (int i = 0; i < Global_I.SafeSquare.Length; i++)
        {
            if (Global_I.SafeSquare[i] == To) { IsToSafe = true; }
            if (Global_I.SafeSquare[i] == From) { IsFromSafe = true; }
        }


        //ADJUST FROM SQUARE
        //get index of the moving piece
        for (int i = 0; i < GCount(From); i++)
        {
            PP = GetPP(From, i);
            if (PP.Player == player && PP.Piece == piece)
            {
                IndexInSafe = i;

            }
        }
        if (IndexInSafe == -123) { GD.Print("Boy are we FUCKED"); }

        //remove the piece from the from array, -1 cuz 0 indexing
        B_I[From].Pie[IndexInSafe] = B_I[From].Pie[GCount(From) - 1];
        //Substract the PieceCount
        B_I[From].PlCount--;
        //END ADJUST FROM SQUARE


        //ADJUST THE TO SQUARE

        if (!IsToSafe)
        {
            if (GCount(to) < 0) { GD.PushError("NEGETIVE COUNT"); }

            //CASE ONE THE TO CELL IS EMPTY
            if (GCount(To) == 0)
            {
                BoardBlockUpdate(To, GCount(To), player, piece);


            }
            //END CASE ONE

            //CASE TWO TO CELL IS NOT EMPTY
            else
            {
                //CASE FIRST To has same player
                if (GetPP(To, 0).Player == player)
                {
                    BoardBlockUpdate(To, GCount(To), player, piece);

                }
                //END CASE FIRST
                //CASE SECOND ENEMY ON CELL
                else
                {// Send all the players on this cell to home recursively
                    lol_Recur=B_I[To].PlCount;
                    for (int i = 0; i < lol_Recur; i++)
                    {
                        GD.Print(lol_Recur);
                        GD.Print("i:"+i);
                        MovePlayer(GetPP(To, i).Player, GetPP(To, i).Piece, Global_I.START_POSI);
                    }
                    GD.Print("final " + GCount(To));
                    //Add This Player To that Block
                    BoardBlockUpdate(To, GCount(To), player, piece);


                }
                //END SECOND CASE
            }
            //END CASE TWO

        }
        else if (IsToSafe)
        {

            BoardBlockUpdate(To, GCount(To), player, piece);
            if (To == Global_I.START_POSI)
            {

            }


        }
        Players_GI[player, piece] = to;
        EmitSignal("MovePiece", player, piece, to);





    }
    //MOVE PLAYER
    void MovePlayera(int player, int piece, int to)
    {

        int From = Players_GI[player, piece];


        //UPDATE BOARD-----------------UPDATE BOARD----------------------
        //**************************************************************
        bool isSafeTo = false;
        bool isSafeFrom = false;

        foreach (var i in Global_I.SafeSquare)
        {
            if (to == i) { isSafeTo = true; }
        }
        foreach (var i in Global_I.SafeSquare)
        {
            if (Players_GI[player, piece] == i) { isSafeFrom = true; }
        }

        //if not safe loop through all the pieces in that block and 
        //delete and resuffel(or team ..ahm)


        if (isSafeFrom)
        {

            //Find the position of the piece that is moveing from the safe square
            if (B_I[From].PlCount > 0)
            {

                int Posi = -123;

                for (int i = 0; i < B_I[From].PlCount; i++)
                {

                    if (B_I[From].Pie[i].Player == player && B_I[From].Pie[i].Piece == piece)
                    {

                        Posi = i;
                        GD.Print(Posi);
                    }

                }

                if (Posi == -123) { GD.Print("WereFUCKED BIG TIME"); }
                B_I[From].Pie[Posi] = B_I[From].Pie[B_I[From].PlCount - 1];
            }

        }

        //currently only 1 player can occupy a no safe square
        //reshuffel the array 
        //add current piece

        if (!isSafeTo)
        {

            //if square contains other player CAPTURE
            if (B_I[to].Pie[0].Player != player)
            {
                //Send all those pieces Home
                for (int i = 0; i < B_I[to].PlCount; i++)
                {
                    //The StartPosi/ Home is a safe sqare so it wont infi loop
                    MovePlayer(B_I[to].Pie[i].Player, B_I[to].Pie[i].Piece, Global_I.START_POSI);
                }
                //add this piece to the now empty board

                BoardBlockUpdate(to, 0, player, piece);

            }
            //if saquare contains only this player REGULAR MOVE
            else
            {
                // add this piece too
                BoardBlockUpdate(to, B_I[to].PlCount, player, piece);
                //  B_I[From].PlCount--;
                //Write for if from was safe

                //CLear Previous block
            }

        }

        if (isSafeTo)
        {
            //just add this one


            GD.Print("to: " + to + " posi: " + B_I[to].PlCount + " player :" + player + piece);
            BoardBlockUpdate(to, B_I[to].PlCount, player, piece);

            //    this.BoardBlockUpdate(to, B_I[to].PlCount, player, piece);


        }





        //----------------------UDATE PLAYER1----------------



        //-----------------------END UPDATE 1--------------
        B_I[From].PlCount--;

        //UPDATE PLAYER

        Players_GI[player, piece] = to;

        EmitSignal("MovePiece", player, piece, to);

    }



    void BoardBlockUpdate(int sq, int posi, int player, int piece)
    {
        //   GD.Print("BoardBlockUpdate");

        B_I[sq].Pie[posi].Player = player;
        B_I[sq].Pie[posi].Piece = piece;
        B_I[sq].PlCount++;
    }






    //----------------DDDDDDDDD EEEEEEEEEEE BBBBBBBBBBB UUUUUUUUUUUU GGGGGGGGGGGGGGG----------





    void _on_DebugTimer_timeout()
    {

        PrintDebugData();
        PrintPlayerData();
    }
    void PrintDebugData()

    {
        for (int i = 0; i < 52; i++)
        {


            DPsq(i);


        }
        DPsq(72);


    }
    void DPsq(int i)
    {
        int j = i;
        if (i == 72) { j = 53; }
        string q0 = "" + i + "-" + B_I[i].PlCount.ToString() +
             "\nP" + B_I[i].Pie[0].Player.ToString() +
             "-" + B_I[i].Pie[0].Piece.ToString();

        string q1 = ":" + B_I[i].Pie[1].Player.ToString() +
                      "-" + B_I[i].Pie[1].Piece.ToString() +
                        "\nP" + B_I[i].Pie[2].Player.ToString() +
                        "-" + B_I[i].Pie[2].Piece.ToString() ;

        string q2 = ":" + B_I[i].Pie[4].Player.ToString() +
                      "-" + B_I[i].Pie[4].Piece.ToString() +
                        "\nP" + B_I[i].Pie[5].Player.ToString() +
                        "-" + B_I[i].Pie[5].Piece.ToString() ;

        string q3 = "X" + B_I[i].Pie[7].Player.ToString() +
                      "-" + B_I[i].Pie[7].Piece.ToString() +
                        "\nX" + B_I[i].Pie[8].Player.ToString() +
                        "-" + B_I[i].Pie[8].Piece.ToString() ;
        CellD[j]
            .Call("PrintS", 0, q0);
        CellD[j]
            .Call("PrintS", 1, q1);
        CellD[j]
            .Call("PrintS", 2, q2);
        CellD[j]
            .Call("PrintS", 3, q3);

    }

    void PrintPlayerData()
    {
        int block = 54;

        for (int i = 0; i < 4; i++)
        {

            for (int j = 0; j < 4; j++)
            {
                string deg = "P" + i + "-" + j + "\n"
                            + Players_GI[i, j];
                CellD[block].Call("PrintS", j, deg);

            }
            block++;
        }
    }
    void PrintDebugBoard(int sq)
    {
        string dis = "" + sq + "-" + B_I[sq].PlCount.ToString() +
                     "\nP" + B_I[sq].Pie[0].Player.ToString() +
                     "-" + B_I[sq].Pie[0].Piece.ToString()


                     ;
        CellD[sq]
            .Call("PrintS", 1, dis);





    }
    PackedScene Cell_I = GD.Load<PackedScene>("res://scenes/DebugCell.tscn");
    Array<Sprite> CellD = new Godot.Collections.Array<Sprite>();
    Global_I G2;
    Array<Vector2> DebugBoard = new Godot.Collections.Array<Vector2>();
    void Draw_DebugBoard()
    {

        Vector2 SC_SZ;

        float CellSize = 0.0f;
        const int ROW = 8;
        const int COL = 9;
        const int SPixel = Global_I.SPRITE_SIZE_CELL;

        // G2.Posi.Resize((ROW * COL) + 1);
        SC_SZ = GetViewport().Size;

        float ShortLen = SC_SZ.x < SC_SZ.y ? SC_SZ.x : SC_SZ.y;

        //ShortLen=600;
        CellSize = ShortLen / 8;
        G2.Scale_L = new Vector2(CellSize / SPixel, CellSize / SPixel);

        float XOffset = 600 + (CellSize / 2);
        float YOffset = (CellSize / 2);

        for (int i = 0, k = 0; i < ROW; i++)
        {
            for (int j = 0; j < COL; j++, k++)
            {
                DebugBoard[k] = new Vector2(CellSize * j + XOffset, CellSize * i + YOffset);

            }
        }


    }
    void DebugTheBoard()
    {



        for (var l = 0; l < B_I.Length; l++)
        {

            CellD[l] = Cell_I.Instance() as Sprite;
            CellD[l].Position = DebugBoard[l];
            CellD[l].Scale = G2.Scale_L;
            AddChild(CellD[l]);


        }


    }






}


