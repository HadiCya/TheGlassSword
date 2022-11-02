using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorCheck : MonoBehaviour
{
    public float moveSpeed;

    // Update is called once per frame

    void OnTriggerEnter2D(Collider2D coll){
        if (coll.gameObject.layer != LayerMask.NameToLayer("Ground")){
            coll.gameObject.SetActive(false);
        }
    }
}
