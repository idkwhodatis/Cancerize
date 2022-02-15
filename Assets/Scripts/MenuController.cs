using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour{
    public GameObject menu;
    public Button resumeBtn;
    public Button exitBtn;
    bool paused=false;
    void Start(){
        resumeBtn.onClick.AddListener(PauseResume);
        exitBtn.onClick.AddListener(Exit);
    }
    public void PauseResume(){
        if(menu.activeSelf){
            Time.timeScale=1;
            menu.SetActive(false);
            paused=false;
        }else{
            Time.timeScale=0;
            menu.SetActive(true);
            paused=true;
        }
    }
    public void Exit(){
        Application.Quit();
    }
    public bool NotPaused(){
        return !paused;
    }
    public void SetPaused(bool b){
        paused=b;
    }
}
