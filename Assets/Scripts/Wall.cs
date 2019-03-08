using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Wall : MonoBehaviour {

    public GameObject[] squares;
    public List<Vector3> roundGrid = new List<Vector3>();
    public List<Vector3> grid = new List<Vector3>();
    public bool stop = false;
    public Setup setups = new Setup();
    public int count = 0;
    System.Random rand = new System.Random();
    public int num;

    // Start is called before the first frame update
    void Start() {
        LoadLevel();
    }

    // Update is called once per frame
    void Update() {
        if (GameObject.Find("Grid").transform.position.y > 0) {
            GameObject.Find("Grid").transform.Translate(new Vector3(0, (float)-0.7, 0) * Time.deltaTime, Space.World);
        } else {
            GameObject.Find("Grid").transform.Translate(new Vector3(0, 0, 0));
            SceneManager.LoadScene("GameOver");
        }
        grid.Clear();
        roundGrid.Clear();
        for (int i = 0; i < squares.Length; i++) {
            roundGrid.Add(new Vector3(Mathf.Round(squares[i].transform.position.x), Mathf.Round(squares[i].transform.position.y), 0));
        }
        for (int i = 0; i < squares.Length; i++) {
            grid.Add(squares[i].transform.position);
        }
        if (count == setups.GetPreset(num).Length) {
            SceneManager.LoadScene("Level");
        }
    }

    public List<Vector3> GetRoundGrid() {        
        return roundGrid;
    }

    public List<Vector3> GetGrid() {
        return grid;
    }
    void LoadLevel() {
        num = rand.Next(0, 1402);
        for (int i = 0; i < setups.GetPreset(num).Length; i++) {
            GameObject.Find("Grid (" + setups.GetPreset(num)[i] + ")").SetActive(false);
        }
        for (int i = 0; i < setups.GetBoard(num).Count; i++) {
            GameObject.Find("Square (" + setups.GetBoard(num)[i] + ")").SetActive(false);
        }
        squares = GameObject.FindGameObjectsWithTag("Blank");
    }
}
