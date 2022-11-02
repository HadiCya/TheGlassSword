using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickVisuak : MonoBehaviour
{
    private PlayerControls playerControls;
    #region InputSystem //Sets up player controls with input system
    private void Awake(){
        playerControls = new PlayerControls();
    }
    private void OnEnable(){
        playerControls.Enable();
    }
    private void OnDisable(){
        playerControls.Disable();
    }
    #endregion

    // Update is called once per frame
    void Update()
    {
        Vector2 joystick = playerControls.Player.Choice.ReadValue<Vector2>();
        //Debug.Log(joystick);
        transform.localPosition = new Vector3(joystick.x, joystick.y)*10;
    }
}
