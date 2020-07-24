using Godot;
using System;
using Godot.Collections;
public class GameManager : Node2D
{
	// vars

	Node2D Board;
	Array<Node2D> Player = new Godot.Collections.Array<Node2D>();
	PackedScene Player_I = GD.Load<PackedScene>("res://scenes/Player.tscn");
	Script pl = GD.Load<Script>("res://scenes/scripts/Player.cs");
	Node2D GameEngine;
	Control Dice;
	Global_I G2;


	GameEngine G1;
 
	[Signal]
	public delegate void MovePlayerTo(int playerNumber, int piece,
									   int square);

	public override void _Ready()
	{
		Dice = GetNode("Dice") as Control;
		Dice.Connect("pressed", this, "GetDIceRoll");

		G2 = (Global_I)GetNode("/root/GlobalI");
		Board = GetNode("Board") as Node2D;
		GameEngine = GetNode("GameEngine") as Node2D;
		GameEngine.Connect("MovePiece", this, "MovePiece_M");

		InitPlayers();

	}

	void MovePiece_M(int player, int piece, int square)
	{
		EmitSignal("MovePlayerTo", player, piece, square);

	}

	void PieceClicked_F(Vector2 pApData)

	{
		int Sq;
		//  	GD.Print(pApData);
		Sq = (int)GameEngine.Call("InputSelected", pApData[0], pApData[1]);
		//  EmitSignal("MovePlayerTo", pApData[0], pApData[1], Sq);
	}
	void InitPlayers()
	{
		for (int i = 0; i < GetChildCount(); i++)
		{
			if (GetChild(i) is Player)
			{
				Player.Add(this.GetChild(i) as Node2D);
			}
		}
		// temp init start posi
		Player[3].Position = new Vector2(G2.Posi[Global_I.LudoBoard[50]].x - 64 * 2,
										  G2.Posi[Global_I.LudoBoard[50]].y);
		Player[0].Position = new Vector2(G2.Posi[Global_I.LudoBoard[4]].x + 64 * 2,
										  G2.Posi[Global_I.LudoBoard[4]].y);
		Player[2].Position = new Vector2(G2.Posi[Global_I.LudoBoard[31]].x - 64 * 2,
										  G2.Posi[Global_I.LudoBoard[31]].y);
		Player[1].Position = new Vector2(G2.Posi[Global_I.LudoBoard[23]].x + 64 * 2,
										  G2.Posi[Global_I.LudoBoard[23]].y);

 
		for (var i = 0; i < Player.Count; i++)
		{
			Player[i].Scale = G2.Scale_L ;

			Player[i]
				.Set("PlayerNumber", i);
			Connect("MovePlayerTo", Player[i], "MoveTo_S");
			Player[i]
				.Connect("PieceClicked_M", this, "PieceClicked_F");
				
		}



	}
}
