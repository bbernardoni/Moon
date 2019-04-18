using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class death_screen : MonoBehaviour {
    public Button restart;
    public Button menu;

    // Start is called before the first frame update
    void Start()
    {
        restart.onClick.AddListener(restart_level);
        menu.onClick.AddListener(to_main_menu);
}

    // Update is called once per frame
    void Update()
    {
    }

    void restart_level() {
        Scene thisScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(thisScene.name);
    }

    void to_main_menu() {
        SceneManager.LoadScene("Main Menu");
    }
}
