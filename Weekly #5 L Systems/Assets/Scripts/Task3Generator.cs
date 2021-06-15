using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Task3Generator : MonoBehaviour
{
    public Grammar myGrammar;

    public Text displayText;

    public PlantDisplay plantDisplay;

    void Start() {
        plantDisplay.displayPlantFromString(myGrammar.currentString);
    }

    void Update() {
        string displayString = "Press R to Reset.\n" +
            "Press Space to expand grammar once\n" +
            "Press Enter to expand grammar x4\n" +
            "\nCurrent String:\n\n" +
            myGrammar.currentString;

        displayText.text = displayString;

        if (Input.GetKeyDown(KeyCode.R)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else if (Input.GetKeyDown(KeyCode.Space)) {
            myGrammar.expandGrammar();
            plantDisplay.displayPlantFromString(myGrammar.currentString);
        }
        else if (Input.GetKeyDown(KeyCode.Return)) {
            for (int i = 0; i < 4; i++) {
                myGrammar.expandGrammar();
            }
            plantDisplay.displayPlantFromString(myGrammar.currentString);
        }
    }
}
