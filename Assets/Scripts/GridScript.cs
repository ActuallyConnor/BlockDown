using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridScript : MonoBehaviour {

    public List<int[]> used = new List<int[]>();

    // Start is called before the first frame update
    void Start() {
        used.Add(new int[] { -3, 8 });
        used.Add(new int[] { -2, 8 });
        used.Add(new int[] { -1, 8 });
        used.Add(new int[] { 0, 8 });
        used.Add(new int[] { 1, 8 });
        used.Add(new int[] { 2, 8 });
        used.Add(new int[] { 3, 8 });
        used.Add(new int[] { -3, 7 });
        used.Add(new int[] { 1, 7 });
        used.Add(new int[] { 2, 7 });
        used.Add(new int[] { 3, 7 });
        used.Add(new int[] { 2, 6 });
        used.Add(new int[] { 3, 6 });
        used.Add(new int[] { 2, 5 });
        used.Add(new int[] { 3, 5 });
        used.Add(new int[] { -3, 4 });
        used.Add(new int[] { -2, 4 });
        used.Add(new int[] { -1, 4 });
        used.Add(new int[] { 0, 4 });
        used.Add(new int[] { -3, 3 });
        used.Add(new int[] { -2, 3 });
        used.Add(new int[] { -1, 3 });
        used.Add(new int[] { 2, 3 });
        used.Add(new int[] { -3, 2 });
        used.Add(new int[] { -1, 2 });
        used.Add(new int[] { 0, 2 });
        used.Add(new int[] { -3, 1 });
        used.Add(new int[] { 0, 1 });
        used.Add(new int[] { 1, 1 });
        used.Add(new int[] { 2, 1 });
        used.Add(new int[] { 3, 1 });
        used.Add(new int[] { -3, 0 });
        used.Add(new int[] { -2, 0 });
        used.Add(new int[] { 1, 0 });
        used.Add(new int[] { 2, 0 });
        used.Add(new int[] { 3, 0 });
    }

    // Update is called once per frame
    void Update() {
        
    }

    public List<int[]> GetUsedSpots() => used;
}
