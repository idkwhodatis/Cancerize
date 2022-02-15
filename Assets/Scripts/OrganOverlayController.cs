using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrganOverlayController : MonoBehaviour {
    bool selected=false;
    public bool invaded;
    List<GameObject> UIs;
    List<GameObject> Organs=new List<GameObject>();
    List<string> structures;
    Color currColor=new Color(1,0,0,0.3960784f);
    static Dictionary<string, int[]> organVals;
    List<int> activatedEvents;

    // Start is called before the first frame update
    void Start() {
        invaded=false;
        UIs=Camera.main.gameObject.GetComponent<CameraMover>().GetUIs();
        structures=new List<string>();
        Organs.Add(GameObject.Find("Liver"));
        Organs.Add(GameObject.Find("Lung"));
        Organs.Add(GameObject.Find("Brain"));
        Organs.Add(GameObject.Find("Intestine"));
        Organs.Add(GameObject.Find("Stomach"));
        organVals=new Dictionary<string,int[]>();
        organVals.Add("Liver",new int[]{500,8,54});
        organVals.Add("Lung",new int[]{100,6,6});
        organVals.Add("Brain",new int[]{1000,40,54});
        organVals.Add("Intestine",new int[]{200,10,10});
        organVals.Add("Stomach",new int[]{400,16,18});
        activatedEvents=new List<int>();
    }

    // Update is called once per frame
    void Update() {
        if(UIs[3].GetComponent<MenuController>().NotPaused()){
            if(Input.GetMouseButton(0)&&!selected&&!UIs[1].GetComponent<UISelected>().IsSelected()&&!UIs[2].GetComponent<UISelected>().IsSelected()){
                gameObject.GetComponent<UnityEngine.U2D.SpriteShapeRenderer>().color=currColor;
                if(UIs[1].activeSelf){
                    if(UIs[1].transform.Find("OrganName").GetComponent<UnityEngine.UI.Text>().text==gameObject.name){
                        UIs[1].SetActive(false);
                    }
                }
                if(UIs[2].activeSelf){
                    if(UIs[2].transform.Find("OrganName").GetComponent<UnityEngine.UI.Text>().text==gameObject.name){
                        UIs[2].SetActive(false);
                    }
                }
            }
        }

        if(UIs[2].activeSelf&&UIs[2].transform.Find("OrganName").GetComponent<UnityEngine.UI.Text>().text==gameObject.name){
            switch(structures.Count){
                case 0:
                    UIs[2].transform.Find("Slot1").transform.Find("Text").GetComponent<UnityEngine.UI.Text>().text="";
                    UIs[2].transform.Find("Slot2").transform.Find("Text").GetComponent<UnityEngine.UI.Text>().text="";
                    break;
                case 1:
                    UIs[2].transform.Find("Slot1").transform.Find("Text").GetComponent<UnityEngine.UI.Text>().text=structures[0];
                    UIs[2].transform.Find("Slot2").transform.Find("Text").GetComponent<UnityEngine.UI.Text>().text="";
                    break;
                case 2:
                    UIs[2].transform.Find("Slot1").transform.Find("Text").GetComponent<UnityEngine.UI.Text>().text=structures[0];
                    UIs[2].transform.Find("Slot2").transform.Find("Text").GetComponent<UnityEngine.UI.Text>().text=structures[1];
                    break;
            }
        }
    }

    void OnMouseDown(){
        if(UIs[3].GetComponent<MenuController>().NotPaused()){
            // GetComponent<Renderer>().enabled = !GetComponent<Renderer>().enabled;
            gameObject.GetComponent<UnityEngine.U2D.SpriteShapeRenderer>().color=new Color(1,0.7529412f,0.7529412f,0.3960784f);
            foreach(var organ in Organs){
                if(organ.name!=gameObject.name){
                    organ.GetComponent<UnityEngine.U2D.SpriteShapeRenderer>().color=currColor;
                }
            }
        
            if(!invaded){
                UIs[2].SetActive(false);
                UIs[1].SetActive(true);
                UIs[1].transform.Find("OrganName").GetComponent<UnityEngine.UI.Text>().text=gameObject.name;
                UIs[1].transform.Find("Invade").GetComponent<UnityEngine.UI.Button>().onClick.RemoveAllListeners();
                UIs[1].transform.Find("Invade").GetComponent<UnityEngine.UI.Button>().onClick.AddListener(Invade);
                var currVal=organVals[gameObject.name];
                UIs[1].transform.Find("RField").GetComponent<UnityEngine.UI.Text>().text=currVal[0].ToString()+"k";
                UIs[1].transform.Find("CField").GetComponent<UnityEngine.UI.Text>().text=currVal[1].ToString()+"k";
                UIs[1].transform.Find("EField").GetComponent<UnityEngine.UI.Text>().text=currVal[2].ToString()+"k";
            }else{
                ShowStructure();
            }
        }
    }

    void OnMouseEnter(){
        selected=true;
    }

    void OnMouseExit(){
        selected=false;
    }

    public void SetSelected(bool b){
        selected=b;
    }

    public void AddStructure(string str){
        if(structures.Count<2){
            structures.Add(str);
        }
    }

    void Invade(){
        var currVal=organVals[gameObject.name];
        if(GameObject.Find("StatusOverlay").GetComponent<StatusListener>().cellNum>=currVal[0]){
            GameObject.Find(UIs[1].transform.Find("OrganName").GetComponent<UnityEngine.UI.Text>().text).GetComponent<OrganOverlayController>().invaded=true;
            UIs[1].GetComponent<UISelected>().SetSelected(false);
            GameObject.Find("StatusOverlay").GetComponent<StatusListener>().cellNum-=currVal[0];
            GameObject.Find("StatusOverlay").GetComponent<StatusListener>().cellSpeed+=currVal[1];
            GameObject.Find("StatusOverlay").GetComponent<StatusListener>().energySpeed+=currVal[2];

            bool hasWin=true;
            foreach(GameObject o in Organs){
                hasWin=hasWin&&o.GetComponent<OrganOverlayController>().invaded;
            }
            if(hasWin){
                UIs[6].GetComponent<WinLoseController>().Win(0);
            }else{
                ShowStructure();
                drawDice();
            }
        }
    }

    public void ShowStructure(){
        UIs[1].SetActive(false);
        UIs[2].SetActive(true);
        UIs[2].transform.Find("OrganName").GetComponent<UnityEngine.UI.Text>().text=gameObject.name;
        switch(structures.Count){
            case 0:
                UIs[2].transform.Find("Slot1").GetComponent<UnityEngine.UI.Button>().onClick.RemoveListener(UIs[4].GetComponent<ChooseOVerlayController>().ShowS);
                UIs[2].transform.Find("Slot2").GetComponent<UnityEngine.UI.Button>().onClick.RemoveListener(UIs[4].GetComponent<ChooseOVerlayController>().ShowS);
                UIs[2].transform.Find("Slot1").GetComponent<UnityEngine.UI.Button>().onClick.AddListener(UIs[4].GetComponent<ChooseOVerlayController>().ShowS);
                UIs[2].transform.Find("Slot2").GetComponent<UnityEngine.UI.Button>().onClick.AddListener(UIs[4].GetComponent<ChooseOVerlayController>().ShowS);
                break;
            case 1:
                UIs[2].transform.Find("Slot1").GetComponent<UnityEngine.UI.Button>().onClick.RemoveListener(UIs[4].GetComponent<ChooseOVerlayController>().ShowS);
                UIs[2].transform.Find("Slot2").GetComponent<UnityEngine.UI.Button>().onClick.RemoveListener(UIs[4].GetComponent<ChooseOVerlayController>().ShowS);
                UIs[2].transform.Find("Slot2").GetComponent<UnityEngine.UI.Button>().onClick.AddListener(UIs[4].GetComponent<ChooseOVerlayController>().ShowS);
                break;
            case 2:
                UIs[2].transform.Find("Slot1").GetComponent<UnityEngine.UI.Button>().onClick.RemoveListener(UIs[4].GetComponent<ChooseOVerlayController>().ShowS);
                UIs[2].transform.Find("Slot2").GetComponent<UnityEngine.UI.Button>().onClick.RemoveListener(UIs[4].GetComponent<ChooseOVerlayController>().ShowS);
                break;
        }
    }

    public void drawDice(){
        int currEvent=Random.Range(0,8);
        while(activatedEvents.Contains(currEvent)){
            currEvent=Random.Range(0,8);
        }
        switch(currEvent){
            case 0:
                UIs[5].GetComponent<EventController>().StartEvent(DialogueLoader.LoadDialogue("surgery"));
                UIs[0].GetComponent<StatusListener>().cellNum/=2;
                UIs[0].GetComponent<StatusListener>().cellSpeed/=2;
                UIs[0].GetComponent<StatusListener>().immuneSpeed/=2;
                activatedEvents.Add(0);
                break;
            case 1:
                UIs[5].GetComponent<EventController>().StartEvent(DialogueLoader.LoadDialogue("radio"));
                if(UIs[0].GetComponent<StatusListener>().hasRadioResist){
                    double temp=(UIs[0].GetComponent<StatusListener>().cellNum/2)*0.1;
                    int temp1=(int) temp;
                    UIs[0].GetComponent<StatusListener>().cellNum-=temp1;
                }else{
                    UIs[0].GetComponent<StatusListener>().cellNum/=2;
                }
                activatedEvents.Add(1);
                break;
            case 2:
                UIs[5].GetComponent<EventController>().StartEvent(DialogueLoader.LoadDialogue("chemo"));
                if(UIs[0].GetComponent<StatusListener>().hasChemoResist){
                    double temp=(UIs[0].GetComponent<StatusListener>().energyNum/2)*0.1;
                    int temp1=(int) temp;
                    UIs[0].GetComponent<StatusListener>().energyNum-=temp1;
                }else{
                    UIs[0].GetComponent<StatusListener>().energyNum/=2;
                }
                activatedEvents.Add(2);
                break;
            case 3:
                UIs[5].GetComponent<EventController>().StartEvent(DialogueLoader.LoadDialogue("hepb"));
                organVals["Liver"][0]/=2;
                activatedEvents.Add(3);
                break;
            case 4:
                UIs[5].GetComponent<EventController>().StartEvent(DialogueLoader.LoadDialogue("helic"));
                organVals["Stomach"][0]/=2;
                activatedEvents.Add(4);
                break;
            case 5:
                UIs[5].GetComponent<EventController>().StartEvent(DialogueLoader.LoadDialogue("smoking"));
                organVals["Lung"][0]/=2;
                activatedEvents.Add(5);
                break;
            case 6:
                UIs[5].GetComponent<EventController>().StartEvent(DialogueLoader.LoadDialogue("obesity"));
                organVals["Intestine"][0]/=2;
                activatedEvents.Add(6);
                break;
            case 7:
                UIs[5].GetComponent<EventController>().StartEvent(DialogueLoader.LoadDialogue("aids"));
                UIs[0].GetComponent<StatusListener>().immuneSpeed=0;
                activatedEvents.Add(7);
                break;
        }
    }
}
