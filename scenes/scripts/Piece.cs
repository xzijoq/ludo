using Godot;
using System;

public class Piece : Sprite
{
	Global_I G2;

	int PieceNumber;
	int ParentPlayerNumber;
	int Square;
	Button B1;

	[Signal]
	public delegate int PieceClicked();

	public override void _Ready()
	{
		G2 = (Global_I)GetNode("/root/GlobalI");
		B1 = (Button)GetNode("B1");
		B1.Connect("pressed", this, "on_Button_pressed");
	}
	void MoveTo(int pieceNumber, int square)
	{

		if (PieceNumber == pieceNumber)
		{
			if (square == Global_I.START_POSI)
			{

				switch (PieceNumber)
				{
					case 0:
						GlobalPosition = GetParent<Node2D>().Position;
						break;
					case 1:
						GlobalPosition = GetParent<Node2D>().Position + new Vector2(72, 0);

						break;
					case 2:
						GlobalPosition = GetParent<Node2D>().Position + new Vector2(0, 72);
						break;
					case 3:
						GlobalPosition = GetParent<Node2D>().Position + new Vector2(72,72);
						break;

					default:
						GD.Print("NEED TO ADJUST STARING POSI IF ADDING MORE PIECES");
						break;

				}

			}
			else
			{
				this.GlobalPosition = G2.Posi[Global_I.LudoBoard[square]];

				Square = square;
			}
		}

	}
	void on_Button_pressed()
	{
		//	GD.Print(PieceNumber);
		//      MoveTo( PieceNumber, Square + 1 );
		EmitSignal("PieceClicked", PieceNumber);
	}
}
