using Godot;
using System;

public class Menu : Node2D
{
    private AudioManager audioManager;
    private AnimationPlayer animator;
    private SceneManager sceneManager;
    private AudioStreamPlayer pressSfx;

    private bool isButtonOpen;
    private bool readyToStart;

    public override void _Ready()
    {
        audioManager = GetNode<AudioManager>("/root/AudioManager");
        audioManager.setMusic(0);
        animator = GetNode<AnimationPlayer>("CanvasLayer/Button2/AnimationPlayer");
        sceneManager = GetNode<SceneManager>("/root/SceneManager");
        pressSfx = GetNode<AudioStreamPlayer>("Audio/Press");
    }

    public void onButtonPressed()
    {
        pressSfx.Play();
        if(isButtonOpen == false)
        {
            isButtonOpen = true;
            animator.Play("Opening");
        }
        else if (readyToStart && isButtonOpen)
        {
            Input.SetMouseMode(Input.MouseMode.Hidden);
            sceneManager.nextScene();
            audioManager.setMusic(1);
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
