using Godot;
using System;
using Godot.Collections;

public class Global_I : Godot.Node
{
    public Vector2 SC_SZ = Vector2.Zero;
    public Vector2 Scale_L = Vector2.Zero;
    public const int START_POSI = 225;

    public const int MAX_PLAYERS = 8;
    public const int MAX_PIECE = 16;//?
    public Array<Vector2> Posi = new Godot.Collections.Array<Vector2>();

    public override void _Ready() { Init__Data(); }

    void Init__Data()
    {
        float CellSize = 0.0f;
        const int ROW = 15;
        const int COL = 15;
        const int SPixel = 64;

        Posi.Resize((ROW * COL) + 1);
        SC_SZ = GetViewport().Size;

        float ShortLen = SC_SZ.x < SC_SZ.y ? SC_SZ.x : SC_SZ.y;
        CellSize = ShortLen / ROW;
        Scale_L = new Vector2(CellSize / SPixel, CellSize / SPixel);

        float XOffset = (CellSize / 2);
        float YOffset = (CellSize / 2) + (SC_SZ.y / 4);

        for (int i = 0, k = 0; i < ROW; i++)
        {
            for (int j = 0; j < COL; j++, k++)
            {
                Posi[k] = new Vector2(CellSize * j + XOffset, CellSize * i + YOffset);

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
    //--boardLudo
    public int[] LudoBoard = {
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
        225


    };

    public static readonly int[] SafeSquare = { 3, 11, 16, 24, 29, 37, 42, 50 };
    public static readonly int[] StartSquare = { 3, 16, 29, 42 };
    public static readonly int[] SwitchSquare = { 1, 14, 27, 40 };
    public static readonly int[] EndSquare = { 56, 61, 66, 71 };

}
