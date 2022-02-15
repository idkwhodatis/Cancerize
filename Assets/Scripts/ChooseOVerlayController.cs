using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseOVerlayController : MonoBehaviour {
    public GameObject Title;
    public GameObject Btn1;
    public GameObject Btn2;
    public GameObject Btn3;
    public GameObject Btn4;
    public GameObject Btn5;
    public GameObject menu;
    public Text Text1;
    public Text Text2;
    public Text Text3;
    public Text Text4;
    public Text Text5;
    public GameObject Status;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }

    void OnDisable(){
        GameObject.Find(GameObject.Find("StructureOverlay").transform.Find("OrganName").GetComponent<UnityEngine.UI.Text>().text).GetComponent<OrganOverlayController>().SetSelected(false);
    }

    public void ShowS(){
        if(Status.GetComponent<StatusListener>().energyNum>=250){
            Status.GetComponent<StatusListener>().energyNum-=250;
            Time.timeScale=0;
            menu.GetComponent<MenuController>().SetPaused(true);
            gameObject.SetActive(true);
            noText();
            Title.GetComponent<UnityEngine.UI.Text>().text="Choose a Structure to Build";
            Btn1.SetActive(false);
            Btn5.SetActive(false);
            Btn2.transform.Find("Text").GetComponent<UnityEngine.UI.Text>().text="Cancer Cell Builder";
            Btn3.transform.Find("Text").GetComponent<UnityEngine.UI.Text>().text="Energy Absorber";
            Btn4.transform.Find("Text").GetComponent<UnityEngine.UI.Text>().text="Mutation";
            Btn2.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(CancerCellBuilder);
            Btn3.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(EnergyAbsorber);
            Btn4.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(Mutation);
        }
    }

    void ChooseS(string str){
        Time.timeScale=1;
        menu.GetComponent<MenuController>().SetPaused(false);
        GameObject.Find(GameObject.Find("StructureOverlay").transform.Find("OrganName").GetComponent<UnityEngine.UI.Text>().text).GetComponent<OrganOverlayController>().AddStructure(str);
        GameObject.Find(GameObject.Find("StructureOverlay").transform.Find("OrganName").GetComponent<UnityEngine.UI.Text>().text).GetComponent<OrganOverlayController>().ShowStructure();
        Btn2.GetComponent<UnityEngine.UI.Button>().onClick.RemoveAllListeners();
        Btn3.GetComponent<UnityEngine.UI.Button>().onClick.RemoveAllListeners();
        Btn4.GetComponent<UnityEngine.UI.Button>().onClick.RemoveAllListeners();
        GameObject.Find(GameObject.Find("StructureOverlay").transform.Find("OrganName").GetComponent<UnityEngine.UI.Text>().text).GetComponent<OrganOverlayController>().SetSelected(true);
        gameObject.SetActive(false);
    }

    void CancerCellBuilder(){
        Status.GetComponent<StatusListener>().cellSpeed+=8;
        ChooseS("B");
    }

    void EnergyAbsorber(){
        Status.GetComponent<StatusListener>().energySpeed+=10;
        ChooseS("E");
    }

    void Mutation(){
        ChooseS("M");
        ShowM();
    }

    void noText(){
        Text1.text="";
        Text2.text="";
        Text3.text="";
        Text4.text="";
        Text5.text="";
    }

    public void ShowM(){
        Time.timeScale=0;
        menu.GetComponent<MenuController>().SetPaused(true);
        gameObject.SetActive(true);
        Title.GetComponent<UnityEngine.UI.Text>().text="Choose a Mutation Direction";
        Btn1.SetActive(true);
        Btn5.SetActive(true);
        Btn1.transform.Find("Text").GetComponent<UnityEngine.UI.Text>().text="Instant Cell Division";
        Btn2.transform.Find("Text").GetComponent<UnityEngine.UI.Text>().text="Optimized Genome";
        Btn3.transform.Find("Text").GetComponent<UnityEngine.UI.Text>().text="Radiation Resistance";
        Btn4.transform.Find("Text").GetComponent<UnityEngine.UI.Text>().text="Chemical Resistance";
        Btn5.transform.Find("Text").GetComponent<UnityEngine.UI.Text>().text="Immune System Attack";
        Text1.text="Double cancer cells, cell generation speed is halved";
        Text2.text="Double cells generation speed, energy speed is halved";
        Text3.text="Reduce the effect of radiation by 90%";
        Text4.text="Reduce the effect of chemical by 90%";
        Text5.text="Reduce immune system resistance by 50%";
        Btn1.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(InstantCellDivision);
        Btn2.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(OptimizedGenome);
        Btn3.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(RadiationResistance);
        Btn4.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(ChemicalResistance);
        Btn5.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(ImmuneSystemAttack);
    }

    void ChooseM(){
        Time.timeScale=1;
        menu.GetComponent<MenuController>().SetPaused(false);
        Btn1.GetComponent<UnityEngine.UI.Button>().onClick.RemoveAllListeners();
        Btn2.GetComponent<UnityEngine.UI.Button>().onClick.RemoveAllListeners();
        Btn3.GetComponent<UnityEngine.UI.Button>().onClick.RemoveAllListeners();
        Btn4.GetComponent<UnityEngine.UI.Button>().onClick.RemoveAllListeners();
        Btn5.GetComponent<UnityEngine.UI.Button>().onClick.RemoveAllListeners();
        GameObject.Find(GameObject.Find("StructureOverlay").transform.Find("OrganName").GetComponent<UnityEngine.UI.Text>().text).GetComponent<OrganOverlayController>().SetSelected(true);
        gameObject.SetActive(false);
    }

    void InstantCellDivision(){
        Status.GetComponent<StatusListener>().cellNum*=2;
        Status.GetComponent<StatusListener>().cellSpeed/=2;
        ChooseM();
    }

    void OptimizedGenome(){
        Status.GetComponent<StatusListener>().cellSpeed*=2;
        Status.GetComponent<StatusListener>().energySpeed/=2;
        ChooseM();
    }

    void RadiationResistance(){
        Status.GetComponent<StatusListener>().hasRadioResist=true;
        ChooseM();
    }

    void ChemicalResistance(){
        Status.GetComponent<StatusListener>().hasChemoResist=true;
        ChooseM();
    }

    void ImmuneSystemAttack(){
        Status.GetComponent<StatusListener>().immuneSpeed/=2;
        ChooseM();
    }
}
