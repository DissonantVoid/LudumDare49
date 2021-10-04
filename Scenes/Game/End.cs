using Godot;
using System;

public class End : Control
{
    private AudioManager audioManager;

    public override void _Ready()
    {
        audioManager = GetNode<AudioManager>("/root/AudioManager");
        audioManager.setMusic(3);
        Input.SetMouseMode(Input.MouseMode.Visible);
    }

}
