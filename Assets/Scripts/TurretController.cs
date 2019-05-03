using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{

    public Transform bullet;

    private Rigidbody2D rb2d;
    private Rigidbody2D player;

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
        Vector2 gun_vec = new Vector2(rb2d.transform.position.x, rb2d.transform.position.y);
        Vector2 player_vec = new Vector2(player.transform.position.x, player.transform.position.y);
        player_vec += player.velocity / 8;
        Vector2 gun_to_player = player_vec - gun_vec;
        Debug.Log(gun_to_player);
        float angle = Mathf.Atan2(gun_to_player.y, gun_to_player.x);
        rb2d.transform.rotation = Quaternion.Euler(0, 0, angle * 180 / Mathf.PI - 90);
    }
}
