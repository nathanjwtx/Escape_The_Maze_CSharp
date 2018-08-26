using Godot;

public class Player : Character
{
    [Signal]
    delegate void Moved();
    [Signal]
    delegate void Dead();
    [Signal]
    delegate void RedKey();
    [Signal]
    delegate void GreenKey();
    [Signal]
    delegate void Win();

    public override void _Ready()
    {
        base._Ready();
        Scale = new Vector2(1.0f, 1.0f);
    }

    public override void _Process(float delta)
    {
        base._Process(delta);
        if (!CanMove) return;
        foreach (var move in _moves.Keys)
        {
            if (Input.IsActionPressed(move))
            {
                if (Move(move))
                {
                    GetNode<AudioStreamPlayer>("Footsteps").Play();
                    EmitSignal("Moved");
                }
            }
        }
    }
    
    async private void _on_Player_area_entered(Godot.Object area)
    {
        var a = (Node2D) area;
        if (a.IsInGroup("enemies"))
        {
            a.Hide();
            GetNode<CollisionShape2D>("CollisionShape2D").Disabled = true;
            SetProcess(false);
            GetNode<AudioStreamPlayer>("Lose").Play();
            GetNode<AnimationPlayer>("AnimationPlayer").Play("Die");
            await ToSignal(GetNode<AnimationPlayer>("AnimationPlayer"), "animation_finished");
            EmitSignal("Dead");
        }
        if (a.HasMethod("PickUps"))
        {
            var p = (Pickup) a;
            p.PickUps();
        }

        if (!a.IsInGroup("enemies"))
        {
            var x = (Pickup) a;
            switch (x.MyType)
            {
                case "key_green":
                    EmitSignal("GreenKey");
                    PlayKeyPickupSound();
                    break;
                case "key_red":
                    EmitSignal("RedKey");
                    PlayKeyPickupSound();
                    break;
                case "star":
                    GetNode<AudioStreamPlayer>("Win").Play();
                    GetNode<CollisionShape2D>("CollisionShape2D").Disabled = true;
                    await ToSignal(GetNode<AudioStreamPlayer>("Win"), "finished");
                    EmitSignal("Win");
                    break;
            }   
        }
    }

    private void PlayKeyPickupSound()
    {
        GetNode<AudioStreamPlayer>("KeyPickup").Play();
    }
    
}

