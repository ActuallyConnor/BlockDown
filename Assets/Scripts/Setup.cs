﻿using System.Collections;
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
        /*------------------------------Single Pieces--------------------------------*/
        presets.Add(new int[] { 0, 1, 2, 11 });
        presets.Add(new int[] { 0, 1, 10, 11 });
        presets.Add(new int[] { 0, 1, 2 });
        presets.Add(new int[] { 0, 1 });
        presets.Add(new int[] { 0, 10, 11 });
        presets.Add(new int[] { 0 });
        presets.Add(new int[] { 1, 2, 3, 12 });
        presets.Add(new int[] { 1, 2, 11, 12 });
        presets.Add(new int[] { 1, 2, 3 });
        presets.Add(new int[] { 1, 2 });
        presets.Add(new int[] { 1, 11, 12 });
        presets.Add(new int[] { 1 });
        presets.Add(new int[] { 2, 3, 4, 13 });
        presets.Add(new int[] { 2, 3, 12, 13 });
        presets.Add(new int[] { 2, 3, 4 });
        presets.Add(new int[] { 2, 3 });
        presets.Add(new int[] { 2, 12, 13 });
        presets.Add(new int[] { 2 });
        presets.Add(new int[] { 3, 4, 5, 14 });
        presets.Add(new int[] { 3, 4, 13, 14 });
        presets.Add(new int[] { 3, 4, 5 });
        presets.Add(new int[] { 3, 4 });
        presets.Add(new int[] { 3, 13, 14 });
        presets.Add(new int[] { 3 });
        presets.Add(new int[] { 4, 5, 6, 15 });
        presets.Add(new int[] { 4, 5, 14, 15 });
        presets.Add(new int[] { 4, 5, 6 });
        presets.Add(new int[] { 4, 5 });
        presets.Add(new int[] { 4, 14, 15 });
        presets.Add(new int[] { 4 });
        presets.Add(new int[] { 5, 6, 7, 16 });
        presets.Add(new int[] { 5, 6, 15, 16 });
        presets.Add(new int[] { 5, 6, 7 });
        presets.Add(new int[] { 5, 6 });
        presets.Add(new int[] { 5, 15, 16 });
        presets.Add(new int[] { 5 });
        presets.Add(new int[] { 6, 7, 8, 17 });
        presets.Add(new int[] { 6, 7, 16, 17 });
        presets.Add(new int[] { 6, 7, 8 });
        presets.Add(new int[] { 6, 7 });
        presets.Add(new int[] { 6, 16, 17 });
        presets.Add(new int[] { 6 });
        presets.Add(new int[] { 7, 8, 9, 18 });
        presets.Add(new int[] { 7, 8, 17, 18 });
        presets.Add(new int[] { 7, 8, 9 });
        presets.Add(new int[] { 7, 8 });
        presets.Add(new int[] { 7, 17, 18 });
        presets.Add(new int[] { 7 });
        presets.Add(new int[] { 8, 9, 18, 19 });
        presets.Add(new int[] { 8, 9 });
        presets.Add(new int[] { 8, 18, 19 });
        presets.Add(new int[] { 8 });
        presets.Add(new int[] { 9, 10, 11 });
        presets.Add(new int[] { 9 });
        presets.Add(new int[] { 10, 11, 12, 21 });
        presets.Add(new int[] { 10, 11, 20, 21 });
        presets.Add(new int[] { 10, 11, 12 });
        presets.Add(new int[] { 10, 11 });
        presets.Add(new int[] { 10, 20, 21 });
        presets.Add(new int[] { 10 });
        presets.Add(new int[] { 11, 12, 13, 22 });
        presets.Add(new int[] { 11, 12, 21, 22 });
        presets.Add(new int[] { 11, 12, 13 });
        presets.Add(new int[] { 11, 12 });
        presets.Add(new int[] { 11, 21, 22 });
        presets.Add(new int[] { 11 });
        presets.Add(new int[] { 12, 13, 14, 23 });
        presets.Add(new int[] { 12, 13, 22, 23 });
        presets.Add(new int[] { 12, 13, 14 });
        presets.Add(new int[] { 12, 13 });
        presets.Add(new int[] { 12, 22, 23 });
        presets.Add(new int[] { 12 });
        presets.Add(new int[] { 13, 14, 15, 24 });
        presets.Add(new int[] { 13, 14, 23, 24 });
        presets.Add(new int[] { 13, 14, 15 });
        presets.Add(new int[] { 13, 14 });
        presets.Add(new int[] { 13, 23, 24 });
        presets.Add(new int[] { 13 });
        presets.Add(new int[] { 14, 15, 16, 25 });
        presets.Add(new int[] { 14, 15, 24, 25 });
        presets.Add(new int[] { 14, 15, 16 });
        presets.Add(new int[] { 14, 15 });
        presets.Add(new int[] { 14, 24, 25 });
        presets.Add(new int[] { 14 });
        presets.Add(new int[] { 15, 16, 17, 26 });
        presets.Add(new int[] { 15, 16, 25, 26 });
        presets.Add(new int[] { 15, 16, 17 });
        presets.Add(new int[] { 15, 16 });
        presets.Add(new int[] { 15, 25, 26 });
        presets.Add(new int[] { 15 });
        presets.Add(new int[] { 16, 17, 18, 27 });
        presets.Add(new int[] { 16, 17, 26, 27 });
        presets.Add(new int[] { 16, 17, 18 });
        presets.Add(new int[] { 16, 17 });
        presets.Add(new int[] { 16, 26, 27 });
        presets.Add(new int[] { 16 });
        presets.Add(new int[] { 17, 18, 19, 28 });
        presets.Add(new int[] { 17, 18, 27, 28 });
        presets.Add(new int[] { 17, 18, 19 });
        presets.Add(new int[] { 17, 18 });
        presets.Add(new int[] { 17, 27, 28 });
        presets.Add(new int[] { 17 });
        presets.Add(new int[] { 18, 19, 28, 29 });
        presets.Add(new int[] { 18, 19 });
        presets.Add(new int[] { 18, 28, 29 });
        presets.Add(new int[] { 18 });
        presets.Add(new int[] { 19, 20, 21 });
        presets.Add(new int[] { 19 });
        presets.Add(new int[] { 20, 21, 22, 31 });
        presets.Add(new int[] { 20, 21, 30, 31 });
        presets.Add(new int[] { 20, 21, 22 });
        presets.Add(new int[] { 20, 21 });
        presets.Add(new int[] { 20, 30, 31 });
        presets.Add(new int[] { 20 });
        presets.Add(new int[] { 21, 22, 23, 32 });
        presets.Add(new int[] { 21, 22, 31, 32 });
        presets.Add(new int[] { 21, 22, 23 });
        presets.Add(new int[] { 21, 22 });
        presets.Add(new int[] { 21, 31, 32 });
        presets.Add(new int[] { 21 });
        presets.Add(new int[] { 22, 23, 24, 33 });
        presets.Add(new int[] { 22, 23, 32, 33 });
        presets.Add(new int[] { 22, 23, 24 });
        presets.Add(new int[] { 22, 23 });
        presets.Add(new int[] { 22, 32, 33 });
        presets.Add(new int[] { 22 });
        presets.Add(new int[] { 23, 24, 25, 34 });
        presets.Add(new int[] { 23, 24, 33, 34 });
        presets.Add(new int[] { 23, 24, 25 });
        presets.Add(new int[] { 23, 24 });
        presets.Add(new int[] { 23, 33, 34 });
        presets.Add(new int[] { 23 });
        presets.Add(new int[] { 24, 25, 26, 35 });
        presets.Add(new int[] { 24, 25, 34, 35 });
        presets.Add(new int[] { 24, 25, 26 });
        presets.Add(new int[] { 24, 25 });
        presets.Add(new int[] { 24, 34, 35 });
        presets.Add(new int[] { 24 });
        presets.Add(new int[] { 25, 26, 27, 36 });
        presets.Add(new int[] { 25, 26, 35, 36 });
        presets.Add(new int[] { 25, 26, 27 });
        presets.Add(new int[] { 25, 26 });
        presets.Add(new int[] { 25, 35, 36 });
        presets.Add(new int[] { 25 });
        presets.Add(new int[] { 26, 27, 28, 37 });
        presets.Add(new int[] { 26, 27, 36, 37 });
        presets.Add(new int[] { 26, 27, 28 });
        presets.Add(new int[] { 26, 27 });
        presets.Add(new int[] { 26, 36, 37 });
        presets.Add(new int[] { 26 });
        presets.Add(new int[] { 27, 28, 29, 38 });
        presets.Add(new int[] { 27, 28, 37, 38 });
        presets.Add(new int[] { 27, 28, 29 });
        presets.Add(new int[] { 27, 28 });
        presets.Add(new int[] { 27, 37, 38 });
        presets.Add(new int[] { 27 });
        presets.Add(new int[] { 28, 29, 38, 39 });
        presets.Add(new int[] { 28, 29 });
        presets.Add(new int[] { 28, 38, 39 });
        presets.Add(new int[] { 28 });
        presets.Add(new int[] { 29, 30, 31 });
        presets.Add(new int[] { 29 });
        presets.Add(new int[] { 30, 31, 32, 41 });
        presets.Add(new int[] { 30, 31, 40, 41 });
        presets.Add(new int[] { 30, 31, 32 });
        presets.Add(new int[] { 30, 31 });
        presets.Add(new int[] { 30, 40, 41 });
        presets.Add(new int[] { 30 });
        presets.Add(new int[] { 31, 32, 33, 42 });
        presets.Add(new int[] { 31, 32, 41, 42 });
        presets.Add(new int[] { 31, 32, 33 });
        presets.Add(new int[] { 31, 32 });
        presets.Add(new int[] { 31, 41, 42 });
        presets.Add(new int[] { 31 });
        presets.Add(new int[] { 32, 33, 34, 43 });
        presets.Add(new int[] { 32, 33, 42, 43 });
        presets.Add(new int[] { 32, 33, 34 });
        presets.Add(new int[] { 32, 33 });
        presets.Add(new int[] { 32, 42, 43 });
        presets.Add(new int[] { 32 });
        presets.Add(new int[] { 33, 34, 35, 44 });
        presets.Add(new int[] { 33, 34, 43, 44 });
        presets.Add(new int[] { 33, 34, 35 });
        presets.Add(new int[] { 33, 34 });
        presets.Add(new int[] { 33, 43, 44 });
        presets.Add(new int[] { 33 });
        presets.Add(new int[] { 34, 35, 36, 45 });
        presets.Add(new int[] { 34, 35, 44, 45 });
        presets.Add(new int[] { 34, 35, 36 });
        presets.Add(new int[] { 34, 35 });
        presets.Add(new int[] { 34, 44, 45 });
        presets.Add(new int[] { 34 });
        presets.Add(new int[] { 35, 36, 37, 46 });
        presets.Add(new int[] { 35, 36, 45, 46 });
        presets.Add(new int[] { 35, 36, 37 });
        presets.Add(new int[] { 35, 36 });
        presets.Add(new int[] { 35, 45, 46 });
        presets.Add(new int[] { 35 });
        presets.Add(new int[] { 36, 37, 38, 47 });
        presets.Add(new int[] { 36, 37, 46, 47 });
        presets.Add(new int[] { 36, 37, 38 });
        presets.Add(new int[] { 36, 37 });
        presets.Add(new int[] { 36, 46, 47 });
        presets.Add(new int[] { 36 });
        presets.Add(new int[] { 37, 38, 39, 48 });
        presets.Add(new int[] { 37, 38, 47, 48 });
        presets.Add(new int[] { 37, 38, 39 });
        presets.Add(new int[] { 37, 38 });
        presets.Add(new int[] { 37, 47, 48 });
        presets.Add(new int[] { 37 });
        presets.Add(new int[] { 38, 39, 48, 49 });
        presets.Add(new int[] { 38, 39 });
        presets.Add(new int[] { 38, 48, 49 });
        presets.Add(new int[] { 38 });
        presets.Add(new int[] { 39, 40, 41 });
        presets.Add(new int[] { 39 });
        presets.Add(new int[] { 40, 41, 42 });
        presets.Add(new int[] { 40, 41 });
        presets.Add(new int[] { 40 });
        presets.Add(new int[] { 41, 42, 43 });
        presets.Add(new int[] { 41, 42 });
        presets.Add(new int[] { 41 });
        presets.Add(new int[] { 42, 43, 44 });
        presets.Add(new int[] { 42, 43 });
        presets.Add(new int[] { 42 });
        presets.Add(new int[] { 43, 44, 45 });
        presets.Add(new int[] { 43, 44 });
        presets.Add(new int[] { 43 });
        presets.Add(new int[] { 44, 45, 46 });
        presets.Add(new int[] { 44, 45 });
        presets.Add(new int[] { 44 });
        presets.Add(new int[] { 45, 46, 47 });
        presets.Add(new int[] { 45, 46 });
        presets.Add(new int[] { 45 });
        presets.Add(new int[] { 46, 47, 48 });
        presets.Add(new int[] { 46, 47 });
        presets.Add(new int[] { 46 });
        presets.Add(new int[] { 47, 48, 49 });
        presets.Add(new int[] { 47, 48 });
        presets.Add(new int[] { 47 });
        presets.Add(new int[] { 48, 49 });
        presets.Add(new int[] { 48 });
        presets.Add(new int[] { 49, 50, 51 });
        presets.Add(new int[] { 49 });

    }

    public int[] GetPreset(int index) => presets[index];

    public List<int> GetBoard(int index) {
        foreach (int i in presets[index]) {
            defaultBoard.Remove(i);
        }
        return defaultBoard;
    }
}

