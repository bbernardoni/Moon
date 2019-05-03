using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBossMusic : MonoBehaviour
{
    public int boss_level;
    // Start is called before the first frame update
    void Awake()
    {
        GameManager.instance.CombatCounter = 0;
        GameManager.instance.isBoss = true;
        GameManager.instance.SetBoss(boss_level);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
