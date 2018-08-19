using Godot;
using System;
using System.Collections.Generic;

public class Global : Node
{
    public List<string> Levels = new List<string>
    {
        "res://Levels/Level1/Level1.tscn",
        "res://Levels/Level2/Level2a.tscn",
        "res://Levels/Level3/Level3.tscn"
    };

    public string StartScreen = "res://UI/StartScreen.tscn";
    public string EndScreen = "res://UI/EndScreen.tscn";

    public int CurrentLevel;
    public int Score;

    public Node CurrentScene { get; set; }
    
    public Viewport Root;
    
    public override void _Ready()
    {
        Root = GetTree().GetRoot();
    }
    
    public void NewGame()
    {
        CurrentLevel = -1;
        CurrentScene = Root.GetChild(Root.GetChildCount() - 1);
        Score = 0;
        GotoScene();
    }

    public void GameOver()
    {
        GetTree().ChangeScene(EndScreen);
    }

    public void GotoScene()
    {
        CallDeferred(nameof(NextLevel));
    }
    
    public void NextLevel()
    {
        CurrentLevel += 1;
        GD.Print(CurrentLevel);
        if (CurrentLevel >= Levels.Count)
        {
            GameOver();
        }
        else
        {
            CurrentScene.Free();   
            var nextScene = (PackedScene) GD.Load(Levels[CurrentLevel]);
            CurrentScene = nextScene.Instance();
            GetTree().GetRoot().AddChild(CurrentScene);
            GetTree().SetCurrentScene(CurrentScene);
        }
    }
}
