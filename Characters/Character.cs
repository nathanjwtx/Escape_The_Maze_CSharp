using Godot;
using System;


public class Character : Area2D
{
    [Export] public int Speed;

    public int TileSize = 64;
    protected bool CanMove = true;
    protected string Facing = "right";
    private Tween _moveTween;

    public Dictionary<string, Vector2> _moves = new Dictionary<string, Vector2>
    {
        {"right", new Vector2(1, 0)},
        {"left", new Vector2(-1, 0)},
        {"up", new Vector2(0, -1)},
        {"down", new Vector2(0, 1)}
    };

    public Dictionary<string, RayCast2D> Raycasts;

    public override void _Ready()
    {
        Raycasts = new Dictionary<string, RayCast2D>
        {
            {"right", GetNode<RayCast2D>("RayCastRight")},
            {"left", GetNode<RayCast2D>("RayCastLeft")},
            {"up", GetNode<RayCast2D>("RayCastUp")},
            {"down", GetNode<RayCast2D>("RayCastDown")}
        };

    }

    public bool Move(string dir)
    {
        AnimationPlayer _player;
        _player = GetNode<AnimationPlayer>("AnimationPlayer");
        _player.PlaybackSpeed = Speed;
        Facing = dir;
        if (Raycasts[Facing].IsColliding())
        {
            return false;
        }
        _player.Play(Facing);
        _moveTween = GetNode<Tween>("MoveTween");
        _moveTween.InterpolateProperty(this, "position", Position, Position + _moves[Facing] * TileSize, 
            Convert.ToSingle(1.0 / Speed), Tween.TransitionType.Sine, Tween.EaseType.InOut);
        _moveTween.Start();
        return true;
    }
    
//    public override void _Process(float delta)
//    {
//        // Called every frame. Delta is time since last frame.
//        // Update game logic here.
//        
//    }
    
    private void _on_MoveTween_tween_completed(Godot.Object @object, NodePath key)
    {
        CanMove = true;
    }
}



