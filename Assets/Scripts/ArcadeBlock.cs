using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcadeBlock : MonoBehaviour {

    public bool snap = false;

    public int pieces = 0;
    List<Vector3> roundFreeWall;
    List<Vector3> wall;
    public int count = 0;
    public GameObject[] spots;
    public List<Vector3> check = new List<Vector3>();
    public GameObject[] onBoard;
    bool fits;

    public Vector3 TPos;
    public Vector3 LPos;
    public Vector3 LinePos;
    public Vector3 TwoPos;
    public Vector3 DotPos;
    public Vector3 SquarePos;
    public GameObject[] stop;


    public GameObject selectedObject;

    void Start() {
        TPos = new Vector3((float)-0.5, -6, 0);
        LPos = new Vector3(-3, -6, 0);
        SquarePos = new Vector3(3, -6, 0);

        LinePos = new Vector3(-3, (float)-8.5, 0);
        TwoPos = new Vector3(1, (float)-8.5, 0);
        DotPos = new Vector3(4, (float)-8.5, 0);

        stop = GameObject.FindGameObjectsWithTag("Block");

        selectedObject = GameObject.Find("T");
    }

    void Update() {
        MovePieceWithJoyStick();
        int button = GetSelectedButton();
        if (button == 0) {
            if (DoesPieceFitHere()) {
                selectedObject = GameObject.Find("Line");
                //Overlap();
                //Placed();
                //PlacingPieceWinsGame();
            }
        }
    }

    int ThirtyMinusTimePassed() {
        int passed = 50 - (int)Time.timeSinceLevelLoad;
        if (passed < 1) {
            return 1;
        }
        return 30 - (int)Time.timeSinceLevelLoad;
    }

    //public bool selectedPiece = false;

    float prevHorizontal = 0;
    float prevVertical = 0;

    void MovePieceWithJoyStick() {
        //if (selectedPiece) {
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");
            Vector3 currentObject = GameObject.Find("T").transform.position;

            if (IsDirectionCentered(horizontal, prevHorizontal) || IsDirectionCentered(vertical, prevVertical)) {
                //Debug.Log("Horz: " + Input.GetAxis("Horizontal"));
                //Debug.Log("Vert: " + Input.GetAxis("Vertical"));
                selectedObject.transform.position = new Vector3(currentObject.x + horizontal, currentObject.y + vertical, currentObject.z);
            }
            prevHorizontal = horizontal;
            prevVertical = vertical;
        //}
    }

    bool IsDirectionCentered(float direction, float prevDirection) {

        return Math.Abs(direction) > double.Epsilon && Math.Abs(prevDirection) < double.Epsilon;
    }

    float horizontal;
    float vertical;
    bool isLastJoyStickPositionCentered = true;

    Vector3 GetJoyStickPosition(Vector3 currentPiecePosition) {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        if (Math.Abs(horizontal) > double.Epsilon && Math.Abs(vertical) > double.Epsilon) {
            if (isLastJoyStickPositionCentered) {
                isLastJoyStickPositionCentered = false;
                return new Vector3(currentPiecePosition.x + horizontal, currentPiecePosition.y + vertical, currentPiecePosition.z);
            }
        } else {
            isLastJoyStickPositionCentered = true;
        }

        return currentPiecePosition;
    }

    int keyPressed = -1;

    int GetSelectedButton() {

        TimeSpan elapsed = DateTime.Now - start;

        if (elapsed.Milliseconds > 100) {
            start = DateTime.Now;
            if (Input.GetKey("joystick button 0")) {
                keyPressed = 0;
                //Debug.Log("joystick button 0");
            } else if (Input.GetKey("joystick button 1")) {
                keyPressed = 1;
                //Debug.Log("joystick button 1");
            } else if (Input.GetKey("joystick button 2")) {
                keyPressed = 2;
                //Debug.Log("joystick button 2");
            } else if (Input.GetKey("joystick button 3")) {
                keyPressed = 3;
                //Debug.Log("joystick button 3");
            }
        }

        return keyPressed;
    }

    DateTime start = DateTime.Now;

    void PlacingPieceWinsGame() {
        if (GameObject.Find("Grid").GetComponent<ArcadeWall>().count >= GameObject.Find("Grid").GetComponent<ArcadeWall>().setups.GetPreset(0).Length) {
            GameObject.Find("Grid").GetComponent<ArcadeWall>().setups.SetPassed(GameObject.Find("Grid").GetComponent<ArcadeWall>().setups.GetPassed() + ThirtyMinusTimePassed());
            foreach (GameObject go in stop) {
                go.transform.Translate(new Vector3(0, 0, 0));
            }
        }
    }

    void Overlap() {
        string[] shapes = { "T", "L", "Line", "Two", "Dot", "Square" };
        List<Vector3> piece = new List<Vector3>();
        List<Vector3> onBoard = new List<Vector3>();
        foreach (string shape in shapes) {
            if (Equals(shape, selectedObject.name)) {
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
                    //Reset();
                }
            }
        }
    }

    void Placed() {
        if (snap == true /* && GOCentre.y <= -6*/) {
            GameObject.Find("Grid").GetComponent<ArcadeWall>().count += pieces;
            GameObject.Find("click").GetComponent<PlayClick>().PlayAudio();
        } else {
            GameObject.Find("woosh").GetComponent<PlayClick>().PlayAudio();
        }
    }

    bool DoesPieceFitHere() {
        spots = GameObject.FindGameObjectsWithTag("Blank");
        //onBoard = GameObject.FindGameObjectsWithTag("Square");

        string pieceName = selectedObject.name;

        bool objectCanBePlaced = true;

        for (int i = 1; i < 4; i++) {
            foreach (GameObject tile in spots) {
                if (GameObject.Find(pieceName + i) != null) {

                    if (Math.Abs(GameObject.Find(pieceName + i).transform.position.x - tile.transform.position.x) < double.Epsilon && Math.Abs(GameObject.Find(pieceName + i).transform.position.y - tile.transform.position.y) < double.Epsilon) {
                        objectCanBePlaced = false;
                        Debug.Log(pieceName + i + " doesn't fit");
                    } else {
                        Debug.Log(pieceName + i + " does fit");
                    }
                }
            }
        }

        return objectCanBePlaced;

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
            a = selectedObject.transform.position.x - che.x;
            b = selectedObject.transform.position.y - che.y;
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

        selectedObject.transform.position = closest;
        if (pieces > 1) {
            for (int j = 1; j < 4; j++) {
                if (GameObject.Find(selectedObject.name + j) != null) {
                    if (check.Contains(GameObject.Find(selectedObject.name + j).transform.position)) {
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
            selectedObject.transform.position = second; // first spot failed
            if (pieces > 1) {
                for (int j = 1; j < 4; j++) {
                    if (GameObject.Find(selectedObject.name + j) != null) {
                        if (check.Contains(GameObject.Find(selectedObject.name + j).transform.position)) {
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
                selectedObject.transform.position = third; // second spot failed
                if (pieces > 1) {
                    for (int j = 1; j < 4; j++) {
                        if (GameObject.Find(selectedObject.name + j) != null) {
                            if (check.Contains(GameObject.Find(selectedObject.name + j).transform.position)) {
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
                    //PutBack(); // third spot failed
                    fits = false;
                    snap = false;
                }
            }
        }
    }

}
