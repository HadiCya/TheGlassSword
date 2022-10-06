using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D col){
        if (col.gameObject.layer == LayerMask.NameToLayer("Enemy") || col.gameObject.layer == LayerMask.NameToLayer("Bullet")){
            GetComponentInParent<Weapons>().hit = true;
            GetComponentInParent<Weapons>().didBridge = false;
            GetComponentInParent<Weapons>().currShield = false;
            GetComponentInParent<Weapons>().option = 3;
            Destroy(col.gameObject);
            gameObject.SetActive(false);
        }
    }

}
