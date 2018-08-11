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
                    EmitSignal("Moved");
                }
            }
        }
    }
    
    private void _on_Player_area_entered(Godot.Object area)
    {
        var a = (Node2D) area;
        var x = (Pickup) a;
        if (a.IsInGroup("enemies"))
        {
            EmitSignal("Dead");
        }
        if (a.HasMethod("PickUps"))
        {
            var p = (Pickup) a;
            p.PickUps();
        }

        switch (x.MyType)
        {
                case "key_green":
                    EmitSignal("GreenKey");
                    break;
                case "key_red":
                    EmitSignal("RedKey");
                    break;
                case "star":
                    EmitSignal("Win");
                    break;
        }
    }
    
}

