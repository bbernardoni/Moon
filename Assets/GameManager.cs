using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;     //Allows other scripts to call functions from SoundManager.
    [SerializeField] private MusicManager musicManager;
    public bool inCombat;
    public bool isStopped;
    public bool isMenu;
    public GameObject player;
    [SerializeField] public int CombatCounter;
    public bool useKeyboard;
    public int tutorial;

    void Awake()
    {
        player = GameObject.FindWithTag("Player");
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
        CombatCounter = 0;
        inCombat = false;
        isStopped = false;
        useKeyboard = true;
        tutorial = 0;
    }
    void Update()
    {
        if (player != null)
        {
            if (CombatCounter > 0) inCombat = true;
            else inCombat = false;
            
            bool isBoss = GetComponent<LevelManagement>().IsBossLevel();
            if (inCombat & !isBoss & player.GetComponent<Health>().alive)
            {
                SetCombat();
            }
            else if (player.GetComponent<Health>().alive & !isBoss)
            {
                ExitCombat();
            }

            if (!player.GetComponent<Health>().alive)
            {
                SetDeath();
                CombatCounter = 0;
            }            
            if (MusicManager.MusicState.Death == musicManager.current_music && player.GetComponent<Health>().alive)
            {
                CombatCounter = 0;
                ExitCombat();
            }

        }
        else
        {
            player = GameObject.FindWithTag("Player");
        }
        if(player == null)
        {
            //Main Menu
            SetMenu();
        }

    }

    /// <summary>
    /// incr > 0 increases aggro count by one, incr <= 0 decreases aggro count by one
    /// </summary>
    /// <param name="incr"></param>
    public void AggroCounter(int incr, bool aggroed)
    {
        if (incr > 0 && !aggroed)
            CombatCounter++;
        else if (CombatCounter > 0 && incr <= 0)
            CombatCounter--;
    }


    private void SetCombat()
    {
        if (MusicManager.MusicState.Combat == musicManager.current_music) return;
        musicManager.FadeMusic();
        musicManager.next_music = MusicManager.MusicState.Combat;
    }

    private void ExitCombat()
    {
        if (MusicManager.MusicState.OutOfCombat == musicManager.current_music) return;
        musicManager.FadeMusic();
        musicManager.next_music = MusicManager.MusicState.OutOfCombat;
    }

    public void SetBoss(int i)
    {
        MusicManager.MusicState music;
        if(i == 1) music = MusicManager.MusicState.Boss1;
        else       music = MusicManager.MusicState.Boss2;

        if (music == musicManager.current_music) return;
        musicManager.FadeMusic();
        musicManager.next_music = music;
    }

    private void SetDeath()
    {
        if (MusicManager.MusicState.Death == musicManager.current_music) return;
        musicManager.FadeMusic();
        musicManager.next_music = MusicManager.MusicState.Death;
    }


    private void SetMenu()
    {
        if (MusicManager.MusicState.Menu == musicManager.current_music) return;
        musicManager.FadeMusic();
        musicManager.next_music = MusicManager.MusicState.Menu;
    }

    void OnGUI() {
        if(useKeyboard) {
            if(isControlerInput()) {
                useKeyboard = false;
                Debug.Log("Switched to controller");
            }
        }
        else {
            if(isMouseKeyboard()) {
                useKeyboard = true;
                Debug.Log("Switched to keyboard");
            }
        }
    }

    private bool isMouseKeyboard() {
        // mouse & keyboard buttons
        if(Event.current.isKey || Event.current.isMouse) {
            return true;
        }
        // mouse movement
        if(Input.GetAxis("Mouse X") != 0.0f || Input.GetAxis("Mouse Y") != 0.0f) {
            return true;
        }
        return false;
    }

    private bool isControlerInput() {
        // joystick buttons
        for(KeyCode button = KeyCode.Joystick1Button0; button < KeyCode.Joystick1Button19; button++) {
            if(Input.GetKey(button))
                return true;
        }

        // joystick axis
        if(Input.GetAxis("Gun X") != 0.0f || Input.GetAxis("Gun Y") != 0.0f ||
           Input.GetAxis("Joy X") != 0.0f || Input.GetAxis("Joy Y") != 0.0f ||
           Input.GetAxis("Joy TG") != 0.0f) {
            return true;
        }

        return false;
    }
}
