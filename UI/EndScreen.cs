using Godot;
using System;

public class EndScreen : Control
{
    // Member variables here, example:
    // private int a = 2;
    // private string b = "textvar";

    public override void _Ready()
    {
        // Called every time the node is added to the scene.
        // Initialization here
        
    }

    private void _on_Timer_timeout()
    {
        var global = (Global) GetNode("/root/Global");
        GetTree().ChangeScene(global.StartScreen);
    }
//    public override void _Process(float delta)
//    {
//        // Called every frame. Delta is time since last frame.
//        // Update game logic here.
//        
//    }
}
