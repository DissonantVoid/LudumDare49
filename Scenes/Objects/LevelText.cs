using Godot;
using System;

public class LevelText : CanvasLayer
{
    private Label label;

    [Export]
    private string title;

    public override void _Ready()
    {
        label = GetNode<Label>("Label");
        label.Text = title;
    }
}
