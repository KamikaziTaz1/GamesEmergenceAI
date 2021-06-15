using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeDrawer : MonoBehaviour
{
    public LifeManager lifeManager;
    public int width, height, size;

    Color[] array_Main;
    Texture2D textureLife;
    MeshRenderer meshrend;

    public Gradient lifeGradient;
    //going to read from life manager and output onto the material as a texture
    // Start is called before the first frame update
    public void Initialize(int w, int h, int s, LifeManager _life)
    {
        width = w;
        height = h;
        lifeManager = _life;
        size = s;
        array_Main = new Color[size];

        meshrend = GetComponent<MeshRenderer>();

        //initialize array with blank values
        for(int i = 0; i < width; i++){
            for(int j = 0; j < height; j++){
                array_Main[j*height + i] = new Color(1f, 0f, 0f);
            }
        }
        //create tex
        textureLife = new Texture2D(width, height);
        textureLife.filterMode = FilterMode.Point;

        meshrend.material.SetTexture("_MainTex", textureLife);
    }
    //passes into world of cells, adn converts to pixels
    public void DrawLife(LifeManager.Cell[,] cells){
        for(int y = 0; y < height; y++){
            for(int x = 0; x < width; x++){
                if(cells[x, y].alive == 1){
                    float agePercent = (float)cells[x, y].age / 10f;
                    array_Main[x + (y*width)] = lifeGradient.Evaluate(agePercent);
                }else if(cells[x, y].alive == 0){
                    array_Main[x+(y*width)] = new Color(0f, 0f, 0f);
                }
            }
    }

    textureLife.SetPixels(array_Main);
    textureLife.Apply();
    }
}
