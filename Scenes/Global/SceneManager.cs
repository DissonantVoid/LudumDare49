using Godot;
using System;
using System.Collections.Generic;

public class SceneManager : CanvasLayer
{
    private AnimationPlayer ExplosionAnimator;

    public Ball currentBall;
    public Magnet currentMagnet;

    private Dictionary<int, string> scenes = new Dictionary<int, string>() 
                                            {
                                                {0,"res://Scenes/Game/Levels/Level1.tscn"},
                                                {1,"res://Scenes/Game/Levels/Level2.tscn"},
                                                {2,"res://Scenes/Game/Levels/Level3.tscn"},
                                                {3,"res://Scenes/Game/Levels/Level4.tscn"},
                                                {4,"res://Scenes/Game/Levels/Level5.tscn"},
                                                {5,"res://Scenes/Game/Levels/Level6.tscn"},
                                                {6,"res://Scenes/Game/Levels/Level7.tscn"},
                                                {7,"res://Scenes/Game/Levels/Level8.tscn"},
                                                {8,"res://Scenes/Game/Levels/Level9.tscn"},
                                                {9,"res://Scenes/Game/End.tscn"},
                                            };
    private int currentSceneIndex = -1;

    public override void _Ready()
    {
        ExplosionAnimator = GetNode<AnimationPlayer>("Explosion/AnimationPlayer");
    }

    public void onLoose()
    {
        ExplosionAnimator.Play("FadeIn");
    }

    public void ChangeScene(string name)
    {
        GetTree().ChangeScene(name);
    }

    public void nextScene()
    {
        currentSceneIndex++;
        GetTree().ChangeScene(scenes[currentSceneIndex]);
    }

    public void onAnimationFinished(string name)
    {
        if(name == "FadeIn")
        {
            GetTree().ReloadCurrentScene();
            ExplosionAnimator.Play("FadeOut");
        }
    }
}
