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
    public string ScoreFile = "user://highscore.txt";

    private int _currentLevel;
    public int Score;
    public int Highscore;

    public Node CurrentScene { get; set; }
    
    public Viewport Root;
    
    public override void _Ready()
    {
        Root = GetTree().GetRoot();
        var screenSize = OS.GetScreenSize();
        var windowSize = OS.GetWindowSize();
        OS.SetWindowPosition(screenSize * 0.5f - windowSize * 0.5f);
    }
    
    public void NewGame()
    {
        _currentLevel = -1;
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
        _currentLevel += 1;
        GD.Print(_currentLevel);
        if (_currentLevel >= Levels.Count)
        {
            GameOver();
        }
        else
        {
            CurrentScene.Free();   
            var nextScene = (PackedScene) GD.Load(Levels[_currentLevel]);
            CurrentScene = nextScene.Instance();
            GetTree().GetRoot().AddChild(CurrentScene);
            GetTree().SetCurrentScene(CurrentScene);
        }
    }

    public void Setup()
    {
        File f = new File();
        if (f.FileExists(ScoreFile))
        {
            f.Open(ScoreFile, 1);
            string content = f.GetAsText();
            Highscore = Convert.ToInt32(content);
            f.Close();
        }
    }
}
