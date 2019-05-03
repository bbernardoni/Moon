using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gun_ui : MonoBehaviour
{
    public GameObject[] gun_objects;
    public PlayerController player;
    public Image gun_image;
    public Sprite pistol;
    public Sprite shotgun;
    public Sprite railgun;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        switch (player.guns[player.currentGun])
        {
            case "Pistol1":
                gun_image.sprite = pistol;
                break;
            case "Shotgun1":
                gun_image.sprite = shotgun;
                break;
            case "Railgun1":
                gun_image.sprite = railgun;
                break;
            default:
                gun_image.sprite = pistol;
                break;

        }
    }
}
