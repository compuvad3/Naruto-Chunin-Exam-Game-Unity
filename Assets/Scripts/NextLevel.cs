using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour {

    // Create a GameObject variable for entrance icon
    [Header("Visual Cue")]
    [SerializeField] private GameObject entranceIcon;

    // Create a GameObject variable for destination icon
    [SerializeField] private GameObject destinationIcon;

    // Keep track if the player is in the range
    bool playerInRange;


    private void Awake() {
        playerInRange = false;
        entranceIcon.SetActive(false);                  // make the entrance icon hidden at the start of the level
        destinationIcon.SetActive(true);                // make the destination icon visible at the start of the level
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

    // Show the entrance icon only if the Player is in the collider's range
    private void Update() {
        if (playerInRange) {
            entranceIcon.SetActive(true);
            destinationIcon.SetActive(false);

            // If the player pressed the interact button, move to the next scene
            if (Input.GetButtonDown("Submit"))
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else {
            entranceIcon.SetActive(false);
            destinationIcon.SetActive(true);
        }
    }
}
