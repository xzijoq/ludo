using Godot;
using System;

public class Piece : Sprite
{
    Global_I G2;

    int    PieceNumber;
    int    ParentPlayerNumber;
    int    Square;
    Button B1;

    [Signal]
    public delegate int PieceClicked();

    public override void _Ready()
    {
        G2 = ( Global_I ) GetNode( "/root/GlobalI" );
        B1 = ( Button ) GetNode( "B1" );
        B1.Connect( "pressed", this, "on_Button_pressed" );
    }
    void MoveTo( int pieceNumber, int square )
    {
        if ( PieceNumber == pieceNumber ) {
            this.GlobalPosition = G2.Posi[G2.LudoBoard[square]];

            Square = square;
        }
    }
    void on_Button_pressed()
    {
        //	GD.Print(PieceNumber);
        //      MoveTo( PieceNumber, Square + 1 );
        EmitSignal( "PieceClicked", PieceNumber );
    }
}
