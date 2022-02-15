using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusListener : MonoBehaviour {
    public int cellNum;
    public int cellSpeed;
    public int immuneSpeed;
    public int energyNum;
    public int energySpeed;
    public bool hasRadioResist=false;
    public bool hasChemoResist=false;
    public Text CellNum;
    public Text CellSpeed;
    public Text ImmuneSpeed;
    public Text EnergyNum;
    public Text EnergySpeed;
    float timer;
    public GameObject winloseOverlay;

    // Start is called before the first frame update
    void Start() {
        cellNum=300;
        cellSpeed=16;
        immuneSpeed=3;
        energyNum=1;
        energySpeed=18;
    }

    // Update is called once per frame
    void Update() {
        timer += Time.deltaTime;
        if(timer>=1){
            timer=0;
            cellNum+=cellSpeed-immuneSpeed;
            energyNum+=energySpeed;

            if(cellNum>=5000){
                winloseOverlay.GetComponent<WinLoseController>().Win(1);
            }
            if(energyNum>=10000){
                winloseOverlay.GetComponent<WinLoseController>().Win(2);
            }
            if(cellNum<0){
                winloseOverlay.GetComponent<WinLoseController>().Lose();
            }
        }
        CellNum.text=cellNum.ToString()+"k";
        CellSpeed.text=cellSpeed.ToString()+"k";
        ImmuneSpeed.text=immuneSpeed.ToString()+"k";
        EnergyNum.text=energyNum.ToString()+"k";
        EnergySpeed.text=energySpeed.ToString()+"k";
    }
}
