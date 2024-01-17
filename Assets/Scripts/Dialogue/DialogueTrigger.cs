using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {

    // Create a GameObject variable for "Visual Cue" (dialogue icon)
    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;      // using SerializeField so that GameObject visualCue shows up in the Inspector

    // Create a SerializeField for the inkJSON
    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

    // Keep track if the player is in the range
    bool playerInRange;


    private void Awake() {
        playerInRange = false;
        visualCue.SetActive(false);                     // make the "Visual Cue" (dialogue icon) hidden at the start of the level
    }


    // Show the "Visual Cue" (dialogue icon) only if the Player is in the collider's range and the dialogue is not playing
    private void Update() {
        if (playerInRange && !DialogueManager.GetInstance().dialogueIsPlaying) {
            visualCue.SetActive(true);

            // If the player pressed the interact button, start the dialogue
            if (Input.GetButtonDown("Submit"))
                DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
        }
        else
            visualCue.SetActive(false);
    }


    // These two functions detect when another collider enters/exits the collider of the GameObject this script is attached to
    // Note that any collider will be detected, including the Stage collider
    // Since we want only the Player collider to be detected, we can use the Player tag
    private void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.tag == "Player")
            playerInRange = true;                       // update the playerInRange as soon as the Player enters the collider
    }


    private void OnTriggerExit2D(Collider2D collider) {
        if (collider.gameObject.tag == "Player")
            playerInRange = false;                      // update the playerInRange as soon as the Player exits the collider
    }
}
