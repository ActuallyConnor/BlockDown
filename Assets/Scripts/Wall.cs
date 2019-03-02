using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Wall : MonoBehaviour {
    public GameObject[] squares;
    public List<Vector3> roundGrid = new List<Vector3>();
    public List<Vector3> grid = new List<Vector3>();
    public int[] active = new int[] {
        12, 13, 14, 16, 17, 
        23, 25, 26, 
        32, 33, 34, 36, 37
    };
    public int[] inactive = new int[] {
        0, 1, 2, 3, 4, 5, 6, 7, 8, 9,
        10, 11, 15, 18, 19,
        20, 21, 22, 24, 27, 28, 29,
        30, 31, 35, 38, 39,
        40, 41, 42, 43, 44, 45, 46, 47, 48, 49
    };
    // Start is called before the first frame update
    void Start() {
        foreach (int i in active) {
            GameObject.Find("Grid (" + i + ")").SetActive(false);            
        }
        foreach (int i in inactive) {
            GameObject.Find("Square (" + i + ")").SetActive(false);
        }        
        //GameObject.Find("Square (" + 0 + ")").SetActive(false);
        squares = GameObject.FindGameObjectsWithTag("Blank");
        Debug.Log(squares.Length);
    }

    // Update is called once per frame
    void Update() {
        if (AtBottom()) {
            GameObject.Find("Grid").transform.Translate(Vector3.down * Time.deltaTime, Space.World);
        } else {
            GameObject.Find("Grid").transform.Translate(new Vector3(0, 0, 0));
            //SceneManager.LoadScene("GameOver");
        }
    }

    public GameObject[] GetSquares() {
        return squares;
    }

    public bool AtBottom() {
        if (GameObject.Find("Grid").transform.position.y > 0) {
            return true;
        }
        return false;
    }

    public List<Vector3> GetRoundGrid() {
        for (int i = 0; i < squares.Length; i++) {
            roundGrid.Add(new Vector3(Mathf.Round(squares[i].transform.position.x), Mathf.Round(squares[i].transform.position.y), 0));
        }
        return roundGrid;
    }

    public List<Vector3> GetGrid() {
        for (int i = 0; i < squares.Length; i++) {
            grid.Add(squares[i].transform.position);
        }
        return grid;
    }
}
