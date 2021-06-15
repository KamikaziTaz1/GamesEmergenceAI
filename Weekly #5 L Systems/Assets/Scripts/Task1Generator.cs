using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Task1Generator : MonoBehaviour {

    public Grammar myGrammar;

    public Text displayText;


    void Update() {
        string displayString = "Press R to Reset.\n" +
            "Press Space to expand grammar once\n" +
            "Press Enter to expand grammar x5\n" +
            "\nCurrent String:\n\n" +
            myGrammar.currentString;

        displayText.text = displayString;

        if (Input.GetKeyDown(KeyCode.R)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else if (Input.GetKeyDown(KeyCode.Space)) {
            myGrammar.expandGrammar();
        }
        else if (Input.GetKeyDown(KeyCode.Return)) {
            for (int i = 0; i < 5; i++) {
                myGrammar.expandGrammar();
            }
        }
    }

}
