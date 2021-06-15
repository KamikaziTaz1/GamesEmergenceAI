using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task2Randomizer : MonoBehaviour
{
    public int minWidth = 4;
    public int maxWidth = 16;

    public int minHeight = 3;
    public int maxHeight = 9;

    // Randomize the parameters for task 1
    void Awake() {
        Task2Generator generator = GetComponent<Task2Generator>();
        generator.gridWidth = Random.Range(minWidth, maxWidth + 1);
        generator.gridHeight = Random.Range(minHeight, maxHeight + 1);
    }
}
