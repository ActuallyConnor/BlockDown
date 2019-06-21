using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Setup {

    System.Random rand = new System.Random();

    List<int> defaultBoard = new List<int>() {
        0, 1, 2, 3, 4, 5, 6, 7, 8,
        10, 11, 12, 13, 14, 15, 16, 17, 18,
        20, 21, 22, 23, 24, 25, 26, 27, 28,
        30, 31, 32, 33, 34, 35, 36, 37, 38,
        40, 41, 42, 43, 44, 45, 46, 47, 48,
        50, 51, 52, 53, 54, 55, 56, 57, 58
    };
    List<int[]> presets = new List<int[]>();
    public static int passed = 0;
    public int num = 2;
    public int[] created;

    public Setup() {
        if (GetPassed() > 19) {
            created = Create(6);
        } else if (GetPassed() > 13) {
            created = Create(5);
        } else if (GetPassed() > 7) {
            created = Create(4);
        } else if (GetPassed() > 2) {
            created = Create(3);
        } else {
            created = Create(2);
        }        
        presets.Add(created);
    }

    public int[] GetPreset(int index) => presets[index];

    public List<int> GetBoard(int index) {
        foreach (int i in presets[index]) {
            defaultBoard.Remove(i);
        }
        return defaultBoard;
    }

    int[] Create(int p) {
        string[] toUse = RandGen();

        List<int> onBoard = new List<int>();
        switch (toUse[0]) {
            case "T":                
                onBoard.AddRange(TShape(23));
                break;
            case "L":
                onBoard.AddRange(LShape(23));
                break;
            case "Square":
                onBoard.AddRange(Square(23));
                break;
            case "Line":
                onBoard.AddRange(Line(23));
                break;
            case "Two":
                onBoard.AddRange(Two(23));
                break;
            case "Dot":
                onBoard.AddRange(Dot(23));
                break;
        }
        Console.WriteLine(toUse[0]);
        Console.WriteLine("First piece placed");
        onBoard = Place(onBoard, toUse[1]);
        if (p > 2) {
            onBoard = Place(onBoard, toUse[2]);
        }
        if (p > 3) {
            onBoard = Place(onBoard, toUse[3]);
        }
        if (p > 4) {
            onBoard = Place(onBoard, toUse[4]);
        }
        if (p > 5) {
            onBoard = Place(onBoard, toUse[5]);
        }
        return onBoard.ToArray();
    }

    List<int> Place(List<int> onBoard, string toUse) {
        List<int> placeChanges = new List<int>();
        List<int> temp = new List<int>();
        int oldp = 0;
        int newp = 0;
        int store = 0;
        bool placed = false;
        bool again = false;
        while (placed == false) {
            again = false;
            placeChanges.Add(1); placeChanges.Add(-1); placeChanges.Add(10); placeChanges.Add(-10);
            temp = onBoard.ToList();
            oldp = rand.Next(0, onBoard.Count);
            store = onBoard[oldp];
            newp = rand.Next(0, placeChanges.Count);
            store += placeChanges[newp];
            switch (toUse) {
                case "T":
                    temp.AddRange(TShape(store));
                    break;
                case "L":
                    temp.AddRange(LShape(store));
                    break;
                case "Square":
                    temp.AddRange(Square(store));
                    break;
                case "Line":
                    temp.AddRange(Line(store));
                    break;
                case "Two":
                    temp.AddRange(Two(store));
                    break;
                case "Dot":
                    temp.AddRange(Dot(store));
                    break;
            }
            foreach (int t in temp) {
                if (t > 58 || t < 0 || t == 9 || t == 19 || t == 29 || t == 39 || t == 49) {
                    again = true;
                }
            }
            if (temp.Distinct().Count() != temp.Count || again == true) { // first place didn't work
                again = false;
                store = 0;
                temp = onBoard.ToList();
                oldp = rand.Next(0, onBoard.Count);
                store = onBoard[oldp];
                newp = rand.Next(0, placeChanges.Count);
                store += placeChanges[newp];
                switch (toUse) {
                    case "T":
                        temp.AddRange(TShape(store));
                        break;
                    case "L":
                        temp.AddRange(LShape(store));
                        break;
                    case "Square":
                        temp.AddRange(Square(store));
                        break;
                    case "Line":
                        temp.AddRange(Line(store));
                        break;
                    case "Two":
                        temp.AddRange(Two(store));
                        break;
                    case "Dot":
                        temp.AddRange(Dot(store));
                        break;
                }
                foreach (int t in temp) {
                    if (t > 58 || t < 0 || t == 9 || t == 19 || t == 29 || t == 39 || t == 49) {
                        again = true;
                    }
                }
                if (temp.Distinct().Count() != temp.Count || again == true) { // second place didn't work
                    again = false;
                    store = 0;
                    temp = onBoard.ToList();
                    oldp = rand.Next(0, onBoard.Count);
                    store = onBoard[oldp];
                    newp = rand.Next(0, placeChanges.Count);
                    store += placeChanges[newp];
                    switch (toUse) {
                        case "T":
                            temp.AddRange(TShape(store));
                            break;
                        case "L":
                            temp.AddRange(LShape(store));
                            break;
                        case "Square":
                            temp.AddRange(Square(store));
                            break;
                        case "Line":
                            temp.AddRange(Line(store));
                            break;
                        case "Two":
                            temp.AddRange(Two(store));
                            break;
                        case "Dot":
                            temp.AddRange(Dot(store));
                            break;
                    }
                    foreach (int t in temp) {
                        if (t > 58 || t < 0 || t == 9 || t == 19 || t == 29 || t == 39 || t == 49) {
                            again = true;
                        }
                    }
                    if (temp.Distinct().Count() != temp.Count || again == true) { // third place didn't work
                        again = false;
                        store = 0;
                        temp = onBoard.ToList();
                        oldp = rand.Next(0, onBoard.Count);
                        store = onBoard[oldp];
                        newp = rand.Next(0, placeChanges.Count);
                        store += placeChanges[newp];
                        switch (toUse) {
                            case "T":
                                temp.AddRange(TShape(store));
                                break;
                            case "L":
                                temp.AddRange(LShape(store));
                                break;
                            case "Square":
                                temp.AddRange(Square(store));
                                break;
                            case "Line":
                                temp.AddRange(Line(store));
                                break;
                            case "Two":
                                temp.AddRange(Two(store));
                                break;
                            case "Dot":
                                temp.AddRange(Dot(store));
                                break;
                        }
                        foreach (int t in temp) {
                            if (t > 58 || t < 0 || t==9 || t==19 || t==29 || t==39 || t== 49) {
                                again = true;
                            }
                        }
                        if (temp.Distinct().Count() != temp.Count || again == true) { // fourth place didn't work
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
        Console.WriteLine(toUse);
        Console.WriteLine("Piece placed");
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