using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragingScript : MonoBehaviour {

    public GameObject gameObjectToDrag; // refer to GO that is being dragged

    public Vector3 GOCentre;
    public Vector3 touchPosition; // touch or click position
    public Vector3 offset; // vector between touchpoint and object centre
    public Vector3 newGOCentre; // new centre of GO after move

    RaycastHit hit; // store hit object info

    public bool draggingMode = false;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
#if UNITY_EDITOR
        ClickToDrag();
#endif
        TouchToDrag();
    }

    void ClickToDrag() {
        // first fram when user clicks left mouse
        if (Input.GetMouseButtonDown(0)) {
            // convert mouse click position to a ray
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            // if the ray hits a collider (not 2Dcollider)
            if (Physics.Raycast(ray, out hit)) {
                gameObjectToDrag = hit.collider.gameObject;
                GOCentre = gameObjectToDrag.transform.position;
                touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                offset = touchPosition - GOCentre;
                draggingMode = true;
            }
        }// every frame when user holds onto left mouse button
        if (Input.GetMouseButton(0)) {
            if (draggingMode) {
                touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                newGOCentre = touchPosition - offset;
                gameObjectToDrag.transform.position = new Vector3(newGOCentre.x, newGOCentre.y, GOCentre.z);
            }
        }
        // when mouse is released
        if (Input.GetMouseButtonUp(0)) {
            draggingMode = false;
            if (Mathf.Round(gameObjectToDrag.transform.position.x) == -2 && Mathf.Round(gameObjectToDrag.transform.position.y) == 7) {
                gameObjectToDrag.transform.position = new Vector3(Mathf.Round(gameObjectToDrag.transform.position.x), Mathf.Round(gameObjectToDrag.transform.position.y), GOCentre.z);
            } else {
                gameObjectToDrag.transform.position = new Vector3(-1, -2, GOCentre.z);
            }
            // gameObjectToDrag.transform.position = new Vector3(Mathf.Round(gameObjectToDrag.transform.position.x), Mathf.Round(gameObjectToDrag.transform.position.y), GOCentre.z);
        }
    }

    void TouchToDrag() {
        foreach (Touch touch in Input.touches) {
            switch (touch.phase) {
                // when just touch
                case TouchPhase.Began:
                    Ray ray = Camera.main.ScreenPointToRay(touch.position);
                    if (Physics.SphereCast(ray, 0.3f, out hit)) {
                        gameObjectToDrag = hit.collider.gameObject;
                        GOCentre = gameObjectToDrag.transform.position;
                        touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        offset = touchPosition - GOCentre;
                        draggingMode = true;
                    }
                    break;
                case TouchPhase.Moved:
                    if (draggingMode) {
                        touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        newGOCentre = touchPosition - offset;
                        gameObjectToDrag.transform.position = new Vector3(newGOCentre.x, newGOCentre.y, GOCentre.z);
                    }
                    break;
                case TouchPhase.Ended:
                    draggingMode = false;
                    break;
            }
        }
    }
}
