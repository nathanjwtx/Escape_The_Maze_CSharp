using Godot;
using System;
using System.Collections.Generic;

public class Enemy : Character
{
    private Random _random1;
    private List<string> _moveKeys;
    private Timer _spawn;

    public override void _Ready()
    {
        base._Ready();
        _spawn = GetNode<Timer>("SpawnTimer");
        _random1 = new Random();
        CanMove = false;
        _moveKeys = new List<string>();
        foreach (var movesKey in _moves.Keys)
        {
            _moveKeys.Add(movesKey);
        }

        Facing = _moveKeys[_random1.Next(0, _moveKeys.Count)];
        _spawn.Start();
    }

    // move this code to regular method then link to a player emiited signal for turn-based goodness
    public override void _Process(float delta)
    {
        if (CanMove)
        {
            // might need to adjust the random ocurence of direction changing
            if (Move(Facing) == false || _random1.Next(0, 11) > 5)
            {
                Facing = _moveKeys[_random1.Next(0, 4)];
                CanMove = true;
            }

            CanMove = false;
        }   
    }
    
    private void _on_SpawnTimer_timeout()
    {
        CanMove = true;
    }
}

