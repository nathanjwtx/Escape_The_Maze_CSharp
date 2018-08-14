using Godot;
using System;

public class StartScreen : Control
{
    
    public override void _Ready()
    {
    
    }

    public override void _Input(InputEvent @event)
    {
        base._Input(@event);
        if (@event.IsActionPressed("ui_select"))
        {
            var global = (Global) GetNode("/root/Global");
            global.NewGame();
        }
    }
}
