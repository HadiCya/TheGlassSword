using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 3f);
    }
    void OnTriggerEnter2D(Collider2D col){
        if (col.gameObject.layer == LayerMask.NameToLayer("Player")){
            col.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
        // if (col.gameObject.layer == LayerMask.NameToLayer("Shield")){
        //     GameObject player = GameObject.FindGameObjectWithTag("Player");
        //     player.GetComponent<Weapons>().didBridge = false;
        //     player.GetComponent<Weapons>().hit = true;
        //     player.GetComponent<Weapons>().currShield = false;
        //     player.GetComponent<Weapons>().option = 3;
        //     col.gameObject.SetActive(false);
        //     Destroy(gameObject);
        // }
        //Add ground coll
    }
}
