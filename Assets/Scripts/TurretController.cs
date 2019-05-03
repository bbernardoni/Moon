using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{

    public Transform bullet;
    public Health hp;

    private Rigidbody2D rb2d;
    private Rigidbody2D player;
    private float cooldown = 0;
    private bool agroed = false;

    public Transform explosion;
    public AudioClip explosionSound;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (hp.alive)
        {
            Vector2 gun_vec = new Vector2(rb2d.transform.position.x, rb2d.transform.position.y);
            Vector2 player_vec = new Vector2(player.transform.position.x, player.transform.position.y);
            Vector2 gun_to_player = player_vec - gun_vec;
            RaycastHit2D see = Physics2D.Linecast(rb2d.transform.position, player_vec); //check if the enemy can see the player
            //Debug.Log(see.transform.tag);
            if(see.transform.tag == "Player")
            {
                float angle = Mathf.Atan2(gun_to_player.y, gun_to_player.x);
                rb2d.transform.rotation = Quaternion.Euler(0, 0, angle * 180 / Mathf.PI + 90);
                cooldown += Time.fixedDeltaTime;
                if(cooldown > 0.5)
                {
                    cooldown = 0;
                    var shooting = Instantiate(bullet, rb2d.transform.position, Quaternion.Euler(0, 0, angle * 180 / Mathf.PI));
                    shooting.tag = "EnemyAttack";
                }

                if(!agroed)
                    GameManager.instance.AggroCounter(1, false);
                agroed = true;
            }
            else {
                if(agroed)
                    GameManager.instance.AggroCounter(0, false);
                agroed = false;
            }
            
            
        }
        else {
            if(agroed)
                GameManager.instance.AggroCounter(0, false);
            SoundManager.instance.PlaySingle(explosionSound);
            Instantiate(explosion, rb2d.transform.position, Quaternion.identity);
            Destroy(rb2d.transform.parent.gameObject);
        }
    }
}
