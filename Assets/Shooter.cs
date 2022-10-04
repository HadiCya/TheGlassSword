using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private GameObject bullet;
    public float shootSpeed = 4f;
    private float timer = 4f;

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D right = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.right), 10f, playerLayer);
        RaycastHit2D left = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.left), 10f, playerLayer);
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.right) * 10f, Color.red);
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.left) * 10f, Color.red);
        
        if ((right || left)){
            timer -= Time.deltaTime;
            if (timer <= 0f){
                GameObject bulletInstance = Instantiate(bullet, new Vector2(transform.position.x - 1f, transform.position.y), transform.rotation);
                bulletInstance.GetComponent<Rigidbody2D>().velocity = right ? Vector3.right * 20f : Vector3.right * -20f;
                timer = shootSpeed;
            }
        }
        
    }
}
