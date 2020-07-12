using Godot;
using System;
using Godot.Collections;

public class Global_I : Godot.Node
{
    public Vector2        SC_SZ   = Vector2.Zero;
    public Vector2        Scale_L = Vector2.Zero;
    public Array<Vector2> Posi    = new Godot.Collections.Array<Vector2>();

    public override void _Ready() { Init__Data(); }

    
    void          Init__Data()
    {
        float     CellSize = 0.0f;
        const int ROW      = 15;
        const int COL      = 15;
        const int SPixel   = 64;

        Posi.Resize( ROW * COL );
        SC_SZ = GetViewport().Size;

        float ShortLen = SC_SZ.x < SC_SZ.y ? SC_SZ.x : SC_SZ.y;
        CellSize       = ShortLen / ROW;
        Scale_L        = new Vector2( CellSize / SPixel, CellSize / SPixel );

        float XOffset = ( CellSize / 2 );
        float YOffset = ( CellSize / 2 ) + ( SC_SZ.y / 4 );

        for ( int i = 0, k = 0; i < ROW; i++ ) {
            for ( int j = 0; j < COL; j++, k++ ) {
                Posi[k] = new Vector2( CellSize * j + XOffset,
                                       CellSize * i + YOffset );
            }
        }
    }
    //--boardLudo
    public int[] LudoBoard = {
        // index starts at 1 adjust it

        6, 7, 8, 23, 38, 53, 68, 83, 99, 100, 101, 102, 103, 104, 119, 134, 133,
        132, 131, 130, 129, 143, 158, 173, 188, 203, 218, 217, 216, 201, 186,
        171, 156, 141, 125, 124, 123, 122, 121, 120, 105, 90, 91, 92, 93, 94,
        95, 81, 66, 51, 36, 21,
        // inSideSafe
        22, 37, 52, 67, 82, 118, 117, 115, 116, 114, 142, 157, 172, 187, 202,
        106, 107, 108, 109, 110 };
}
