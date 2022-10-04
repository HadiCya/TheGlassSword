using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update

    void Start()
    {
        Destroy(gameObject, 3f);
    }
    void OnTriggerEnter2D(Collider2D col){
        GameObject.FindGameObjectWithTag("Player").GetComponent<Weapons>().hit = true;
        GameObject.FindGameObjectWithTag("Player").GetComponent<Weapons>().option = 1;
        if (col.gameObject.layer == LayerMask.NameToLayer("Enemy")){
            Destroy(col.gameObject);
        }
        Destroy(gameObject);
    }
}
