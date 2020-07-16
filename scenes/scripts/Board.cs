using Godot;
using System;
using Godot.Collections;

public class Board : Node2D
{
    //--lol

    PackedScene Cell_I = GD.Load<PackedScene>("res://scenes/Cell.tscn");
    Array<Sprite> Cell = new Godot.Collections.Array<Sprite>();
    Texture SafeCell = GD.Load<Texture>("res://assets/PlayerIcons/safecell.png");

    Global_I G2;
    public override void _Ready() 
    {
        G2 = (Global_I)GetNode("/root/GlobalI");
        InitBaseBoard();
   //      DebugBoard();
        InitBoard();

    }
    void InitBaseBoard(){

        float CellSize = 0.0f;
        const int ROW = Global_I.MAX_ROW;
        const int COL = Global_I.MAX_COL;
        const int SPixel =Global_I.SPRITE_SIZE_CELL;

        G2.Posi.Resize((ROW * COL) + 1);
        G2.SC_SZ = GetViewport().Size;
        G2.SC_SZ.x=600;
        float ShortLen = G2.SC_SZ.x < G2.SC_SZ.y ? G2.SC_SZ.x : G2.SC_SZ.y;

        //ShortLen=600;
        CellSize = ShortLen / ROW;
        G2.Scale_L = new Vector2(CellSize / SPixel, CellSize / SPixel);

        float XOffset = (CellSize / 2);
        float YOffset = (CellSize / 2) + (G2.SC_SZ.y / 4);

        for (int i = 0, k = 0; i < ROW; i++)
        {
            for (int j = 0; j < COL; j++, k++)
            {
                G2.Posi[k] = new Vector2(CellSize * j + XOffset, CellSize * i + YOffset);

            }
        }
        for (int i = ROW - 1, k = 0; i >= 0; i--)
        {
            for (int j = 0; j < COL; j++, k++)
            {
                //90 degree
                //    Posi[k] = new Vector2(CellSize * i + XOffset, CellSize * j + YOffset);
            }
        }
        for (int i = ROW - 1, k = 0; i >= 0; i--)
        {
            for (int j = COL - 1; j >= 0; j--, k++)
            {
                //180degree
                // Posi[k] = new Vector2(CellSize * j + XOffset, CellSize * i + YOffset);
            }
        }
        for (int i = 0, k = 0; i < ROW; i++)
        {
            for (int j = COL - 1; j >= 0; j--, k++)
            {
                //270degree
                // Posi[k] = new Vector2(CellSize * i + XOffset, CellSize * j + YOffset);
            }
        }

    }

    //--initBoard
    void InitBoard()
    {
        Cell.Resize(Global_I.LudoBoard.Length);
        for (var l = 0; l < Global_I.LudoBoard.Length; l++)
        {
          //  G2.LudoBoard[l] += 1;

        }
        for (var l = 0; l < Global_I.LudoBoard.Length; l++)
        {
            var k = Global_I.LudoBoard[l];
            Cell[l] = Cell_I.Instance() as Sprite;
            Cell[l].Position = G2.Posi[k];
            Cell[l].Scale = G2.Scale_L;

            Cell[l]
                .Call("SetId", l);
            AddChild(Cell[l]);
        }
        foreach (var i in Global_I.SafeSquare)
        {
            if(i!=225)
            Cell[i].Texture = SafeCell;

        }


    }
    void DebugBoard()
    {

        Cell.Resize(G2.Posi.Count);



        for (var l = 0; l < G2.Posi.Count; l++)
        {
            //var k            = G2.LudoBoard[l] ;
            Cell[l] = Cell_I.Instance() as Sprite;
            Cell[l].Position = G2.Posi[l];
            Cell[l].Scale = G2.Scale_L;
            Cell[l]
                     .Call("SetId", l);


            AddChild(Cell[l]);
        }
    }
}
