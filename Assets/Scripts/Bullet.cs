using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * (Time.deltaTime * 10f));
    }

    void OnCollisionEnter2D(Collision2D coll){
        Destroy(gameObject);
    }
}
