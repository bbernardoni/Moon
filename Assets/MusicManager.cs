using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public enum MusicState
    {
        OutOfCombat, Combat, Eerie, Boss, Death, Idle
    }

    private bool musicInPlay;

    public AudioClip[] CombatMusic;
    public AudioClip[] OutOfCombatMusic;
    public AudioClip[] DeathMusic;
    public AudioClip[] BossMusic;

    public MusicState current_music;

    void Awake()
    {
        musicInPlay = false;
        current_music = MusicState.OutOfCombat;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        switch (current_music)
        {
            case MusicState.Combat :
                if (!musicInPlay)
                {
                    SoundManager.instance.PlayMusic(CombatMusic[0]);
                    musicInPlay = true;
                }
                break;
        }   
    }
}
