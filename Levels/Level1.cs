using Godot;
using System;
using System.Collections;
using System.Collections.Generic;
using Array = Godot.Array;

public class Level1 : Node2D
{
    [Export] public PackedScene Enemy;
    [Export] public PackedScene Pickup;

    private TileMap _items;
    private TileMap _ground;
    private Random _random;
    private int cellID;
    private List<Vector2> _doors = new List<Vector2>();
    private Player _player;
    
    public override void _Ready()
    {
        _items = GetNode<TileMap>("Items");
        _ground = GetNode<TileMap>("Ground");
        _random = new Random();
        _items.Hide();
        SetCameraLimits();
        TileMap walls = GetNode<TileMap>("Walls");
        var doorID = walls.TileSet.FindTileByName("door_red");
        GD.Print(doorID);
        foreach (Vector2 cell in walls.GetUsedCellsById(doorID))
        {
            _doors.Add(cell);
        }

        SpawnItems();
        _player.Connect("dead", _player, "GameOver");
        _player.Connect("GrabbedKey", _player, "_on_Player_Grabbed_Key");
        _player.Connect("win", _player, "_on_Player_win");
    }

    private void SetCameraLimits()
    {
        Rect2 mapSize = _ground.GetUsedRect();
        Vector2 cellSize = _ground.CellSize;
        _player.GetNode<Camera2D>("Camera2D").LimitLeft = Convert.ToInt32(mapSize.Position.x * cellSize.x);
        _player.GetNode<Camera2D>("Camera2D").LimitTop = Convert.ToInt32(mapSize.Position.y * cellSize.y);
        _player.GetNode<Camera2D>("Camera2D").LimitRight = Convert.ToInt32(mapSize.End.x * cellSize.x);
        _player.GetNode<Camera2D>("Camera2D").LimitBottom = Convert.ToInt32(mapSize.End.x * cellSize.x);
    }

    private static void SpawnItems()
    {
        
    }
//    public override void _Process(float delta)
//    {
//        // Called every frame. Delta is time since last frame.
//        // Update game logic here.
//        
//    }
}
