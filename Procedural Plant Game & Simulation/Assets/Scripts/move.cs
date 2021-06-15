using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    public Rigidbody rb;
    public float moveSpeed;

    public GameObject pl;

    public Growth growth;

    public LifeDrawer lifeDrawer;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 movement = new Vector3(horizontal, 0, vertical);
        rb.AddForce(movement * moveSpeed); 

       PlayerDrawLogic();
    }

    void PlayerDrawLogic(){
        RaycastHit hit;
        if(!Physics.Raycast(pl.transform.position, pl.transform.TransformDirection(Vector3.forward * 10), out hit)) return;
        
        Vector2 cellPos = ConvertCoordToWorldCell(hit.textureCoord);
        growth.DrawLife((int)cellPos.x, (int)cellPos.y);
        }

        Vector2 ConvertCoordToWorldCell(Vector2 coord){
            Vector2 toReturn = Vector2.zero;
            toReturn.x = Mathf.FloorToInt(coord.x * growth.width);
            toReturn.y = Mathf.FloorToInt(coord.y * growth.height);

            return toReturn;
        }
    }
