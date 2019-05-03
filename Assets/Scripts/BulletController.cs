using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed;
    public float dmg;
    public float maxDistance;

    private float distance;

    void Start()
    {
        distance = 0;
    }

    void Update()
    {
        if (transform.parent.tag == "PlayerAttack")
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime * 3);
        }else{
            transform.Translate(Vector2.right * speed * Time.deltaTime * 2);

        }
        distance += speed * Time.deltaTime * 2; 
        if(distance >= maxDistance)
        {
            Destroy(transform.parent.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player" && collision.tag != "Ethereal" && transform.parent.tag == "PlayerAttack" && collision.tag!="Untagged") 
        {
            if (collision.tag == "Killable")
            {
                collision.gameObject.GetComponent<Health>().takeDamage(dmg);
                EnemyController enemy = collision.gameObject.GetComponent<EnemyController>();
                if(enemy) {
                    if(transform.parent.name == "PistolBullet(Clone)"){
                        enemy.ReceiveStun();
                        enemy.KnockBack(transform.right, 0.15f);
                    }else if(transform.parent.name == "ShotgunBullet(Clone)"){
                        enemy.KnockBack(transform.right, 0.4f);
                    }
                }
            }
            Destroy(transform.parent.gameObject);
        }
        else if(transform.parent.tag == "Energy" && collision.tag == "Player")
        {
            collision.gameObject.GetComponent<Health>().takeDamage(dmg);
            Destroy(transform.parent.gameObject);
        }
        else if (collision.tag != "Killable" && (transform.parent.tag == "EnemyAttack" || transform.parent.tag == "BossAttack") && collision.tag != "BossAttack")
        {
            if(collision.tag == "Player")
            {
                collision.gameObject.GetComponent<Health>().takeDamage(dmg);
            }
            Destroy(transform.parent.gameObject);
        }
    }
}
