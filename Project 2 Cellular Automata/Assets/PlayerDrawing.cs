 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDrawing : MonoBehaviour
{
    public Camera cam;
    public LifeDrawer lifeDrawer;
    public LifeManager lifeManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerDrawLogic();
    }

    void PlayerDrawLogic(){
        if(Input.GetMouseButton(0)) return;

        RaycastHit hit;
        if(!Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit)) return;

        Vector2 cellPos = ConvertCoordToWorldCell(hit.textureCoord);
        lifeManager.DrawLife((int)cellPos.x, (int)cellPos.y); 
    }

    Vector2 ConvertCoordToWorldCell(Vector2 coord){
        Vector2 toReturn = Vector2.zero;
        toReturn.x = Mathf.FloorToInt(coord.x*lifeManager.width);
        toReturn.y = Mathf.FloorToInt(coord.y*lifeManager.height);

        return toReturn;
    }
}
//for next week, do some sand logics