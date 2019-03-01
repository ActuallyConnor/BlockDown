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

    public bool draggingMode = false; // is draggable at that moment in time

    public string shape = ""; // what shape is this   

    public GameObject[] used;

    public GameObject[] objs; // array of all GameObjects on the screen that are active

    Vector3 TPos = new Vector3(-4, -6, 0);
    Vector3 LPos = new Vector3(0, -6, 0);
    Vector3 LinePos = new Vector3(3, -6, 0);
    Vector3 TwoPos = new Vector3(-2, -9, 0);
    Vector3 DotPos = new Vector3(2, -9, 0);

    bool TFirst = true; // TShape's first move
    bool LFirst = true; // LShape's first move
    bool LineFirst = true; // Line's first move
    bool TwoFirst = true; // Two's first move
    // bool DotFirst = true;

    Renderer blockRender;
    Renderer blockRender1;
    Renderer blockRender2;
    Renderer blockRender3;

    public bool TSnap = false;
    public bool LSnap = false;
    public bool LineSnap = false;
    public bool TwoSnap = false;
    public bool DotSnap = false;


    public bool canRotate = true; // shape can rotate
    public bool limitRotate = false; // shape can rotate only 90 degrees   

    // Start is called before the first frame update
    void Start() {
        used = FindObjectOfType<Wall>().GetGrid();
        objs = GameObject.FindGameObjectsWithTag("Block");               
    }

    // Update is called once per frame
    void Update() {   
        /*if (TSnap) {
            GameObject.Find("T").transform.Translate(Vector3.down * Time.deltaTime, Space.World);
        }
        if (LSnap) {
            GameObject.Find("L").transform.Translate(Vector3.down * Time.deltaTime, Space.World);
        }
        if (LineSnap) {
            GameObject.Find("Line").transform.Translate(Vector3.down * Time.deltaTime, Space.World);
        }
        if (TwoSnap) {
            GameObject.Find("Two").transform.Translate(Vector3.down * Time.deltaTime, Space.World);
        }
        if (DotSnap) {
            GameObject.Find("Dot").transform.Translate(Vector3.down * Time.deltaTime, Space.World);
        }*/
    }
    
    string GetShape() => shape; // returns the shape in question

    void OnMouseDown() { // on initial mouse click down
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // raycast out from click spot to identify box collider the click is in
        // if the ray hits a collider (not 2Dcollider)
        if (Physics.Raycast(ray, out hit)) { // if raycast hits
            gameObjectToDrag = hit.collider.gameObject; // gameobject that will be dragged equals what the raycast hit
            GOCentre = gameObjectToDrag.transform.position; // sets original position before the move
            touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); // sets the position in which the mouse clicked, used for the offset
            offset = touchPosition - GOCentre; // sets the offset between the click position and the centre of the object before the move
            draggingMode = true; // object is now draggable
            SendToTop();
        }
        
    }

    void OnMouseDrag() { // as mouse is moving
        if (draggingMode) { // if dragging mode is enabled
            touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); // sets the position in which the mouse clicked, used for the offset
            newGOCentre = touchPosition - offset; // new centre as mouse is dragging piece
            gameObjectToDrag.transform.position = new Vector3(newGOCentre.x, newGOCentre.y, GOCentre.z); // this is all to show how the piece is moving around as it is being dragged
        }
    }

    void OnMouseUp() {
        draggingMode = false;
        IsValid();
        SendToBottom();
    }

    void SendToTop() {
        switch(gameObjectToDrag.name) {
            case "T":
                blockRender = GameObject.Find("T").GetComponent<Renderer>();
                blockRender1 = GameObject.Find("T1").GetComponent<Renderer>();
                blockRender2 = GameObject.Find("T2").GetComponent<Renderer>();
                blockRender3 = GameObject.Find("T3").GetComponent<Renderer>();
                blockRender.sortingLayerName = "Top";
                blockRender1.sortingLayerName = "Top";
                blockRender2.sortingLayerName = "Top";
                blockRender3.sortingLayerName = "Top";
                TSnap = false;
                break;
            case "L":
                blockRender = GameObject.Find("L").GetComponent<Renderer>();
                blockRender1 = GameObject.Find("L1").GetComponent<Renderer>();
                blockRender2 = GameObject.Find("L2").GetComponent<Renderer>();
                blockRender.sortingLayerName = "Top";
                blockRender1.sortingLayerName = "Top";
                blockRender2.sortingLayerName = "Top";
                LSnap = false;
                break;
            case "Line":
                blockRender = GameObject.Find("Line").GetComponent<Renderer>();
                blockRender1 = GameObject.Find("Line1").GetComponent<Renderer>();
                blockRender2 = GameObject.Find("Line2").GetComponent<Renderer>();
                blockRender.sortingLayerName = "Top";
                blockRender1.sortingLayerName = "Top";
                blockRender2.sortingLayerName = "Top";
                LineSnap = false;
                break;
            case "Two":
                blockRender = GameObject.Find("Two").GetComponent<Renderer>();
                blockRender1 = GameObject.Find("Two1").GetComponent<Renderer>();
                blockRender.sortingLayerName = "Top";
                blockRender1.sortingLayerName = "Top";
                TwoSnap = false;
                break;
            case "Dot":
                blockRender = GameObject.Find("Dot").GetComponent<Renderer>();
                blockRender.sortingLayerName = "Top";
                DotSnap = false;
                break;
        }
    }

    void SendToBottom() {
        switch(gameObjectToDrag.name) {
            case "T":
                blockRender = GameObject.Find("T").GetComponent<Renderer>();
                blockRender1 = GameObject.Find("T1").GetComponent<Renderer>();
                blockRender2 = GameObject.Find("T2").GetComponent<Renderer>();
                blockRender3 = GameObject.Find("T3").GetComponent<Renderer>();
                blockRender.sortingLayerName = "Blocks";
                blockRender1.sortingLayerName = "Blocks";
                blockRender2.sortingLayerName = "Blocks";
                blockRender3.sortingLayerName = "Blocks";
                break;
            case "L":
                blockRender = GameObject.Find("L").GetComponent<Renderer>();
                blockRender1 = GameObject.Find("L1").GetComponent<Renderer>();
                blockRender2 = GameObject.Find("L2").GetComponent<Renderer>();
                blockRender.sortingLayerName = "Blocks";
                blockRender1.sortingLayerName = "Blocks";
                blockRender2.sortingLayerName = "Blocks";
                break;
            case "Line":
                blockRender = GameObject.Find("Line").GetComponent<Renderer>();
                blockRender1 = GameObject.Find("Line1").GetComponent<Renderer>();
                blockRender2 = GameObject.Find("Line2").GetComponent<Renderer>();
                blockRender.sortingLayerName = "Blocks";
                blockRender1.sortingLayerName = "Blocks";
                blockRender2.sortingLayerName = "Blocks";
                break;
            case "Two":
                blockRender = GameObject.Find("Two").GetComponent<Renderer>();
                blockRender1 = GameObject.Find("Two1").GetComponent<Renderer>();
                blockRender.sortingLayerName = "Blocks";
                blockRender1.sortingLayerName = "Blocks";
                break;
            case "Dot":
                blockRender = GameObject.Find("Dot").GetComponent<Renderer>();
                blockRender.sortingLayerName = "Top";
                break;
        }
    }

    public void IsValid() {
        switch (GetShape()) {
            case "T":
                TShape();                
                if (gameObjectToDrag.transform.position != TPos && TFirst == true) {
                    for (int i = 0; i < objs.Length; i++) {
                        if (objs[i].name == "L") {
                            objs[i].transform.position = TPos;
                        }
                    }
                    TFirst = false;
                    TSnap = true;
                }
                break;
            case "L":
                LShape();                
                if (gameObjectToDrag.transform.position != LPos && LFirst == true) {
                    for (int i = 0; i < objs.Length; i++) {
                        if (objs[i].name == "Line") {
                            objs[i].transform.position = LPos;
                        }
                    }
                    LFirst = false;
                    LSnap = true;
                }
                break;
            case "Line":
                Line();
                if (gameObjectToDrag.transform.position != LinePos && LineFirst == true) {
                    for (int i = 0; i < objs.Length; i++) {
                        if (objs[i].name == "Two") {
                            objs[i].transform.position = LinePos;
                        }
                    }
                    LineFirst = false;
                    LineSnap = true;
                }
                break;
            case "Two":
                Two();
                if (gameObjectToDrag.transform.position != TwoPos && TwoFirst == true) {
                    for (int i = 0; i < objs.Length; i++) {
                        if (objs[i].name == "Dot") {
                            objs[i].transform.position = TwoPos;
                        }
                    }
                    TwoFirst = false;
                    TwoSnap = true;
                }
                break;
            case "Dot":
                Dot();
                DotSnap = true;
                break;
        }
    }


    public void TShape() {        
        switch ((int)gameObjectToDrag.transform.rotation.eulerAngles.z) {
            case 0:               
                for (int i = 0; i < used.Length; i++) {
                    if (Mathf.Round(gameObjectToDrag.transform.position.x) < -4 || Mathf.Round(gameObjectToDrag.transform.position.x) + 2 > 5) { // if x coords go outside of grid
                        gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);                        
                    } else if (Mathf.Round(gameObjectToDrag.transform.position.y) - 1 < used[used.Length-1].transform.position.y || Mathf.Round(gameObjectToDrag.transform.position.y) > used[0].transform.position.y) { // if y coords go outside of grid
                        gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
                    } else if (used[i].transform.position.x == Mathf.Round(gameObjectToDrag.transform.position.x) && used[i].transform.position.y == Mathf.Round(gameObjectToDrag.transform.position.y)) { // 0,0
                        gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
                    } else if (used[i].transform.position.x == Mathf.Round(gameObjectToDrag.transform.position.x) + 1 && used[i].transform.position.y == Mathf.Round(gameObjectToDrag.transform.position.y)) { // 1,0
                        gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
                    } else if (used[i].transform.position.x == Mathf.Round(gameObjectToDrag.transform.position.x) + 2 && used[i].transform.position.y == Mathf.Round(gameObjectToDrag.transform.position.y)) { // 2,0
                        gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
                    } else if (used[i].transform.position.x == Mathf.Round(gameObjectToDrag.transform.position.x) + 1 && used[i].transform.position.y == Mathf.Round(gameObjectToDrag.transform.position.y) - 1) { // 1,-1
                        gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
                    } else {
                        gameObjectToDrag.transform.position = new Vector3(Mathf.Round(gameObjectToDrag.transform.position.x), Mathf.Round(gameObjectToDrag.transform.position.y), GOCentre.z);
                    }
                }                
                break;
            case 90:
                for (int i = 0; i < used.Length; i++) {
                    if (Mathf.Round(gameObjectToDrag.transform.position.x) < -4 || Mathf.Round(gameObjectToDrag.transform.position.x) + 1 > 5) { // if x coords go outside of grid
                        gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
                    } else if (Mathf.Round(gameObjectToDrag.transform.position.y) < used[used.Length-1].transform.position.y || Mathf.Round(gameObjectToDrag.transform.position.y) + 2 > used[0].transform.position.y) { // if y coords go outside of grid
                        gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
                    } else if (used[i].transform.position.x == (int)Mathf.Round(gameObjectToDrag.transform.position.x) && used[i].transform.position.y == (int)Mathf.Round(gameObjectToDrag.transform.position.y)) { // 0,0
                        gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
                    } else if (used[i].transform.position.x == (int)Mathf.Round(gameObjectToDrag.transform.position.x) && used[i].transform.position.y == (int)Mathf.Round(gameObjectToDrag.transform.position.y) + 1) { // 0,1
                        gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
                    } else if (used[i].transform.position.x == (int)Mathf.Round(gameObjectToDrag.transform.position.x) && used[i].transform.position.y == (int)Mathf.Round(gameObjectToDrag.transform.position.y) + 2) { // 0,2
                        gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
                    } else if (used[i].transform.position.x == (int)Mathf.Round(gameObjectToDrag.transform.position.x) + 1 && used[i].transform.position.y == (int)Mathf.Round(gameObjectToDrag.transform.position.y) + 1) { // 1,1
                        gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
                    } else {
                        gameObjectToDrag.transform.position = new Vector3(Mathf.Round(gameObjectToDrag.transform.position.x), Mathf.Round(gameObjectToDrag.transform.position.y), GOCentre.z);
                    }
                }
                break;
            case 180:
                for (int i = 0; i < used.Length; i++) {
                    if (Mathf.Round(gameObjectToDrag.transform.position.x) - 2 < -4 || Mathf.Round(gameObjectToDrag.transform.position.x) > 5) { // if x coords go outside of grid
                        gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
                    } else if (Mathf.Round(gameObjectToDrag.transform.position.y) < used[used.Length-1].transform.position.y || Mathf.Round(gameObjectToDrag.transform.position.y) + 1 > used[0].transform.position.y) { // if y coords go outside of grid
                        gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
                    } else if (used[i].transform.position.x == (int)Mathf.Round(gameObjectToDrag.transform.position.x) && used[i].transform.position.y == (int)Mathf.Round(gameObjectToDrag.transform.position.y)) { // 0,0
                        gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
                    } else if (used[i].transform.position.x == (int)Mathf.Round(gameObjectToDrag.transform.position.x) - 1 && used[i].transform.position.y == (int)Mathf.Round(gameObjectToDrag.transform.position.y)) { // -1,0
                        gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
                    } else if (used[i].transform.position.x == (int)Mathf.Round(gameObjectToDrag.transform.position.x) - 2 && used[i].transform.position.y == (int)Mathf.Round(gameObjectToDrag.transform.position.y)) { // -2,0
                        gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
                    } else if (used[i].transform.position.x == (int)Mathf.Round(gameObjectToDrag.transform.position.x) - 1 && used[i].transform.position.y == (int)Mathf.Round(gameObjectToDrag.transform.position.y) + 1) { // -1,1
                        gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
                    } else {
                        gameObjectToDrag.transform.position = new Vector3(Mathf.Round(gameObjectToDrag.transform.position.x), Mathf.Round(gameObjectToDrag.transform.position.y), GOCentre.z);
                    }
                }
                break;
            case 270:
                for (int i = 0; i < used.Length; i++) {
                    if (Mathf.Round(gameObjectToDrag.transform.position.x) - 1 < -4 || Mathf.Round(gameObjectToDrag.transform.position.x) > 5) { // if x coords go outside of grid
                        gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
                    } else if (Mathf.Round(gameObjectToDrag.transform.position.y) - 2 < used[used.Length-1].transform.position.y || Mathf.Round(gameObjectToDrag.transform.position.y) > used[0].transform.position.y) { // if y coords go outside of grid
                        gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
                    } else if (used[i].transform.position.x == (int)Mathf.Round(gameObjectToDrag.transform.position.x) && used[i].transform.position.y == (int)Mathf.Round(gameObjectToDrag.transform.position.y)) { // 0,0
                        gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
                    } else if (used[i].transform.position.x == (int)Mathf.Round(gameObjectToDrag.transform.position.x) && used[i].transform.position.y == (int)Mathf.Round(gameObjectToDrag.transform.position.y) - 1) { // 0,-1
                        gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
                    } else if (used[i].transform.position.x == (int)Mathf.Round(gameObjectToDrag.transform.position.x) && used[i].transform.position.y == (int)Mathf.Round(gameObjectToDrag.transform.position.y) - 2) { // 0,-2
                        gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
                    } else if (used[i].transform.position.x == (int)Mathf.Round(gameObjectToDrag.transform.position.x) - 1 && used[i].transform.position.y == (int)Mathf.Round(gameObjectToDrag.transform.position.y) - 1) { // -1,-1
                        gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
                    } else {
                        gameObjectToDrag.transform.position = new Vector3(Mathf.Round(gameObjectToDrag.transform.position.x), Mathf.Round(gameObjectToDrag.transform.position.y), GOCentre.z);
                    }
                }
                break;
        }
        GameObject[] TOnBoard = new GameObject[4];
        TOnBoard[0] = GameObject.Find("T");
        TOnBoard[1] = GameObject.Find("T1");
        TOnBoard[2] = GameObject.Find("T2");
        TOnBoard[3] = GameObject.Find("T3");
        GameObject[] onBoard = new GameObject[9];
        onBoard[0] = GameObject.Find("L");
        onBoard[1] = GameObject.Find("L1");
        onBoard[2] = GameObject.Find("L2");
        onBoard[3] = GameObject.Find("Line");
        onBoard[4] = GameObject.Find("Line1");
        onBoard[5] = GameObject.Find("Line2");
        onBoard[6] = GameObject.Find("Two");
        onBoard[7] = GameObject.Find("Two1");
        onBoard[8] = GameObject.Find("Dot");
        for (int i = 0; i < TOnBoard.Length; i++) {
            for (int j = 0; j < onBoard.Length; j++) {
                if (TOnBoard[i].transform.position.x == onBoard[j].transform.position.x && TOnBoard[i].transform.position.y == onBoard[j].transform.position.y) {
                    gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
                } else {
                    gameObjectToDrag.transform.position = new Vector3(Mathf.Round(gameObjectToDrag.transform.position.x), Mathf.Round(gameObjectToDrag.transform.position.y), GOCentre.z);
                }
            }
        }
    }

    public void LShape() {
        switch ((int)gameObjectToDrag.transform.rotation.eulerAngles.z) {
            case 0:
                for (int i = 0; i < used.Length; i++) {
                    if (Mathf.Round(gameObjectToDrag.transform.position.x) < -4 || Mathf.Round(gameObjectToDrag.transform.position.x) + 1 > 5) { // if x coords go outside of grid
                        gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
                    } else if (Mathf.Round(gameObjectToDrag.transform.position.y) - 1 < used[used.Length-1].transform.position.y || Mathf.Round(gameObjectToDrag.transform.position.y) > used[0].transform.position.y) { // if y coords go outside the grid
                        gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
                    } else if (used[i].transform.position.x == (int)Mathf.Round(gameObjectToDrag.transform.position.x) && used[i].transform.position.y == (int)Mathf.Round(gameObjectToDrag.transform.position.y)) { // 0,0
                        gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
                    } else if (used[i].transform.position.x == (int)Mathf.Round(gameObjectToDrag.transform.position.x) && used[i].transform.position.y == (int)Mathf.Round(gameObjectToDrag.transform.position.y) - 1) { // 0,-1
                        gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
                    } else if (used[i].transform.position.x == (int)Mathf.Round(gameObjectToDrag.transform.position.x) + 1 && used[i].transform.position.y == (int)Mathf.Round(gameObjectToDrag.transform.position.y) - 1) { // 1,-1
                        gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
                    } else {
                        gameObjectToDrag.transform.position = new Vector3(Mathf.Round(gameObjectToDrag.transform.position.x), Mathf.Round(gameObjectToDrag.transform.position.y), GOCentre.z);
                    }
                }                
                break;
            case 90:
                for (int i = 0; i < used.Length; i++) {
                    if (Mathf.Round(gameObjectToDrag.transform.position.x) < -4 || Mathf.Round(gameObjectToDrag.transform.position.x) + 1 > 5) { // if x coords go outside of grid
                        gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
                    } else if (Mathf.Round(gameObjectToDrag.transform.position.y) < used[used.Length-1].transform.position.y || Mathf.Round(gameObjectToDrag.transform.position.y) + 1 > used[0].transform.position.y) { // if y coords go outside the grid
                        gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
                    } else if (used[i].transform.position.x == (int)Mathf.Round(gameObjectToDrag.transform.position.x) && used[i].transform.position.y == (int)Mathf.Round(gameObjectToDrag.transform.position.y)) { // 0,0
                        gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
                    } else if (used[i].transform.position.x == (int)Mathf.Round(gameObjectToDrag.transform.position.x) + 1 && used[i].transform.position.y == (int)Mathf.Round(gameObjectToDrag.transform.position.y)) { // 1,0
                        gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
                    } else if (used[i].transform.position.x == (int)Mathf.Round(gameObjectToDrag.transform.position.x) + 1 && used[i].transform.position.y == (int)Mathf.Round(gameObjectToDrag.transform.position.y) + 1) { // 1,1
                        gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
                    } else {
                        gameObjectToDrag.transform.position = new Vector3(Mathf.Round(gameObjectToDrag.transform.position.x), Mathf.Round(gameObjectToDrag.transform.position.y), GOCentre.z);
                    }
                }
                break;
            case 180:
                for (int i = 0; i < used.Length; i++) {
                    if (Mathf.Round(gameObjectToDrag.transform.position.x) - 1 < -4 || Mathf.Round(gameObjectToDrag.transform.position.x) > 5) { // if x coords go outside of grid
                        gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
                    } else if (Mathf.Round(gameObjectToDrag.transform.position.y) < used[used.Length-1].transform.position.y || Mathf.Round(gameObjectToDrag.transform.position.y) + 1 > used[0].transform.position.y) { // if y coords go outside the grid
                        gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
                    } else if (used[i].transform.position.x == (int)Mathf.Round(gameObjectToDrag.transform.position.x) && used[i].transform.position.y == (int)Mathf.Round(gameObjectToDrag.transform.position.y)) { // 0,0
                        gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
                    } else if (used[i].transform.position.x == (int)Mathf.Round(gameObjectToDrag.transform.position.x) && used[i].transform.position.y == (int)Mathf.Round(gameObjectToDrag.transform.position.y) + 1) { // 0,1
                        gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
                    } else if (used[i].transform.position.x == (int)Mathf.Round(gameObjectToDrag.transform.position.x) - 1 && used[i].transform.position.y == (int)Mathf.Round(gameObjectToDrag.transform.position.y) + 1) { // -1,1
                        gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
                    } else {
                        gameObjectToDrag.transform.position = new Vector3(Mathf.Round(gameObjectToDrag.transform.position.x), Mathf.Round(gameObjectToDrag.transform.position.y), GOCentre.z);
                    }
                }
                break;
            case 270:
                for (int i = 0; i < used.Length; i++) {
                    if (Mathf.Round(gameObjectToDrag.transform.position.x) - 1 < -4 || Mathf.Round(gameObjectToDrag.transform.position.x) > 5) { // if x coords go outside of grid
                        gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
                    } else if (Mathf.Round(gameObjectToDrag.transform.position.y) - 1 < used[used.Length-1].transform.position.y || Mathf.Round(gameObjectToDrag.transform.position.y) > used[0].transform.position.y) { // if y coords go outside the grid
                        gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
                    } else if (used[i].transform.position.x == (int)Mathf.Round(gameObjectToDrag.transform.position.x) && used[i].transform.position.y == (int)Mathf.Round(gameObjectToDrag.transform.position.y)) { // 0,0
                        gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
                    } else if (used[i].transform.position.x == (int)Mathf.Round(gameObjectToDrag.transform.position.x) - 1 && used[i].transform.position.y == (int)Mathf.Round(gameObjectToDrag.transform.position.y)) { // -1,0
                        gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
                    } else if (used[i].transform.position.x == (int)Mathf.Round(gameObjectToDrag.transform.position.x) - 1 && used[i].transform.position.y == (int)Mathf.Round(gameObjectToDrag.transform.position.y) - 1) { // -1,-1
                        gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
                    } else {
                        gameObjectToDrag.transform.position = new Vector3(Mathf.Round(gameObjectToDrag.transform.position.x), Mathf.Round(gameObjectToDrag.transform.position.y), GOCentre.z);
                    }
                }
                break;
        }
        GameObject[] LOnBoard = new GameObject[3];
        LOnBoard[0] = GameObject.Find("L");
        LOnBoard[1] = GameObject.Find("L1");
        LOnBoard[2] = GameObject.Find("L2");
        GameObject[] onBoard = new GameObject[10];
        onBoard[0] = GameObject.Find("T");
        onBoard[1] = GameObject.Find("T1");
        onBoard[2] = GameObject.Find("T2");
        onBoard[3] = GameObject.Find("T3");
        onBoard[4] = GameObject.Find("Line");
        onBoard[5] = GameObject.Find("Line1");
        onBoard[6] = GameObject.Find("Line2");
        onBoard[7] = GameObject.Find("Two");
        onBoard[8] = GameObject.Find("Two1");
        onBoard[9] = GameObject.Find("Dot");
        for (int i = 0; i < LOnBoard.Length; i++) {
            for (int j = 0; j < onBoard.Length; j++) {
                if (Mathf.Round(LOnBoard[i].transform.position.x) == Mathf.Round(onBoard[j].transform.position.x) && Mathf.Round(LOnBoard[i].transform.position.y) == Mathf.Round(onBoard[j].transform.position.y)) {
                    gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
                } else {
                    gameObjectToDrag.transform.position = new Vector3(Mathf.Round(gameObjectToDrag.transform.position.x), Mathf.Round(gameObjectToDrag.transform.position.y), GOCentre.z);
                }
            }
        }
    }

    public void Dot() {
        for (int i = 0; i < used.Length; i++) {
            if (Mathf.Round(gameObjectToDrag.transform.position.x) < -4 || Mathf.Round(gameObjectToDrag.transform.position.x) > 5) { // if x coords go outside of grid
                gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
            } else if (Mathf.Round(gameObjectToDrag.transform.position.y) < used[used.Length-1].transform.position.y || Mathf.Round(gameObjectToDrag.transform.position.y) > used[0].transform.position.y) { // if y coords go outside the grid
                gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
            } else if (used[i].transform.position.x == (int)Mathf.Round(gameObjectToDrag.transform.position.x) && used[i].transform.position.y == (int)Mathf.Round(gameObjectToDrag.transform.position.y)) { // 0,0
                gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
            } else {
                gameObjectToDrag.transform.position = new Vector3(Mathf.Round(gameObjectToDrag.transform.position.x), Mathf.Round(gameObjectToDrag.transform.position.y), GOCentre.z);
            }
        }
        GameObject[] DotOnBoard = new GameObject[1];
        DotOnBoard[0] = GameObject.Find("Dot");;
        GameObject[] onBoard = new GameObject[12];
        onBoard[0] = GameObject.Find("T");
        onBoard[1] = GameObject.Find("T1");
        onBoard[2] = GameObject.Find("T2");
        onBoard[3] = GameObject.Find("T3");
        onBoard[4] = GameObject.Find("Line");
        onBoard[5] = GameObject.Find("Line1");
        onBoard[6] = GameObject.Find("Line2");
        onBoard[7] = GameObject.Find("Two");
        onBoard[8] = GameObject.Find("Two1");
        onBoard[9] = GameObject.Find("L");
        onBoard[10] = GameObject.Find("L1");
        onBoard[11] = GameObject.Find("L2");
        for (int i = 0; i < DotOnBoard.Length; i++) {
            for (int j = 0; j < onBoard.Length; j++) {
                if (Mathf.Round(DotOnBoard[i].transform.position.x) == Mathf.Round(onBoard[j].transform.position.x) && Mathf.Round(DotOnBoard[i].transform.position.y) == Mathf.Round(onBoard[j].transform.position.y)) {
                    gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
                } else {
                    gameObjectToDrag.transform.position = new Vector3(Mathf.Round(gameObjectToDrag.transform.position.x), Mathf.Round(gameObjectToDrag.transform.position.y), GOCentre.z);
                }
            }
        }
    }

    public void Line() {
        switch ((int)gameObjectToDrag.transform.rotation.eulerAngles.z) {
            case 0:
                for (int i = 0; i < used.Length; i++) {
                    if (Mathf.Round(gameObjectToDrag.transform.position.x) < -4 || Mathf.Round(gameObjectToDrag.transform.position.x) > 5) { // if x coords go outside of grid
                        gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
                    } else if (Mathf.Round(gameObjectToDrag.transform.position.y) < used[used.Length-1].transform.position.y || Mathf.Round(gameObjectToDrag.transform.position.y) > used[0].transform.position.y) { // if y coords go outside the grid
                        gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
                    } else if (used[i].transform.position.x == (int)Mathf.Round(gameObjectToDrag.transform.position.x) && used[i].transform.position.y == (int)Mathf.Round(gameObjectToDrag.transform.position.y)) { // 0,0
                        gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
                    } else if (used[i].transform.position.x == (int)Mathf.Round(gameObjectToDrag.transform.position.x) + 1 && used[i].transform.position.y == (int)Mathf.Round(gameObjectToDrag.transform.position.y)) { // 1,0
                        gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
                    } else if (used[i].transform.position.x == (int)Mathf.Round(gameObjectToDrag.transform.position.x) + 2 && used[i].transform.position.y == (int)Mathf.Round(gameObjectToDrag.transform.position.y)) { // 2,0
                        gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
                    } else {
                        gameObjectToDrag.transform.position = new Vector3(Mathf.Round(gameObjectToDrag.transform.position.x), Mathf.Round(gameObjectToDrag.transform.position.y), GOCentre.z);
                    }
                }                
                break;
            case 90:
                for (int i = 0; i < used.Length; i++) {
                    if (Mathf.Round(gameObjectToDrag.transform.position.x) < -4 || Mathf.Round(gameObjectToDrag.transform.position.x) > 5) { // if x coords go outside of grid
                        gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
                    } else if (Mathf.Round(gameObjectToDrag.transform.position.y) < used[used.Length-1].transform.position.y || Mathf.Round(gameObjectToDrag.transform.position.y) > used[0].transform.position.y) { // if y coords go outside the grid
                        gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
                    } else if (used[i].transform.position.x == (int)Mathf.Round(gameObjectToDrag.transform.position.x) && used[i].transform.position.y == (int)Mathf.Round(gameObjectToDrag.transform.position.y)) { // 0,0
                        gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
                    } else if (used[i].transform.position.x == (int)Mathf.Round(gameObjectToDrag.transform.position.x) && used[i].transform.position.y == (int)Mathf.Round(gameObjectToDrag.transform.position.y) + 1) { // 0,1
                        gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
                    } else if (used[i].transform.position.x == (int)Mathf.Round(gameObjectToDrag.transform.position.x) && used[i].transform.position.y == (int)Mathf.Round(gameObjectToDrag.transform.position.y) + 2) { // 0,2
                        gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
                    } else {
                        gameObjectToDrag.transform.position = new Vector3(Mathf.Round(gameObjectToDrag.transform.position.x), Mathf.Round(gameObjectToDrag.transform.position.y), GOCentre.z);
                    }
                }
                break;
        }
        GameObject[] LineOnBoard = new GameObject[3];
        LineOnBoard[0] = GameObject.Find("Line");
        LineOnBoard[1] = GameObject.Find("Line1");
        LineOnBoard[2] = GameObject.Find("Line2");
        GameObject[] onBoard = new GameObject[10];
        onBoard[0] = GameObject.Find("T");
        onBoard[1] = GameObject.Find("T1");
        onBoard[2] = GameObject.Find("T2");
        onBoard[3] = GameObject.Find("T3");
        onBoard[4] = GameObject.Find("L");
        onBoard[5] = GameObject.Find("L1");
        onBoard[6] = GameObject.Find("L2");
        onBoard[7] = GameObject.Find("Two");
        onBoard[8] = GameObject.Find("Two1");
        onBoard[9] = GameObject.Find("Dot");
        for (int i = 0; i < LineOnBoard.Length; i++) {
            for (int j = 0; j < onBoard.Length; j++) {
                if (Mathf.Round(LineOnBoard[i].transform.position.x) == Mathf.Round(onBoard[j].transform.position.x) && Mathf.Round(LineOnBoard[i].transform.position.y) == Mathf.Round(onBoard[j].transform.position.y)) {
                    gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
                } else {
                    gameObjectToDrag.transform.position = new Vector3(Mathf.Round(gameObjectToDrag.transform.position.x), Mathf.Round(gameObjectToDrag.transform.position.y), GOCentre.z);
                }
            }
        }
    }

    public void Two() {
        switch ((int)gameObjectToDrag.transform.rotation.eulerAngles.z) {
            case 0:
                for (int i = 0; i < used.Length; i++) {
                    if (Mathf.Round(gameObjectToDrag.transform.position.x) < -4 || Mathf.Round(gameObjectToDrag.transform.position.x) > 5) { // if x coords go outside of grid
                        gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
                    } else if (Mathf.Round(gameObjectToDrag.transform.position.y) < used[used.Length-1].transform.position.y || Mathf.Round(gameObjectToDrag.transform.position.y) > used[0].transform.position.y) { // if y coords go outside the grid
                        gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
                    } else if (used[i].transform.position.x == (int)Mathf.Round(gameObjectToDrag.transform.position.x) && used[i].transform.position.y == (int)Mathf.Round(gameObjectToDrag.transform.position.y)) { // 0,0
                        gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
                    } else if (used[i].transform.position.x == (int)Mathf.Round(gameObjectToDrag.transform.position.x) + 1 && used[i].transform.position.y == (int)Mathf.Round(gameObjectToDrag.transform.position.y)) { // 1,0
                        gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
                    } else {
                        gameObjectToDrag.transform.position = new Vector3(Mathf.Round(gameObjectToDrag.transform.position.x), Mathf.Round(gameObjectToDrag.transform.position.y), GOCentre.z);
                    }
                }
                break;
            case 90:
                for (int i = 0; i < used.Length; i++) {
                    if (Mathf.Round(gameObjectToDrag.transform.position.x) < -4 || Mathf.Round(gameObjectToDrag.transform.position.x) > 5) { // if x coords go outside of grid
                        gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
                    } else if (Mathf.Round(gameObjectToDrag.transform.position.y) < used[used.Length-1].transform.position.y || Mathf.Round(gameObjectToDrag.transform.position.y) > used[0].transform.position.y) { // if y coords go outside the grid
                        gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
                    } else if (used[i].transform.position.x == (int)Mathf.Round(gameObjectToDrag.transform.position.x) && used[i].transform.position.y == (int)Mathf.Round(gameObjectToDrag.transform.position.y)) { // 0,0
                        gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
                    } else if (used[i].transform.position.x == (int)Mathf.Round(gameObjectToDrag.transform.position.x) && used[i].transform.position.y == (int)Mathf.Round(gameObjectToDrag.transform.position.y) + 1) { // 0,1
                        gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
                    } else {
                        gameObjectToDrag.transform.position = new Vector3(Mathf.Round(gameObjectToDrag.transform.position.x), Mathf.Round(gameObjectToDrag.transform.position.y), GOCentre.z);
                    }
                }
                break;
        }
        GameObject[] TwoOnBoard = new GameObject[2];
        TwoOnBoard[0] = GameObject.Find("Two");
        TwoOnBoard[1] = GameObject.Find("Two1");
        GameObject[] onBoard = new GameObject[11];
        onBoard[0] = GameObject.Find("T");
        onBoard[1] = GameObject.Find("T1");
        onBoard[2] = GameObject.Find("T2");
        onBoard[3] = GameObject.Find("T3");
        onBoard[4] = GameObject.Find("L");
        onBoard[5] = GameObject.Find("L1");
        onBoard[6] = GameObject.Find("L2");
        onBoard[7] = GameObject.Find("Line");
        onBoard[8] = GameObject.Find("Line1");
        onBoard[9] = GameObject.Find("Line2");
        onBoard[10] = GameObject.Find("Dot");
        for (int i = 0; i < TwoOnBoard.Length; i++) {
            for (int j = 0; j < onBoard.Length; j++) {
                if (Mathf.Round(TwoOnBoard[i].transform.position.x) == Mathf.Round(onBoard[j].transform.position.x) && Mathf.Round(TwoOnBoard[i].transform.position.y) == Mathf.Round(onBoard[j].transform.position.y)) {
                    gameObjectToDrag.transform.position = new Vector3(GOCentre.x, GOCentre.y, GOCentre.z);
                } else {
                    gameObjectToDrag.transform.position = new Vector3(Mathf.Round(gameObjectToDrag.transform.position.x), Mathf.Round(gameObjectToDrag.transform.position.y), GOCentre.z);
                }
            }
        }
    }
}
