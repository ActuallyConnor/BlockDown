using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour {

    public GameObject gameObjectToDrag;
    public Vector3 gameObjectCenter;
    public Vector3 touchPosition;
    public Vector3 offset;
    public Vector3 newGameObjectCenter;
    RaycastHit hitCollider;
    public bool isDraggable;
    public bool snapsInPlace;
    public Vector3 TPos;
    public Vector3 LPos;
    public Vector3 LinePos;
    public Vector3 TwoPos;
    public Vector3 DotPos;
    public Vector3 SquarePos;
    public int pieces;
    List<Vector3> wallRoundedToNearestInt;
    List<Vector3> wall;
    public int count;
    public GameObject[] stopDeterminer;
    public GameObject[] blankSquares;
    public List<Vector3> overlapCheck = new List<Vector3>();
    public GameObject[] onBoard;
    bool fits;
    public float sideA;
    public float sideB;
    public float sideC;
    public float least = 1000;
    public Vector3 closest;
    public Vector3 second;
    public Vector3 third;

    // Start is called before the first frame update
    void Start() {
        PlacePiecesOnBoard();
    }

    // Update is called once per frame
    void Update() {
        ChangeWallSpeed();
    }

    void ChangeWallSpeed() {
        if (snapsInPlace && GameObject.Find("Grid").transform.position.y > 0 && GameObject.Find("Grid").GetComponent<Wall>().count < GameObject.Find("Grid").GetComponent<Wall>().setups.GetPreset(0).Length) {
            if (GameObject.Find("Grid").GetComponent<Wall>().setups.GetPassed() > 59) {
                SetWallSpeed(-1.4f);
            } else if (GameObject.Find("Grid").GetComponent<Wall>().setups.GetPassed() > 49) {
                SetWallSpeed(-1.3f);
            } else if (GameObject.Find("Grid").GetComponent<Wall>().setups.GetPassed() > 39) {
                SetWallSpeed(-1.2f);
            } else if (GameObject.Find("Grid").GetComponent<Wall>().setups.GetPassed() > 29) {
                SetWallSpeed(-1.1f);
            } else if (GameObject.Find("Grid").GetComponent<Wall>().setups.GetPassed() > 19) {
                SetWallSpeed(-1.0f);
            } else if (GameObject.Find("Grid").GetComponent<Wall>().setups.GetPassed() > 13) {
                SetWallSpeed(-1.1f);
            } else if (GameObject.Find("Grid").GetComponent<Wall>().setups.GetPassed() > 7) {
                SetWallSpeed(-1.3f);
            } else if (GameObject.Find("Grid").GetComponent<Wall>().setups.GetPassed() > 2) {
                SetWallSpeed(-1.4f);
            } else {
                SetWallSpeed(-1.5f);
            }
        }
    }

    void SetWallSpeed(float wallSpeed) {
        GameObject.Find(gameObjectToDrag.name).transform.Translate(new Vector3(0, wallSpeed, 0) * Time.deltaTime, Space.World);

    }

    void PlacePiecesOnBoard() {
        TPos = new Vector3((float)-0.5, -6, 0);
        LPos = new Vector3(-3, -6, 0);
        SquarePos = new Vector3(3, -6, 0);
        LinePos = new Vector3(-3, (float)-8.5, 0);
        TwoPos = new Vector3(1, (float)-8.5, 0);
        DotPos = new Vector3(4, (float)-8.5, 0);

        stopDeterminer = GameObject.FindGameObjectsWithTag("Block");
    }

    IEnumerator Wait() {
        yield return new WaitForSeconds(1);

    }

    void OnMouseDown() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hitCollider)) {
            gameObjectToDrag = hitCollider.collider.gameObject;
            gameObjectCenter = gameObjectToDrag.transform.position;
            touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            offset = touchPosition - gameObjectCenter;
            isDraggable = true;
            SendToTopLayer();
        }
    }

    void OnMouseDrag() {
        if (isDraggable) {
            touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            newGameObjectCenter = touchPosition - offset;
            gameObjectToDrag.transform.position = new Vector3(newGameObjectCenter.x, newGameObjectCenter.y, gameObjectCenter.z);
        }
    }

    void OnMouseUp() {
        isDraggable = false;
        Pythagorean();
        Overlap();
        Placed();
        SinglePiecePutBack();
        SendToBottomLayer();
        DoesPlacingPieceWin();
        Resize();
    }

    void SinglePiecePutBack() {
        if (pieces == 1 && gameObjectToDrag.transform.position.y <= -6 && gameObjectCenter.y > -6) {
            PutBack();
        }
    }

    void DoesPlacingPieceWin() {
        if (GameObject.Find("Grid").GetComponent<Wall>().count >= GameObject.Find("Grid").GetComponent<Wall>().setups.GetPreset(0).Length) {
            GameObject.Find("Grid").GetComponent<Wall>().setups.SetPassed(GameObject.Find("Grid").GetComponent<Wall>().setups.GetPassed() + 1);
            foreach (GameObject go in stopDeterminer) {
                go.transform.Translate(new Vector3(0, 0, 0));
                StartCoroutine("WinWait");
            }
        }
    }


    public const double EPSILON = 4.94065645841247E-324;

    void Resize() {
        if (gameObjectToDrag.transform.position.y > -6) {
            if (Math.Abs(gameObjectToDrag.GetComponent<BoxCollider>().size.x % 1 - 0.5) < EPSILON) {
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
        if (pieces > 1) {
            for (int i = 1; i < pieces; i++) {
                GameObject.Find(gameObjectToDrag.name + i).GetComponent<Renderer>().sortingLayerName = "Top";
            }
        }
    }

    void SendToBottomLayer() {
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
        snapsInPlace = false;
    }

    void Overlap() {
        string[] shapes = { "T", "L", "Line", "Two", "Dot", "Square" };
        List<Vector3> piece = new List<Vector3>();
        CheckIfPiecesOverlap(piece, shapes);
        KeepOrRemovePiece(piece);
    }

    void CheckIfPiecesOverlap(List<Vector3> piece, string[] shapes) {
        for (int i = 0; i < shapes.Length; i++) {
            string shape = shapes[i];
            if (Equals(shape, gameObjectToDrag.name)) {
                TemporarilyAddPieceToBoard(piece, shape);
            } else {
                CreateTemporaryPiece(shape);
            }
        }
    }

    void TemporarilyAddPieceToBoard(List<Vector3> piece, string shape) {
        piece.Add(GameObject.Find(shape).transform.position);
        for (int i = 1; i < pieces; i++) {
            piece.Add(GameObject.Find(shape + i).transform.position);
        }
    }

    void CreateTemporaryPiece(string shape) {
        new List<Vector3>().Add(GameObject.Find(shape).transform.position);
        for (int i = 1; i < 5; i++) {
            if (GameObject.Find(shape + i) != null) {
                new List<Vector3>().Add(GameObject.Find(shape + i).transform.position);
            }
        }
    }

    void KeepOrRemovePiece(List<Vector3> piece) {
        for (int i = 0; i < piece.Count; i++) {
            for (int j = 0; j < new List<Vector3>().Count; j++) {
                if (piece[i] == new List<Vector3>()[j]) {
                    Reset();
                }
            }
        }
    }

    void PutBack() {
        Reset();
        snapsInPlace = false;
        fits = false;
        DecreaseWallPiecesCount();
    }

    void DecreaseWallPiecesCount() {
        if (gameObjectCenter.y > -6 && gameObjectToDrag.transform.position.y <= -6 && !(gameObjectCenter.y <= -6 && gameObjectToDrag.transform.position.y <= -6)) {
            if (GameObject.Find("Grid").GetComponent<Wall>().count - pieces >= 0) {
                GameObject.Find("Grid").GetComponent<Wall>().count -= pieces;
            } else {
                GameObject.Find("Grid").GetComponent<Wall>().count = 0;
            }
        }
    }

    void Placed() {
        if (snapsInPlace == true && gameObjectCenter.y <= -6) {
            GameObject.Find("Grid").GetComponent<Wall>().count += pieces;
            GameObject.Find("click").GetComponent<PlayClick>().PlayAudio();
        } else {
            GameObject.Find("woosh").GetComponent<PlayClick>().PlayAudio();
        }
    }

    void Pythagorean() {
        SetupPythagorean();
        ApplyPythagorean();
        OnePiecePythagorean();
        TryAllPlaces();
    }

    void SetupPythagorean() {
        blankSquares = GameObject.FindGameObjectsWithTag("Blank");
        onBoard = GameObject.FindGameObjectsWithTag("Block");
        overlapCheck.Clear();
        foreach (GameObject spot in blankSquares) {
            overlapCheck.Add(spot.transform.position);
        }
        fits = false;
        closest = TPos;
        second = TPos;
        third = TPos;
    }

    void ApplyPythagorean() {
        foreach (Vector3 check in overlapCheck) {
            sideA = gameObjectToDrag.transform.position.x - check.x;
            sideB = gameObjectToDrag.transform.position.y - check.y;
            sideA = Mathf.Pow(sideA, 2);
            sideB = Mathf.Pow(sideB, 2);
            sideC = sideA + sideB;
            sideC = Mathf.Sqrt(sideC);
            IsCTheShortest(check);
        }
    }

    void IsCTheShortest(Vector3 check) {
        if (sideC < least && sideC < 1.5) {
            third = second;
            second = closest;
            closest = check;
            least = sideC;
        }
    }

    void OnePiecePythagorean() {
        gameObjectToDrag.transform.position = closest;
        if (pieces > 1) {
            PositionPieceCheckIfFits();
        } else fits |= pieces == 1;
    }

    void PositionPieceCheckIfFits() {
        for (int j = 1; j < 4; j++) {
            if (GameObject.Find(gameObjectToDrag.name + j) != null) {
                if (overlapCheck.Contains(GameObject.Find(gameObjectToDrag.name + j).transform.position)) {
                    fits = true;
                } else {
                    fits = false;
                    snapsInPlace = false;
                    break;
                }
            }
        }
    }

    void SpotFailed(Vector3 tryNumberVector) {
        gameObjectToDrag.transform.position = tryNumberVector;
        if (pieces > 1) {
            for (int j = 1; j < 4; j++) {
                if (GameObject.Find(gameObjectToDrag.name + j) != null) {
                    if (overlapCheck.Contains(GameObject.Find(gameObjectToDrag.name + j).transform.position)) {
                        fits = true;
                    } else {
                        fits = false;
                        snapsInPlace = false;
                        break;
                    }
                }
            }
        }
    }

    void TryAllPlaces() {
        if (fits == true) {
            snapsInPlace = true;
        } else {
            SpotFailed(second);
            if (fits == true) {
                snapsInPlace = true;
            } else {
                SpotFailed(third);
                if (fits == true) {
                    snapsInPlace = true;
                } else {
                    PutBack(); // third spot failed
                    fits = false;
                    snapsInPlace = false;
                }
            }
        }
    }
}
