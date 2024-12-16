using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // For TextMeshPro UI components

public class IntroductionExercise : Exercise
{
    public override void StartExercise()
    {
        // Hide the start panel and show the exercise panel
        startPanel.SetActive(false);
        exercisePanel.SetActive(true);
        ResetExercise();
        PlayHandAnimation();
        DisplayLetter();
        UpdateProgress();
        state = State.Check;
    }

    public override void CheckInput() //Checks if the hand sign matches the required sign
    {
        if (udpSocket != null && udpSocket.handSignID > -1) // Ensure the UDP socket is set up and has a valid HandID
        {
            string currentWord = words[currentWordIndex]; // Get the current word from the list
            int currentHandID = ConvertCharToInt(currentWord[currentCharIndex]); // Convert the current character to an integer

            if (currentHandID == udpSocket.handSignID) // Check if the hand sign matches the required sign
            {
                timer += Time.deltaTime; // Increment the timer

                if (timer >= holdTime) // Check if the correct hand sign is held for the required time
                {
                    if (wordCompleted == true)
                    {
                        isCorrect = true; //Set isCorrect to true
                        ShowFeedback(isCorrect); //Shows the correct mark or wrong mark with a sound
                        UpdateWord(); //Updates wordIndex and sets wordCompleted = false;

                        if (currentWordIndex < words.Count) //Checks if there are any more words left
                        {
                            DisplayLetter(); //Displays the next letter
                            UpdateProgress(); //Updates the rpogress text
                            PlayHandAnimation(); //Plays the animation for the next letter
                        }
                        else //Else, end the exercise
                        {
                            GoToCompletionPanel(); //Opens the completionpanel
                            return; // Exit the function as there are no more words to process
                        }
                    }
                    else
                    {
                        currentCharIndex++; //Increment currentCharIndex if the word is not completed
                        isCorrect = true; //Set isCorrect to true
                        ShowFeedback(isCorrect); //Show the correct mark
                        DisplayLetter(); //Display the next letter
                        PlayHandAnimation(); //Plays the animation for the next letter
                        UpdateProgress(); //Updates the progress text
                    }
                    timer = 0f; // Reset the timer after successful processing
                }
            }
            else
            {
                timer = 0f; // Reset the timer if the hand sign is incorrect
            }
        }
        else
        {
            timer = 0f; // Reset the timer if there's no valid input
        }
    }
}