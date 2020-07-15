using Godot;
using System;
using Godot.Collections;

public class Player : Node2D
{
    Button B1 = new Button();

    int              PlayerNumber;
    [Export] Texture t1;
    // Global_I         G2;
    [Signal]
    public delegate void MovePiece( int pieceNumber, int square );

    [Signal]
    public delegate Vector2 PieceClicked_M();

    Array<Sprite> S = new Godot.Collections.Array<Sprite>();

    public override void _Ready()
    {
        // G2 = ( Global_I ) GetNode( "/root/GlobalI" );
        GetSpritesRef();
        SetSprites( t1 );
    }

    void MoveTo_S( int playerNumber, int piece, int pos )
    {
        if ( playerNumber == PlayerNumber ) {
            EmitSignal( "MovePiece", piece, pos );
        }
    }

    void SetStartPosition( Vector2 sPosi ) {}
    void GetSpritesRef()
    {
        for ( int i = 0; i < GetChildCount(); i++ ) {
            if ( GetChild( i ) is Piece ) {
                S.Add( this.GetChild( i ) as Sprite );
            }
        }
        for ( int i = 0; i < S.Count; i++ ) {
            S [i]
                .Set( "PieceNumber", i );
            S [i]
                .Connect( "PieceClicked", this, "PieceClicked_P" );
            Connect( "MovePiece", S[i], "MoveTo" );
        }
    }

    public void SetSprites( Texture texture )
    {
        if ( texture != null ) {
            foreach ( var i in S ) { i.Texture = texture; }
        }
    }

    void PieceClicked_P( int pieceNumber )
    {
        EmitSignal( "PieceClicked_M",
                    new Vector2( PlayerNumber, pieceNumber ) );
    }
}
