using Godot;
using System;

public class Ball : RigidBody2D
{
    private AnimationPlayer animator;
    private AudioStreamPlayer explosionSfx;

    private SceneManager sceneManager;
    private const float speed = 500f;
    private const float maxSpeed = 70f;
    private bool hasLost;

    private Teleporter tpTo;

    public override void _EnterTree()
    {
        animator = GetNode<AnimationPlayer>("Ball/AnimationPlayer");
        sceneManager = GetNode<SceneManager>("/root/SceneManager");
        sceneManager.currentBall = this;
        explosionSfx = GetNode<AudioStreamPlayer>("Audio/Explode");
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
                ApplyCentralImpulse(new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * ((Bouncer)body).bounceSpeed * GetPhysicsProcessDeltaTime());
                ((Bouncer)body).onBounce();
            }
        }
    }

    public void teleportTo(Teleporter tpTo)
    {
        this.tpTo = tpTo;
        animator.Play("Dissapear");
    }

    private void onAnimatorAnimationFinished(string name)
    {
        if (name == "Dissapear")
        {
            //have do tp like this because 2d physics in godot really sucks
            var t = new Transform2D(new Vector2(1, 0),new Vector2(0, 1), tpTo.GlobalPosition);
            Physics2DServer.BodySetState(this.GetRid(), Physics2DServer.BodyState.Transform, t);

            tpTo.onTeleportTo();
            animator.Play("Apear"); //Note: apear will automatically call "Glow" animation after it's done
        }
    }

    public void loose()
    {
        if (hasLost == true) return;

        hasLost = true;
        explosionSfx.Play();
        animator.Play("Explode");
        SetPhysicsProcess(false);
        sceneManager.onLoose();
    }

    public void win()
    {
        Sleeping = true;
        Hide();
        SetPhysicsProcess(false);
    }
}
