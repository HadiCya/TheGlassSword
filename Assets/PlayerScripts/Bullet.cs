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
        // RaycastHit2D hitscan = Physics2D.Raycast(transform.position, transform.TransformDirection(bulletDirection ? Vector2.right : Vector2.left), 0.2f, LayerMask.NameToLayer("Enemy"));
        // Debug.DrawRay(transform.position, bulletDirection ? Vector2.right : Vector2.left, Color.green, 0.2f);
        // if (hitscan){
        //     Debug.Log("hit");
        //     endBullet();
        //     player.GetComponent<Weapons>().option = 1;
        //     hitscan.collider.gameObject.SetActive(false);
        //     gameObject.SetActive(false);
        // }
        if (timer <= 0){
            endBullet();
            gameObject.SetActive(false);
        }
        timer -= Time.deltaTime;
    }
    void OnTriggerEnter2D(Collider2D col){
        endBullet();
        player.GetComponent<Weapons>().option = 1;
        if (col.gameObject.layer == LayerMask.NameToLayer("Enemy")){
            col.gameObject.SetActive(false);
        }
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        gameObject.SetActive(false);
    }

    void endBullet(){
        player.GetComponent<Weapons>().hit = true;
        player.GetComponent<Weapons>().currBullet = false;
    }
}
