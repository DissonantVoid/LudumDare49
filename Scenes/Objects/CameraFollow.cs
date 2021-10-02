using Godot;
using System;

public class CameraFollow : Camera2D
{
    private SceneManager sceneManager;

    public override void _Ready()
    {
        sceneManager = GetNode<SceneManager>("/root/SceneManager");
    }

    public override void _Process(float delta)
    {
        GlobalPosition = sceneManager.currentBall.GlobalPosition;
    }
}
