using Godot;
using System;

public class Container : Area2D
{
    private SceneManager sceneManager;
    private AnimationPlayer animator;

    public override void _Ready()
    {
        sceneManager = GetNode<SceneManager>("/root/SceneManager");
        animator = GetNode<AnimationPlayer>("Sprite/AnimationPlayer");
    }

    private void onBodyEntered(Node2D body)
    {
        if(body is Ball)
        {
            sceneManager.currentBall.win();
            animator.Play("Closing");
        }
    }
}
