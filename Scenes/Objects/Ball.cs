using Godot;
using System;

public class Ball : RigidBody2D
{
    private AnimationPlayer animator;

    private SceneManager sceneManager;
    private const float speed = 500f;
    private const float maxSpeed = 70f;
    private bool hasLost;

    private Vector2 teleportPos;

    public override void _EnterTree()
    {
        animator = GetNode<AnimationPlayer>("Ball/AnimationPlayer");
        sceneManager = GetNode<SceneManager>("/root/SceneManager");
        sceneManager.currentBall = this;
    }

    public override void _PhysicsProcess(float delta)
    {
        //movement
        if (sceneManager.currentMagnet.isActive)
        {
            Vector2 speedUnderMax = new Vector2(speed,speed);
            if(Mathf.Abs(LinearVelocity.x) + speed > maxSpeed) speedUnderMax.x = (maxSpeed - Mathf.Abs(LinearVelocity.x) + speed);
            if(Mathf.Abs(LinearVelocity.y) + speed > maxSpeed) speedUnderMax.y = (maxSpeed - Mathf.Abs(LinearVelocity.y) + speed);

            Vector2 direction = (sceneManager.currentMagnet.GlobalPosition - GlobalPosition).Normalized();
            ApplyCentralImpulse(direction * speedUnderMax * delta);
        }
    }

    private void onBodyShapeEntered(int bodyID,Node2D body, int bodyShape, int localShape)
    {
        //collision
        if (body.IsInGroup("Wall"))
        {
            loose();
        }
        else if (body is Bouncer)
        {
            if (bodyShape == 0)
            {
                float angle = body.Rotation;
                ApplyCentralImpulse(new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * speed * 100f * GetPhysicsProcessDeltaTime());
                ((Bouncer)body).onBounce();
            }
        }
    }

    public void teleportTo(Vector2 pos)
    {
        teleportPos = pos;
        animator.Play("Dissapear");
    }

    private void onAnimatorAnimationFinished(string name)
    {
        if (name == "Dissapear")
        {
            GD.Print("from: " + GlobalPosition + " To: " + teleportPos);
            GlobalPosition = teleportPos;
            animator.Play("Apear"); //Note: apear will automatically call "Glow" animation after it's done
        }
    }

    public void loose()
    {
        if (hasLost == true) return;

        hasLost = true;
        animator.Play("Explode");
        SetPhysicsProcess(false);
        sceneManager.onLoose();
    }

    public void win()
    {
        Sleeping = true;
        Hide();
        SetPhysicsProcess(false);
        sceneManager.nextScene();
    }
}
