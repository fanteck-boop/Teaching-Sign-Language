using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ExerciseScript : MonoBehaviour
{
    // Assign the letters and number of letters in the inspector
    [SerializeField] private List<string> letters; // List of letters
    private int currentLetterIndex = 0; // Tracks the current letter to match

    //Something
    [SerializeField] private string placeholderText;

    // References to UI elements
    [SerializeField] private TMP_InputField inputField; // Input field where the user types
    [SerializeField] private Button checkAnswerButton; // Submit button
    [SerializeField] private TextMeshProUGUI letterDisplay;
    [SerializeField] private TextMeshProUGUI placeholder;

    // Property for the player's input
    public string PlayerInput { get; private set; } // Public getter, private setter


    void Start()
    {
        // Add listener to the submit button
        checkAnswerButton.onClick.AddListener(CheckInput);

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
                Debug.Log("Exercise completed!");
                // You can add logic for when the exercise is finished
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
        if (placeholder != null)
        {
            placeholder.text = placeholderText;
        }
    }

    // Function to update the letter display with the current letter
    private void UpdateLetterDisplay()
    {
        if (currentLetterIndex < letters.Count)
        {
            letterDisplay.text = letters[currentLetterIndex];
        }
    }

    private void CompleteExercise()
    {
        Debug.Log("Exercise done");
    }
}
