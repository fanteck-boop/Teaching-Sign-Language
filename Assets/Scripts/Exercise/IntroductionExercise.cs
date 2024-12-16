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
        //animationManager.ResetAnimationState();
        ResetExercise();
        PlayHandAnimation();
        DisplayLetter();
        UpdateProgress();
        state = State.Check;
    }

   public override void CheckInput()
{
    if (udpSocket != null && udpSocket.handSignID > -1) // Check if the UDP socket is active and a valid hand sign ID is received
    {
        string currentWord = words[currentWordIndex]; // Retrieve the current word from the list
        int currentHandID = ConvertCharToInt(currentWord[currentCharIndex]); // Convert the current character to its corresponding hand sign ID

        if (currentHandID == udpSocket.handSignID) // Check if the received hand sign ID matches the expected value
        {
            Debug.Log("Hand sign matches"); // Log a match confirmation
            timer += Time.deltaTime; // Increment the timer based on elapsed time

            if (timer >= holdTime) // Check if the hold time requirement is met
            {
                Debug.Log("Holdtime reached"); // Log that the hold time has been achieved

                if (wordCompleted == true) // Check if the current word is fully completed
                {
                    currentCharIndex++; // Move to the next character in the word
                    isCorrect = true; // Set the input as correct
                    ShowFeedback(isCorrect); // Display positive feedback
                    UpdateWord(); // Update the current word state

                    if (currentWordIndex < words.Count) // Verify if more words are available
                    {
                        DisplayLetter(); // Display the next letter
                        UpdateProgress(); // Update the progress UI
                        PlayHandAnimation(); // Trigger an animation for the new letter
                    }
                    else
                    {
                        GoToCompletionPanel(); // If no more words, show the completion panel
                        return; // Exit the method to prevent further execution
                    }
                }
                else
                {
                    currentCharIndex++; // Move to the next character
                    isCorrect = true; // Set the input as correct
                    ShowFeedback(isCorrect); // Display positive feedback
                    DisplayLetter(); // Display the next letter
                    PlayHandAnimation(); // Trigger the animation for the next letter
                    UpdateProgress(); // Update progress indicators
                }

                timer = 0f; // Reset the timer after processing the input
            }
        }
        else
        {
            timer = 0f; // Reset the timer if the hand sign ID is incorrect
        }
    }
    else
    {
        timer = 0f; // Reset the timer if no valid input is provided
    }
}



    public override void GoToCompletionPanel()
    {
        exercisePanel.SetActive(false);
        completedPanel.SetActive(true);
        state = State.NoCheck;
    }

    public override void CompleteExercise()
    {
        StartCoroutine(CompleteExerciseCoroutine());
    }

    public IEnumerator CompleteExerciseCoroutine()
    {
        // Check if the exerciseObject is active
        if (completedPanel.activeSelf)
        {
            if (isCompleted == false)
            {
                isCompleted = true;
                associatedLock.AddCompletedExercises();
                associatedLock.MarkIconAsCompleted(unlockedIcon, completedIcon);
            }
            // Hide the completion panel and the exercise, and go back to the level menu
            ResetExercise();
            // Optionally, wait for a frame to ensure the reset is processed, or for the animator to reset.
            yield return new WaitForEndOfFrame();

            completedPanel.SetActive(false);
            exercisePanel.SetActive(false);
            startPanel.SetActive(true);
            exercise.SetActive(false);
            navManager.ToggleLevelMenu();
        }
    }

    public override void Exit()
    {
        state = State.NoCheck;
        StartCoroutine(ExitExercise());
    }

}