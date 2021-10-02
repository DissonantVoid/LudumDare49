using Godot;
using System;

public class Menu : Node2D
{
    private AnimationPlayer animator;
    private SceneManager sceneManager;

    private bool isButtonOpen;
    private bool readyToStart;

    public override void _Ready()
    {
        animator = GetNode<AnimationPlayer>("CanvasLayer/Button2/AnimationPlayer");
        sceneManager = GetNode<SceneManager>("/root/SceneManager");
    }

    public void onButtonPressed()
    {
        if(isButtonOpen == false)
        {
            isButtonOpen = true;
            animator.Play("Opening");
        }
        else if (readyToStart && isButtonOpen)
        {
            Input.SetMouseMode(Input.MouseMode.Hidden);
            sceneManager.nextScene();
        }
    }

    public void onAnimationFinished(string name)
    {
        if(name == "Opening")
        {
            animator.Play("NotCovered");
            readyToStart = true;
        }
    }
}
