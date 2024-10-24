using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // For TextMeshPro UI components

public class Exercise : MonoBehaviour
{
    // Reference to the NavigationManager script (to toggle the level menu)
    public NavigationManager navManager;

    // List of letters for the exercise (set in the inspector)
    [SerializeField] private List<string> letters; // List of letters
    private int currentLetterIndex = 0; // Keeps track of the current letter

     // Placeholder text for the input field
    [SerializeField] private string placeholderText = "Skriv bogstav her";

    // UI element references (set in the inspector)
    [SerializeField] private TMP_InputField inputField; // Text input field
    [SerializeField] private Button checkAnswerButton; // Button to submit the input
    [SerializeField] private TextMeshProUGUI letterDisplay; // UI element to display the current letter
    [SerializeField] private TextMeshProUGUI placeholder; // UI element for placeholder text

    // Property for the player's input
    public string PlayerInput { get; private set; } // Public getter, private setter

    // GameObject references for the exercise and the completion panel
    [SerializeField] private GameObject exerciseObject;
    [SerializeField] private GameObject exerciseCompletedPanel;
    [SerializeField] private GameObject levelsObject;

    // The exercise collection number (set in the inspector or dynamically)
    public int exerciseCollectionNumber = 1;

    void Start()
    {
        // Add listener to the submit button to call CheckInput() when clicked
        checkAnswerButton.onClick.AddListener(CheckInput);

        // Make sure the exercise UI is hidden and shows the correct panels
        exerciseCompletedPanel.SetActive(false);
        exerciseObject.SetActive(false);

        // Initialize the exercise by resetting the input field and showing the first letter
        ResetInputField();
        UpdateLetterDisplay();
    }

    // Function to check the player's input when they click the submit button
    private void CheckInput()
    {
        // Get the current input from the input field
        PlayerInput = inputField.text;

        // Compare the player's input with the current letter
        if (PlayerInput.ToLower() == letters[currentLetterIndex].ToLower())
        {
            // If correct, move to the next letter
            currentLetterIndex++;

            // Check if there are more letters to display
            if (currentLetterIndex < letters.Count)
            {
                // Reset the input field and show the next letter
                ResetInputField();
                UpdateLetterDisplay();
            }
            else
            {
                // If all letters are completed, show the completion panel and reset the exercise
                exerciseCompletedPanel.SetActive(true);
                ResetExercise();
            }
        }
    }

    // Function to reset the input field
    private void ResetInputField()
    {
        inputField.text = placeholderText;
    }

    // This functions needs to be remade into a function thats works together with astrids.
    private void UpdateLetterDisplay()
    {
        if (currentLetterIndex < letters.Count)
        {
            letterDisplay.text = letters[currentLetterIndex];
        }
    }

    // Function called when the exercise is completed
    public void CompleteExercise()
    {
        // Check if the exerciseObject is active
        if (exerciseObject.activeSelf)
        {
            // Mark the exercise as completed in the GameManager's dictionary
            string key = exerciseObject.name + "Completed";
            if (GameManager.Instance.CurrentExercises.TryGetValue(key, out bool isCompleted) && !isCompleted)
            {
                GameManager.Instance.CurrentExercises[key] = true; // Set exercise status as completed
            }
            // Hide the completion panel and the exercise, and go back to the level menu
            exerciseCompletedPanel.SetActive(false);
            exerciseObject.SetActive(false);
            navManager.ToggleLevelMenu();
        }
    }

    // Function to reset the exercise to the initial state
    private void ResetExercise()
    {
        currentLetterIndex = 0; // Reset to the first letter
        ResetInputField(); // Clear the input field
        UpdateLetterDisplay(); // Update the display to show the first letter
    }
}
