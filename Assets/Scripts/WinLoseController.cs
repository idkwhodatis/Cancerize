using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinLoseController : MonoBehaviour {
    public Text title;
    public Text winInfo;
    public Button ok;
    public GameObject menu;
    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }
    
    public void Win(int info){
        Time.timeScale=0;
        gameObject.SetActive(true);
        menu.GetComponent<MenuController>().SetPaused(true);
        title.text="You Win!";
        switch(info){
            case 0:
                winInfo.text="Cancer Spreads: You have invaded all organs.";
                break;
            case 1:
                winInfo.text="Cancer Grows: The cancer cell Number has reached 5k.";
                break;
            case 2:
                winInfo.text="Energy Depletion: The energy has reached 10k.";
                break;
        }
        ok.onClick.AddListener(ShowCredits);
    }

    public void Lose(){
        Time.timeScale=0;
        gameObject.SetActive(true);
        menu.GetComponent<MenuController>().SetPaused(true);
        title.text="Cancers cannot beat human!";
        ok.onClick.AddListener(ShowCredits);
    }

    void ShowCredits(){
        SceneManager.LoadScene(sceneName:"Credits");
    }
}
