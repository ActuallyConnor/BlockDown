using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSystem : MonoBehaviour {

    

    void Start() {
        
    }

    public void PlayAgain() {
        SceneManager.LoadScene("Level");
    }
    public void FreePlay() {
        SceneManager.LoadScene("FreePlay");
    }
    public void MainMenu() {
        SceneManager.LoadScene("NewGame");
    }
    
}
