using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour{
    public GameObject statusBar;
    public GameObject invationPanel;
    public GameObject structurePanel;
    public GameObject MenuOverlay;
    public GameObject choosePanel;
    public GameObject eventOverlay;
    public GameObject winloseOverlay;
    List<GameObject> UIs=new List<GameObject>();
    // Start is called before the first frame update
    void Start(){
        UIs.Add(statusBar);
        UIs.Add(invationPanel);
        UIs.Add(structurePanel);
        UIs.Add(MenuOverlay);
        UIs.Add(choosePanel);
        UIs.Add(eventOverlay);
        UIs.Add(winloseOverlay);
    }

    // Update is called once per frame
    void Update(){
        Vector3 pos=transform.position;
        var mousePosition = Input.mousePosition;
        float scroll = Input.GetAxis ("Mouse ScrollWheel");
        float ortho=Camera.main.orthographicSize;
        float prev=ortho;
 
        if (mousePosition.x <= 0){
            pos.x-=5*Time.deltaTime;
        } else if (mousePosition.x >= Screen.width - 1){
            pos.x+=5*Time.deltaTime;
        }
        
        if (mousePosition.y <= 0){
            pos.y-=7*Time.deltaTime;
        } else if (mousePosition.y >= Screen.height- 1){
            pos.y+=7*Time.deltaTime;
        }
        
        if(Math.Abs(pos.x)>2){
            pos.x=pos.x>0?2:-2;
        }
        if(pos.y>5.5f+5-ortho){
            pos.y=5.5f+5-ortho;
        }else if(pos.y<-5+ortho-5){
            pos.y=-5+ortho-5;
        }

        transform.position=pos;


        if (scroll != 0.0f) {
            ortho -= scroll * 10;
            ortho = Mathf.Clamp (ortho, 4, 10.25f);
            foreach(var ui in UIs){
                ui.transform.localScale=new Vector3(ui.transform.localScale.x*ortho/prev,ui.transform.localScale.y*ortho/prev,1);
            }
        }
        Camera.main.orthographicSize = ortho;
    }

    public List<GameObject> GetUIs(){
        return UIs;
    }
}
