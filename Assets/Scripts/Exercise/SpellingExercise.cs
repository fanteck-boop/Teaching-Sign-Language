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
        //Logic for checking input
        DisplayLetter();
        DisplayWord();
        if (wordCompleted == true)
        {
            UpdateWord(); //Updates index to go to the next word in the word list

            if (currentWordIndex < words.Count)
            {
                // Display the letter for the next word
                DisplayLetter();
            }
            else
            {
                // No more words left, mark the exercise as complete
                GoToCompletionPanel();
                return; // Exit the function as there are no more words to process
            }
        }
    }

    public override void ResetExercise()
    {
        currentWordIndex = 0; // Reset to the first word
        currentCharIndex = 0; // Reset to the first character
        DisplayWord(); // Display the first word again
        DisplayLetter(); // Display the first letter again
    }

    // Display the full current word in currentWordText
    private void DisplayWord()
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

    public override void GoToCompletionPanel()
    {
        exercisePanel.SetActive(false);
        completedPanel.SetActive(true);
    }

    //OVERRIDE START EXERCISE!!
}
