using Godot;
using System;

public class Bouncer : StaticBody2D
{
    private AnimationPlayer animator;
    private Timer refillTimer;
    private AudioStreamPlayer bounceSfx;

    [Export] public float bounceSpeed = 60000f;
    public bool canBounce { private set; get; } = true;

    public override void _Ready()
    {
        animator = GetNode<AnimationPlayer>("Sprite/AnimationPlayer");
        refillTimer = GetNode<Timer>("RefillTimer");
        bounceSfx = GetNode<AudioStreamPlayer>("Audio/Bounce");
    }

    public void onBounce()
    {
        if (canBounce == false) return;
        animator.Play("Out");
        refillTimer.Start();
        canBounce = false;
        bounceSfx.Play();
    }

    public void onRefillTimerTimeout()
    {
        animator.Play("In");
        canBounce = true;
    }
}
