using Godot;
using System;

public class Pickup : Area2D
{
    private Dictionary<string, string> Textures = new Dictionary<string, string>
    {
        {"coin", "res://Assets/coin.png"},
        {"key_red", "res://Assets/keyRed.png"},
        {"star", "res://Assets/star.png"}
    };

    private Tween _tween;
    private Sprite _sprite;

    public override void _Ready()
    {
        _tween = GetNode<Tween>("Tween");
        _sprite = GetNode<Sprite>("Sprite");
        _tween.InterpolateProperty(_sprite, "scale", new Vector2(1, 1), new Vector2(3, 2), 0.5f,
            Tween.TransitionType.Quad, Tween.EaseType.InOut);
        _tween.InterpolateProperty(_sprite, "modulate", new Color(1.0f, 1.0f, 1.0f),
            new Color(1.0f, 1.0f, 1.0f, 0.0f), 0.5f, Tween.TransitionType.Quad, Tween.EaseType.InOut);
    }

    private void Init(string textType, Vector2 pos)
    {
        _sprite.Texture = (Texture) ResourceLoader.Load(Textures[textType], "texture");
//        GD.Load()
        Position = pos;
    }

    public void PickUps()
    {
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


