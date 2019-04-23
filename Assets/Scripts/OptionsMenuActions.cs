using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class OptionsMenuActions : MonoBehaviour {
    public GameObject backMenu;
    public AudioMixer mixer;
    public AudioSource sfxTest;

    private bool sfxChanged = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {
        if(!sfxTest.isPlaying && sfxChanged) {
            sfxTest.Play();
            sfxChanged = false;
        }
    }

    public void SetMusicVol(float volume) {
        mixer.SetFloat("musicVol", Mathf.Log(volume) * 20);
    }

    public void SetSFXVol(float volume) {
        mixer.SetFloat("sfxVol", Mathf.Log(volume) * 20);

        if(!sfxTest.isPlaying) {
            sfxTest.Play();
            sfxChanged = false;
        }
        else {
            sfxChanged = true;
        }
    }

    public void Back() {
        backMenu.SetActive(true);
        gameObject.SetActive(false);
    }
}
