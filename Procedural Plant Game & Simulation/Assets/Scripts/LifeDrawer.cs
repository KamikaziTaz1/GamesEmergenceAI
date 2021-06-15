using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeDrawer : MonoBehaviour
{
    public Growth growth;
    public int width, height, size;

    Color[] array_Main;
    Texture2D textureLife;
    MeshRenderer meshRenderer;

    public GameObject ShadowCreator;

    public Gradient lifeGrad;

    public Gradient deathGrad;

    public LayerMask CALayer;

    public float offset1 = -1;

    public float offset2;

    public float offset3;

    public float offset4;

    public float offset5;

    public bool isHitting = false;


    public void Initialize(int w, int h, int s, Growth _growth){
        width = w;
        height = h;
        growth = _growth;
        size = s;
        array_Main = new Color[size];

        meshRenderer = GetComponent<MeshRenderer>();

        for(int i = 0; i < width; i++){
            for(int j = 0; j < height; j++){
                array_Main[j*height + i]=new Color(1f, 0f, 0f);
            }
        }
        textureLife = new Texture2D(width, height);
        textureLife.filterMode = FilterMode.Point;

        meshRenderer.material.SetTexture("_MainTex", textureLife);
    }

    public void DrawLife(Growth.Cell[,] cells){
        for(int y = 0; y < height; y++){
            for(int x = 0; x < width; x++){
                if(cells[x, y].alive == 1){
                    float agePercent = (float)cells[x, y].age / 10f;
                    array_Main[x + (y*width)] = lifeGrad.Evaluate(agePercent);
                }else if(cells[x, y].alive == 0){
                    array_Main[x+(y*width)] = new Color(0f, 0f, 0f);
                }
            }
        }

        textureLife.SetPixels(array_Main);
        textureLife.Apply();
    }

    public void DrawDeath(Growth.Cell[,] cells){
        for(int y = 0; y < height; y++){
            for(int x = 0; x < width; x++){
                if(cells[x, y].alive == 1){
                    float agePercent = (float)cells[x, y].age / 10f;
                    array_Main[x + (y*width)] = lifeGrad.Evaluate(agePercent);
                }else if(cells[x, y].alive == 0){
                    array_Main[x+(y*width)] = new Color(0f, 0f, 0f);
                }
            }
        }
    }

    private float gizmoScale = 2f;

    private void OnDrawGizmos()
    {
        if(Application.isPlaying){
            Gizmos.color = Color.red;
            Vector3 direction = ShadowCreator.transform.TransformDirection(Vector3.forward * 10);
            Gizmos.DrawRay(ShadowCreator.transform.position, direction);
        }
    }

    public void FixedUpdate(){

    }
//alive & dead states
//look at previous life manager and life drawer
//if not in shadow, has to be dead, if in shadow, will steadily grow/will alternate between alive or dead
//we want to know the CA coordinates that the shadow is influencing.  
//Take an object and give it same rot as directional light so you know direction.  Use 6 raycasts for the 6 verticies
//copy rotation and raycast from the vector and use that as the bounds of the shadow and in there, alternate
//raycast from sphere, at point, grab cells a certain radius away from that point and do GOL
}
