using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Wall : MonoBehaviour {
    public GameObject[] squares;
    // Start is called before the first frame update
    void Start() {       
        GameObject.Find("Grid (12)").SetActive(false);
        GameObject.Find("Grid (13)").SetActive(false);
        GameObject.Find("Grid (14)").SetActive(false);
        GameObject.Find("Grid (16)").SetActive(false);
        GameObject.Find("Grid (17)").SetActive(false);

        GameObject.Find("Grid (23)").SetActive(false);
        GameObject.Find("Grid (25)").SetActive(false);
        GameObject.Find("Grid (26)").SetActive(false);

        GameObject.Find("Grid (32)").SetActive(false);
        GameObject.Find("Grid (33)").SetActive(false);
        GameObject.Find("Grid (34)").SetActive(false);
        GameObject.Find("Grid (36)").SetActive(false);
        GameObject.Find("Grid (37)").SetActive(false);
        squares = GameObject.FindGameObjectsWithTag("Square");
    }

    // Update is called once per frame
    void Update() {
        /*if (GameObject.Find("Grid").transform.position.y > 0) {
            GameObject.Find("Grid").transform.Translate(Vector3.down * Time.deltaTime, Space.World);
        } else {
            GameObject.Find("Grid").transform.Translate(new Vector3(0, 0, 0));
            SceneManager.LoadScene("GameOver");
        }*/
    }

    public GameObject[] GetGrid() {
        return squares;
    }
}
