using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float MaxHP;
    public float RemainingHP;
    public Rigidbody2D rb2d;
    public UIController uIController;

    public bool alive = true;

    void Start()
    {
        RemainingHP = MaxHP;
    }

    // Update is called once per frame
    public bool takeDamage(float dmg)
    {
        RemainingHP -= dmg;
        if (RemainingHP <= 0)
        {
            alive = false;
            if(uIController)
                uIController.GameOver();
            Debug.Log("Killed!");
            return true;
        }
        PlayerSoundController psc = GetComponent<PlayerSoundController>();
        if(psc)
            psc.TakeDamage();
        Debug.Log("Damage!");
        return false;
    }

    public void takeHeal(float heal)
    {
        if ((RemainingHP += heal) > MaxHP)
            RemainingHP = MaxHP;
        else
            RemainingHP += heal;
    }

}
