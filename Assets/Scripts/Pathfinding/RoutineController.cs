﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoutineController : MonoBehaviour
{
    public enum Routine
    {
        Patrol, Alert, Aggro, Forget
    }

    Routine routine;

    public bool aggroedOnce;

    void Start()
    {
        routine = Routine.Patrol;
        aggroedOnce = false;
    }

    void Update()
    {
        if (routine == Routine.Forget)
            routine = Routine.Patrol;
    }

    public bool SetAlert()
    {
        if (routine == Routine.Alert) return false;
        routine = Routine.Alert;
        return true;
    }

    public bool GetAlert()
    {
        if (routine == Routine.Alert) return true; else return false;
    }

    public bool SetForget()
    {
        if (routine == Routine.Forget) return false;
        routine = Routine.Forget;
        return true;
    }

    public bool GetForget()
    {
        if (routine == Routine.Forget) return true; else return false;
    }

    public bool SetAggro()
    {
        if (routine == Routine.Aggro) return false;
        //Debug.Log("Hi");
        GameManager.instance.AggroCounter(1, aggroedOnce);
        aggroedOnce = true;
        routine = Routine.Aggro;
        return true;
    }

    public bool GetAggro()
    {
        if (routine == Routine.Aggro) return true; else return false;
    }

    public bool SetPatrol()
    {
        if (routine == Routine.Patrol) return false;
        routine = Routine.Patrol;
        return true;
    }

    public bool GetPatrol()
    {
        if (routine == Routine.Patrol) return true; else return false;
    }
}
