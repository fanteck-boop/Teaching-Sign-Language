using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // For TextMeshPro UI components

public class IntroductionExercise : Exercise
{

    public void Start()
    {
        OrderPanels(); 
    }

    public override void CheckInput()
    {
        // Check if the current input matches the current character in the word
        if (currentWordIndex < words.Count)
        {
            UpdateLetter(); // Display next character or word, if applicable
            PlayHandAnimation();
        }
        else
        {
            // Show completion panel if all words are completed
            completedPanel.SetActive(true);
            ResetExercise();
        }
    }

    public override void ResetExercise()
    {
        currentWordIndex = 0; // Reset to the first word
        currentCharIndex = 0; // Reset to the first character of the word
    }

    public void StartHandAnimation()
    {
        // Play the hand animation
        UpdateLetter(); // Display next character or word, if applicable
        PlayHandAnimation();
    }

}
