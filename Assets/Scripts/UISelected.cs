using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISelected : MonoBehaviour {
    bool selected=false;

    void OnMouseEnter(){
        selected=true;
    }

    void OnMouseExit(){
        selected=false;
    }

    public bool IsSelected(){
        return selected;
    }

    public void SetSelected(bool b){
        selected=b;
    }
}
