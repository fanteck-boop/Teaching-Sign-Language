using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

abstract class IntroExerciseLevel : LevelTemplate // Ensure it inherits from LevelTemplate
{
    public char[] lettersToPractice;             // Letters to practice in this exercise
    public float displayDuration = 2.0f;           // How long to display each letter
    public TMP_Text feedbackText;                   // TMP Text element for feedback to the player
    public TMP_InputField inputField;    // Input field for player input
    public BlendShapeManager BlendShapeManager;    // Reference to the BlendShapeManager

    private int currentLetterIndex = 0;            // Track the current letter being practiced

    // Override this to start the gesture exercise
    protected override string GetWord()
    {
        // Returning an empty string since this level focuses on gestures, not spelling a word
        return string.Empty;
    }

    void Start()
    {
        // Optional: Clear input field and set feedback text
        inputField.text = "";
        feedbackText.text = "";
        StartExercise();
    }

    // Start the exercise by displaying the letters one by one
    public void StartExercise()
    {
        // Define a string containing all the letters you want to test
        string allLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZÆØÅ"; // Include your letters here

        // Convert the string to a char array and set it to the lettersToPractice
        lettersToPractice = allLetters.ToCharArray();

        // Start practicing the letters
        if (lettersToPractice.Length > 0)
        {
            StartCoroutine(PracticeLetters());
        }
        else
        {
            Debug.LogWarning("No letters to practice!");
        }
    }

    private IEnumerator PracticeLetters()
    {
        while (currentLetterIndex < lettersToPractice.Length)
        {
            char currentLetter = lettersToPractice[currentLetterIndex];
            yield return StartCoroutine(BlendShapeManager.SpellWord(currentLetter.ToString())); // Display the current letter's blendshape

            // Display the letter for a certain duration
            yield return new WaitForSeconds(displayDuration);

            // Clear the input field for the player to type
            inputField.text = "";

            // Wait for the player to type the corresponding letter
            bool letterConfirmed = false;

            while (!letterConfirmed)
            {
                // Check if the player has pressed enter
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    // Check if the typed letter matches the current letter
                    if (inputField.text.ToUpper() == currentLetter.ToString())
                    {
                        feedbackText.text = $"Correct! {currentLetter}";
                        letterConfirmed = true; // Move to the next letter
                        currentLetterIndex++;
                    }
                    else
                    {
                        feedbackText.text = $"Incorrect! Try again.";
                    }

                    // Clear input for the next attempt
                    inputField.text = "";
                }

                yield return null; // Wait for the next frame before checking again
            }
        }

        // Exercise complete, return to the menu or next level
        ReturnToMenu();
    }

    private void ReturnToMenu()
    {
        Debug.Log("Exercise complete! Returning to menu...");
        // Add your scene transition logic here
    }
}
