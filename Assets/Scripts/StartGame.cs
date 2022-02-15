using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour{
    public Button startBtn;
    public Button exitBtn;
    void Start(){
        startBtn.onClick.AddListener(Starts);
        exitBtn.onClick.AddListener(Exits);
    }
    public void Starts(){
        SceneManager.LoadScene(sceneName:"MainScene");
    }
    public void Exits(){
        Application.Quit();
    }
}
