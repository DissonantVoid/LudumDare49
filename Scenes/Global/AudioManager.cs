using Godot;
using System;

public class AudioManager : Node2D
{
    private AudioStreamPlayer audioPlayer;
    [Export] private AudioStreamSample[] music;

    public override void _Ready()
    {
        audioPlayer = GetNode<AudioStreamPlayer>("AudioStreamPlayer");
    }

    public void setMusic(byte index)
    {
        audioPlayer.Stream = music[index];
        audioPlayer.Play();
    }

    private void onAudioFinished()
    {
        if(audioPlayer.Stream == music[1])
        {
            setMusic(2);
        }
    }
}
