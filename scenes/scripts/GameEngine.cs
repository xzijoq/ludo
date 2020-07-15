using Godot;
using System;

public class GameEngine : Node2D
{



    [Signal]
    public delegate void MovePiece(int Pl, int Pi, int Sq);

    struct pInfo
    {
        public int player;
        public int piece;
        pInfo(int what)
        {
            player = -1;
            piece = -1;
        }
    }
    const int Sp=    Global_I.START_POSI;

    pInfo[] Board_I = new pInfo[72];
    int Turn = 0;
    public int[,] Players_GI =
        new int[4, 4]{ {Sp,Sp,Sp,Sp },
                       {Sp,Sp,Sp,Sp },
                       {Sp,Sp,Sp,Sp },
                       {Sp,Sp,Sp,Sp } };              // 4 players with 4 pieces
                                                      // public int[] InternalLudoBoard_GI = new int[72];  // outer upto 51
    Random DiceR = new Random();



    int tempMove = 2;


    int InputSelected(int player, int piece)
    {
        if (Players_GI[player, piece] == Global_I.START_POSI)
        {
            MovePlayer(player, piece, Global_I.StartSquare[player]);

            Board_I[Global_I.StartSquare[player]].player = player;

            Board_I[Global_I.StartSquare[player]].piece = piece;
            return Global_I.StartSquare[player];
        }

        int From = Players_GI[player, piece];

        int To = From + 2;



        if (Board_I[To].player != player && Board_I[To].player != -1)
        {
            MovePlayer(Board_I[To].player, Board_I[To].piece, 225);

        }






        MovePlayer(player, piece, To);
        Board_I[To].player = player;
        Board_I[To].piece = piece;
        Board_I[From].player = -1;
        Board_I[From].piece = -1;


        return To;
    }

    void MovePlayer(int player, int piece, int to)
    {

        Players_GI[player, piece] = to;
                    GD.Print(to);
        EmitSignal("MovePiece", player, piece, to);
    }
}
