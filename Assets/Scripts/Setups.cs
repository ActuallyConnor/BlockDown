using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setups {

    List<int[]> arr = new List<int[]>();

    public Setups() {
        arr.Add(new int[] { 12, 13, 14, 16, 17, 23, 25, 26, 32, 33, 34, 36, 37 });
    }

    public int[] GetSetup(int index) {
        return arr[index];
    }
    public List<int> GetWall(int index) {
        List<int> wall = new List<int>();
        for (int i = 0; i < 50; i++) {
            for (int j = 0; j < arr.Count; j++) {
                if (arr[index][j] != i) {
                    wall.Add(i);
                    Debug.Log(i);
                }
            }
        }
        return wall;
    }
}
