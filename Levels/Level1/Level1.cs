using Godot;

public class Level1 : Level
{   
    
    public override void _Ready()
    {
        _enemyCount = 4;
//        _enemyCount = 1;
        base._Ready();
    }
}