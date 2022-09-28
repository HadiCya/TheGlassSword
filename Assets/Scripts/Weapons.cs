using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    private PlayerControls playerControls;
    private Vector2 direction;
    public GameObject bullet;
    private string action = "";

    private void Awake(){
        playerControls = new PlayerControls();
    }

    private void OnEnable(){
        playerControls.Enable();
    }

    private void OnDisable(){
        playerControls.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        direction = playerControls.Player.Choice.ReadValue<Vector2>();
        direction.Set(direction.x, direction.y);
        Vector2 v2 = new Vector2(0,1);
        if (action == ""){
        if (direction == Vector2.up){
            action = "sword";
        } else if (direction == Vector2.right){
            action = "bullet";
        } else if (direction == Vector2.down){
            action = "shield";
        } else if (direction == Vector2.left){
            action = "bridge";
        }
        }

        bool temp = playerControls.Player.Fire.IsPressed();
        if (temp){
            switch (action){
                case "bullet":
                    Instantiate(bullet, transform.position, transform.rotation);
                    Debug.Log("BULLET");
                    action = "";
                    break;
                case "sword":
                    Debug.Log("SWORD");
                    action = "";
                    break;
                case "shield":
                    Debug.Log("SHIELD");
                    action = "";
                    break;
                case "bridge":
                    Debug.Log("BRIDGE");
                    action = "";
                    break;
            }
        }
    }
}
