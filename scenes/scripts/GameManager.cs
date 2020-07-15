using Godot;
using System;
using Godot.Collections;
public class GameManager : Node2D
{
    // vars
    PackedScene   Board_I = GD.Load<PackedScene>( "res://scenes/Board.tscn" );
    Node2D        Board;
    Array<Node2D> Player   = new Godot.Collections.Array<Node2D>();
    PackedScene   Player_I = GD.Load<PackedScene>( "res://scenes/Player.tscn" );
    Script        pl = GD.Load<Script>( "res://scenes/scripts/Player.cs" );
    Node2D        GameEngine;
    Control       tempB;
    Global_I      G2;
    GameEngine    G1;
    int           turn;
    [Signal]
    public delegate void MovePlayerTo( int playerNumber, int piece,
                                       int square );

    public override void _Ready()
    {
        tempB = GetNode( "Hud" ) as Control;
        tempB.Connect( "pressed", this, "tempB_Pressed" );

        G2         = ( Global_I ) GetNode( "/root/GlobalI" );
        Board      = Board_I.Instance() as Node2D;
        GameEngine = GetNode( "GameEngine" ) as Node2D;
        AddChild( Board );

        InitPlayers();
        //       EmitSignal( "MovePlayerTo", 0, 0, 33 );

        //	EmitSignal("MovePlayerTo", 1,0,25);
    }
    void tempB_Pressed()
    {
        int[] temp;
        temp = GameEngine.Call( "TempMove" ) as int[];
        //      GD.Print( temp[0] );
	GD.Print(temp);

        EmitSignal( "MovePlayerTo", 0, 0, temp[3] );
    }

    // initPlayers
    void InitPlayers()
    {
        for ( int i = 0; i < GetChildCount(); i++ ) {
            if ( GetChild( i ) is Player ) {
                Player.Add( this.GetChild( i ) as Node2D );
            }
        }

        for ( var i = 0; i < Player.Count; i++ ) {
            Player[i].Scale = G2.Scale_L;
            Player [i]
                .Set( "PlayerNumber", i );
            Connect( "MovePlayerTo", Player[i], "MoveTo_S" );
            Player [i]
                .Connect( "PieceClicked_M", this, "PieceClicked_F" );
        }

        // temp init start posi
        Player[0].Position = new Vector2( G2.Posi[G2.LudoBoard[50]].x - 64 * 2,
                                          G2.Posi[G2.LudoBoard[50]].y );
        Player[1].Position = new Vector2( G2.Posi[G2.LudoBoard[4]].x + 64 * 2,
                                          G2.Posi[G2.LudoBoard[4]].y );
        Player[2].Position = new Vector2( G2.Posi[G2.LudoBoard[31]].x - 64 * 2,
                                          G2.Posi[G2.LudoBoard[31]].y );
        Player[3].Position = new Vector2( G2.Posi[G2.LudoBoard[23]].x + 64 * 2,
                                          G2.Posi[G2.LudoBoard[23]].y );
    }
    void PieceClicked_F( Vector2 pApData )

    {
        int Sq;
        //	GD.Print(pApData);
        Sq = ( int ) GameEngine.Call( "InputSelected", pApData[0], pApData[1] );
        EmitSignal( "MovePlayerTo", pApData[0], pApData[1], Sq );
    }
}
