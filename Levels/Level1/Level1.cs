using Godot;

public class Level1 : Level
{
    [Export] public PackedScene Testing;
    
    private Pickup _packedScene;
    
    public override void _Ready()
    {
        base._Ready();
        if (PickupScene == null)
        {
            GD.Print("null");
        }
        else
        {
            SpawnItems(PickupScene);
        }
    }
}