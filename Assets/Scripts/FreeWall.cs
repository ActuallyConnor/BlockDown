using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FreeWall : MonoBehaviour {

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
    // Start is called before the first frame update
    void Start() {
        LoadLevel();
        GameObject.Find("Text").GetComponent<TextMesh>().text = (setups.GetPassed()).ToString();
    }

    // Update is called once per frame
    void Update() {
        if (GameObject.Find("Grid").transform.position.y > 0.5 && count < setups.GetPreset(0).Length) {
        } else if (count >= setups.GetPreset(0).Length) {
            StartCoroutine("WinWait");
        } else {
            setups.SetPassed(0);
            SceneManager.LoadScene("GameOver");
        }
        if (count >= setups.GetPreset(0).Length) {
            StartCoroutine("WinWait");
        }
        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            GameObject.Find("Grid").GetComponent<Wall>().setups.SetPassed(GameObject.Find("Grid").GetComponent<Wall>().setups.GetPassed() + 1);
            SceneManager.LoadScene("FreePlay");
        }
    }

    IEnumerator WinWait() {
        yield return new WaitForSeconds((float)0.25);
        SceneManager.LoadScene("FreePlay");
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
