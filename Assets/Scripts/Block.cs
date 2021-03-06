﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Block : MonoBehaviour {
 
    public bool snap = false;

    public int pieces = 0;
    List<Vector3> roundWall;
    List<Vector3> wall;
    public int count = 0;
    public GameObject[] spots;
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

        float gridY = GameObject.Find("Grid").transform.position.y;
        int wallCount = GameObject.Find("Grid").GetComponent<Wall>().count;
        int numberOfSpacesInWall = GameObject.Find("Grid").GetComponent<Wall>().setups.GetPreset(0).Length;
        int levelsPassed = GameObject.Find("Grid").GetComponent<Wall>().setups.GetPassed();
        Transform currentMovingObject = GameObject.Find(gameObjectToDrag.name).transform;

        int[] levelCheckpoints = { -1, 2, 7, 13, 19, 29, 39, 49, 59 };
        float[] speeds = { -1.5f, -1.4f, -1.2f, -1.1f, -1.0f, -1.1f, -1.2f, -1.3f, -1.4f };

        if (snap && gridY > 0 && wallCount < numberOfSpacesInWall) {
            for (int i = speeds.Length - 1; i > -1; i--) {
                if (levelsPassed > levelCheckpoints[i]) {
                    currentMovingObject.Translate(new Vector3(0, speeds[i], 0) * Time.deltaTime, Space.World);
                    break;
                }
            }
        }        
    }

    IEnumerator WinWait() {        
        yield return new WaitForSeconds(1);
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
        SnapPieceToClosestOpenSqareOrPutBack();
        Overlap();
        Placed();
        SinglePiecePutBack();
        SendToBottom();
        PlacingPieceWinsGame();
        Resize();
    }

    void SinglePiecePutBack() {
        if (pieces == 1 && gameObjectToDrag.transform.position.y <= -6 && GOCentre.y > -6) {
            PutBack();
        }
    }

    void PlacingPieceWinsGame() {
        if (GameObject.Find("Grid").GetComponent<Wall>().count >= GameObject.Find("Grid").GetComponent<Wall>().setups.GetPreset(0).Length) {
            GameObject.Find("Grid").GetComponent<Wall>().setups.SetPassed(GameObject.Find("Grid").GetComponent<Wall>().setups.GetPassed() + 1);
            foreach (GameObject go in stop) {
                go.transform.Translate(new Vector3(0, 0, 0));
                StartCoroutine("WinWait");
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
        float[,] positioning = { { 3f, 2f }, { 2f, 2f}, { 2f, 2f }, { 3f, 1f }, { 2f, 1f }, { 1f, 1f } };
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
            if (GameObject.Find("Grid").GetComponent<Wall>().count - pieces >= 0) {
                GameObject.Find("Grid").GetComponent<Wall>().count -= pieces;
            } else {
                GameObject.Find("Grid").GetComponent<Wall>().count = 0;
            }
        }        
    }

    void Placed() {
        if (snap == true && GOCentre.y <= -6) {
            GameObject.Find("Grid").GetComponent<Wall>().count += pieces;
            GameObject.Find("click").GetComponent<PlayClick>().PlayAudio();
        } else {
            GameObject.Find("woosh").GetComponent<PlayClick>().PlayAudio();
        }
    }


    private float a;
    private float b;
    private float c;

    private Vector3 closest;
    private Vector3 second;
    private Vector3 third;

    public List<Vector3> check = new List<Vector3>();

    void SnapPieceToClosestOpenSqareOrPutBack()
    {
        GenerateVirtualBoard();

        DetermineClosestBlock();

        gameObjectToDrag.transform.position = closest;
        HandleIfIsSinglePiece();

        PythagoreanOperations();
    }

    private void GenerateVirtualBoard()
    {
        spots = GameObject.FindGameObjectsWithTag("Blank");
        onBoard = GameObject.FindGameObjectsWithTag("Block");
        check.Clear();
        foreach (GameObject spot in spots)
        {
            check.Add(spot.transform.position);
        }
    }

    private void DetermineClosestBlock()
    {
        float least = 1000;
        fits = false;
        closest = TPos;
        second = TPos;
        third = TPos;
        foreach (Vector3 che in check)
        {
            a = gameObjectToDrag.transform.position.x - che.x;
            b = gameObjectToDrag.transform.position.y - che.y;
            a = Mathf.Pow(a, 2);
            b = Mathf.Pow(b, 2);
            c = a + b;
            c = Mathf.Sqrt(c);
            if (c < least && c < 1.5)
            {
                third = second;
                second = closest;
                closest = che;
                least = c;
            }
        }
    }

    private void HandleIfIsSinglePiece()
    {
        if (pieces == 1)
        {
            fits = true;
        } else if (pieces > 1)
        {
            for (int j = 1; j < 4; j++)
            {
                if (GameObject.Find(gameObjectToDrag.name + j) != null)
                {
                    if (check.Contains(GameObject.Find(gameObjectToDrag.name + j).transform.position))
                    {
                        fits = true;
                    }
                    else
                    {
                        fits = false;
                        snap = false;
                        break;
                    }
                }
            }
        }       
    }

    private void PythagoreanOperations()
    {
        if (!NthPythagorean(second))
        {
            if (!NthPythagorean(third))
            {
                LastPythagorean();
            }
        }     
    }

    private bool NthPythagorean(Vector3 nthSpot)
    {
        if (fits)
        {
            snap = true;
            return true;
        }
        else
        {
            gameObjectToDrag.transform.position = nthSpot; // first spot failed            
            LoopAndDetermineIfFitsAndSnaps();
            return false;
        }
    }    

    private void LastPythagorean()
    {
        if (fits)
        {
            snap = true;
        }
        else
        {
            PutBack(); // third spot failed
            fits = false;
            snap = false;
        }
    }

    private void LoopAndDetermineIfFitsAndSnaps() {
        if (pieces > 1)
        {
            for (int j = 1; j < 4; j++)
            {
                if (GameObject.Find(gameObjectToDrag.name + j) != null)
                {
                    if (check.Contains(GameObject.Find(gameObjectToDrag.name + j).transform.position))
                    {
                        fits = true;
                    }
                    else
                    {
                        fits = false;
                        snap = false;
                        break;
                    }
                }
            }
        }
    }

}
