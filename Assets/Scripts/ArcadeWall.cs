﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ArcadeWall : MonoBehaviour {
    public GameObject[] squares;
    public bool stop = false;
    public Setup setups = new Setup();
    public int count = 0;
    System.Random rand = new System.Random();
    public Game game = new Game();
    public static int passed = 0;

    public int GetPassed() {
        return passed / 2;
    }

    void Start() {
        LoadLevel();
        GameObject.Find("Text").GetComponent<TextMesh>().text = setups.GetPassed().ToString();
        PlayerPrefs.SetInt("score", setups.GetPassed());
        int highscore = PlayerPrefs.GetInt("highscore", 0);
        if (setups.GetPassed() > highscore) {
            highscore = setups.GetPassed();

            PlayerPrefs.SetInt("highscore", highscore);
        }
    }

    void Update() {

        float gridY = GameObject.Find("Grid").transform.position.y;
        int numberOfSpacesInWall = setups.GetPreset(0).Length;

        if (gridY > 0.5 && count < numberOfSpacesInWall) {

        } else if (count >= setups.GetPreset(0).Length) {
            SceneManager.LoadScene("Arcade");
        } else {
            setups.SetPassed(0);
            SceneManager.LoadScene("GameOver");
        }
        if (count >= setups.GetPreset(0).Length) {
            SceneManager.LoadScene("Arcade");
        }
        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            setups.SetPassed(GameObject.Find("Grid").GetComponent<Wall>().setups.GetPassed() + 1);
            SceneManager.LoadScene("Arcade");
        }
    }

    void LoadLevel() {
        for (int i = setups.GetPreset(0).Length - 1; i > -1; i--) {
            GameObject.Find("Grid (" + setups.GetPreset(0)[i] + ")").SetActive(false);
        }
        for (int i = setups.GetBoard(0).Count - 1; i > -1; i--) {
            GameObject.Find("Square (" + setups.GetBoard(0)[i] + ")").SetActive(false);
        }
        squares = GameObject.FindGameObjectsWithTag("Blank");
    }

    public int[] GetSetup() {
        return setups.GetPreset(0);
    }
}
