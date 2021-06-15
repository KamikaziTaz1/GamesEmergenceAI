using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenshotDemo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)){
            string filetype = ".png";
            ScreenCapture.CaptureScreenshot("screenshot example" + "_" + System.DateTime.Now.ToString("MM-dd-yy_hh_mm_ss") + "_" + UnityEngine.SceneManagement.SceneManager.GetActiveScene().name + filetype);
            Debug.Log("ScreenshotGet");
        }    
    }
}
