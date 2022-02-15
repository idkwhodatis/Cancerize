using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Init : MonoBehaviour {
    public MenuController menu;
    public GameObject eventOverlay;

    // Start is called before the first frame update
    void Start() {
        GameObject.Find("InvationOverlay").SetActive(false);
        GameObject.Find("StructureOverlay").SetActive(false);
        eventOverlay.GetComponent<EventController>().StartEvent(DialogueLoader.LoadDialogue("init"),true,DialogueLoader.LoadDialogue("init2"));
    }

    // Update is called once per frame
    void Update() {
        if(Input.GetKeyDown(KeyCode.Escape)){
            menu.PauseResume();
        }
    }
}
