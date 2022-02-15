using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventController : MonoBehaviour {
    public GameObject menu;
    public GameObject eventOverlay;
    public Text title;
    public Text eventInfo;
    public Button ok;
    Queue<string>[] infoAfter;
    void Start(){

    }
    public void StartEvent(Queue<string>[] info, bool after=false, Queue<string>[] info2=null){
        Time.timeScale=0;
        eventOverlay.SetActive(true);
        menu.GetComponent<MenuController>().SetPaused(true);
        title.text=info[0].Dequeue();
        eventInfo.text=info[1].Dequeue();
        ok.onClick.RemoveAllListeners();
        ok.onClick.AddListener(CloseEvent);
        if(after){
            infoAfter=info2;
            ok.onClick.RemoveAllListeners();
            ok.onClick.AddListener(StartEventAfter);
        }
    }

    public void StartEventAfter(){
        title.text=infoAfter[0].Dequeue();
        eventInfo.text=infoAfter[1].Dequeue();
        ok.onClick.RemoveAllListeners();
        ok.onClick.AddListener(CloseEvent);
    }

    public void CloseEvent(){
        Time.timeScale=1;
        eventOverlay.SetActive(false);
        menu.GetComponent<MenuController>().SetPaused(false);
    }

}
