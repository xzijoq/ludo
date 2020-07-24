using Godot;
using System;
using Godot.Collections;

public class Global_I : Godot.Node
{
	public Vector2 SC_SZ = Vector2.Zero;
	public Vector2 Scale_L = Vector2.Zero;
	public const int START_POSI = 72;
	public const int END_POSI = 73;

	public const int MAX_ROW = 15;

	public const int MAX_COL = 15;
	public const int SPRITE_SIZE_CELL = 64;

	public const int MAX_PLAYERS = 8;
	public const int MAX_PIECE = 16;//?
	public const int MAX_DICE_FACES=6;
	public Array<Vector2> Posi = new Godot.Collections.Array<Vector2>();



	//--boardLudo
	public static readonly int[] LudoBoard = {
		6, 7, 8, 23, 38, 53, 68, 83,                                     // 0-7
		99, 100, 101, 102, 103, 104, 119, 134, 133, 132, 131, 130, 129,  // 8-28
		143, 158, 173, 188, 203, 218, 217, 216, 201, 186, 171, 156,
		141,                                                        // 21-33
		125, 124, 123, 122, 121, 120, 105, 90, 91, 92, 93, 94, 95,  // 34-46
		81, 66, 51, 36, 21,                                         // 47-51
		// inSideSafe
		22, 37, 52, 67, 82,       // 52-56
		118, 117, 116, 115, 114,  // 57-61
		202, 187, 172, 157, 142,  // 62-66
		106, 107, 108, 109, 110,  // 67-71
		START_POSI,END_POSI


	};

	public static readonly int[] SafeSquare =
		{ 3, 11, 16, 24, 29, 37, 42, 50 ,START_POSI,END_POSI};
	public static readonly int[] StartSquare = { 3, 6, 29, 42 };
	public static readonly int[] SwitchSquare = { 1, 14, 27, 40 };
	public static readonly int[] SwitchIntoSq = { 52, 57, 62, 67 };
	public static readonly int[] EndSquare = { 56, 61, 66, 71 };

}
