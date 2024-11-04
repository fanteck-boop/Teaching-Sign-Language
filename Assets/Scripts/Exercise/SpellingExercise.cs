using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // For TextMeshPro UI components

public class SpellingExercise : Exercise
{
    [SerializeField] private TextMeshProUGUI currentWordText;

    public void Start()
    {
        OrderPanels();
    }

    public override void CheckInput()
    {
        // Display the current word and letter
        UpdateWordDisplay();

        // Check if the current word and character are within bounds
        if (currentWordIndex < words.Count)
        {
            // Display the current letter in letterDisplay
            UpdateLetter();
        }
        else
        {
            // If all words are completed, show the completion panel and reset the exercise
            completedPanel.SetActive(true);
            CompleteExercise();
        }
    }

    public override void ResetExercise()
    {
        currentWordIndex = 0; // Reset to the first word
        currentCharIndex = 0; // Reset to the first character
        UpdateWordDisplay(); // Display the first word again
        UpdateLetter(); // Display the first letter again
    }

    // Display the full current word in currentWordText
    private void UpdateWordDisplay()
    {
        if (currentWordIndex < words.Count)
        {
            currentWordText.text = words[currentWordIndex];
        }
        else
        {
            currentWordText.text = ""; // Clear text if no more words
        }
    }
}
