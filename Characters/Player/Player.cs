using Godot;

public class Player : Character
{
    [Signal]
    delegate void Moved();
    [Signal]
    delegate void Dead();
    [Signal]
    delegate void GrabbedKey();
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
        if (a.IsInGroup("enemies"))
        {
            EmitSignal("Dead");
        }

        if (a.IsInGroup("Pickup"))
        {
            EmitSignal("Pickup");
        }

        if (area.GetClass() == "KeyRed")
        {
            EmitSignal("GrabbedKey");
        }

        if (area.GetClass() == "star")
        {
            EmitSignal("Win");
        }
    }
}

