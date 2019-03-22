﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FreeWall : MonoBehaviour {

    public GameObject[] squares;
    public List<Vector3> roundGrid = new List<Vector3>();
    public List<Vector3> grid = new List<Vector3>();
    public bool stop = false;
    public Setup setups = new Setup();
    public int count = 0;
    System.Random rand = new System.Random();
    public Game game = new Game();
    public static int passed = 0;

    public int GetPassed() {
        return passed / 2;
    }
    // Start is called before the first frame update
    void Start() {
        LoadLevel();
        GameObject.Find("Text").GetComponent<TextMesh>().text = (setups.GetPassed()).ToString();
    }

    // Update is called once per frame
    void Update() {
        /*if (GameObject.Find("Grid").transform.position.y > 0 && count < setups.GetPreset(0).Length) {
            GameObject.Find("Grid").transform.Translate(new Vector3(0, (float)-0.7, 0) * Time.deltaTime, Space.World);
        } else if (count >= setups.GetPreset(0).Length) {
            GameObject.Find("Grid").transform.Translate(new Vector3(0, 0, 0));
            StartCoroutine("WinWait");
        } else {
            GameObject.Find("Grid").transform.Translate(new Vector3(0, 0, 0));
            setups.SetPassed(0);
            SceneManager.LoadScene("GameOver");
        }
        grid.Clear();
        roundGrid.Clear();
        for (int i = 0; i < squares.Length; i++) {
            roundGrid.Add(new Vector3(Mathf.Round(squares[i].transform.position.x), Mathf.Round(squares[i].transform.position.y), 0));
        }*/
        for (int i = 0; i < squares.Length; i++) {
            grid.Add(squares[i].transform.position);
        }
        if (count >= setups.GetPreset(0).Length) {
            //StartCoroutine("WinWait");
            SceneManager.LoadScene("FreePlay");
        }
        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            SceneManager.LoadScene("FreePlay");
        }
    }

    IEnumerator WinWait() {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("FreePlay");
    }

    public List<Vector3> GetRoundGrid() {
        return roundGrid;
    }

    public List<Vector3> GetGrid() {
        return grid;
    }
    void LoadLevel() {
        for (int i = 0; i < setups.GetPreset(0).Length; i++) {
            GameObject.Find("Grid (" + (setups.GetPreset(0)[i]) + ")").SetActive(false);
        }
        for (int i = 0; i < setups.GetBoard(0).Count; i++) {
            GameObject.Find("Square (" + (setups.GetBoard(0)[i]) + ")").SetActive(false);
        }
        squares = GameObject.FindGameObjectsWithTag("Blank");
    }

    public int[] GetSetup() => setups.GetPreset(0);
}
