using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour {

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

    // Start is called before the first frame update
    void Start() {
        TPos = new Vector3((float)-0.5, -6, 0);
        LPos = new Vector3(-3, -6, 0);
        SquarePos = new Vector3(3, -6, 0);

        LinePos = new Vector3((float)-3.5, -9, 0);
        TwoPos = new Vector3((float)0.5, -9, 0);
        DotPos = new Vector3((float)3.5, (float)-9, 0);

        Debug.Log(GameObject.Find("Grid").GetComponent<Wall>().count);

    }

    // Update is called once per frame
    void Update() {
        if (snap && GameObject.Find("Grid").transform.position.y > 0) {
            GameObject.Find(gameObjectToDrag.name).transform.Translate(new Vector3(0, (float)-0.7, 0) * Time.deltaTime, Space.World);
        }
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
            case "Square":
                gameObjectToDrag.transform.position = SquarePos;
                break;
        }
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
                        GameObject.Find("Grid").GetComponent<Wall>().count = GameObject.Find("Grid").GetComponent<Wall>().count + 1;
                        Debug.Log(GameObject.Find("Grid").GetComponent<Wall>().count);
                    } else {
                        PutBack();
                    }
                    break;
                case 2:
                    if (roundWall.Contains(shape[0]) && roundWall.Contains(shape[1])) {
                        gameObjectToDrag.transform.position = wall[roundWall.IndexOf(shape[0])];
                        snap = true;
                        GameObject.Find("Grid").GetComponent<Wall>().count = GameObject.Find("Grid").GetComponent<Wall>().count + 2;
                        Debug.Log(GameObject.Find("Grid").GetComponent<Wall>().count);

                } else {
                        PutBack();
                    }
                    break;
                case 3:
                    if (roundWall.Contains(shape[0]) && roundWall.Contains(shape[1]) && roundWall.Contains(shape[2])) {
                        gameObjectToDrag.transform.position = wall[roundWall.IndexOf(shape[0])];
                        snap = true;
                        GameObject.Find("Grid").GetComponent<Wall>().count = GameObject.Find("Grid").GetComponent<Wall>().count + 3;
                        Debug.Log(GameObject.Find("Grid").GetComponent<Wall>().count);
                } else {
                        PutBack();
                    }
                    break;
                case 4:
                    if (roundWall.Contains(shape[0]) && roundWall.Contains(shape[1]) && roundWall.Contains(shape[2]) && roundWall.Contains(shape[3])) {
                        gameObjectToDrag.transform.position = wall[roundWall.IndexOf(shape[0])];
                        snap = true;
                        GameObject.Find("Grid").GetComponent<Wall>().count = GameObject.Find("Grid").GetComponent<Wall>().count + 4;
                        Debug.Log(GameObject.Find("Grid").GetComponent<Wall>().count);
                } else {
                        PutBack();
                    }
                    break;
        }
    }

    void PutBack() {
        Reset();
        snap = false;
        if (GOCentre.y > -6 && gameObjectToDrag.transform.position.y <= -6) {
            if (GameObject.Find("Grid").GetComponent<Wall>().count - pieces >= 0) {
                GameObject.Find("Grid").GetComponent<Wall>().count = GameObject.Find("Grid").GetComponent<Wall>().count - pieces;
                Debug.Log(GameObject.Find("Grid").GetComponent<Wall>().count);
            } else {
                GameObject.Find("Grid").GetComponent<Wall>().count = 0;
                Debug.Log(GameObject.Find("Grid").GetComponent<Wall>().count);
            }
        }        
    }
}
