using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundController : MonoBehaviour
{
    public AudioClip[] footsteps;
    public AudioClip[] takeDamage;
    public AudioClip[] firePistol;
    public AudioClip[] fireShotgun;
    public AudioClip[] fireRailgun;

    // public static SoundManager instance = null;     //Allows other scripts to call functions from SoundManager.             

    public AudioSource playerFS;
    public AudioSource playerGun;
    public AudioSource playerVoice;
    public AudioSource playerShotgun;
    public AudioSource playerRailgun;

    public float lowPitchRange = .95f;              //The lowest a sound effect will be randomly pitched.
    public float highPitchRange = 1.05f;            //The highest a sound effect will be randomly pitched.

    //RandomizeSfx chooses randomly between various audio clips and slightly changes their pitch.
    public void RandomizeSfx(AudioClip[] clips, AudioSource efxSource)
    {
        //Generate a random number between 0 and the length of our array of clips passed in.
        int randomIndex = Random.Range(0, clips.Length);

        //Choose a random pitch to play back our clip at between our high and low pitch ranges.
        float randomPitch = Random.Range(lowPitchRange, highPitchRange);

        //Set the pitch of the audio source to the randomly chosen pitch.
        efxSource.pitch = randomPitch;

        //Set the clip to the clip at our randomly chosen index.
        efxSource.clip = clips[randomIndex];

        //Play the clip.
        efxSource.Play();
    }

    protected void PlaySingle(AudioClip clip, AudioSource efxSource)
    {
        efxSource.clip = clip;
        efxSource.Play();
    }

    public void FootHitsGround()
    {
        RandomizeSfx(footsteps, playerFS);
    }

    public void TakeDamage()
    {
        RandomizeSfx(takeDamage, playerVoice);
    }

    public void FirePistol()
    {
        RandomizeSfx(firePistol, playerGun);
    }

    public void FireShotgun()
    {
        RandomizeSfx(fireShotgun, playerShotgun);
    }

    public void FireRailgun()
    {
        RandomizeSfx(fireRailgun, playerShotgun);
    }
}
