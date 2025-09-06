using System.Threading.Tasks;
using Godot;

[GlobalClass]
public partial class RoomChanger : Area2D
{
    [Export]
    private string currentRoomName;
    [Export]
    private string newRoomName;
    public override void _Ready()
    {
        Connect(Area2D.SignalName.BodyEntered, Callable.From<Node2D>(OnRoomChangerEntered));
    }
    public virtual void OnRoomChangerEntered(Node2D body)
    {
        if (body.IsInGroup("player"))
        {
            Globals.Instance.lastRoom = currentRoomName;
            SignalBus.Instance.EmitSignal(SignalBus.SignalName.RoomChanged, newRoomName);
            
        }

    }
    
}
