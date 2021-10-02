using Godot;
using System;

public class Magnet : Sprite
{
    private CPUParticles2D emiter1;
    private CPUParticles2D emiter2;
    private AudioStreamPlayer audio;

    private SceneManager sceneManager;
    public bool isActive { private set; get; }

    public override void _EnterTree()
    {
        audio = GetNode<AudioStreamPlayer>("Audio/AudioStreamPlayer");
        sceneManager = GetNode<SceneManager>("/root/SceneManager");
        sceneManager.currentMagnet = this;
    }

    public override void _Ready()
    {
        emiter1 = GetNode<CPUParticles2D>("Emitter1");
        emiter2 = GetNode<CPUParticles2D>("Emitter2");
    }

    public override void _Process(float delta)
    {
        //emmition
        isActive = Input.IsActionPressed("ActivateMagnet");

        if(isActive)
        {
            if(audio.Playing == false)audio.Play();
            emiter1.Emitting = true;
            emiter2.Emitting = true;
        }
        else
        {
            audio.Stop();
            emiter1.Emitting = false;
            emiter2.Emitting = false;
        }

        //follow mouse
        Vector2 mousePos = GetGlobalMousePosition();
        GlobalPosition = mousePos;

        //rotation
        Vector2 ballPos = sceneManager.currentBall.GlobalPosition;
        Vector2 direction = ballPos - GlobalPosition;
        Rotation = Mathf.Atan2(direction.y, direction.x) - Mathf.Deg2Rad(90);
    }
}
