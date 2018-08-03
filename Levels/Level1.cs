using Godot;
using System;
using System.Collections.Generic;

public class Level1 : Node2D
{
    [Export] public PackedScene Enemy;
    [Export] public PackedScene Pickup;

    private TileMap _items;
    private Random _random;
    
    public override void _Ready()
    {
        _items = GetNode<TileMap>("Items");
        _random = new Random();
        _items.Hide();
        
    }

//    public override void _Process(float delta)
//    {
//        // Called every frame. Delta is time since last frame.
//        // Update game logic here.
//        
//    }
}
