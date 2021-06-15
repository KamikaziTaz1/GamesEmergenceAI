using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task3Generator : MonoBehaviour
{
   public const float TILE_SIZE = 2f;

    public GameObject playerPrefab;
    public GameObject wallPrefab;

    public GameObject otherThingPrefab;

    // These are randomized by the task randomizer
    public int gridWidth = 20;
    public int gridHeight = 10;

    bool[,] _wallGrid;
    bool[,] _nextWallGrid;

    bool[,] _otherThingGrid;


    void Start() {
        _wallGrid = new bool[gridWidth, gridHeight];
        _nextWallGrid = new bool[gridWidth, gridHeight];
        _otherThing = new bool [gridWidth, gridHeight];

        fillGrid();
        spawnWalls();
        spawnPlayer();

        // Move ourself based on our width
        float offsetX = gridWidth * TILE_SIZE / 2f - TILE_SIZE / 2f;
        float offsetY = gridHeight * TILE_SIZE / 2f - TILE_SIZE / 2f;
        transform.position -= new Vector3(offsetX, offsetY, 0);
    }

    void fillGrid() {
        // Your code for 2-a here:

        // Feel free to fill the grid with random walls. 
        // Keep in mind that this will be the starting point for the CA steps.
        float chanceToStartAlive = 0.45f;

       bool[][] initializeGrid(bool[,] _nextWallGrid);
        for(int x=0; x<gridWidth; x++){
            for(int y=0; y<gridHeight; y++){
                if(Random() < chanceToStartAlive){
                    _nextWallGrid[x][y] =true;
                }
            }
        }
        return _nextWallGrid;

        return new Vector2Int(gridWidth, gridHeight);
    }

    Vector2Int getPlayerSpawnPos() {
        // Your code for 2-a here:

        // Default implementation: You'll want to replace this
        return new Vector2Int(gridWidth, gridHeight);
    }

    bool nextCAValue(int x, int y) {
        // Your code for 2-b here:
        // A common way to approach CA for dungeon/cave generation is to 
        // count the number of wall neighbors to a given point and decide
        // based on that number if a wall should appear or remain in the given point.
        // Note that _wallGrid contains the current wall grid and shouldn't be modified until the end of the CA step.
        bool[][] doStimulationStep(boolean[] _wallGrid){
            bool[][] _nextWallGrid = new boolean[gridWidth][gridHeight];
            for(int x=0; x<_wallGrid.length; x++){
                int nbs = countAliveNeighbors(_wallGrid, x, y);
            }
            if(_wallGrid[x][y]){
                if(nbs < deathLimit){
                    newMap[x][y] = false;
                }
                else{
                    newMap[x][y] = true;
                }
            }
            else{
                if(nbs > birthLimit){
                    newMap[x][y] = true;
                }
                else{
                    newMap[x][y] = false;
                }
            }
        }
        // Default implementation: you'll want to replace this
        return Random.value <= 0.5f;
    }

    void performCAStep() {
        // How a CA step is performed.

        // First, we create the next grid by using nextCAValue for each grid location.
        for (int x = 0; x < gridWidth; x++) {
            for (int y = 0; y < gridHeight; y++) {
                _nextWallGrid[x, y] = nextCAValue(x, y);
            }
        }

        // Then we update the current grid to match _nextWallGrid
        for (int x = 0; x < gridWidth; x++) {
            for (int y = 0; y < gridHeight; y++) {
                _wallGrid[x, y] = _nextWallGrid[x, y];
            }
        }


    }



    void respawnWalls() {
        foreach (Transform child in transform) {
            Destroy(child.gameObject);
        }
        spawnWalls();
        spawnPlayer();
    }

    void spawnOtherThing(){
        for (int x = 0; x < gridWidth; x++) {
            for (int y = 0; y < gridHeight; y++) {
                if (_wallGrid[x, y]) {
                    GameObject otherObj = Instantiate(otherThingPrefab);
                    otherObj.transform.parent = transform;
                    otherObj.transform.localPosition = new Vector3(x * TILE_SIZE, y * TILE_SIZE, 0);
                }
            }
        } 
    }

    void spawnWalls() {
        // Spawns the walls according to _wallGrid
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
        // Spawns the player at the position provided by getPlayerSpawnPos
        Vector2Int playerPos = getPlayerSpawnPos();
        GameObject playerObj = Instantiate(playerPrefab);
        playerObj.transform.parent = transform;
        playerObj.transform.localPosition = new Vector3(playerPos.x * TILE_SIZE, playerPos.y * TILE_SIZE, 0);
    }




    void Update() {
        if (Input.GetKeyDown(KeyCode.R)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else if (Input.GetKeyDown(KeyCode.Space)) {
            performCAStep();
            respawnWalls();
        }
    }
}
