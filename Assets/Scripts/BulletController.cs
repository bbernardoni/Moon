using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed;

    void Start(){

    }
    
    void Update(){
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if(collision.tag != "Player")
            Destroy(transform.parent.gameObject);
    }
}
