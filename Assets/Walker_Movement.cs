using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walker_Movement : Walker_AI
{
    public Rigidbody2D rb2d;
    public int _moveSpeed;
    public int _dz;

    // Start is called before the first frame update
    void Start()
    {
        setMoveSpeed(_moveSpeed);
        setdz(_dz);
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb2d.AddForce(Vector2.right/_moveSpeed);
        
    }
}
