using Godot;

public class Level1 : Level
{   
    private Pickup _packedScene;
    
    public override void _Ready()
    {
        _enemyCount = 4;
        base._Ready();
    }
}