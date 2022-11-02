using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class YouWin : MonoBehaviour
{
    public GameObject winLose;

    void Update(){
        if (winLose.GetComponent<TMPro.TextMeshProUGUI>().text != ""){
            Invoke("RestartGame", 1.5f);
        }
    }

    void OnTriggerEnter2D(Collider2D coll){
       if (coll.gameObject.layer == LayerMask.NameToLayer("Player")){
            winLose.GetComponent<TMPro.TextMeshProUGUI>().text = "YOU WIN";
       }
    }
    void RestartGame(){
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
