using Godot;
using System;

public class Hud : Control
{

    [Signal]
    public delegate void pressed();

    Label Roll;
    public override void _Ready()
    {Roll=GetNode("RollShow")as Label;
        
    }

    void _on_Button_pressed(){
	EmitSignal("pressed");
    }

    void UpdateRoll(int roll){

        Roll.Text=roll.ToString();
    }
}
