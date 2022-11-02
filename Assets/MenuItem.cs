using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuItem : MonoBehaviour
{
    public Color selectColor;
    public Color baseColor;
    public Color disabledColor;
    public Image background; 

    void Start()
    {
        background.color = baseColor; 
    }

    public void Select(){
        background.color = selectColor;
    }

    public void deSelect(){
        background.color = baseColor;
    }

    public void grayOut(){
        background.color = disabledColor;
    }

    public void notSelect(){
        if (background.color != disabledColor){
            deSelect();
        }
    }
}
