using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed;
    public float dmg;
    public float distance;

    void Start()
    {
        distance = 0;
    }

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime *2);
        distance += speed * Time.deltaTime * 2; 
        if(distance >= 5 && transform.parent.name == "ShotgunBullet(Clone)"){
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
                if(transform.parent.name == "PistolBullet(Clone)"){
                    collision.gameObject.GetComponent<EnemyController>().ReceiveStun();
                }
            }
            Destroy(transform.parent.gameObject);
        }
        else if (collision.tag != "Killable" && transform.parent.tag == "EnemyAttack")
        {
            if(collision.tag == "Player")
            {
                collision.gameObject.GetComponent<Health>().takeDamage(dmg);
            }
            Destroy(transform.parent.gameObject);
        }
    }
}
