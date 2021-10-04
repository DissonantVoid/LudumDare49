using Godot;
using System;

public class Container : Area2D
{
    private SceneManager sceneManager;
    private AnimationPlayer animator;
    private AudioStreamPlayer inSFX;

    public override void _Ready()
    {
        sceneManager = GetNode<SceneManager>("/root/SceneManager");
        animator = GetNode<AnimationPlayer>("Sprite/AnimationPlayer");
        inSFX = GetNode<AudioStreamPlayer>("Audio/In");
    }

    private void onBodyEntered(Node2D body)
    {
        if(body is Ball)
        {
            sceneManager.currentBall.win();
            animator.Play("Closing");
            inSFX.Play();
        }
    }

    private void onAnimationDone(string name)
    {
        if(name == "Closing")
            sceneManager.nextScene();
    }
}
