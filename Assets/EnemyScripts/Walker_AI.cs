using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walker_AI : MonoBehaviour
{

    int dz;
    int moveSpeed;

    public void setMoveSpeed(int speed)
    {
        moveSpeed = speed;
    }

    public int getMoveSpeed()
    {
        return moveSpeed;
    }

    public void setdz(int newdz)
    {
        dz = newdz;
    }

    public int getdz()
    {
        return dz;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
