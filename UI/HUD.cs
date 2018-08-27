using Godot;
using System;

public class HUD : CanvasLayer
{

    private Label _score;
    private Global _global;
    
    public override void _Ready()
    {
        _global = (Global) GetNode("/root/Global");
        _score = GetNode<Label>("MarginContainer/ScoreLabel");
        _score.Text = Convert.ToString(_global.Score);
    }

    public void UpdateScore(int value)
    {
        _global.Score += value;
        _score.Text = Convert.ToString(_global.Score);
    }
}
