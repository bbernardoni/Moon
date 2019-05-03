using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using Pathfinding;

public class Boss2Controller : MonoBehaviour
{
    public Health hp;
    public Transform corpse;
    public Transform lazer;
    public Transform bullet;
    public Transform gunPivot;
    public Transform shotgun;
    public Transform enemy;
    public Transform wall;
    public Transform explosion;

    private Rigidbody2D rb2d;
    private Rigidbody2D player;

    private float gunCooldown = 0;
    private float maxSpeed = 2;
    private bool agroed = false;
    private bool shootingGun = true;
    private bool first_door = false;
    private bool second_door = true;
    private bool player_is_alive = true;
    private bool enemy_spawned = false;
    private Vector3 currDirection = new Vector3(0.3f, -1, 0);

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (player.GetComponent<Health>().RemainingHP <= 0)
        {
            player_is_alive = false;
        }
        if(!enemy_spawned && hp.GetComponent<Health>().RemainingHP <= 750)
        {
            //Instantiate(enemy, new Vector3(16, 4, 0), Quaternion.identity);
            enemy_spawned = true;
        }
    }

    void FixedUpdate()
    {
        if (hp.alive && player_is_alive)
        {
            Vector2 enemy_vec = new Vector2(rb2d.transform.position.x, rb2d.transform.position.y);
            Vector2 player_vec = new Vector2(player.transform.position.x, player.transform.position.y);
            Vector2 enemy_to_player = player_vec - enemy_vec;
            if (!agroed)
            {
                float distance = enemy_to_player.sqrMagnitude;
                if (distance < 260)
                {
                    agroed = true;
                    if (first_door == false)
                    {
                        Instantiate(wall, new Vector3(-22.75f, -11.5f, 0), Quaternion.identity);
                        first_door = true;
                    }
                }
            }
            if (agroed)
            {
                aimGun(shotgun, gunPivot);
                move();
                handleShooting();
            }
        }
        else if (!hp.alive)
        {
            Instantiate(explosion, rb2d.transform.position, Quaternion.identity);
            Instantiate(corpse, rb2d.transform.position, Quaternion.Euler(0, 0, -10));
            Destroy(rb2d.gameObject);
            if (second_door == true)
            {
                Destroy(wall.gameObject);
                second_door = false;
            }
        }
    }

    private void aimGun(Transform gun, Transform gunPivot)
    {
        Vector2 player_vec = new Vector2(player.transform.position.x, player.transform.position.y);
        player_vec += player.velocity / 8; //woww this makes it tough
        Vector2 gun_vec = new Vector2(gun.transform.position.x, gun.transform.position.y);
        gun_vec = player_vec - gun_vec;

        float angle = Mathf.Atan2(gun_vec.y, gun_vec.x);
        gunPivot.rotation = Quaternion.Euler(0, 0, angle * 180 / Mathf.PI);
    }

    private void move()
    {
        if (rb2d.transform.position.y > 3.0)
        {
            currDirection.y = -1;
            random_switch();
            gunCooldown = 0;
        }
        else if(rb2d.transform.position.y < -6.0)
        {
            currDirection.y = 1;
            random_switch();
            gunCooldown = 0;
        }
        if(rb2d.transform.position.x > 32.5)
        {
            currDirection.x = -0.3f;
        }
        else if(rb2d.transform.position.x < 31.5)
        {
            currDirection.x = 0.3f;
        }
        currDirection.Normalize();
        rb2d.velocity = currDirection * maxSpeed;
    }

    private void random_switch()
    {
        if (shootingGun)
        {
            double random = Random.Range(0, 2.5f);
            if (random < 1.0f)
            {
                shootingGun = false;
                maxSpeed = 4;
            }
        }
        else
        {
            shootingGun = true;
            maxSpeed = 2;
        }
    }

    private void handleShooting()
    {
        gunCooldown += 1;
        if (shootingGun)
        {
            if (gunCooldown > 250)
            {
                gunCooldown = 0;
                shootGun();
            }
        }
        else
        {
            if(gunCooldown > 150)
            {
                gunCooldown = 0;
                shootBullet(lazer, Quaternion.Euler(0,180,0));
            }
        }
    }

    private void shootGun()
    {
        //GetComponent<PlayerSoundController>().FireShotgun();
        Quaternion bullet2Rotation = Quaternion.Euler(shotgun.rotation.eulerAngles.x, shotgun.rotation.eulerAngles.y, shotgun.rotation.eulerAngles.z - 25);
        Quaternion bullet3Rotation = Quaternion.Euler(shotgun.rotation.eulerAngles.x, shotgun.rotation.eulerAngles.y, shotgun.rotation.eulerAngles.z - 50);
        Quaternion bullet4Rotation = Quaternion.Euler(shotgun.rotation.eulerAngles.x, shotgun.rotation.eulerAngles.y, shotgun.rotation.eulerAngles.z + 25);
        Quaternion bullet5Rotation = Quaternion.Euler(shotgun.rotation.eulerAngles.x, shotgun.rotation.eulerAngles.y, shotgun.rotation.eulerAngles.z + 50);
        

        shootBullet(bullet, shotgun.rotation);
        shootBullet(bullet, bullet2Rotation);
        shootBullet(bullet, bullet3Rotation);
        shootBullet(bullet, bullet4Rotation);
        shootBullet(bullet, bullet5Rotation);
    }

    void shootBullet(Transform bulletType, Quaternion rotation)
    {
        Instantiate(bulletType, shotgun.position, rotation);
    }
}
