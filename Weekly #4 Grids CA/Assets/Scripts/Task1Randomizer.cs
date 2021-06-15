using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task1Randomizer : MonoBehaviour {

    public int minWidth = 4;
    public int maxWidth = 16;

    public int minHeight = 3;
    public int maxHeight = 9;

    public float minNumExtraDensity = 0.2f;
    public float maxNumExtraDensity = 0.5f;

    // Randomize the parameters for task 1
    void Awake() {
        Task1Generator generator = GetComponent<Task1Generator>();
        generator.gridWidth = Random.Range(minWidth, maxWidth + 1);
        generator.gridHeight = Random.Range(minHeight, maxHeight + 1);

        float extraDensity = Random.Range(minNumExtraDensity, maxNumExtraDensity);
        generator.numExtraWalls = Mathf.FloorToInt(extraDensity * generator.gridWidth * generator.gridHeight);
    }

}
