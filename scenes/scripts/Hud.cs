using Godot;
using System;

public class Hud : Control
{

    [Signal]
    public delegate void pressed();
    public override void _Ready()
    {
        
    }

    void _on_Button_pressed(){
	EmitSignal("pressed");
    }
}
