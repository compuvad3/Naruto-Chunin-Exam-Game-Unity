using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStarter : MonoBehaviour {

    public GameObject instructionText;

    // Start is called before the first frame update
    void Start() {
        instructionText.SetActive(true);
    }

    // Update is called once per frame
    void Update() {

        if (Input.GetButton("Submit")) {
            instructionText.SetActive(false);

            // Restore player's control over the main character
            NarutoMovementChapter1Scene1.playerControl = true;
        }
    }
}
