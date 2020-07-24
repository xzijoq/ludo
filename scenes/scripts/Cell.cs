 
using Godot;
using System;

public class Cell : Sprite
{
	int id=0;
	public override void _Ready()
	{
		
	}
	public void SetId(int newId){
	this.id=newId;

	GetNode<Label>("B1").Text=newId.ToString();
//	GD.Print(newId);

	
	}


}
