using System.Linq;
using Godot;
using Godot.Collections;
public partial class TransportingPlayerController : Node2D
{
    [Export]
    private Player player;
    private Array<Node> markers = new();
    public override void _Ready()
    {

        markers = GetChildren();
        
        SignalBus.Instance.Connect(SignalBus.SignalName.RoomChanged, Callable.From<string>(MovePlayer));
    }
    private void MovePlayer(string newRoomName)
    {
        string markerName = newRoomName + "From" + Globals.Instance.lastRoom;
        GD.Print(markerName);
        foreach (Marker2D marker in markers)
        {
            if (markerName == marker.Name)
            {
                player.GlobalPosition = marker.GlobalPosition;
                break;
            }
        }
        
    }
}
