using Godot;
using System;
using System.Collections.Generic;

public class Global : Node
{
    private List<string> Levels = new List<string>
    {
        "res://Levels/Level1.tscn"
    };

    public string StartScreen = "res://UI/StartScreen.tscn";
    public string EndScreen = "res://UI/EndScreen.tscn";

    private int CurrentLevel;
    private int Score;

    public override void _Ready()
    {
//        Viewport root = GetTree().GetRoot();

    }

    public void NewGame()
    {
        CurrentLevel = -1;
        Score = 0;
        NextLevel();
    }

    public void GameOver()
    {
        GetTree().ChangeScene("EndScene");
    }

    public void NextLevel()
    {
        CurrentLevel += 1;
        if (CurrentLevel >= Levels.Count)
        {
            GameOver();
        }
        else
        {
            GetTree().ChangeScene(Levels[CurrentLevel]);
        }
    }
//    public override void _Process(float delta)
//    {
//        // Called every frame. Delta is time since last frame.
//        // Update game logic here.
//        
//    }
}
