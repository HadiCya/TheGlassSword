using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{

    void OnColliderEnter2D(Collision2D col){
        if (col.gameObject.layer == LayerMask.NameToLayer("Enemy") || col.gameObject.layer == LayerMask.NameToLayer("Bullet")){
            GetComponentInParent<Weapons>().didBridge = false;
            GetComponentInParent<Weapons>().hit = true;
            GetComponentInParent<Weapons>().currShield = false;
            GetComponentInParent<Weapons>().option = 3;
            Debug.Log("huh??");
            Destroy(col.gameObject);
            gameObject.SetActive(false);
        }
    }

}
