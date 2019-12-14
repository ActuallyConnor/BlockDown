using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FreeBlock : MonoBehaviour {

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

    void Start() {
        TPos = new Vector3((float)-0.5, -6, 0);
        LPos = new Vector3(-3, -6, 0);
        SquarePos = new Vector3(3, -6, 0);

        LinePos = new Vector3(-3, (float)-8.5, 0);
        TwoPos = new Vector3(1, (float)-8.5, 0);
        DotPos = new Vector3(4, (float)-8.5, 0);

        stop = GameObject.FindGameObjectsWithTag("Block");
    }

    void Update() {
        MovePieceWithJoyStick();
        int button = GetSelectedButton();    
        if (button == 0) {
            gameObjectToDrag = GameObject.Find("T");
            Pythagorean();
            Overlap();
            Placed();
            SinglePiecePutBack();
            SendToBottom();
            PlacingPieceWinsGame();
            Resize();
        }
    }

    public GameObject gameObjectToDrag;
    public Vector3 GOCentre;
    public Vector3 touchPosition;
    public Vector3 offset;
    public Vector3 newGOCentre;
    RaycastHit hit;
    public bool draggingMode = false;

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
        SinglePiecePutBack();
        SendToBottom();
        PlacingPieceWinsGame();
        Resize();
    }

    int ThirtyMinusTimePassed() {
        int passed = 50 - (int)Time.timeSinceLevelLoad;
        if (passed < 1) {
            return 1;
        }
        return 30 - (int)Time.timeSinceLevelLoad;
    }

    void SinglePiecePutBack() {
        if (pieces == 1 && gameObjectToDrag.transform.position.y <= -6 && GOCentre.y > -6) {
            PutBack();
        }
    }

    void PlacingPieceWinsGame() {
        if (GameObject.Find("Grid").GetComponent<FreeWall>().count >= GameObject.Find("Grid").GetComponent<FreeWall>().setups.GetPreset(0).Length) {
            GameObject.Find("Grid").GetComponent<FreeWall>().setups.SetPassed(GameObject.Find("Grid").GetComponent<FreeWall>().setups.GetPassed() + ThirtyMinusTimePassed());
            foreach (GameObject go in stop) {
                go.transform.Translate(new Vector3(0, 0, 0));
            }
        }
    }

    void Resize() {
        if (gameObjectToDrag.transform.position.y > -6) {
            setPieceSize(gameObjectToDrag.name, 0f);
        } else {
            setPieceSize(gameObjectToDrag.name, 0.5f);
        }
    }

    void setPieceSize(string currentPiece, float increment) {
        string[] pieceTypes = { "T", "L", "Square", "Line", "Two", "Dot" };
        float[,] positioning = { { 3f, 2f }, { 2f, 2f }, { 2f, 2f }, { 3f, 1f }, { 2f, 1f }, { 1f, 1f } };
        BoxCollider currentBox = gameObjectToDrag.GetComponent<BoxCollider>();

        for (int i = 0; i < pieceTypes.Length; i++) {
            if (string.Equals(currentPiece, pieceTypes[i])) {
                currentBox.size = new Vector3(positioning[i, 0] + increment, positioning[i, 1] + increment, 0);
            }
        }
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
        string[] pieceTypes = { "T", "L", "Square", "Line", "Two", "Dot" };
        Vector3[] positions = { TPos, LPos, SquarePos, LinePos, TwoPos, DotPos };

        for (int i = 0; i < pieceTypes.Length; i++) {
            if (string.Equals(gameObjectToDrag.name, pieceTypes[i])) {
                gameObjectToDrag.transform.position = positions[i];
                break;
            }
        }
        snap = false;
    }

    void Overlap() {
        string[] shapes = { "T", "L", "Line", "Two", "Dot", "Square" };
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
        if (GOCentre.y > -6 && gameObjectToDrag.transform.position.y <= -6 && !(GOCentre.y <= -6 && gameObjectToDrag.transform.position.y <= -6)) {
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
            GameObject.Find("click").GetComponent<PlayClick>().PlayAudio();
        } else {
            GameObject.Find("woosh").GetComponent<PlayClick>().PlayAudio();
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

    public bool selectedPiece = false;

    float prevHorizontal = 0;
    float prevVertical = 0;

    void MovePieceWithJoyStick() {
        if (selectedPiece) {
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");
            Vector3 currentObject = GameObject.Find("T").transform.position;

            if (IsDirectionCentered(horizontal, prevHorizontal) || IsDirectionCentered(vertical, prevVertical)) {
                Debug.Log("Horz: " + Input.GetAxis("Horizontal"));
                Debug.Log("Vert: " + Input.GetAxis("Vertical"));
                GameObject.Find("T").transform.position = new Vector3(currentObject.x + horizontal, currentObject.y + vertical, currentObject.z);
            }
            prevHorizontal = horizontal;
            prevVertical = vertical;
        }
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
                Debug.Log("joystick button 0");
            } else if (Input.GetKey("joystick button 1")) {
                keyPressed = 1;
                Debug.Log("joystick button 1");
            } else if (Input.GetKey("joystick button 2")) {
                keyPressed = 2;
                Debug.Log("joystick button 2");
            } else if (Input.GetKey("joystick button 3")) {
                keyPressed = 3;
                Debug.Log("joystick button 3");
            }
        }

        return keyPressed;
    }

    DateTime start = DateTime.Now; 

    void MoveHighLight() {
        if (GetSelectedButton() == 2) {
            // Move highlight left
        } else if (GetSelectedButton() == 3) {
            // Move highlight right
        }
    }
}
