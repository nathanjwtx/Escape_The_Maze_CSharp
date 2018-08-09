using Godot;
using System;
using System.Collections;
using System.Collections.Generic;
using Array = Godot.Array;

public class Level1 : Node2D
{
    [Export] public PackedScene EnemyScene;
    [Export] public PackedScene PickupScene;

    private static TileMap _items;
    private TileMap _ground;
    private TileMap _walls;
    private Random _random;
    private int cellID;
    private List<Vector2> _doors = new List<Vector2>();
    private Player _player;
    private HUD _hud;
    
    public override void _Ready()
    {
        _hud = GetNode<HUD>("HUD");
        _player = GetNode<Player>("PlayerOne");
        _items = GetNode<TileMap>("Items");
        _ground = GetNode<TileMap>("Ground");
        _random = new Random();
        _items.Hide();
        SetCameraLimits();
        _walls = GetNode<TileMap>("Walls");
        var doorID = _walls.TileSet.FindTileByName("door_red");
        foreach (Vector2 cell in _walls.GetUsedCellsById(doorID))
        {
            _doors.Add(cell);
        }

        SpawnItems();
        _player.Connect("Dead", _player, "GameOver");
        _player.Connect("GrabbedKey", _player, "_on_Player_Grabbed_Key");
        _player.Connect("Win", _player, "_on_Player_win");
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

    private void SpawnItems()
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
                case "coin": case "key_red": case "star":
                    Pickup p = (Pickup) PickupScene.Instance();
                    p.Init(cellType, pos);
                    AddChild(p);
                    p.Connect("CoinPickup", _hud, "UpdateScore");
                    break;
                default:
                    break;
            }
        }
    }

    private void GameOver()
    {
        var global = (Global) GetNode("root/Global");
        global.GameOver();
    }

    private void _on_Player_win()
    {
        var global = (Global) GetNode("root/Global");
        global.NextLevel();
    }

    private void _on_Player_grabbed_key()
    {
        foreach (var door in _doors)
        {
            _walls.SetCellv(door, -1);
        }
    }

}
