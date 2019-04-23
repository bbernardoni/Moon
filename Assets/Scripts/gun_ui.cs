using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gun_ui : MonoBehaviour
{
    public GameObject[] gun_objects;
    public PlayerController player;
    public Image gun_image;
    public Sprite pistol;
    public Sprite shotgun;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        switch (player.guns[player.currentGun])
        {
            case "pistol":
                gun_image.sprite = pistol;
                break;
            case "shotgun":
                gun_image.sprite = shotgun;
                break;
            default:
                gun_image.sprite = pistol;
                break;

        }
    }
}
