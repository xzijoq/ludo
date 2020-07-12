using Godot;
using System;
using Godot.Collections;

public class Board : Node2D
{
    //--lol

    PackedScene   Cell_I = GD.Load<PackedScene>( "res://scenes/Cell.tscn" );
    Array<Sprite> Cell   = new Godot.Collections.Array<Sprite>();

    Global_I             G2;
    public override void _Ready()
    {
        G2 = ( Global_I ) GetNode( "/root/GlobalI" );
        InitBoard();
    }

    //--initBoard
    void InitBoard()
    {
        Cell.Resize( G2.LudoBoard.Length );


	
        for ( var l = 0; l < G2.LudoBoard.Length; l++ ) {
            var k            = G2.LudoBoard[l] ;
            Cell[l]          = Cell_I.Instance() as Sprite;
            Cell[l].Position = G2.Posi[k];
            Cell[l].Scale    = G2.Scale_L;

            Cell [l]
                .Call( "SetId", l );
            AddChild( Cell[l] );
        }
    }
}
