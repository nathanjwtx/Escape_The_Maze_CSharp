using Godot;
using System;

public class Level2 : Level
{
    // Member variables here, example:
    // private int a = 2;
    // private string b = "textvar";

    public override void _Ready()
    {
        _enemyCount = 5;
        base._Ready();
    }
}
