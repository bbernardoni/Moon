using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAbility : MonoBehaviour
{
    public DashState currentState = DashState.Ready;
    public Frames frame = Frames.WindUp;
    public float cooldownTime = 2f;
    public float windUpTime = 0.1f;
    public float dmgTime = 2f;
    public float vlnTime = 3f;
    public float dashTime;

    public bool dashing;

    public Vector2 savedVelocity;

    public Rigidbody2D rb2d;

    // Update is called once per frame
    void Update()
    {
        switch(currentState)
        {
            case DashState.Ready:
                if (Input.GetKeyDown(KeyCode.LeftShift))
                {
                    savedVelocity = rb2d.velocity;
                    Debug.Log("Dashing");
                    dashing = true;
                    currentState = DashState.Dash;
                }
                break;
            case DashState.Dash:
                dashTime += Time.deltaTime * 9;
                switch (frame)
                {
                    case Frames.WindUp:
                        if (dashTime >= windUpTime)
                            frame = Frames.PreDash;
                        break;
                    case Frames.PreDash:
                        rb2d.velocity = new Vector2(rb2d.velocity.x * 6, rb2d.velocity.y * 6);
                        Debug.Log("Incremented Speed");
                        frame = Frames.Damage;
                        break;
                    case Frames.Damage:
                        // Damage logic
                        if (dashTime >= dmgTime)
                            frame = Frames.Vulnerable;
                        break;
                    case Frames.Vulnerable:
                        // Vulnerability logic
                        if (dashTime >= vlnTime)
                            frame = Frames.End;
                        break;
                    case Frames.End:
                        dashTime = cooldownTime;
                        frame = Frames.WindUp;
                        currentState = DashState.Cooldown;
//                        rb2d.velocity = savedVelocity;
                        break;
                }
                break;
            case DashState.Cooldown:
                dashTime -= Time.deltaTime;
                dashing = false;
                if (dashTime <= 0)
                {
                    dashTime = 0;
                    currentState = DashState.Ready;
                }
                break;
        }

   
        
    }

    public enum DashState
    {
        Ready, Dash, Cooldown
    }

    public enum Frames
    {
        WindUp, PreDash, Damage, Vulnerable, End
    }
}
