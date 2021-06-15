using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Task1Generator : MonoBehaviour {

    public const float TILE_SIZE = 2f;

    public GameObject playerPrefab;
    public GameObject wallPrefab;

    // These are randomized by the task randomizer
    public int gridWidth = 20;
    public int gridHeight = 10;

    public int numExtraWalls = 2;

    bool[,] _wallGrid;

    void Start() {
        _wallGrid = new bool[gridWidth - 1, gridHeight - 1];

        fillGrid();
        spawnWalls();
        spawnPlayer();

        // Move ourself based on our width and height so we're centered
        float offsetX = gridWidth * TILE_SIZE / 2f - TILE_SIZE/2f;
        float offsetY = gridHeight * TILE_SIZE / 2f - TILE_SIZE/2f;
        transform.position -= new Vector3(offsetX, offsetY, 0);
    }

    void fillGrid() {
		// Your code for 1-a goes here:
		// Reminder, you want to spawn walls all along the border
		// (i.e. grid positions <0,0>, <0,1>,...,<0,gridHeight-1> would all have walls (and so on for the other sides))
		// Additionally, don't forget to spawn extra walls according to numExtraWalls
        _wallGrid[gridWidth-1, gridHeight-1] = true;

        for(int x = 0; x < gridWidth; x++){
            for(int y = 0; y < gridHeight; y++){
                _wallGrid[0, y] = true;
                _wallGrid[x, 0] = true;
                _wallGrid[x, gridWidth-1, y] = true;
                _wallGrid[x, gridHeight-1] = true;
            }
            for(int i = 0; i < numExtraWalls; i++){
                _wallGrid[Random.Range(0, gridWidth - 2), Random.Range(0, gridHeight - 2)] = true;
            }
       }
    }

    Vector2Int getPlayerSpawnPos() {
        // Your code for 1-b here:
        
        // Default implementation: You'll want to replace this
        return new Vector2Int(Random.Range(1, gridWidth-1), Random.Range(1, gridHeight - 1));
    }




    void spawnWalls() {
        // Spawns walls at the locations provided by _wallGrid
        for (int x = 0; x < gridWidth; x++) {
            for (int y = 0; y < gridHeight; y++) {
                if (_wallGrid[x, y]) {
                    GameObject wallObj = Instantiate(wallPrefab);
                    wallObj.transform.parent = transform;
                    wallObj.transform.localPosition = new Vector3(x * TILE_SIZE, y * TILE_SIZE, 0);
                }
            }
        }
    }

    void spawnPlayer() {
        // Spawns the player at the location provided by getPlayerSpawnPos
        Vector2Int playerPos = getPlayerSpawnPos();
        GameObject playerObj = Instantiate(playerPrefab);
        playerObj.transform.parent = transform;
        playerObj.transform.localPosition = new Vector3(playerPos.x * TILE_SIZE, playerPos.y * TILE_SIZE, 0);
    }




    void Update() {
        if (Input.GetKeyDown(KeyCode.R)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
