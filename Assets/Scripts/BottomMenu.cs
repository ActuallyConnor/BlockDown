using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BottomMenu : MonoBehaviour
{
    RaycastHit hit;
    static bool volume = true;
    // Start is called before the first frame update
    void Start()
    {
        if (volume == true) {
            AudioListener.volume = 1;
            GameObject.Find("slash").transform.position = new Vector3((float)7.5, 14, 0);

        } else {
            AudioListener.volume = 0;
            GameObject.Find("slash").transform.position = new Vector3(4, (float)-10.5, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnMouseDown() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit)) {
            if (hit.collider.gameObject.name == "arrow") {
                SceneManager.LoadScene("MainMenu");
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
