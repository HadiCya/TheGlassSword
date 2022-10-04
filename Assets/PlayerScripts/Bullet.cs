using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    private float timer = 3f;

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
        GameObject.FindGameObjectWithTag("Player").GetComponent<Weapons>().option = 1;
        if (col.gameObject.layer == LayerMask.NameToLayer("Enemy")){
            Destroy(col.gameObject);
        }
        Destroy(gameObject);
    }

    void endBullet(){
        GameObject.FindGameObjectWithTag("Player").GetComponent<Weapons>().hit = true;
        GameObject.FindGameObjectWithTag("Player").GetComponent<Weapons>().currBullet = false;
    }
}
