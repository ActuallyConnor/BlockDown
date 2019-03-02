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

    public Vector3[] tetris;
    public List<Vector3> roundWall;
    public List<Vector3> wall;

    public bool snap = false;

    public Vector3 TPos;

    // Start is called before the first frame update
    void Start() {
        TPos = GameObject.Find("T").transform.position;
    }

    // Update is called once per frame
    void Update() {
        if (snap && GameObject.Find("Grid").transform.position.y > 0) {
            GameObject.Find("T").transform.Translate(Vector3.down * Time.deltaTime, Space.World);
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
        TShape();
    }

    void TShape() {
        roundWall = FindObjectOfType<Wall>().GetRoundGrid();
        wall = FindObjectOfType<Wall>().GetGrid();
        tetris = new Vector3[] {
            new Vector3(Mathf.Round(GameObject.Find("T").transform.position.x), Mathf.Round(GameObject.Find("T").transform.position.y), 0),
            new Vector3(Mathf.Round(GameObject.Find("T1").transform.position.x), Mathf.Round(GameObject.Find("T1").transform.position.y), 0),
            new Vector3(Mathf.Round(GameObject.Find("T2").transform.position.x), Mathf.Round(GameObject.Find("T2").transform.position.y), 0),
            new Vector3(Mathf.Round(GameObject.Find("T3").transform.position.x), Mathf.Round(GameObject.Find("T3").transform.position.y), 0)
        };
        if (roundWall.Contains(tetris[0]) && roundWall.Contains(tetris[1]) && roundWall.Contains(tetris[2]) && roundWall.Contains(tetris[3])) {
            gameObjectToDrag.transform.position = wall[roundWall.IndexOf(tetris[0])];
            snap = true;
        } else {
            gameObjectToDrag.transform.position = TPos;
            snap = false;
        }
    }  
}
