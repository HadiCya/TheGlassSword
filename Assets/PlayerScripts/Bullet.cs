using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    private float timer = 3f;
    GameObject player;

    void Start(){
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        if (timer <= 0){
            endBullet();
            Destroy(gameObject);
        }
        timer -= Time.deltaTime;
    }
    void OnTriggerEnter2D(Collider2D col){
        endBullet();
        player.GetComponent<Weapons>().option = 1;
        if (col.gameObject.layer == LayerMask.NameToLayer("Enemy") || col.gameObject.layer == LayerMask.NameToLayer("Bullet")){
            Destroy(col.gameObject);
        }
        Destroy(gameObject);
    }

    void endBullet(){
        player.GetComponent<Weapons>().hit = true;
        player.GetComponent<Weapons>().didBridge = false;
        player.GetComponent<Weapons>().currBullet = false;
    }
}
