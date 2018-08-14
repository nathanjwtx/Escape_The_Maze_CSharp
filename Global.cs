using Godot;
using System;
using System.Collections.Generic;

public class Global : Node
{
    public List<string> Levels = new List<string>
    {
        "res://Levels/Level1/Level1.tscn",
        "res://Levels/Level2/Level2.tscn"
    };

    public string StartScreen = "res://UI/StartScreen.tscn";
    public string EndScreen = "res://UI/EndScreen.tscn";

    private int CurrentLevel;
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
        GotoScene(Levels[0]);
//        GotoScene();
    }

    public void GameOver()
    {
        GetTree().ChangeScene(EndScreen);
    }

    public void GotoScene(string path)
    {
        CallDeferred(nameof(NextLevel), path);
    }
    
    public void NextLevel(string path)
    {
//        CurrentLevel += 1;
        GD.Print(CurrentLevel);
        if (CurrentLevel >= Levels.Count)
        {
            GameOver();
        }
        else
        {
//            if (CurrentLevel> -1)
//            {
                CurrentScene.Free();   
//            }
//            CurrentLevel += 1;
//            GD.Print($"Next Level: {CurrentLevel}");
//            GD.Print($"Next Level: {Levels[CurrentLevel]}");
            var nextScene = (PackedScene) GD.Load(path);
            CurrentScene = nextScene.Instance();
            GetTree().GetRoot().AddChild(CurrentScene);
            GetTree().SetCurrentScene(CurrentScene);
        }
    }
}
