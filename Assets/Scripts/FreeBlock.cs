using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FreeBlock : MonoBehaviour {

    public GameObject gameObjectToDrag;
    public Vector3 GOCentre;
    public Vector3 touchPosition;
    public Vector3 offset;
    public Vector3 newGOCentre;
    RaycastHit hit;
    public bool draggingMode = false;
    public bool snap = false;
    public Vector3 TPos;
    public Vector3 LPos;
    public Vector3 LinePos;
    public Vector3 TwoPos;
    public Vector3 DotPos;
    public Vector3 SquarePos;
    public int pieces = 0;
    List<Vector3> roundWall;
    List<Vector3> wall;
    public int count = 0;
    public GameObject[] stop;
    public GameObject[] spots;
    public List<Vector3> check = new List<Vector3>();
    public GameObject[] onBoard;
    bool fits;

    // Start is called before the first frame update
    void Start() {
        TPos = new Vector3((float)-0.5, -6, 0);
        LPos = new Vector3(-3, -6, 0);
        SquarePos = new Vector3(3, -6, 0);

        LinePos = new Vector3(-3, -9, 0);
        TwoPos = new Vector3(1, -9, 0);
        DotPos = new Vector3(4, (float)-9, 0);

        //Debug.Log(GameObject.Find("Grid").GetComponent<Wall>().count);
        stop = GameObject.FindGameObjectsWithTag("Block");
    }

    // Update is called once per frame
    void Update() {
        /*if (snap && GameObject.Find("Grid").transform.position.y > 0 && GameObject.Find("Grid").GetComponent<Wall>().count < GameObject.Find("Grid").GetComponent<Wall>().setups.GetPreset(0).Length) {
            GameObject.Find(gameObjectToDrag.name).transform.Translate(new Vector3(0, (float)-0.7, 0) * Time.deltaTime, Space.World);
        }
        if (GameObject.Find("Grid").GetComponent<Wall>().count >= GameObject.Find("Grid").GetComponent<Wall>().setups.GetPreset(0).Length) {
            foreach (GameObject go in stop) {
                go.transform.Translate(new Vector3(0, 0, 0));
                StartCoroutine("WinWait");
            }
        }*/
    }

    IEnumerator WinWait() {
        yield return new WaitForSeconds(1);
    }

    void OnMouseDown() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit)) {
            gameObjectToDrag = hit.collider.gameObject;
            GOCentre = gameObjectToDrag.transform.position;
            touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            offset = touchPosition - GOCentre;
            draggingMode = true;
            SendToTop();
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
        Pythagorean();
        Overlap();
        Placed();
        if (pieces == 1 && gameObjectToDrag.transform.position.y <= -6 && GOCentre.y > -6) {
            PutBack();
        }
        SendToBottom();
        Debug.Log(GameObject.Find("Grid").GetComponent<FreeWall>().count);
    }

    void SendToTop() {
        GameObject.Find(gameObjectToDrag.name).GetComponent<Renderer>().sortingLayerName = "Top";
        if (pieces > 1) {
            for (int i = 1; i < pieces; i++) {
                GameObject.Find(gameObjectToDrag.name + i).GetComponent<Renderer>().sortingLayerName = "Top";
            }
        }
    }

    void SendToBottom() {
        GameObject.Find(gameObjectToDrag.name).GetComponent<Renderer>().sortingLayerName = "Blocks";
        if (pieces > 1) {
            for (int i = 1; i < pieces; i++) {
                GameObject.Find(gameObjectToDrag.name + i).GetComponent<Renderer>().sortingLayerName = "Blocks";
            }
        }
    }

    void Reset() {
        switch (gameObjectToDrag.name) {
            case "T":
                gameObjectToDrag.transform.position = TPos;
                break;
            case "L":
                gameObjectToDrag.transform.position = LPos;
                break;
            case "Line":
                gameObjectToDrag.transform.position = LinePos;
                break;
            case "Two":
                gameObjectToDrag.transform.position = TwoPos;
                break;
            case "Dot":
                gameObjectToDrag.transform.position = DotPos;
                break;
            case "Square":
                gameObjectToDrag.transform.position = SquarePos;
                break;
        }
        snap = false;
    }

    void Overlap() {
        string[] shapes = new string[] { "T", "L", "Line", "Two", "Dot", "Square" };
        List<Vector3> piece = new List<Vector3>();
        List<Vector3> onBoard = new List<Vector3>();
        foreach (string shape in shapes) {
            if (Equals(shape, gameObjectToDrag.name)) {
                piece.Add(GameObject.Find(shape).transform.position);
                for (int i = 1; i < pieces; i++) {
                    piece.Add(GameObject.Find(shape + i).transform.position);
                }
            } else {
                onBoard.Add(GameObject.Find(shape).transform.position);
                for (int i = 1; i < 5; i++) {
                    if (GameObject.Find(shape + i) != null) {
                        onBoard.Add(GameObject.Find(shape + i).transform.position);
                    }
                }
            }
        }
        for (int i = 0; i < piece.Count; i++) {
            for (int j = 0; j < onBoard.Count; j++) {
                if (piece[i] == onBoard[j]) {
                    Reset();
                }
            }
        }
    }

    void PutBack() {
        Reset();
        snap = false;
        fits = false;
        if (GOCentre.y > -6 && gameObjectToDrag.transform.position.y <= -6 || snap == false) {
            if (GameObject.Find("Grid").GetComponent<FreeWall>().count - pieces >= 0) {
                GameObject.Find("Grid").GetComponent<FreeWall>().count -= pieces;
            } else {
                GameObject.Find("Grid").GetComponent<FreeWall>().count = 0;
            }
        }
    }

    void Placed() {
        if (snap == true && GOCentre.y <= -6) {
            GameObject.Find("Grid").GetComponent<FreeWall>().count += pieces;
        }
    }

    void Pythagorean() {
        spots = GameObject.FindGameObjectsWithTag("Blank");
        onBoard = GameObject.FindGameObjectsWithTag("Block");
        check.Clear();
        foreach (GameObject spot in spots) {
            check.Add(spot.transform.position);
        }
        float a;
        float b;
        float c;
        float least = 1000;
        fits = false;
        Vector3 closest = TPos;
        Vector3 second = TPos;
        Vector3 third = TPos;
        foreach (Vector3 che in check) {
            a = gameObjectToDrag.transform.position.x - che.x;
            b = gameObjectToDrag.transform.position.y - che.y;
            a = Mathf.Pow(a, 2);
            b = Mathf.Pow(b, 2);
            c = a + b;
            c = Mathf.Sqrt(c);
            if (c < least && c < 1.5) {
                third = second;
                second = closest;
                closest = che;
                least = c;
            }
        }

        gameObjectToDrag.transform.position = closest;
        if (pieces > 1) {
            for (int j = 1; j < 4; j++) {
                if (GameObject.Find(gameObjectToDrag.name + j) != null) {
                    if (check.Contains(GameObject.Find(gameObjectToDrag.name + j).transform.position)) {
                        fits = true;
                    } else {
                        fits = false;
                        snap = false;
                        break;
                    }
                }
            }
        } else if (pieces == 1) {
            fits = true;
        }
        if (fits == true) {
            snap = true;
        } else {
            gameObjectToDrag.transform.position = second; // first spot failed
            if (pieces > 1) {
                for (int j = 1; j < 4; j++) {
                    if (GameObject.Find(gameObjectToDrag.name + j) != null) {
                        if (check.Contains(GameObject.Find(gameObjectToDrag.name + j).transform.position)) {
                            fits = true;
                        } else {
                            fits = false;
                            snap = false;
                            break;
                        }
                    }
                }
            }
            if (fits == true) {
                snap = true;
            } else {
                gameObjectToDrag.transform.position = third; // second spot failed
                if (pieces > 1) {
                    for (int j = 1; j < 4; j++) {
                        if (GameObject.Find(gameObjectToDrag.name + j) != null) {
                            if (check.Contains(GameObject.Find(gameObjectToDrag.name + j).transform.position)) {
                                fits = true;
                            } else {
                                fits = false;
                                snap = false;
                                break;
                            }
                        }
                    }
                }
                if (fits == true) {
                    snap = true;
                } else {
                    PutBack(); // third spot failed
                    fits = false;
                    snap = false;
                }
            }
        }
    }
}

