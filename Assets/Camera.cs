using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public float moveSpeed;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x + (Time.deltaTime * moveSpeed), transform.position.y, transform.position.z);
    }
}
