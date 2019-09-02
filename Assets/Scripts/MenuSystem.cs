using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSystem : MonoBehaviour {

    RaycastHit hit;
    static bool volume = true;

    void Start() {

    }

    void OnMouseDown() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit)) {
            if (hit.collider.gameObject.name == "button") {
                SceneManager.LoadScene("Level");
            }
            if (hit.collider.gameObject.name == "button (1)") {
                SceneManager.LoadScene("FreePlay");
            }
            if (hit.collider.gameObject.name == "button (2)") {
                SceneManager.LoadScene("MainMenu");
            }
			if (hit.collider.gameObject.name == "button (4)") {
				SceneManager.LoadScene("Score");
			}
			if (hit.collider.gameObject.name == "speaker") {
                if (volume == true) {
                    AudioListener.volume = 0;
                    GameObject.Find("slash").transform.position = new Vector3(4, (float)-10.5, 0);
                    volume = false;
                } else {
                    AudioListener.volume = 1;
                    GameObject.Find("slash").transform.position = new Vector3((float)7.5, 4, 0);
                    volume = true;
                }
            }
        }
    }

}
