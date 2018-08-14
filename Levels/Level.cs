using Godot;
using System;
using System.Collections;
using System.Collections.Generic;
using Array = Godot.Array;

public class Level : Node2D
{
    [Export] public PackedScene EnemyScene;
    [Export] public PackedScene PickupScene;

    private static TileMap _items;
    private TileMap _ground;
    private TileMap _walls;
    private TileMap _doors;
    private Random _random;
    private int cellID;
    private Player _player;
    private HUD _hud;
    private Godot.Dictionary<string, Vector2> _doorVector2s = new Godot.Dictionary<string, Vector2>(); 
    
    public override void _Ready()
    {
        _hud = GetNode<HUD>("HUD");
        _player = GetNode<Player>("PlayerOne");
        _items = GetNode<TileMap>("Items");
        _ground = GetNode<TileMap>("Ground");
        _doors = GetNode<TileMap>("Doors");
        _random = new Random();
        _items.Hide();
        SetCameraLimits();
        _walls = GetNode<TileMap>("Walls");
        
        SpawnItems();
        _player.Connect("Dead", _player, "GameOver");
        _player.Connect("RedKey", this, "_on_PlayerOne_RedKey");
        _player.Connect("GreenKey", this, "_on_PlayerOne_GreenKey");
        _player.Connect("Win", this, "_on_PlayerOne_win");
    }

    private void SetCameraLimits()
    {
        Rect2 mapSize = _ground.GetUsedRect();
        Vector2 cellSize = _ground.CellSize;
        _player.GetNode<Camera2D>("Camera2D").LimitLeft = Convert.ToInt32(mapSize.Position.x * cellSize.x - cellSize.x);
        _player.GetNode<Camera2D>("Camera2D").LimitTop = Convert.ToInt32(mapSize.Position.y * cellSize.y - cellSize.x);
        _player.GetNode<Camera2D>("Camera2D").LimitRight = Convert.ToInt32(mapSize.End.x * cellSize.x + cellSize.x);
        _player.GetNode<Camera2D>("Camera2D").LimitBottom = Convert.ToInt32(mapSize.End.x * cellSize.x + cellSize.x);
    }

    public void SpawnItems()
    {
        foreach (Vector2 cell in _items.GetUsedCells())
        {
            int id = _items.GetCellv(cell);
            string cellType = _items.TileSet.TileGetName(id);
            Vector2 pos = _items.MapToWorld(cell) + _items.CellSize / 2;

            switch (cellType)
            {
                case "slime_spawn":
                    var s = (Enemy) EnemyScene.Instance();
                    s.Position = pos;
                    s.TileSize = Convert.ToInt32(_items.CellSize.x);
                    AddChild(s);
                    break;
                case "player_spawn":
                    _player.Position = pos;
                    _player.TileSize = 64;
                    break;
                case "coin": case "key_red": case "star": case "key_green":
                    Pickup p = (Pickup) PickupScene.Instance();
                    p.Init(cellType, pos);
                    AddChild(p);

                    switch (cellType)
                    {
                            case "coin":
                                p.Connect("CoinPickup", _hud, "UpdateScore");
                                break;
                    }
                    break;
            }
        }

        foreach (Vector2 usedCell in _doors.GetUsedCells())
        {
            int id = _doors.GetCellv(usedCell);
            string cellType = _doors.TileSet.TileGetName(id);
            Vector2 pos = _doors.MapToWorld(usedCell) + _doors.CellSize / 2;
            switch (cellType)
            {
                case "door_green":
                    _doorVector2s.Add("door_green", usedCell);
                    break;
                case "door_red":
                    _doorVector2s.Add("door_red", usedCell);
                    break;
            }

        }
    }

    private void GameOver()
    {
        var global = (Global) GetNode("/root/Global");
        global.GameOver();
    }

    private void _on_PlayerOne_win()
    {
        GD.Print("got star");
//        var global = new Global();
        var global = (Global) GetNode("/root/Global");
        global.GotoScene(global.Levels[1]);
//        global.NextLevel();
    }

    private void _on_PlayerOne_RedKey()
    {
        GD.Print("got red key");
//        foreach (var door in _doors)
//        {
//            _walls.SetCellv(door, -1);
//        }
    }

    private void _on_PlayerOne_GreenKey()
    {
        _doors.SetCellv(_doorVector2s["door_green"], -1);
    }
}
