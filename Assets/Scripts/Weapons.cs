using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapons : MonoBehaviour
{
    private PlayerControls playerControls;
    private Vector2 direction;
    public GameObject bullet;
    public Transform shootingPosition;
    private bool isFacingRight;
    private string action = "";
    public GameObject txt;
    private int option;

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
        isFacingRight = GetComponent<Movement>().isFacingRight;
        direction = playerControls.Player.Choice.ReadValue<Vector2>();
        float dirX = direction.x;
        float dirY = direction.y;
        //Debug.Log(direction);
        if ((dirX > -0.3f && dirX < 0.3f) && (dirY > 0.7f && dirY < 1f) && option != 1){
            txt.GetComponent<TMPro.TextMeshProUGUI>().text = "BULLET";
            action = "bullet";
        } else if ((dirX > 0.7f && dirX < 1f) && (dirY > -0.3f && dirY < 0.3f) && option != 2){
            txt.GetComponent<TMPro.TextMeshProUGUI>().text = "SWORD";
            action = "sword";
        } else if ((dirX > -0.3f && dirX < 0.3f) && (dirY > -1f && dirY < -0.7f) && option != 3){
            txt.GetComponent<TMPro.TextMeshProUGUI>().text = "SHIELD";
            action = "shield";
        } else if ((dirX > -1f && dirX < -0.7f) && (dirY > -0.3f && dirY < 0.3f) && option != 4){
            txt.GetComponent<TMPro.TextMeshProUGUI>().text = "BRIDGE";
            action = "bridge";
        }

        bool temp = playerControls.Player.Fire.IsPressed();
        if (temp){
            switch (action){
                case "bullet":
                    GameObject bulletInstance = Instantiate(bullet, shootingPosition.position, transform.rotation);
                    if(isFacingRight){
                        bulletInstance.GetComponent<Rigidbody2D>().velocity = transform.right * 5f;
                    } else {
                        bulletInstance.GetComponent<Rigidbody2D>().velocity = transform.right * -5f;
                    }
                    action = "";
                    option = 1;
                    break;
                case "sword":
                    
                    action = "";
                    option = 2;
                    break;
                case "shield":
                    
                    action = "";
                    option = 3;
                    break;
                case "bridge":
                    
                    action = "";
                    option = 4;
                    break;
            }
        }
    }
}
