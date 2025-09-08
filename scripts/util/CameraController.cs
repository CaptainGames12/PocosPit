using System.Collections.Generic;

using Godot;

public partial class CameraController : Camera2D
{
    private Dictionary<string, RoomScale> roomsScale = new()
    {
        ["DiningRoom"] = new RoomScale(
            left: 0,
            top: 0,
            right: 995,
            bottom: 435,
            zoom: new Vector2(2, 2)
            ),
        ["Reception"] = new RoomScale(
            left: -528,
            top: 160,
            right: -31,
            bottom: 426,
            zoom: new Vector2(3, 3)
            ),
        ["Workshop"] = new RoomScale(
            left: 390,
            top: -390,
            right: 1000,
            bottom: -35,
            zoom: new Vector2(3, 3)
        ),
        ["PanelRoom1"] = new RoomScale(
            left: 807,
            top: -673,
            right: 1051,
            bottom: -411,
            zoom: new Vector2(5, 5)
        ),
        ["StaffRoom"] = new RoomScale(
            left: -640,
            top: -573,
            right: -210,
            bottom: 150,
            zoom: new Vector2(3, 3)
        ),
        ["SecurityRoom"] = new RoomScale(
            left: -623,
            top: -862,
            right: -429,
            bottom: -612,
            zoom: new Vector2(6, 6)
        ),
        ["PanelRoom2"] = new RoomScale(
            left: -130,
            top: -631,
            right: 106,
            bottom: -370,
            zoom: new Vector2(5, 5)
        ),
        ["Kitchen"] = new RoomScale(
            left: -160,
            top: -353,
            right: 371,
            bottom: -66,
            zoom: new Vector2(3, 3)
            ),
    };
    public override void _Ready()
    {
        
        SignalBus.Instance.Connect(SignalBus.SignalName.RoomChanged, Callable.From<string>(ChangeCameraLimit));
    }
    public void ChangeCameraLimit(string newRoom)
    {
        GD.Print(newRoom);
        
        LimitLeft = roomsScale[newRoom].left;
        LimitRight = roomsScale[newRoom].right;
        LimitBottom = roomsScale[newRoom].bottom;
        LimitTop = roomsScale[newRoom].top;
        Zoom = roomsScale[newRoom].zoom;
    }
}
public readonly record struct RoomScale(
    int left,
    int top,
    int right,
    int bottom,
    Vector2 zoom
    );
