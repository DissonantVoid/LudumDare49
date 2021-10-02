using Godot;
using System;

public class LazerGun : StaticBody2D
{
    private AnimationPlayer animator;
    private RayCast2D ray;
    private Line2D lazerLine;
    private Timer shootingTimer;
    private Timer idleTimer;

    [Export] private float shootingTime;
    [Export] private float idleTime;
    [Export] private float lenght = 80;

    private bool isLazerOn = false;

    public override void _Ready()
    {
        animator = GetNode<AnimationPlayer>("Sprite/AnimationPlayer");
        ray = GetNode<RayCast2D>("RayCast2D");
        lazerLine = GetNode<Line2D>("Line2D");
        shootingTimer = GetNode<Timer>("ShootingTimer");
        idleTimer = GetNode<Timer>("IdleTimer");

        shootingTimer.WaitTime = shootingTime;
        idleTimer.WaitTime = idleTime;
        ray.CastTo = new Vector2(0,-lenght);
        lazerLine.SetPointPosition(1, new Vector2(0, -lenght));

        idleTimer.Start();
    }

    public override void _Process(float delta)
    {
        var collider = ray.GetCollider();
        if(collider != null && collider is Ball)
        {
            ((Ball)collider).loose();
        }

        if(idleTimer.IsStopped() == false && idleTimer.TimeLeft < 2)
            animator.Play("AboutToShoot");
    }

    private void onShootingTimerTimeout()
    {
        idleTimer.Start();
        animator.Play("off");
        isLazerOn = false;
        ray.Enabled = false;
        lazerLine.Hide();
    }

    private void onIdleTimerTimeout()
    {
        shootingTimer.Start();
        animator.Play("Shooting");
        isLazerOn = true;
        ray.Enabled = true;
        lazerLine.Show();
    }
}
