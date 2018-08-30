using Godot;

public class Level1 : Level
{
    public override void _Ready()
    {
        base._Ready();
        SpawnEnemies(4);
    }
}