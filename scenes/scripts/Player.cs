using Godot;
using System;

public class Player : Node2D
{

	Button B1=new Button();
	public override void _Ready()
	{
	B1=GetNode<Button>("S1/B1");
	B1.Connect("pressed", this, "On_B1_Pressed");
		
	}
	void On_B1_Pressed(){
	
	
	}
    void MoveIt(Vector2 pos,int a){

	GD.Print(a);
	Position=pos;
    }
  

}
