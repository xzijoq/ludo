using Godot;
using System;

public class GameEngine : Node2D
{
    // Todo Pack it in an int the values are trivialy small
    int[] ActionList = new int[16];
    // first is the num of arguments *3 foollowd by data

    [Signal]
    public delegate void DoStuff( int Pl, int Pi, int Sq );

    int[] Bord_I = new int[72];
    int Turn;
    public int[, ] Players_GI =
        new int[4, 4]{ { 1, 1, 1, 1 },
                       { 1, 1, 1, 1 },
                       { 1, 1, 1, 1 },
                       { 1, 1, 1, 1 } };              // 4 players with 4 pieces
    public int[] InternalLudoBoard_GI = new int[72];  // outer upto 51
    public int[] SafeSquare_GI        = { 3, 11, 16, 24, 29, 37, 42, 50 };
    public int[] StartSquare_GI       = { 3, 16, 29, 42 };
    public int[] SwitchSquare_GI      = { 1, 14, 27, 40 };
    public int[] EndSquare_GI         = { 56, 61, 66, 71 };

    Random DiceR = new Random();

    int[] TempMove()
    {
        ActionList[0] = 1;
        ActionList[1] = 0;  // player

        int a =2;

        ActionList[2] = 2;                              // piece
        ActionList[3] = ( a + Players_GI[0, 0] ) % 71;  // square
        Players_GI[0, 0] += a;
        Turn++;

        return ActionList;
    }

    int InputSelected( int Player, int Piece )
    {
        int a = 4;  // DiceR.Next(1,7);
        int b = ( a + Players_GI[Player, Piece] ) % 71;

        Players_GI[Player, Piece] += a;
        GD.Print( Players_GI[Player, Piece] );
        return b;
    }
}
