using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;     //Allows other scripts to call functions from SoundManager.
    [SerializeField] private MusicManager musicManager;
    public bool inCombat;
    private int CombatCounter;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
        CombatCounter = 0;
        inCombat = false;
    }
    void Update()
    {
        if (CombatCounter > 0)
        {
            inCombat = true;
            SetCombat();
        }
    }

    /// <summary>
    /// incr > 0 increases aggro count by one, incr <= 0 decreases aggro count by one
    /// </summary>
    /// <param name="incr"></param>
    public void AggroCounter(int incr)
    {
        if (incr > 0)
            CombatCounter++;
        else
            CombatCounter--;
    }


    private void SetCombat()
    {
        musicManager.current_music = MusicManager.MusicState.Combat;
    }

    private void SetOutOfCombat()
    {
        musicManager.current_music = MusicManager.MusicState.OutOfCombat;
    }

    private void SetBoss()
    {
        musicManager.current_music = MusicManager.MusicState.Boss;
    }

    private void SetDeath()
    {
        musicManager.current_music = MusicManager.MusicState.Death;
    }
}
