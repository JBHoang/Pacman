using UnityEngine;

public class SoundEffects : MonoBehaviour
{
    public AudioSource src;
    public AudioClip introductionSoundEffect;
    public AudioClip pelletEatenSoundEffect;
    public AudioClip ghostEatenSoundEffect;
    public AudioClip pacmanEatenSoundEffect;

    public AudioSource backgroundSrc;
    public AudioClip BGSoundEffect;

    public void introSoundEffect()
    {
        src.clip = introductionSoundEffect;
        src.Play();
    }

    public void PelletEatenSound()
    {
        src.clip = pelletEatenSoundEffect;
        src.Play();
    }

    public void GhostEatenSound()
    {
        src.clip = ghostEatenSoundEffect;
        src.Play();
    }

    public void pacmanEatenSound()
    {
        src.clip = pacmanEatenSoundEffect;
        src.Play();
    }

    public void PlayBGMusic()
    {
        backgroundSrc.clip = BGSoundEffect;
        backgroundSrc.Play();
    }

}
