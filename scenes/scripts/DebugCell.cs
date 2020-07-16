using Godot;
using System;
using Godot.Collections;

public class DebugCell : Sprite
{

    Array<RichTextLabel> Tx=new Godot.Collections.Array<RichTextLabel>();

    public override void _Ready()
    {
        Tx.Resize(GetChildCount());


        for(int i=0;i<GetChildCount();i++){


            Tx[i]=GetChild(i) as RichTextLabel;
        }
        
    }
    public void PrintS(int quad, string text1){

        Tx[quad].Text=text1;
    }


}
