using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private GameObject bullet;
    public float shootSpeed = 3f;
    public float range = 20f;
    private float timer;
    private bool start;

    // Update is called once per frame
    void Start(){
        timer = shootSpeed;
    }
    void Update()
    {
        RaycastHit2D right = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.right), range, playerLayer);
        RaycastHit2D left = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.left), range, playerLayer);
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.right) * range, Color.red);
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.left) * range, Color.red);
        
        if ((right || left)){
            start = true;
        }
        if(start){
            timer -= Time.deltaTime;
        }
        if (timer <= 0f){
                GameObject bulletInstance = Instantiate(bullet, new Vector2(transform.position.x - 1f, transform.position.y), transform.rotation);
                bulletInstance.GetComponent<Rigidbody2D>().velocity = right ? Vector3.right * 20f : Vector3.right * -20f;
                timer = shootSpeed;
                start = false;
            }
        
    }
}
