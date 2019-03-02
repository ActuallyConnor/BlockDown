using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour {

    public GameObject gameObjectToDrag; // refer to GO that is being dragged
    public Vector3 GOCentre;
    public Vector3 touchPosition; // touch or click position
    public Vector3 offset; // vector between touchpoint and object centre
    public Vector3 newGOCentre; // new centre of GO after move
    RaycastHit hit; // store hit object info
    public bool draggingMode = false; // is draggable at that moment in time    
    public bool snap = false;
    public Vector3 TPos;
    public Vector3 LPos;
    public Vector3 LinePos;
    public Vector3 TwoPos;
    public Vector3 DotPos;
    public int pieces = 0;
    List<Vector3> roundWall;
    List<Vector3> wall;

    // Start is called before the first frame update
    void Start() {
        TPos = new Vector3((float)-0.5, -6, 0);
        LPos = new Vector3(-3, -6, 0);
        DotPos = new Vector3((float)3.5, (float)-6.5, 0);
        LinePos = new Vector3((float)-2.5, -9, 0);
        TwoPos = new Vector3((float)1.5, -9, 0);
    }

    // Update is called once per frame
    void Update() {
        //roundWall = FindObjectOfType<Wall>().GetRoundGrid();
        //wall = FindObjectOfType<Wall>().GetGrid();
        if (snap && GameObject.Find("Grid").transform.position.y > 0) {
            GameObject.Find(gameObjectToDrag.name).transform.Translate(Vector3.down * Time.deltaTime, Space.World);
        }
    }

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
        Shape();
        Overlap();
        SendToBottom();
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
        }
    }

    void Overlap() {
        string[] shapes = new string[] { "T", "L", "Line", "Two", "Dot" };
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
    
    void Shape() {
        roundWall = FindObjectOfType<Wall>().GetRoundGrid();
        wall = FindObjectOfType<Wall>().GetGrid();
        List<Vector3> shape = new List<Vector3> {
            new Vector3(Mathf.Round(GameObject.Find(gameObjectToDrag.name).transform.position.x), Mathf.Round(GameObject.Find(gameObjectToDrag.name).transform.position.y), 0)
        };
        if (pieces > 1) {
            for (int i = 1; i < 5; i++) {
                if (GameObject.Find(gameObjectToDrag.name + i) != null) {
                    shape.Add(new Vector3(Mathf.Round(GameObject.Find(gameObjectToDrag.name + i).transform.position.x), Mathf.Round(GameObject.Find(gameObjectToDrag.name + i).transform.position.y), 0));
                }
            }
        }
        switch (pieces) {
            case 1:
                if (roundWall.Contains(shape[0])) {
                    gameObjectToDrag.transform.position = wall[roundWall.IndexOf(shape[0])];
                    snap = true;
                } else {
                    Reset();
                    snap = false;
                }
                break;
            case 2:
                if (roundWall.Contains(shape[0]) && roundWall.Contains(shape[1])) {
                    gameObjectToDrag.transform.position = wall[roundWall.IndexOf(shape[0])];
                    snap = true;
                } else {
                    Reset();
                    snap = false;
                }
                break;
            case 3:
                if (roundWall.Contains(shape[0]) && roundWall.Contains(shape[1]) && roundWall.Contains(shape[2])) {
                    gameObjectToDrag.transform.position = wall[roundWall.IndexOf(shape[0])];
                    snap = true;
                } else {
                    Reset();
                    snap = false;
                }
                break;
            case 4:
                if (roundWall.Contains(shape[0]) && roundWall.Contains(shape[1]) && roundWall.Contains(shape[2]) && roundWall.Contains(shape[3])) {
                    gameObjectToDrag.transform.position = wall[roundWall.IndexOf(shape[0])];
                    snap = true;
                } else {
                    Reset();
                    snap = false;
                }
                break;
        }
    }
}
