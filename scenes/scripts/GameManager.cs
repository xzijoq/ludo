using Godot;
using System;
using Godot.Collections;
public class GameManager : Node2D
{
    PackedScene Board_I = GD.Load<PackedScene>( "res://scenes/Board.tscn" );
    Node2D      Board;

    int           PlayerCount = 1;
    Array<Node2D> Player      = new Godot.Collections.Array<Node2D>();
    PackedScene   Player_I = GD.Load<PackedScene>( "res://scenes/Player.tscn" );
    Global_I      G2;

    [Signal]
    public delegate void MovePlayer1(int piece,Vector2 pos);

    public override void _Ready()
    {
        G2       = ( Global_I ) GetNode( "/root/GlobalI" );
        Board       = Board_I.Instance() as Node2D;
        AddChild( Board );
        InitPlayers();
	MovePlayer(0, 1);
	Player[0].Call("MoveIt", G2.Posi[G2.LudoBoard[3]],12);
    }

    void InitPlayers()
    {
        Player.Resize( 1 );
        for ( var i = 0; i < PlayerCount; i++ ) {
            Player[i] = Player_I.Instance() as Node2D;

            Player[i].Scale = G2.Scale_L;

            AddChild( Player[i] );
        }
    }
    void MovePlayer( int pNum, int block )
    {
           Player[pNum].Position = G2.Posi[G2.LudoBoard[block]];
    }
}





