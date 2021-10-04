using Godot;
using System;

public class Teleporter : Area2D
{
    private Sprite indicatorSprite;
    private SceneManager sceneManager;
    private AudioStreamPlayer teleportInSfx;
    private AudioStreamPlayer teleportOutSfx;

    [Export(PropertyHint.Enum,"red,blue,gree,yellow")] //red,blue,green,yellow
    private int type;

    [Export] private NodePath otherPairRef;
    private Teleporter otherPair;
    private bool isBallInside;
    private bool canTeleport = true;

    public override void _Ready()
    {
        sceneManager = GetNode<SceneManager>("/root/SceneManager");
        indicatorSprite = GetNode<Sprite>("IndicatorSprite");
        teleportInSfx = GetNode<AudioStreamPlayer>("Audio/TeleportIn");
        teleportOutSfx = GetNode<AudioStreamPlayer>("Audio/TeleportOut");
        if(otherPairRef != null) otherPair = GetNode<Teleporter>(otherPairRef);

        switch (type)
        {
            case 0: indicatorSprite.RegionRect = new Rect2(32, 0, 8, 8); break;
            case 1: indicatorSprite.RegionRect = new Rect2(40, 8, 8, 8); break;
            case 2: indicatorSprite.RegionRect = new Rect2(32, 8, 8, 8); break;
            case 3: indicatorSprite.RegionRect = new Rect2(40, 0, 8, 8); break;
        }
    }

    public override void _Process(float delta)
    {
        if(otherPair != null && isBallInside && canTeleport)
        {
            if(GlobalPosition.DistanceTo(sceneManager.currentBall.GlobalPosition) <= 8)
            {
                otherPair.canTeleport = false;
                sceneManager.currentBall.teleportTo(otherPair);
                isBallInside = false;
                teleportInSfx.Play();
            }
        }
    }

    private void onBodyEntered(Node2D body)
    {
        if (body is Ball)
            isBallInside = true;
    }

    private void onBodyExited(Node2D body)
    {
        if (body is Ball)
        {
            isBallInside = false;
            if (canTeleport == false) canTeleport = true;
        }
    }

    public void onTeleportTo()
    {
        teleportOutSfx.Play();
    }
}
