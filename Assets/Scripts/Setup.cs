using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setup {

    List<int> defaultBoard = new List<int>() {
        0, 1, 2, 3, 4, 5, 6, 7, 8, 9,
        10, 11, 12, 13, 14, 15, 16, 17, 18, 19,
        20, 21, 22, 23, 24, 25, 26, 27, 28, 29,
        30, 31, 32, 33, 34, 35, 36, 37, 38, 39,
        40, 41, 42, 43, 44, 45, 46, 47, 48, 49
    };
    List<int[]> presets = new List<int[]>();

    public Setup() {
        presets.Add(new int[] { 12, 13, 14, 16, 17, 23, 25, 26, 32, 33, 34, 36, 37 });
    }

    public int[] GetPreset(int index) => presets[index];

    public List<int> GetBoard(int index) {
        foreach (int i in presets[index]) {
            defaultBoard.Remove(i);
        }
        return defaultBoard;
    }
}

