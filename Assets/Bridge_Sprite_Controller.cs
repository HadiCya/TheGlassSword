using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge_Sprite_Controller : MonoBehaviour
{
    private SpriteRenderer renderer;
    public Animation animation;
    public double timeUntilBridgeBreaks;

    // Start is called before the first frame update
    void Start()
    {
        
        animation = GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timeUntilBridgeBreaks > 0)
        {
            timeUntilBridgeBreaks -= Time.deltaTime;
            //Debug.Log("Time left: " + timeUntilBridgeBreaks.ToString());
        } else
        {
            Debug.Log("Bridge Destroyed!");
            Destroy(this.gameObject);
        }
    }
}
