using System.Collections.Generic;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;
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
            top: 164,
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
        ["PanelRoom"] = new RoomScale(
            left: 807,
            top: -673,
            right: 1051,
            bottom: -411,
            zoom: new Vector2(4, 4)
        )
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
