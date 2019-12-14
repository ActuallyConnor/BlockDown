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
				Debug.Log(currentPiecePosition.x + horizontal + ", " + currentPiecePosition.y + vertical);
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
		Pieces piecePositions = new Pieces(selectedObject.name, selectedObject.transform.position.x, selectedObject.transform.position.y);
		float[,] pieceCoordinates = piecePositions.pieceCoordinates;

		float[,] blankCoordinates = GetBlankSpacesAsArray();

		bool pieceFitsHere = true;

		for (int i = 0; i < pieceCoordinates.GetLength(0); i++) {
			if (pieceFitsHere) {
				for (int j = 0; j < blankCoordinates.GetLength(0); j++) {
					if (AreFloatsEqual(pieceCoordinates[i, 0], blankCoordinates[j, 0]) && AreFloatsEqual(pieceCoordinates[i, 1], blankCoordinates[j, 1])) {
						pieceFitsHere = false;
					}
				}
			}
		}

		return pieceFitsHere;
	}

	float[,] GetBlankSpacesAsArray() {
		GameObject[] blanks = GameObject.FindGameObjectsWithTag("Blank");
		float[,] blankCoordinates = new float[17, 2];
		int nCount = 0;
		foreach (GameObject blank in blanks) {
			blankCoordinates[nCount, 0] = blank.transform.position.x;
			blankCoordinates[nCount, 1] = blank.transform.position.x;
			nCount++;
		}
		return blankCoordinates;
	}

	bool AreFloatsEqual(float a, float b) {
		return Math.Abs(a - b) < double.Epsilon;
	}

}
