using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Block : MonoBehaviour {

    public GameObject gameObjectToDrag; // refer to GO that is being dragged

    public Vector3 GOCentre;
    public Vector3 touchPosition; // touch or click position
    public Vector3 offset; // vector between touchpoint and object centre
    public Vector3 newGOCentre; // new centre of GO after move

    RaycastHit hit; // store hit object info

    public GridScript wall = new GridScript();

    public bool draggingMode = false;

    public string shape = "";

    public bool canRotate = true;

    public bool limitRotate = false;

    public List<int[]> used;

    public List<int[]> pCoords = new List<int[]>();

    // Start is called before the first frame update
    void Start() {
        used = wall.GetUsedSpots();
    }

    // Update is called once per frame
    void Update() {
    }

    string GetShape() => shape;

    int GetRotation() => (int)gameObjectToDrag.transform.rotation.eulerAngles.z;

    void OnMouseDown() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        // if the ray hits a collider (not 2Dcollider)
        if (Physics.Raycast(ray, out hit)) {
            gameObjectToDrag = hit.collider.gameObject;
            GOCentre = gameObjectToDrag.transform.position;
            touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            offset = touchPosition - GOCentre;
            draggingMode = true;
        }
    }

    void OnMouseDrag() {
        if (draggingMode) {
            touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            newGOCentre = touchPosition - offset;
            gameObjectToDrag.transform.position = new Vector3(newGOCentre.x, newGOCentre.y, GOCentre.z);
        }
    }

    void OnMouseUp() {
        draggingMode = false;
        IsValid();
    }

    void IsValid() {
        switch (GetShape()) {
            case "T":
                TShape();
                break;
            case "L":
                LShape();
                break;
            case "Line":
                Line();
                break;
            case "Two":
                Two();
                break;
        }
    }

    void TShape() {
        switch (GetRotation()) {
            case 0:
                for (int i = 0; i < used.Count; i++) {                    
                    if (Mathf.Round(gameObjectToDrag.transform.position.x) < -3 || Mathf.Round(gameObjectToDrag.transform.position.x) + 2 > 3) { // if x coords go outside of grid
                        gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
                    } else if (Mathf.Round(gameObjectToDrag.transform.position.y) -1 < 0 || Mathf.Round(gameObjectToDrag.transform.position.y) > 8) { // if y coords go outside of grid
                        gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
                    } else if (used[i][0] == (int)Mathf.Round(gameObjectToDrag.transform.position.x) && used[i][1] == (int)Mathf.Round(gameObjectToDrag.transform.position.y)) { // 0,0
                        gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
                    } else if (used[i][0] == (int)Mathf.Round(gameObjectToDrag.transform.position.x) + 1 && used[i][1] == (int)Mathf.Round(gameObjectToDrag.transform.position.y)) { // 1,0
                        gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
                    } else if (used[i][0] == (int)Mathf.Round(gameObjectToDrag.transform.position.x) + 2 && used[i][1] == (int)Mathf.Round(gameObjectToDrag.transform.position.y)) { // 2,0
                        gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
                    } else if (used[i][0] == (int)Mathf.Round(gameObjectToDrag.transform.position.x) + 1 && used[i][1] == (int)Mathf.Round(gameObjectToDrag.transform.position.y) - 1) { // 1,-1
                        gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
                    } else {
                        gameObjectToDrag.transform.position = new Vector3(Mathf.Round(gameObjectToDrag.transform.position.x), Mathf.Round(gameObjectToDrag.transform.position.y), GOCentre.z);
                    }
                }
                break;
            case 90:
                for (int i = 0; i < used.Count; i++) {
                    if (Mathf.Round(gameObjectToDrag.transform.position.x) < -3 || Mathf.Round(gameObjectToDrag.transform.position.x) + 1 > 3) { // if x coords go outside of grid
                        gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
                    } else if (Mathf.Round(gameObjectToDrag.transform.position.y) < 0 || Mathf.Round(gameObjectToDrag.transform.position.y) + 2 > 8) { // if y coords go outside of grid
                        gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
                    } else if (used[i][0] == (int)Mathf.Round(gameObjectToDrag.transform.position.x) && used[i][1] == (int)Mathf.Round(gameObjectToDrag.transform.position.y)) { // 0,0
                        gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
                    } else if (used[i][0] == (int)Mathf.Round(gameObjectToDrag.transform.position.x) && used[i][1] == (int)Mathf.Round(gameObjectToDrag.transform.position.y) + 1) { // 0,1
                        gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
                    } else if (used[i][0] == (int)Mathf.Round(gameObjectToDrag.transform.position.x) && used[i][1] == (int)Mathf.Round(gameObjectToDrag.transform.position.y) + 2) { // 0,2
                        gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
                    } else if (used[i][0] == (int)Mathf.Round(gameObjectToDrag.transform.position.x) + 1 && used[i][1] == (int)Mathf.Round(gameObjectToDrag.transform.position.y) + 1) { // 1,1
                        gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
                    } else {
                        gameObjectToDrag.transform.position = new Vector3(Mathf.Round(gameObjectToDrag.transform.position.x), Mathf.Round(gameObjectToDrag.transform.position.y), GOCentre.z);
                    }
                }
                break;
            case 180:
                for (int i = 0; i < used.Count; i++) {
                    if (Mathf.Round(gameObjectToDrag.transform.position.x) - 2 < -3 || Mathf.Round(gameObjectToDrag.transform.position.x) > 3) { // if x coords go outside of grid
                        gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
                    } else if (Mathf.Round(gameObjectToDrag.transform.position.y) < 0 || Mathf.Round(gameObjectToDrag.transform.position.y) + 1 > 8) { // if y coords go outside of grid
                        gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
                    } else if (used[i][0] == (int)Mathf.Round(gameObjectToDrag.transform.position.x) && used[i][1] == (int)Mathf.Round(gameObjectToDrag.transform.position.y)) { // 0,0
                        gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
                    } else if (used[i][0] == (int)Mathf.Round(gameObjectToDrag.transform.position.x) - 1 && used[i][1] == (int)Mathf.Round(gameObjectToDrag.transform.position.y)) { // -1,0
                        gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
                    } else if (used[i][0] == (int)Mathf.Round(gameObjectToDrag.transform.position.x) - 2 && used[i][1] == (int)Mathf.Round(gameObjectToDrag.transform.position.y)) { // -2,0
                        gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
                    } else if (used[i][0] == (int)Mathf.Round(gameObjectToDrag.transform.position.x) - 1 && used[i][1] == (int)Mathf.Round(gameObjectToDrag.transform.position.y) + 1) { // -1,1
                        gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
                    } else {
                        gameObjectToDrag.transform.position = new Vector3(Mathf.Round(gameObjectToDrag.transform.position.x), Mathf.Round(gameObjectToDrag.transform.position.y), GOCentre.z);
                    }
                }
                break;
            case 270:
                for (int i = 0; i < used.Count; i++) {
                    if (Mathf.Round(gameObjectToDrag.transform.position.x) - 1 < -3 || Mathf.Round(gameObjectToDrag.transform.position.x) > 3) { // if x coords go outside of grid
                        gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
                    } else if (Mathf.Round(gameObjectToDrag.transform.position.y) - 2 < 0 || Mathf.Round(gameObjectToDrag.transform.position.y) > 8) { // if y coords go outside of grid
                        gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
                    } else if (used[i][0] == (int)Mathf.Round(gameObjectToDrag.transform.position.x) && used[i][1] == (int)Mathf.Round(gameObjectToDrag.transform.position.y)) { // 0,0
                        gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
                    } else if (used[i][0] == (int)Mathf.Round(gameObjectToDrag.transform.position.x) && used[i][1] == (int)Mathf.Round(gameObjectToDrag.transform.position.y) - 1) { // 0,-1
                        gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
                    } else if (used[i][0] == (int)Mathf.Round(gameObjectToDrag.transform.position.x) && used[i][1] == (int)Mathf.Round(gameObjectToDrag.transform.position.y) - 2) { // 0,-2
                        gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
                    } else if (used[i][0] == (int)Mathf.Round(gameObjectToDrag.transform.position.x) - 1 && used[i][1] == (int)Mathf.Round(gameObjectToDrag.transform.position.y) - 1) { // -1,-1
                        gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
                    } else {
                        gameObjectToDrag.transform.position = new Vector3(Mathf.Round(gameObjectToDrag.transform.position.x), Mathf.Round(gameObjectToDrag.transform.position.y), GOCentre.z);
                    }
                }
                break;
        }
    }

    void LShape() {
        switch (GetRotation()) {
            case 0:
                for (int i = 0; i < used.Count; i++) {
                    if (Mathf.Round(gameObjectToDrag.transform.position.x) < -3 || Mathf.Round(gameObjectToDrag.transform.position.x) + 1 > 3) { // if x coords go outside of grid
                        gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
                    } else if (Mathf.Round(gameObjectToDrag.transform.position.y) - 1 < 0 || Mathf.Round(gameObjectToDrag.transform.position.y) > 8) { // if y coords go outside the grid
                        gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
                    } else if (used[i][0] == (int)Mathf.Round(gameObjectToDrag.transform.position.x) && used[i][1] == (int)Mathf.Round(gameObjectToDrag.transform.position.y)) { // 0,0
                        gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
                    } else if (used[i][0] == (int)Mathf.Round(gameObjectToDrag.transform.position.x) && used[i][1] == (int)Mathf.Round(gameObjectToDrag.transform.position.y) - 1) { // 0,-1
                        gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
                    } else if (used[i][0] == (int)Mathf.Round(gameObjectToDrag.transform.position.x) + 1 && used[i][1] == (int)Mathf.Round(gameObjectToDrag.transform.position.y) - 1) { // 1,-1
                        gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
                    } else {
                        gameObjectToDrag.transform.position = new Vector3(Mathf.Round(gameObjectToDrag.transform.position.x), Mathf.Round(gameObjectToDrag.transform.position.y), GOCentre.z);
                    }
                }
                break;
            case 90:
                for (int i = 0; i < used.Count; i++) {
                    if (Mathf.Round(gameObjectToDrag.transform.position.x) < -3 || Mathf.Round(gameObjectToDrag.transform.position.x) + 1 > 3) { // if x coords go outside of grid
                        gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
                    } else if (Mathf.Round(gameObjectToDrag.transform.position.y) < 0 || Mathf.Round(gameObjectToDrag.transform.position.y) + 1 > 8) { // if y coords go outside the grid
                        gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
                    } else if (used[i][0] == (int)Mathf.Round(gameObjectToDrag.transform.position.x) && used[i][1] == (int)Mathf.Round(gameObjectToDrag.transform.position.y)) { // 0,0
                        gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
                    } else if (used[i][0] == (int)Mathf.Round(gameObjectToDrag.transform.position.x) + 1 && used[i][1] == (int)Mathf.Round(gameObjectToDrag.transform.position.y)) { // 1,0
                        gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
                    } else if (used[i][0] == (int)Mathf.Round(gameObjectToDrag.transform.position.x) + 1 && used[i][1] == (int)Mathf.Round(gameObjectToDrag.transform.position.y) + 1) { // 1,1
                        gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
                    } else {
                        gameObjectToDrag.transform.position = new Vector3(Mathf.Round(gameObjectToDrag.transform.position.x), Mathf.Round(gameObjectToDrag.transform.position.y), GOCentre.z);
                    }
                }
                break;
            case 180:
                for (int i = 0; i < used.Count; i++) {
                    if (Mathf.Round(gameObjectToDrag.transform.position.x) - 1 < -3 || Mathf.Round(gameObjectToDrag.transform.position.x) > 3) { // if x coords go outside of grid
                        gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
                    } else if (Mathf.Round(gameObjectToDrag.transform.position.y) < 0 || Mathf.Round(gameObjectToDrag.transform.position.y) + 1 > 8) { // if y coords go outside the grid
                        gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
                    } else if (used[i][0] == (int)Mathf.Round(gameObjectToDrag.transform.position.x) && used[i][1] == (int)Mathf.Round(gameObjectToDrag.transform.position.y)) { // 0,0
                        gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
                    } else if (used[i][0] == (int)Mathf.Round(gameObjectToDrag.transform.position.x) && used[i][1] == (int)Mathf.Round(gameObjectToDrag.transform.position.y) + 1) { // 0,1
                        gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
                    } else if (used[i][0] == (int)Mathf.Round(gameObjectToDrag.transform.position.x) - 1 && used[i][1] == (int)Mathf.Round(gameObjectToDrag.transform.position.y) + 1) { // -1,1
                        gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
                    } else {
                        gameObjectToDrag.transform.position = new Vector3(Mathf.Round(gameObjectToDrag.transform.position.x), Mathf.Round(gameObjectToDrag.transform.position.y), GOCentre.z);
                    }
                }
                break;
            case 270:
                for (int i = 0; i < used.Count; i++) {
                    if (Mathf.Round(gameObjectToDrag.transform.position.x) - 1 < -3 || Mathf.Round(gameObjectToDrag.transform.position.x) > 3) { // if x coords go outside of grid
                        gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
                    } else if (Mathf.Round(gameObjectToDrag.transform.position.y) - 1 < 0 || Mathf.Round(gameObjectToDrag.transform.position.y) > 8) { // if y coords go outside the grid
                        gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
                    } else if (used[i][0] == (int)Mathf.Round(gameObjectToDrag.transform.position.x) && used[i][1] == (int)Mathf.Round(gameObjectToDrag.transform.position.y)) { // 0,0
                        gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
                    } else if (used[i][0] == (int)Mathf.Round(gameObjectToDrag.transform.position.x) - 1 && used[i][1] == (int)Mathf.Round(gameObjectToDrag.transform.position.y)) { // -1,0
                        gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
                    } else if (used[i][0] == (int)Mathf.Round(gameObjectToDrag.transform.position.x) - 1 && used[i][1] == (int)Mathf.Round(gameObjectToDrag.transform.position.y) - 1) { // -1,-1
                        gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
                    } else {
                        gameObjectToDrag.transform.position = new Vector3(Mathf.Round(gameObjectToDrag.transform.position.x), Mathf.Round(gameObjectToDrag.transform.position.y), GOCentre.z);
                    }
                }
                break;
        }        
    }

    void Dot() {
        for (int i = 0; i < used.Count; i++) {
            if (Mathf.Round(gameObjectToDrag.transform.position.x) < -3 || Mathf.Round(gameObjectToDrag.transform.position.x) > 3) { // if x coords go outside of grid
                gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
            } else if (Mathf.Round(gameObjectToDrag.transform.position.y) < 0 || Mathf.Round(gameObjectToDrag.transform.position.y) > 8) { // if y coords go outside the grid
                gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
            } else if (used[i][0] == (int)Mathf.Round(gameObjectToDrag.transform.position.x) && used[i][1] == (int)Mathf.Round(gameObjectToDrag.transform.position.y)) { // 0,0
                gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
            } else {
                gameObjectToDrag.transform.position = new Vector3(Mathf.Round(gameObjectToDrag.transform.position.x), Mathf.Round(gameObjectToDrag.transform.position.y), GOCentre.z);
            }
        }
    }

    void Line() {
        switch (GetRotation()) {
            case 0:
                for (int i = 0; i < used.Count; i++) {
                    if (Mathf.Round(gameObjectToDrag.transform.position.x) < -3 || Mathf.Round(gameObjectToDrag.transform.position.x) > 3) { // if x coords go outside of grid
                        gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
                    } else if (Mathf.Round(gameObjectToDrag.transform.position.y) < 0 || Mathf.Round(gameObjectToDrag.transform.position.y) > 8) { // if y coords go outside the grid
                        gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
                    } else if (used[i][0] == (int)Mathf.Round(gameObjectToDrag.transform.position.x) && used[i][1] == (int)Mathf.Round(gameObjectToDrag.transform.position.y)) { // 0,0
                        gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
                    } else if (used[i][0] == (int)Mathf.Round(gameObjectToDrag.transform.position.x) + 1 && used[i][1] == (int)Mathf.Round(gameObjectToDrag.transform.position.y)) { // 1,0
                        gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
                    } else if (used[i][0] == (int)Mathf.Round(gameObjectToDrag.transform.position.x) + 2 && used[i][1] == (int)Mathf.Round(gameObjectToDrag.transform.position.y)) { // 2,0
                        gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
                    } else {
                        gameObjectToDrag.transform.position = new Vector3(Mathf.Round(gameObjectToDrag.transform.position.x), Mathf.Round(gameObjectToDrag.transform.position.y), GOCentre.z);
                    }
                }
                break;
            case 90:
                for (int i = 0; i < used.Count; i++) {
                    if (Mathf.Round(gameObjectToDrag.transform.position.x) < -3 || Mathf.Round(gameObjectToDrag.transform.position.x) > 3) { // if x coords go outside of grid
                        gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
                    } else if (Mathf.Round(gameObjectToDrag.transform.position.y) < 0 || Mathf.Round(gameObjectToDrag.transform.position.y) > 8) { // if y coords go outside the grid
                        gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
                    } else if (used[i][0] == (int)Mathf.Round(gameObjectToDrag.transform.position.x) && used[i][1] == (int)Mathf.Round(gameObjectToDrag.transform.position.y)) { // 0,0
                        gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
                    } else if (used[i][0] == (int)Mathf.Round(gameObjectToDrag.transform.position.x) && used[i][1] == (int)Mathf.Round(gameObjectToDrag.transform.position.y) + 1) { // 0,1
                        gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
                    } else if (used[i][0] == (int)Mathf.Round(gameObjectToDrag.transform.position.x) && used[i][1] == (int)Mathf.Round(gameObjectToDrag.transform.position.y) + 2) { // 0,2
                        gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
                    } else {
                        gameObjectToDrag.transform.position = new Vector3(Mathf.Round(gameObjectToDrag.transform.position.x), Mathf.Round(gameObjectToDrag.transform.position.y), GOCentre.z);
                    }
                }
                break;
        }        
    }

    void Two() {
        switch (GetRotation()) {
            case 0:
                for (int i = 0; i < used.Count; i++) {
                    if (Mathf.Round(gameObjectToDrag.transform.position.x) < -3 || Mathf.Round(gameObjectToDrag.transform.position.x) > 3) { // if x coords go outside of grid
                        gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
                    } else if (Mathf.Round(gameObjectToDrag.transform.position.y) < 0 || Mathf.Round(gameObjectToDrag.transform.position.y) > 8) { // if y coords go outside the grid
                        gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
                    } else if (used[i][0] == (int)Mathf.Round(gameObjectToDrag.transform.position.x) && used[i][1] == (int)Mathf.Round(gameObjectToDrag.transform.position.y)) { // 0,0
                        gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
                    } else if (used[i][0] == (int)Mathf.Round(gameObjectToDrag.transform.position.x) + 1 && used[i][1] == (int)Mathf.Round(gameObjectToDrag.transform.position.y)) { // 1,0
                        gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
                    } else {
                        gameObjectToDrag.transform.position = new Vector3(Mathf.Round(gameObjectToDrag.transform.position.x), Mathf.Round(gameObjectToDrag.transform.position.y), GOCentre.z);
                    }
                }
                break;
            case 90:
                for (int i = 0; i < used.Count; i++) {
                    if (Mathf.Round(gameObjectToDrag.transform.position.x) < -3 || Mathf.Round(gameObjectToDrag.transform.position.x) > 3) { // if x coords go outside of grid
                        gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
                    } else if (Mathf.Round(gameObjectToDrag.transform.position.y) < 0 || Mathf.Round(gameObjectToDrag.transform.position.y) > 8) { // if y coords go outside the grid
                        gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
                    } else if (used[i][0] == (int)Mathf.Round(gameObjectToDrag.transform.position.x) && used[i][1] == (int)Mathf.Round(gameObjectToDrag.transform.position.y)) { // 0,0
                        gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
                    } else if (used[i][0] == (int)Mathf.Round(gameObjectToDrag.transform.position.x) && used[i][1] == (int)Mathf.Round(gameObjectToDrag.transform.position.y) + 1) { // 0,1
                        gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
                    } else {
                        gameObjectToDrag.transform.position = new Vector3(Mathf.Round(gameObjectToDrag.transform.position.x), Mathf.Round(gameObjectToDrag.transform.position.y), GOCentre.z);
                    }
                }
                break;
        }
    }
}
