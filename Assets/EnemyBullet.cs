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
            Debug.Log("PLAYER DETECTED");
            Destroy(col.gameObject);
        }
        Destroy(gameObject);
    }
}
