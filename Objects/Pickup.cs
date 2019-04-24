using Godot;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;


public class Pickup : Area2D
{
    [Signal] delegate void CoinPickup();
    
    private Dictionary<string, string> Textures = new Dictionary<string, string>
    {
        {"coin", "res://Assets/coin.png"},
        {"key_red", "res://Assets/keyRed.png"},
        {"key_green", "res://Assets/keyGreen.png"},
        {"star", "res://Assets/star.png"},
        {"door_green", "res://Assets/doorGreen_lock.png"},
        {"door_red", "res://Assets/doorRed_lock.png"}
    };

    private Tween _tween;
    public Texture _texture;
    public string MyType;

    public override void _Ready()
    {
        _tween = GetNode<Tween>("Tween");
//        _sprite = GetNode<Sprite>("PickupSprite");
        _tween.InterpolateProperty(this, "scale", Scale, Scale * 3, Convert.ToSingle(0.5),
            Tween.TransitionType.Quad, Tween.EaseType.InOut);
        _tween.InterpolateProperty(this, "modulate", new Color(1.0f, 1.0f, 1.0f),
            new Color(1.0f, 1.0f, 1.0f, 0.0f), 0.5f, Tween.TransitionType.Quad, Tween.EaseType.InOut);
    }

    public void Init(string textType, Vector2 pos)
    {
        MyType = textType;
        _texture = (Texture) GD.Load(Textures[textType]);
        GetNode<Sprite>("PickupSprite").Texture = _texture;
        Position = pos;
        if (textType == "coin")    
        {
            AddToGroup("Coins");
        }
    }

    public void PickUps()
    {
        if (IsInGroup("Coins"))
        {
            EmitSignal("CoinPickup", 1);
            GetNode<AudioStreamPlayer>("CoinPickup").Play();
        }

        GetNode<CollisionShape2D>("CollisionShape2D").Disabled = true;
        _tween.Start();
    }
    
    private void _on_Tween_tween_completed(Godot.Object @object, NodePath key)
    {
        QueueFree();
    }
}


