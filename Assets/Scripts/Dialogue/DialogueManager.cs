using Ink.Runtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;


// Singleton class:
public class DialogueManager : MonoBehaviour {

    // Create a SerializeField GameObject dialoguePanel to access the Dialogue Panel so that we can show/hide it depending on if a dialogue is playing
    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;

    // Create a SerializeField TextMeshProUGUI dialogueText to access the Dialogue Text so we can set it according to the ink file contents
    [SerializeField] private TextMeshProUGUI dialogueText;

    // Create a section called "Choices UI" and add an array of GameObjects called choices
    // This allows to have any number of choices as long as it is supported by the "Choices UI"
    // For this game there are max of 2 choices per question
    [Header("Choices UI")]
    [SerializeField] private GameObject[] choices;

    // choicesText keeps track of the text for each choice
    private TextMeshProUGUI[] choicesText;

    // currentStory will keep track of the current Ink file to display
    private Story currentStory;

    // dialogueIsPlaying will keep track of whether or not the dialogue is currently playing
    // { get; private set; } makes it read-only to outside scripts
    public bool dialogueIsPlaying { get; private set; }

    // Create a static instance of the DialogueManager:
    private static DialogueManager instance;


    // Awake() method initialized the DialogueManager instance
    private void Awake() {
        
        // Ensures that only one instance of the class exists! (singleton class)
        if (instance != null)
            Debug.LogWarning("Found more than one Dialogue Manager in the scene");

        instance = this;
    }


    // getInstance() method returns the DialogueManager instance
    public static DialogueManager GetInstance() {
        return instance;
    }


    private void Start() {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);

        // Get all of the choices text
        // Initialize choicesText to an array of the same length as the choices array
        choicesText = new TextMeshProUGUI[choices.Length];

        int index = 0;

        // For each choice in the choices we'll initialize the corresponding text
        foreach (GameObject choice in choices) {
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }
    }


    // Handle the logic traversal of the Ink story
    private void Update() {

        // If dialogue is not playing at all --> return right away since we only want to Update() only when the dialogue is playing!
        if (!dialogueIsPlaying)
            return;

        // Otherwise, check for Player Input if the "Submit" button is pressed
        if (Input.GetButtonUp("Submit"))
            ContinueStory();                    // displays the dialogue's lines
    }


    private IEnumerator ExitDialogueMode() {

        // Wait for 0.2f seconds before exiting the dialogue
        yield return new WaitForSeconds(0.2f);

        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";                 // reset the dialogueText.text to an empty string
    }


    // EnterDialogueMode allows to enter the Dialogue Mode
    // Can be called from the DialogueTrigger script
    public void EnterDialogueMode(TextAsset inkJSON) {
        currentStory = new Story(inkJSON.text);

        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);

        // Display the dialogue's lines
        ContinueStory();
    }


    // Display the dialogue's lines
    private void ContinueStory() {

        // Check if there are lines to be played
        if (currentStory.canContinue) {

            // currentStory.Continue() gives the next line of the dialogue
            // Continue() can be seen as popping a line off of a stack 
            dialogueText.text = currentStory.Continue();

            // Display choices, if any, for the current dialogue line
            DisplayChoices();


            // Check is the stage transition poit was reached
            CheckStageTransition();
        }
        else
            StartCoroutine(ExitDialogueMode());     // when the story cannot continue
    }

    
    // Display the choices appropriately based on the Ink Story
    private void DisplayChoices() {
        
        // Get the list of choices from the current Story (if there are any)
        List<Choice> currentChoices = currentStory.currentChoices;

        // Check if the UI can support the amount of choices that are coming in
        // If there are more current choices that in choices array which represents how many choices our UI can support, we'll log an error
        if (currentChoices.Count > choices.Length)
            Debug.LogError("More choices were given than the UI can support." +
                            "Number of choices given: " + currentChoices.Count);
        
        // Loop through all the GameObjects in the choices and display them according to the current choices from the Ink Story
        int index = 0;

        // Loop through and enable the Choice objects in our UI up to the amount of current choices from the Ink Story
        // This loop will put our UI and current choices in sync
        foreach (Choice choice in currentChoices) {
            
            // For each choice in current choices, we'll set up our choice UI object for that index to be active
            choices[index].gameObject.SetActive(true);

            // Also, set the UI text to be equal to the choice text
            choicesText[index].text = choice.text;
            index++;
        }

        // Go through the remaining UI choices that are supported and hide them
        for (int i = index; i < choices.Length; i++)
            choices[i].gameObject.SetActive(false);

        StartCoroutine(SelectFirstChoice());
    }


    // Define the 1st selected choice
    private IEnumerator SelectFirstChoice() {
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(choices[0].gameObject);
    }


    public void MakeChoice(int choiceIndex) {
        currentStory.ChooseChoiceIndex(choiceIndex);
    }


    // Return the number of shurikens stored in the Ink file
    public int GetShurikenCountFromInk() {
        return (int) currentStory.variablesState["shurikens"];
    }


    // Return the winning rate stored in the Ink file
    public int GetWinningRateFromInk() {
        return (int) currentStory.variablesState["winningRate"];
    }


    // Return the value that represents if the end was reached
    public int CheckStageTransition() {
        return (int) currentStory.variablesState["endReached"];
    }
}
