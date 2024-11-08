using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // For TextMeshPro UI components

public class ReadingExercise : Exercise
{
     // Placeholder text for the input field
    private string placeholderText;

    // UI element references (set in the inspector)
    [SerializeField] private TMP_InputField inputField; // Text input field
    [SerializeField] private Button checkAnswerButton; // Button to submit the input
    [SerializeField] private TextMeshProUGUI placeholder; // UI element for placeholder text

    // Property for the player's input
    public string PlayerInput { get; private set; } // Public getter, private setter

    public void Start()
    {
        // Add listener to the submit button to call CheckInput() when clicked
        checkAnswerButton.onClick.AddListener(CheckInput);

        OrderPanels();

        // Initialize the exercise by resetting the input field and showing the first letter
        ResetInputField();
        PlayHandAnimation();
    }

    // Function to check the player's input when they click the submit button
    public override void CheckInput()
    { 
        // Get the current input from the input field
        PlayerInput = inputField.text;

        // Compare the player's input with the current letter
        if (PlayerInput.ToLower() == words[currentWordIndex].ToLower())
        {
            // If correct, move to the next letter
            currentWordIndex++;

            // Check if there are more letters to display
            if (currentWordIndex < words.Count)
            {
                // Reset the input field and show the next letter
                ResetInputField();
                Debug.Log(words[currentWordIndex]);
                PlayHandAnimation();
            }
            else
            {
                // If all letters are completed, show the completion panel and reset the exercise
                GoToCompletionPanel();
                ResetExercise();
            }
        }
    }

    // Function to reset the input field
    private void ResetInputField()
    {
        inputField.text = placeholderText;
    }

    // Function to reset the exercise to the initial state
    public override void ResetExercise()
    {
        currentWordIndex = 0; // Reset to the first letter
        currentCharIndex = 0; // Reset to the first letter
        ResetInputField(); // Clear the input field
    }

    public override void GoToCompletionPanel()
    {
        exercisePanel.SetActive(false);
        completedPanel.SetActive(true);
        animationManager.StopLetterAnimation();
    }

    public override void StartExercise()
    {
        // Hide the start panel and show the exercise panel
        animationManager.StopLetterAnimation();
        startPanel.SetActive(false);
        exercisePanel.SetActive(true);

        // Reset the exercise to the initial state
        ResetExercise();
        PlayHandAnimation();
    }

}

