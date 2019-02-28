using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridScript {

    public List<int[]> occupied;

    public GridScript() {
        occupied = new List<int[]> {
            new int[] { -3, 8 },
            new int[] { -2, 8 },
            new int[] { -1, 8 },
            new int[] { 0, 8 },
            new int[] { 1, 8 },
            new int[] { 2, 8 },
            new int[] { 3, 8 },
            new int[] { -3, 7 },
            new int[] { 1, 7 },
            new int[] { 2, 7 },
            new int[] { 3, 7 },
            new int[] { 2, 6 },
            new int[] { 3, 6 },
            new int[] { 2, 5 },
            new int[] { 3, 5 },
            new int[] { -3, 4 },
            new int[] { -2, 4 },
            new int[] { -1, 4 },
            new int[] { 0, 4 },
            new int[] { -3, 3 },
            new int[] { -2, 3 },
            new int[] { -1, 3 },
            new int[] { 2, 3 },
            new int[] { -3, 2 },
            new int[] { -1, 2 },
            new int[] { 0, 2 },
            new int[] { -3, 1 },
            new int[] { 0, 1 },
            new int[] { 1, 1 },
            new int[] { 2, 1 },
            new int[] { 3, 1 },
            new int[] { -3, 0 },
            new int[] { -2, 0 },
            new int[] { 1, 0 },
            new int[] { 2, 0 },
            new int[] { 3, 0 }
        };
    }

    public List<int[]> GetUsedSpots() => occupied;

    public void MoveSpot(int[] old, int[] move) {
        occupied.Remove(old);
        occupied.Add(move);
    }

    public void AddPiece(string piece, int rotate, int x, int y) {
        switch (piece) {
            case "T":
                switch (rotate) {
                    case 0:
                        occupied.Add(new int[] { x, y });
                        occupied.Add(new int[] { x + 1, y });
                        occupied.Add(new int[] { x + 2, y });
                        occupied.Add(new int[] { x + 1, y - 1 });
                        break;
                    case 90:
                        occupied.Add(new int[] { x, y });
                        occupied.Add(new int[] { x, y + 1 });
                        occupied.Add(new int[] { x, y + 2 });
                        occupied.Add(new int[] { x + 1, y + 1 });
                        break;
                    case 180:
                        occupied.Add(new int[] { x, y });
                        occupied.Add(new int[] { x - 1, y });
                        occupied.Add(new int[] { x - 2, y });
                        occupied.Add(new int[] { x - 1, y + 1 });
                        break;
                    case 270:
                        occupied.Add(new int[] { x, y });
                        occupied.Add(new int[] { x, y - 1 });
                        occupied.Add(new int[] { x, y - 2 });
                        occupied.Add(new int[] { x - 1, y - 1 });
                        break;
                }
                break;
            case "L":
                switch (rotate) {
                    case 0:
                        break;
                    case 90:
                        break;
                    case 180:
                        break;
                    case 270:
                        break;
                }
                break;
            case "Line":
                switch (rotate) {
                    case 0:
                        break;
                    case 90:
                        break;
                }
                break;
            case "Two":
                switch (rotate) {
                    case 0:
                        break;
                    case 90:
                        break;
                }
                break;
        }
    }

    public void RemovePiece(string piece, int rotate, int x, int y) {
        List<int[]> used = new List<int[]>();
        switch(piece) {
            case "T":
                switch(rotate) {
                    case 0:
                        used.Add(new int[] { x, y });
                        used.Add(new int[] { x + 1, y });
                        used.Add(new int[] { x + 2, y });
                        used.Add(new int[] { x + 1, y - 1 });
                        occupied.Remove(used[0]);
                        occupied.Remove(used[1]);
                        occupied.Remove(used[2]);
                        occupied.Remove(used[3]);
                        break;
                    case 90:
                        used.Add(new int[] { x, y });
                        used.Add(new int[] { x, y + 1 });
                        used.Add(new int[] { x, y + 2 });
                        used.Add(new int[] { x + 1, y + 1 });
                        occupied.Remove(used[0]);
                        occupied.Remove(used[1]);
                        occupied.Remove(used[2]);
                        occupied.Remove(used[3]);
                        break;
                    case 180:
                        used.Add(new int[] { x, y });
                        used.Add(new int[] { x - 1, y });
                        used.Add(new int[] { x - 2, y });
                        used.Add(new int[] { x - 1, y + 1 });
                        occupied.Remove(used[0]);
                        occupied.Remove(used[1]);
                        occupied.Remove(used[2]);
                        occupied.Remove(used[3]);
                        break;
                    case 270:
                        used.Add(new int[] { x, y });
                        used.Add(new int[] { x, y - 1 });
                        used.Add(new int[] { x, y - 2 });
                        used.Add(new int[] { x - 1, y - 1 });
                        occupied.Remove(used[0]);
                        occupied.Remove(used[1]);
                        occupied.Remove(used[2]);
                        occupied.Remove(used[3]);
                        break;
                }
                break;
            case "L":
                switch (rotate) {
                    case 0:
                        break;
                    case 90:
                        break;
                    case 180:
                        break;
                    case 270:
                        break;
                }
                break;
            case "Line":
                switch (rotate) {
                    case 0:
                        break;
                    case 90:
                        break;
                }
                break;
            case "Two":
                switch (rotate) {
                    case 0:
                        break;
                    case 90:
                        break;
                }
                break;
        }
    }
}
