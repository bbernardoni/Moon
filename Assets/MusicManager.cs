﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public enum MusicState
    {
        OutOfCombat, Combat, Eerie, Boss1, Boss2, Death, Idle, FadeMusic, Menu
    }

    private bool musicInPlay;

    public AudioClip[] CombatMusic;
    public AudioClip[] OutOfCombatMusic;
    public AudioClip[] DeathMusic;
    public AudioClip[] BossMusic;
    public AudioClip[] MenuMusic;

    public MusicState current_music;
    public MusicState next_music;

    void Awake()
    {
        musicInPlay = false;
        current_music = MusicState.Menu;
    }

    // Update is called once per frame
    void Update()
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
            case MusicState.FadeMusic:
                if(!SoundManager.instance.FadeOutMusic()) {
                    musicInPlay = false;
                    current_music = next_music;
                }
                break;
            case MusicState.Idle:
                musicInPlay = false;
                current_music = next_music;
                break;
            case MusicState.OutOfCombat:
                if (!musicInPlay)
                {
                    SoundManager.instance.PlayMusic(OutOfCombatMusic[0]);
                    musicInPlay = true;
                }
                break;
            case MusicState.Death :
                if (!musicInPlay)
                {
                    SoundManager.instance.PlayMusic(DeathMusic[0]);
                    musicInPlay = true;
                }
                break;
            case MusicState.Boss1:
                if(!musicInPlay) {
                    SoundManager.instance.PlayMusic(BossMusic[0]);
                    musicInPlay = true;
                }
                break;
            case MusicState.Boss2:
                if(!musicInPlay) {
                    SoundManager.instance.PlayMusic(BossMusic[1]);
                    musicInPlay = true;
                }
                break;
            case MusicState.Menu:
                if (!musicInPlay)
                {
                    SoundManager.instance.PlayMusic(MenuMusic[0]);
                    musicInPlay = true;
                }
                break;
        }   
    }

    public void FadeMusic()
    {
        current_music = MusicState.FadeMusic;
    }


}
