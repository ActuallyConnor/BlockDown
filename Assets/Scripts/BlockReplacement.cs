using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class BlockReplacement : MonoBehaviour {

    public GameObject gameObjectToDrag;

    public Vector3 gameObjectCenter;
    public Vector3 touchPosition;
    public Vector3 touchPositionOffsetFromCenter;
    public Vector3 newGameObjectCenter;

    RaycastHit hitDetector;

    public bool isDraggable = false;
    public bool isSnappedIntoPlace = false;

    public Vector3 TPosition;
    public Vector3 LPosition;
    public Vector3 linePosition;
    public Vector3 twoPosition;
    public Vector3 dotPosition;
    public Vector3 squarePosition;

    public float sideA;
    public float sideB;
    public float sideC;
    public float least = 1000;
    public Vector3 closest;
    public Vector3 second;
    public Vector3 third;

    public int numberPiecesOnBoard = 0;

    public List<Vector3> approximatedWall;
    public List<Vector3> wall;

    public int count = 0;

    public GameObject[] stopDeterminer;

    public GameObject[] blankSquare;

    public List<Vector3> checkOverlaps = new List<Vector3>();

    public GameObject[] piecesOnBoard;

    public bool fitsInSpace;

    public double EPSILON = 4.94065645841247E-324;

    // Use this for initialization
    void Start() {
        PositionPiecesOnBoard();
    }

    // Update is called once per frame
    void Update() {

    }

    /*
     * To set all the initialplacements of the pieces
     */
    void PositionPiecesOnBoard() {
        TPosition = new Vector3((float)-0.5, -6, 0);
        LPosition = new Vector3(-3, -6, 0);
        squarePosition = new Vector3(3, -6, 0);
        linePosition = new Vector3(-3, (float)-8.5, 0);
        twoPosition = new Vector3(1, (float)-8.5, 0);
        dotPosition = new Vector3(4, (float)-8.5, 0);

        stopDeterminer = GameObject.FindGameObjectsWithTag("Block");
    }

    /*
     * To perform action when touch event begins
     */
    void OnMouseDown() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hitDetector)) {
            gameObjectToDrag = hitDetector.collider.gameObject;
            gameObjectCenter = gameObjectToDrag.transform.position;
            touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            touchPositionOffsetFromCenter = touchPosition - gameObjectCenter;
            isDraggable = true;
            SendToTopLayer();
        }
    }


    /*
     * To perform action when user is dragging piece
     */
    private void OnMouseDrag() {
        if (isDraggable) {
            touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            newGameObjectCenter = touchPosition - touchPositionOffsetFromCenter;
            gameObjectToDrag.transform.position = new Vector3(newGameObjectCenter.x, newGameObjectCenter.y, newGameObjectCenter.z);
        }
    }

    private void OnMouseUp() {
        isDraggable = false;
        Pythagorean();
        DoesItOverlap();
        Placed();
        SinglePiecePutBack();
        SendToBottomLayer();
        PlacedPieceWinsLevel();
        ResizeGameObjectOnDrop();
    }

    void SinglePiecePutBack() {
        if (numberPiecesOnBoard == 1 && gameObjectToDrag.transform.position.y <= -6 && gameObjectCenter.y > -6) {
            PutBackToPreviousPosition();
        }
    }

    void PlacedPieceWinsLevel() {
        if (GameObject.Find("Grid").GetComponent<Wall>().count >= GameObject.Find("Grid").GetComponent<Wall>().setups.GetPreset(0).Length) {
            GameObject.Find("Grid").GetComponent<Wall>().setups.SetPassed(GameObject.Find("Grid").GetComponent<Wall>().setups.GetPassed() + 1);
            foreach (GameObject go in stopDeterminer) {
                go.transform.Translate(new Vector3(0, 0, 0));
                StartCoroutine("WinWait");
            }
        }
    }

    void ResizeGameObjectOnDrop() {
        if (gameObjectToDrag.transform.position.y > -6) { 
            if (Math.Abs(gameObjectToDrag.GetComponent<BoxCollider>().size.x % 1 - 0.5) < EPSILON) { // due to comparing floating point numbers
                gameObjectToDrag.GetComponent<BoxCollider>().size = new Vector3(gameObjectToDrag.GetComponent<BoxCollider>().size.x - 0.5f, gameObjectToDrag.GetComponent<BoxCollider>().size.y - 0.5f, 0);
            }
        } else {
            if (Math.Abs(gameObjectToDrag.GetComponent<BoxCollider>().size.x % 1 - 0.0) < EPSILON) {
                gameObjectToDrag.GetComponent<BoxCollider>().size = new Vector3(gameObjectToDrag.GetComponent<BoxCollider>().size.x + 0.5f, gameObjectToDrag.GetComponent<BoxCollider>().size.y + 0.5f, 0);
            }
        }
    }

    void SendToTopLayer() {
        GameObject.Find(gameObjectToDrag.name).GetComponent<Renderer>().sortingLayerName = "Top";
        if (numberPiecesOnBoard > 1) {
            for (int i = 1; i < numberPiecesOnBoard; i++) {
                GameObject.Find(gameObjectToDrag.name + i).GetComponent<Renderer>().sortingLayerName = "Top";
            }
        }
    }

    void SendToBottomLayer() {
        GameObject.Find(gameObjectToDrag.name).GetComponent<Renderer>().sortingLayerName = "Blocks";
        if (numberPiecesOnBoard > 1) {
            for (int i = 1; i < numberPiecesOnBoard; i++) {
                GameObject.Find(gameObjectToDrag.name + i).GetComponent<Renderer>().sortingLayerName = "Blocks";
            }
        }
    }

    void ResetToOriginalPositions() {
        switch (gameObjectToDrag.name) {
            case "T":
                gameObjectToDrag.transform.position = TPosition;
                break;
            case "L":
                gameObjectToDrag.transform.position = LPosition;
                break;
            case "Line":
                gameObjectToDrag.transform.position = linePosition;
                break;
            case "Two":
                gameObjectToDrag.transform.position = twoPosition;
                break;
            case "Dot":
                gameObjectToDrag.transform.position = dotPosition;
                break;
            case "Square":
                gameObjectToDrag.transform.position = squarePosition;
                break;
        }
        isSnappedIntoPlace = false;
    }

    void DoesItOverlap() {

    }

    void PutBackToPreviousPosition() {

    }

    void Placed() {

    }

    void Pythagorean() {
        PreparePythagorean();
        DistanceToClosestEmptySpace();
    }

    void PreparePythagorean() {
        blankSquare = GameObject.FindGameObjectsWithTag("Blank");
        piecesOnBoard = GameObject.FindGameObjectsWithTag("Block");
        checkOverlaps.Clear();
        foreach (GameObject spot in blankSquare) {
            checkOverlaps.Add(spot.transform.position);
        }
        fitsInSpace = false;
        closest = TPosition;
        second = TPosition;
        third = TPosition;
    }

    void DistanceToClosestEmptySpace() {
        foreach (Vector3 check in checkOverlaps) {
            sideA = gameObjectToDrag.transform.position.x - check.x;
            sideB = gameObjectToDrag.transform.position.y - check.y;
            sideA = Mathf.Pow(sideA, 2);
            sideB = Mathf.Pow(sideB, 2);
            sideC = sideA + sideB;
            sideC = Mathf.Sqrt(sideC);
            IsSideCSmallestDistance(check);
        }
    }

    void IsSideCSmallestDistance(Vector3 checker) {
        if (sideC < least && sideC < 1.5) {
            third = second;
            second = closest;
            closest = checker;
            least = sideC;
        }
    }

    IEnumerator Wait(int waitTime) {
        yield return new WaitForSeconds(waitTime);

    }
}
