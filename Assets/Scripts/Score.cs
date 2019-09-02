using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour {

    void Start() {
        if (GameObject.Find("Text")) {
            GameObject.Find("Text").GetComponent<TextMesh>().text = PlayerPrefs.GetInt("score").ToString();
        }
        if (GameObject.Find("FreePlay (2)")) {
            GameObject.Find("FreePlay (2)").GetComponent<TextMesh>().text = PlayerPrefs.GetInt("highscore").ToString();
        }
        if (GameObject.Find("FreePlay (1)")) {
            GameObject.Find("FreePlay (1)").GetComponent<TextMesh>().text = PlayerPrefs.GetInt("freescore").ToString();
        }
    }

    void Update() {
        
    }
}
