using Godot;
using System;
using System.Security.Cryptography.X509Certificates;

public class Pickup : Area2D
{
    [Signal] delegate void CoinPickup();
    
    private Dictionary<string, string> Textures = new Dictionary<string, string>
    {
        {"coin", "res://Assets/coin.png"},
        {"key_red", "res://Assets/keyRed.png"},
        {"key_green", "res://Assets/keyGreen.png"},
        {"star", "res://Assets/star.png"}
    };

    private Tween _tween;
    private Sprite _sprite = new Sprite();
    public Texture _texture;
    public string MyType;
    private int _count;

    public override void _Ready()
    {
        _tween = GetNode<Tween>("Tween");
//        _sprite = GetNode<Sprite>("PickupSprite");
        _tween.InterpolateProperty(_sprite, "scale", new Vector2(1, 1), new Vector2(3, 2), 0.5f,
            Tween.TransitionType.Quad, Tween.EaseType.InOut);
        _tween.InterpolateProperty(_sprite, "modulate", new Color(1.0f, 1.0f, 1.0f),
            new Color(1.0f, 1.0f, 1.0f, 0.0f), 0.5f, Tween.TransitionType.Quad, Tween.EaseType.InOut);
    }

    public void Init(string textType, Vector2 pos)
    {
        _count += 1;
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
            GD.Print(MyType);
            EmitSignal("CoinPickup", 2);
        }
        GetNode<CollisionShape2D>("CollisionShape2D").Disabled = true;
        _tween.Start();
    }
    
    private void _on_Tween_tween_completed(Godot.Object @object, NodePath key)
    {
        QueueFree();
    }

//    public override void _Process(float delta)
//    {
//        // Called every frame. Delta is time since last frame.
//        // Update game logic here.
//        
//    }
}


