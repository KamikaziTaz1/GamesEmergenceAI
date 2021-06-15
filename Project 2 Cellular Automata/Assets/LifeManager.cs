using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeManager : MonoBehaviour
{

    public float randFillPercent = 0.5f;

    public LifeDrawer lifeDraw;
    public int width, height;

    Cell[,] worldCells;
    Cell[,] worldCellsNext;
    private int size;
    private int sx, sy; //internal x & y size of world
    public float ageLimit;
    //in charge of cellular automata but life is just an array
    // Start is called before the first frame update
    void Start()
    {
        //set up initial variables
      worldCells = new Cell[width, height];
      worldCellsNext = new Cell[width, height];
      sx = width;
      sy = height;
      size = width * height;

        //initialize world
      for(int x = 0; x < sx; x++){
          for(int y = 0; y < sy; y++){
              worldCells[x, y] = new Cell(x, y, Cell.Type.GOL);
              worldCellsNext[x, y] = new Cell(x, y, Cell.Type.GOL);
          }
      }

        //put random cells in
        RandomFill(randFillPercent);

      lifeDraw.Initialize(width, height, size, this);
    }

    void RandomFill(float fillAmt){
        for(int x = 0; x < sx; x++){
          for(int y = 0; y < sy; y++){
              float rand = Random.value;
              if(rand<fillAmt){
                  worldCells[x, y].alive = 1;
                  worldCells[x, y].cellType = Cell.Type.GOL;
                  worldCells[x, y].cellType = Cell.Type.Sand;
              }
          }
      }
    }

    public float lifeUpdateTimer = 1f;
    private float timer = 0;

    // Update is called once per frame
    void Update()
    {
        if(timer <= 0){
        timer = lifeUpdateTimer;
        MainLifeLogic();
        lifeDraw.DrawLife(worldCells);
        }else{
            timer -= Time.deltaTime;
        }
    }

    void MainLifeLogic()//go through and do life logic
    {
        //go through all cells

        for(int x = 0; x < sx; x++){
          for(int y = 0; y < sy; y++){
              //life logic
              if(worldCells[x, y].type == Cell.Type.GOL){
              //count alive neighbors
              int count = 0;
              count = neighbors(x, y);
            

              if(worldCells[x, y].alive == 0){
                  //check if something can be born
                  if(count == 1 || count == 2 || count == 3){//am dead, how many neighbors trigger a birth
                    worldCellsNext[x,y].alive = 1;//come alive
                  }else{
                      //am dead and stayed dead
                  }
              }else if(worldCells[x, y].alive == 1){
                  //check if cell should die
                  if(count == 4|| count == 5 || count == 6 || count == 7){//am alive, how many neighbors trigger me dying
                    worldCellsNext[x, y].alive = 0;//die
                    worldCellsNext[x, y].age = 0;
                  }else{
                      //am alive and stayed alive
                      worldCellsNext[x, y].age += 1;

                      if(worldCellsNext[x, y].age >= 10){
                          worldCellsNext[x, y].alive = 0;
                          worldCellsNext[x, y].age = 0;
                      }
                  }
              }
              //falling sand logic
              }else if(worldCells[x, y].type == Cell.Type.Sand){
                  if(worldCellsNext[x, y].y > sy / 3 && worldCellsNext[x, y].x > sx /3){
                      if(worldCells[x, y].alive == 1){
                          if(worldCells[x, y - 1].alive == 0){
                              worldCellsNext[x, y].alive = 0;
                              worldCellsNext[x, y-1].alive = 1;
                              worldCellsNext[x, y-1].cellType = Cell.Type.Sand;
                          }
                          else if (worldCells[x, y-1].alive == 1){
                              if(worldCells[x + 1, y - 1].alive == 1 && worldCells[x-1, y-1].alive == 1){
                                  worldCellsNext[x, y].alive = 1;
                              }else if (worldCells[x + 1, y - 1].alive == 1 && worldCells[x - 1, y - 1].alive == 0){
                                  worldCellsNext[x, y].alive = 0;
                                  worldCellsNext[x-1, y-1].alive = 1;
                              }else if(worldCells[x-1, y-1].alive == 1 && worldCells[x+1, y-1].alive == 0){
                                  worldCellsNext[x, y].alive = 0;
                                  worldCellsNext[x + 1, y - 1].alive = 1;
                              }else if (worldCells[x+1, y-1].alive == 0 && worldCells[x-1, y-1].alive == 0){
                                  float chance = Random.value;

                                  if(chance < .5f){
                                      worldCellsNext[x, y].alive = 0;
                                      worldCellsNext[x+1, y-1].alive = 1;
                                  }else if(chance >= .5f){
                                      worldCellsNext[x, y].alive = 0;
                                      worldCellsNext[x-1, y-1].alive = 1;
                                  }
                              }
                          }
                        }
                    }
                    else if (worldCellsNext[x, y].y - 1 <= sy / 3 || worldCellsNext[x, y].x <= sx/4){
                        worldCellsNext[x, y].alive = 1;
                    }
              //sand logic

                }
            }
        }
    //set current life equal to next one
        for(int x = 0; x < sx; x++){
          for(int y = 0; y < sy; y++){
              worldCells[x, y].age = worldCellsNext[x, y].age;
              worldCells[x, y].alive = worldCellsNext[x, y].alive;
              worldCells[x, y].type = worldCellsNext[x, y].type;
          }
        }
    }

    int neighbors(int x, int y){ //bundles up for loop into 8 lines and adding up the alive status
    //modulo ensures that whenever it goes past the edge, it checks the first column
        return worldCells[(x + 1) % sx, y].alive +
		worldCells[x, (y + 1) % sy].alive +
		worldCells[(x + sx - 1) % sx, y].alive +
		worldCells[x, (y + sy - 1) % sy].alive +
		worldCells[(x + 1) % sx, (y + 1) % sy].alive +
		worldCells[(x + sx - 1) % sx, (y + 1) % sy].alive +
		worldCells[(x + sx - 1) % sx, (y + sy - 1) % sy].alive +
		worldCells[(x + 1) % sx, (y + sy - 1) % sy].alive;
    }

    public void DrawLife(int x, int y){
        worldCellsNext[x, y].age = 0;
        worldCellsNext[x, y].alive = 1;
        worldCellsNext[x, y].cellType = Cell.Type.Sand;
    }

    public class Cell
    {
        public enum Type { GOL, Sand, Wall}

        public Type type;

        public int x, y; //coordinates of cell
        public int alive; //is the cell alive?
        public float cellAge;
        public Type cellType;
        //other properties?
        public int age;

        public Cell(int _x, int _y, Type cellType){
            x = _x;
            y = _y;
            alive = 0;
            age = 0;
            type = cellType;
            //if we have other properties, initialize them here
        }
    }
}
