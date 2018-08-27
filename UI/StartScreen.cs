using Godot;
using System;

public class StartScreen : Control
{
    private Global _global;
    
    public override void _Ready()
    {
        _global = (Global) GetNode("/root/Global");
        Label hs = GetNode<Label>("HighScore");
        hs.Text = $"High Score: {_global.Highscore}";
    }

    public override void _Input(InputEvent @event)
    {
        base._Input(@event);
        if (@event.IsActionPressed("ui_select"))
        {
            _global.NewGame();
        }
    }
}
