using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Growth : MonoBehaviour
{
    public float randFillPercent = 0.5f;

    public LifeDrawer lifeDrawer;
    public int width, height;

    public GameObject playerObj;

    Cell[,] worldCells;
    Cell[,] worldCellsNext;

    Cell[,] deadCells;

    Cell[,] deadCellsNext;
    private int size;
    private int sx, sy;
    public float ageLimit;

    public LayerMask CALayer;

    public bool isHitting = false;
    //look at previous CA Code
    //Every cell around the shadow is dead
    //Every cell within the shadow alternates between alive and dead and will change depending on where the shadow goes
    // Start is called before the first frame update
    void Start()
    {
        worldCells = new Cell[width, height];
        worldCellsNext = new Cell[width, height];
        sx = width;
        sy = height;
        size = width * height;

        for(int x = 0; x < sx; x++){
            for(int y = 0; y < sy; y++){
                worldCells[x, y] = new Cell(x, y, Cell.Type.shadow);
                worldCellsNext[x, y] = new Cell(x, y, Cell.Type.shadow);
            }
        }

        RandomFill(randFillPercent);

        lifeDrawer.Initialize(width, height, size, this);
    }
    //randomly put cells in

    void RandomFill(float fillAmt){
        for(int x = 0; x < sx; x++){
            for(int y = 0; y < sy; y++){
                float rand = Random.value;
                if(rand<fillAmt){
                    worldCells[x, y].alive = 1;
                    worldCells[x, y].cellType = Cell.Type.shadow;
                }
            }
        }
    }

    public float lifeUpdateTimer = 1f;
    private float timer = 0;
    // Update is called once per frame
    void Update()
    {
      if(timer <= 0 && isHitting == true){
          timer = lifeUpdateTimer;
          MainLifeLogic();
          lifeDrawer.DrawLife(worldCells);
      }else if(isHitting == false){
          timer = lifeUpdateTimer;
          SubLifeLogic();
          lifeDrawer.DrawLife(worldCells);
      }
      else{
          timer -= Time.deltaTime;
      }  
    }

    public void FixedUpdate()
    {
        RaycastHit hit;

        if(Physics.Raycast(playerObj.transform.position, playerObj.transform.TransformDirection(Vector3.forward * 10), out hit, Mathf.Infinity, CALayer)){
            Debug.DrawRay(playerObj.transform.position, playerObj.transform.TransformDirection(Vector3.forward * 10), Color.red);
            isHitting = true;
            Debug.Log("Did hit");
        }else if(!Physics.Raycast(playerObj.transform.position, playerObj.transform.TransformDirection(Vector3.forward * 10), out hit, Mathf.Infinity, CALayer)){
            isHitting = false;
            Debug.Log("Not Hitting");
        }
    }

    void MainLifeLogic()
    {
        for(int x = 0; x < sx; x++){
            for(int y = 0; y < sy; y++){
                if(worldCells[x, y].type == Cell.Type.shadow){
                    int count = 0;
                    count = neighbors(x, y);

                    if(worldCells[x, y].alive == 0){
                        //check if something can be born
                        if(count == 1 || count == 2 || count == 3){
                            worldCellsNext[x, y].alive = 1;
                        }else{

                        }
                    }else if(worldCells[x, y].alive == 1){
                        if(count == 4 || count == 5 || count == 6 || count == 7){
                            worldCellsNext[x, y].alive = 0;
                            worldCellsNext[x, y].age = 0;
                        }else{
                            worldCellsNext[x, y].age += 1;

                            if(worldCellsNext[x, y].age >= 10){
                                worldCellsNext[x, y].alive = 0;
                                worldCellsNext[x, y].age = 0;
                            }
                        }
                    }
                }
            }
        }
        for(int x = 0; x < sx; x++){
            for(int y = 0; y < sy; y++){
                worldCells[x, y].age = worldCellsNext[x, y].age;
                worldCells[x, y].alive = worldCellsNext[x, y].alive;
                worldCells[x, y].type = worldCellsNext[x, y].type;
            }
        }
    }
    int neighbors(int x, int y){
        return worldCells[(x + 1) % sx, y].alive +
		worldCells[x, (y + 1) % sy].alive +
		worldCells[(x + sx - 1) % sx, y].alive +
		worldCells[x, (y + sy - 1) % sy].alive +
		worldCells[(x + 1) % sx, (y + 1) % sy].alive +
		worldCells[(x + sx - 1) % sx, (y + 1) % sy].alive +
		worldCells[(x + sx - 1) % sx, (y + sy - 1) % sy].alive +
		worldCells[(x + 1) % sx, (y + sy - 1) % sy].alive;
    }

    void SubLifeLogic(){
        for(int x = 0; x < sx; x++){
            for(int y = 0; y < sy; y++){
                if(worldCells[x, y].type == Cell.Type.light){
                    int count = 0;
                    count = neighbors(x, y);

                    if(worldCells[x, y].alive == 0){
                        //check if something can be born
                        if(count == 2 || count == 3 || count == 4){
                            worldCellsNext[x, y].alive = 0;
                        }else{

                        }
                    }else if(worldCells[x, y].alive == 1){
                        if(count == 6 || count == 7 || count == 8 || count == 9){
                            worldCellsNext[x, y].alive = 0;
                            worldCellsNext[x, y].age = 0;
                        }else{
                            worldCellsNext[x, y].age += 0;

                            if(worldCellsNext[x, y].age >= 8){
                                worldCellsNext[x, y].alive = 0;
                                worldCellsNext[x, y].age = 0;
                            }
                        }
                    }
                }
            }
         }
         for(int x = 0; x < sx; x++){
            for(int y = 0; y < sy; y++){
                worldCells[x, y].age = worldCellsNext[x, y].age;
                worldCells[x, y].alive = worldCellsNext[x, y].alive;
                worldCells[x, y].type = worldCellsNext[x, y].type;
            }
        }
    }

    public void DrawLife(int x, int y){
        worldCellsNext[x, y].age = 0;
        worldCellsNext[x, y].alive = 1;
        worldCellsNext[x, y].cellType = Cell.Type.shadow;
    }

    public void DrawDeath(int x, int y){
        worldCellsNext[x, y].age = 0;
        worldCellsNext[x, y].alive = 1;
        worldCellsNext[x, y].cellType = Cell.Type.light;
    }

    public class Cell
    {
        public enum Type { shadow, light }
        public Type type;
        public int x, y;
        public int alive;

        public float cellAge;

        public Type cellType;

        public int age;

        public Cell(int _x, int _y, Type cellType){
            x = _x;
            y = _y;
            alive = 0;
            age = 0;
            type = cellType;
        }
    }
}
 