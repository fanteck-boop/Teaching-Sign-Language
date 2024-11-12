using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // For TextMeshPro UI components

public class IntroductionExercise : Exercise
{
    //Done for now
    public void Start()
    {
        OrderPanels();
        Debug.Log("HELLLOOOO");
    }

    public override void CheckInput()
    {
        //Logic for checking input
        DisplayLetter();
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
        PlayHandAnimation(); //Plays the animations for that word.
    }

    public override void GoToCompletionPanel()
    {
        ResetExercise();
        exercisePanel.SetActive(false);
        completedPanel.SetActive(true);
    }
}
