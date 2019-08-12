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

    void Start() {
        LoadLevel();
        GameObject.Find("Text").GetComponent<TextMesh>().text = (setups.GetPassed()).ToString();
    }

    void Update() {
        ContinueOrEndGame();
        CountExceedsEmptySpaces();
        OnPCSkipToNextLevel();
    }

    void ContinueOrEndGame() {
        if (GameObject.Find("Grid").transform.position.y > 0.5 && count < setups.GetPreset(0).Length) {
            int[] levels = { 0, 2, 7, 13, 19, 29, 39, 49, 59 };
            float[] wallSpeed = { -1.5f, -1.4f, -1.3f, -1.1f, -1.0f, -1.2f, -1.3f, -1.4f };
            for (int i = 0; i < levels.Length; i++) {
                SetWallSpeed(levels[i], wallSpeed[i]);
            }
        } else if (count >= setups.GetPreset(0).Length) {
            AfterWinStopMovingWall();
        } else {
            AfterLossStopMovingWall();
        }
    }

    void SetWallSpeed(int levelsPassed, float speed) {
        if (setups.GetPassed() > levelsPassed) {
            GameObject.Find("Grid").transform.Translate(new Vector3(0, speed, 0) * Time.deltaTime, Space.World);

        }
    }

    void AfterWinStopMovingWall() {
        GameObject.Find("Grid").transform.Translate(new Vector3(0, 0, 0));
        StartCoroutine("WinWait");
    }

    void AfterLossStopMovingWall() {
        GameObject.Find("Grid").transform.Translate(new Vector3(0, 0, 0));
        setups.SetPassed(0);
        SceneManager.LoadScene("GameOver");
    }

    void CountExceedsEmptySpaces() {
        if (count >= setups.GetPreset(0).Length) {
            StartCoroutine("WinWait");
        }
    }

    void OnPCSkipToNextLevel() {
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

    public int[] GetSetup() {
        return setups.GetPreset(0);
    }
}
