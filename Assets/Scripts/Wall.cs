using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Wall : MonoBehaviour {

    public GameObject[] squares;
    public List<Vector3> roundGrid = new List<Vector3>();
    public List<Vector3> grid = new List<Vector3>();
    public bool stop = false;
    public Setups setups = new Setups();
    public int[] active;
    public List<int> baseWall = new List<int>();

    // Start is called before the first frame update
    void Start() {        
        foreach (int j in setups.GetWall(0)) {
            GameObject.Find("Square (" + j + ")").SetActive(false);
        }
        foreach (int i in setups.GetSetup(0)) {
            GameObject.Find("Grid (" + i + ")").SetActive(false);
        }
        //GameObject.Find("Square (" + 0 + ")").SetActive(false);
        squares = GameObject.FindGameObjectsWithTag("Blank");
        Debug.Log(squares.Length);
    }

    // Update is called once per frame
    void Update() {
        if (GameObject.Find("Grid").transform.position.y > 0) {
            GameObject.Find("Grid").transform.Translate(Vector3.down * Time.deltaTime, Space.World);
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
    }

    public List<Vector3> GetRoundGrid() {        
        return roundGrid;
    }

    public List<Vector3> GetGrid() {
        return grid;
    }
}
