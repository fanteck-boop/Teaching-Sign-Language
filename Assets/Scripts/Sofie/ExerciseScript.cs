using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ExerciseScript : MonoBehaviour
{
    //Reference to manager
    public NavigationManager navManager;

    // Assign the letters and number of letters in the inspector
    [SerializeField] private List<string> letters; // List of letters
    private int currentLetterIndex = 0; // Tracks the current letter to match

    //Something
    [SerializeField] private string placeholderText = "Skriv bogstav her";

    // References to UI elements
    [SerializeField] private TMP_InputField inputField; // Input field where the user types
    [SerializeField] private Button checkAnswerButton; // Submit button
    [SerializeField] private TextMeshProUGUI letterDisplay;
    [SerializeField] private TextMeshProUGUI placeholder;

    // Property for the player's input
    public string PlayerInput { get; private set; } // Public getter, private setter

    //Exercises and Level references
    [SerializeField] private GameObject exerciseObject;
    [SerializeField] private GameObject exerciseCompletedPanel;
    [SerializeField] private GameObject levelsObject;

    //Bool
    private bool isCompleted = false;

    void Start()
    {
        // Add listener to the submit button
        checkAnswerButton.onClick.AddListener(CheckInput);

        //Make sure exercises is inactive and toggles correctly
        exerciseCompletedPanel.SetActive(false);
        exerciseObject.SetActive(false);

        // Reset input field and start with the first letter
        ResetInputField();
        UpdateLetterDisplay();
    }

    // Function to check the input when the user clicks the button
    private void CheckInput()
    {
        // Set PlayerInput to the current input from the field
        PlayerInput = inputField.text;

        // Check if the input matches the current letter
        if (PlayerInput.ToLower() == letters[currentLetterIndex].ToLower())
        {
            Debug.Log("Correct!");

            // Move to the next letter if available
            currentLetterIndex++;

            if (currentLetterIndex < letters.Count)
            {
                // Reset the input field for the next letter
                ResetInputField();
                UpdateLetterDisplay();
            }

            else
            {
                Debug.Log("Exercise done!");
                exerciseCompletedPanel.SetActive(true);
                ResetExercise();
            }
        }

        else
        {
            Debug.Log("Incorrect, try again!");
        }
    }

    // Function to reset the input field
    private void ResetInputField()
    {
        inputField.text = placeholderText;
    }

    // Function to update the letter display with the current letter
    private void UpdateLetterDisplay()
    {
        if (currentLetterIndex < letters.Count)
        {
            letterDisplay.text = letters[currentLetterIndex];
        }
    }

    public void CompleteExercise() //Function to close exercise and activate Level menu
    {
        if (isCompleted == false)
        {
            GameManager.Instance.AddExercisesCompleted();
        }

        isCompleted = true;

        if (exerciseObject.activeSelf) // Check if exerciseObject is active
        {
            exerciseCompletedPanel.SetActive(false);
            exerciseObject.SetActive(false);
            navManager.ToggleLevelMenu();
        }
    }

    private void ResetExercise() //resets the exercise
    {
        currentLetterIndex = 0; // Reset to the first letter
        ResetInputField(); // Clear the input field
        UpdateLetterDisplay(); // Update the display to show the first letter
    }
}
