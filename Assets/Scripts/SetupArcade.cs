using System;
using System.Collections.Generic;
using System.Linq;

public class SetupArcade {

    public Random rand = new Random();

    List<int> defaultBoard = new List<int> {
        0, 1, 2, 3, 4, 5, 6, 7, 8,
        10, 11, 12, 13, 14, 15, 16, 17, 18,
        20, 21, 22, 23, 24, 25, 26, 27, 28,
        30, 31, 32, 33, 34, 35, 36, 37, 38,
        40, 41, 42, 43, 44, 45, 46, 47, 48,
        50, 51, 52, 53, 54, 55, 56, 57, 58
    };

    List<int[]> presets = new List<int[]>();
    public static int passed;
    public int num = 2;
    public int[] created;

    public SetupArcade() {
        int[] checkpoints = { -1, 2, 7, 13, 19 };
        int[] createNumbers = { 2, 3, 4, 5, 6 };

        for (int i = checkpoints.Length - 1; i > -1; i--) {
            if (GetPassed() >= checkpoints[i]) {
                created = Create(6/*createNumbers[i]*/);
                break;
            }
        }

        //created = Create(6);

        presets.Add(created);
    }

    public int[] GetPreset(int index) {
        return presets[index];
    }

    public List<int> GetBoard(int index) {
        foreach (int i in presets[index]) {
            defaultBoard.Remove(i);
        }
        return defaultBoard;
    }

    int[] Create(int p) {
        string[] toUse = RandGen();
        List<int> onBoard = PlaceFirstPieceOnBoard(toUse[0]);
        onBoard = Place(onBoard, toUse[1]);
        for (int i = 5; i > 1; i--) {
            if (p > i) {
                onBoard = Place(onBoard, toUse[i]);
                break;
            }
        }
        return onBoard.ToArray();
    }

    List<int> PlaceFirstPieceOnBoard(string firstPiece) {
        List<int> onBoard = new List<int>();
        string[] shapeTypes = { "T", "L", "Square", "Line", "Two", "Dot" };
        int[][] shapeSetups = { TShape(23), LShape(23), Square(23), Line(23), Two(23), Dot(23) };
        for (int i = 0; i < shapeTypes.Length; i++) {
            if (string.Equals(firstPiece, shapeTypes[i])) {
                onBoard.AddRange(shapeSetups[i]);
                break;
            }
        }
        return onBoard;
    }

    List<int> CheckIfPieceFits(List<int> temp, int store, string toUse) {
        string[] shapeTypes = { "T", "L", "Square", "Line", "Two", "Dot" };
        int[][] shapeSetups = { TShape(store), LShape(store), Square(store), Line(store), Two(store), Dot(store) };
        for (int i = 0; i < shapeTypes.Length; i++) {
            if (string.Equals(toUse, shapeTypes[i])) {
                temp.AddRange(shapeSetups[i]);
                break;
            }
        }
        return temp;
    }

    bool PieceFitsInBoard(int k) {
        bool outside = k < 0 || k > 58;
        bool notAllowed = false;
        int[] outsideNumbers = { 9, 19, 29, 39, 49 };
        for (int i = 0; i < outsideNumbers.Length; i++) {
            if (k == outsideNumbers[i]) {
                notAllowed = true;
                break;
            }
        }
        return outside || notAllowed;
    }

    List<int> Place(List<int> onBoard, string toUse) {
        List<int> placeChanges = new List<int>();
        List<int> temp;
        int oldp;
        int newp;
        int store;
        bool placed = false;
        bool again;

        while (placed == false) {
            again = false;
            placeChanges.Add(1); placeChanges.Add(-1); placeChanges.Add(10); placeChanges.Add(-10);
            temp = onBoard.ToList();
            oldp = rand.Next(0, onBoard.Count);
            store = onBoard[oldp];
            newp = rand.Next(0, placeChanges.Count);
            store += placeChanges[newp];

            temp = CheckIfPieceFits(temp, store, toUse);

            foreach (int t in temp) {
                again = PieceFitsInBoard(t);
                if (again == true) {
                    break;
                }
            }

            if (temp.Distinct().Count() != temp.Count || again == true) {
                again = false;
                temp = onBoard.ToList();
                oldp = rand.Next(0, onBoard.Count);
                store = onBoard[oldp];
                newp = rand.Next(0, placeChanges.Count);
                store += placeChanges[newp];

                temp = CheckIfPieceFits(temp, store, toUse);

                foreach (int t in temp) {
                    again = PieceFitsInBoard(t);
                    if (again == true) {
                        break;
                    }
                }

                if (temp.Distinct().Count() != temp.Count || again == true) {
                    again = false;
                    temp = onBoard.ToList();
                    oldp = rand.Next(0, onBoard.Count);
                    store = onBoard[oldp];
                    newp = rand.Next(0, placeChanges.Count);
                    store += placeChanges[newp];

                    temp = CheckIfPieceFits(temp, store, toUse);

                    foreach (int t in temp) {
                        again = PieceFitsInBoard(t);
                        if (again == true) {
                            break;
                        }
                    }
                    if (temp.Distinct().Count() != temp.Count || again == true) {
                        again = false;
                        temp = onBoard.ToList();
                        oldp = rand.Next(0, onBoard.Count);
                        store = onBoard[oldp];
                        newp = rand.Next(0, placeChanges.Count);
                        store += placeChanges[newp];

                        temp = CheckIfPieceFits(temp, store, toUse);

                        foreach (int t in temp) {
                            again = PieceFitsInBoard(t);
                            if (again == true) {
                                break;
                            }
                        }

                        if (temp.Distinct().Count() != temp.Count || again == true) {
                            again = false;
                            placed = false;
                        } else {
                            onBoard = temp.ToList();
                            placed = true;
                        }
                    } else {
                        onBoard = temp.ToList();
                        placed = true;
                    }
                } else {
                    onBoard = temp.ToList();
                    placed = true;
                }
            } else {
                onBoard = temp.ToList();
                placed = true;
            }
        }
        return onBoard.ToList();
    }

    int[] TShape(int num) {
        return new int[] {
                num, num+1, num+2, num+11
            };
    }
    int[] LShape(int num) {
        return new int[] {
                num, num+10, num+11
            };
    }
    int[] Square(int num) {
        return new int[] {
                num, num+1, num+10, num+11
            };
    }
    int[] Line(int num) {
        return new int[] {
                num, num+1, num+2
            };
    }
    int[] Two(int num) {
        return new int[] {
                num, num+1
            };
    }
    int[] Dot(int num) {
        return new int[] {
                num
            };
    }

    string[] RandGen() {
        List<string> toUse = new List<string>() {
                "T", "L", "Square", "Line", "Two", "Dot"
            };
        string[] used = new string[6];
        for (int i = 0; i < 6; i++) {
            used[i] = toUse[rand.Next(0, toUse.Count)];
            toUse.Remove(used[i]);
        }
        Console.WriteLine("Rand Generated");
        return used;
    }

    public int GetPassed() {
        return passed;
    }
    public void SetPassed(int n) => passed = n;
}