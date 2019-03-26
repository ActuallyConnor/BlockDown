using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Wall : MonoBehaviour {

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
            if (setups.GetPassed() > 59) {
                GameObject.Find("Grid").transform.Translate(new Vector3(0, (float)-1.4, 0) * Time.deltaTime, Space.World);
            } else if (setups.GetPassed() > 49) {
                GameObject.Find("Grid").transform.Translate(new Vector3(0, (float)-1.3, 0) * Time.deltaTime, Space.World);
            } else if (setups.GetPassed() > 39) {
                GameObject.Find("Grid").transform.Translate(new Vector3(0, (float)-1.2, 0) * Time.deltaTime, Space.World);
            } else if (setups.GetPassed() > 29) {
                GameObject.Find("Grid").transform.Translate(new Vector3(0, (float)-1.1, 0) * Time.deltaTime, Space.World);
            } else if (setups.GetPassed() > 19) {
                GameObject.Find("Grid").transform.Translate(new Vector3(0, (float)-1.0, 0) * Time.deltaTime, Space.World);
            } else if (setups.GetPassed() > 13) {
                GameObject.Find("Grid").transform.Translate(new Vector3(0, (float)-1.1, 0) * Time.deltaTime, Space.World);
            } else if (setups.GetPassed() > 7) {
                GameObject.Find("Grid").transform.Translate(new Vector3(0, (float)-1.2, 0) * Time.deltaTime, Space.World);
            } else if (setups.GetPassed() > 2) {
                GameObject.Find("Grid").transform.Translate(new Vector3(0, (float)-1.4, 0) * Time.deltaTime, Space.World);
            } else {
                GameObject.Find("Grid").transform.Translate(new Vector3(0, (float)-1.5, 0) * Time.deltaTime, Space.World);
            }            
        } else if (count >= setups.GetPreset(0).Length) {
            GameObject.Find("Grid").transform.Translate(new Vector3(0, 0, 0));
            StartCoroutine("WinWait");
        } else {
            GameObject.Find("Grid").transform.Translate(new Vector3(0, 0, 0));
            setups.SetPassed(0);
            SceneManager.LoadScene("GameOver");
        }
        if (count >= setups.GetPreset(0).Length) {
            StartCoroutine("WinWait");            
        }
        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            SceneManager.LoadScene("Level");
        }
    }

    IEnumerator WinWait() {        
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Level");
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
