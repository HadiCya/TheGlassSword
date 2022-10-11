using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    private float timer = 3f;
    GameObject player;
    bool bulletDirection;

    void Start(){
        player = GameObject.FindGameObjectWithTag("Player");
        bulletDirection = player.GetComponent<Movement>().isFacingRight;
    }
    void Update()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = bulletDirection ? Vector3.right * 20f : Vector3.right * -20f;
        if (timer <= 0){
            endBullet();
            gameObject.SetActive(false);
        }
        timer -= Time.deltaTime;
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        endBullet();
        if (col.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            player.GetComponent<Weapons>().option = 1;
            col.gameObject.SetActive(false);
        }
        gameObject.SetActive(false);
        
    }

    void endBullet(){
        player.GetComponent<Weapons>().hit = true;
        player.GetComponent<Weapons>().currBullet = false;
    }
}
