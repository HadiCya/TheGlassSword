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

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col){
        if (col.gameObject.layer == LayerMask.NameToLayer("Enemy")){
            Debug.Log("DESTROY");
            Destroy(col.gameObject);
        }
        Destroy(gameObject);
    }

}
