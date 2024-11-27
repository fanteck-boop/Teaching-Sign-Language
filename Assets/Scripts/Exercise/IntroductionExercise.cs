using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // For TextMeshPro UI components

public class IntroductionExercise : Exercise
{
    private float timer = 0f; // Timer to track how long the correct hand sign is held
    private const float holdTime = 1f; // Time required to hold the correct sign

    public void LateUpdate()
    {
        CheckInput();
    }

    public override void CheckInput()
    {
        Debug.Log("Checks input");
        // Ensure the UDP socket is set up and has a valid HandID

        Debug.Log("UPD Socket handSign is " + udpSocket.handSignID);
        if (udpSocket != null && udpSocket.handSignID > -1)
        {
            string currentWord = words[currentWordIndex];

            Debug.Log(currentWord[currentCharIndex]);

            int currentHandID = ConvertCharToInt(currentWord[currentCharIndex]);

            Debug.Log("The correct handsign is " + currentHandID);

            if (currentHandID == udpSocket.handSignID)
            {
                Debug.Log("Hand sign matches");

                timer += Time.deltaTime;

                if (timer >= holdTime)
                {
                    Debug.Log("Holdtime reached");

                    if (wordCompleted == true)
                    {
                        currentCharIndex++;
                        isCorrect = true;
                        ShowFeedback(isCorrect);
                        UpdateWord(); //Updates wordIndex and sets wordCompleted = false;

                        if (currentWordIndex < words.Count)
                        {
                            // Display the letter for the next word
                            DisplayLetter();
                            UpdateProgress();
                            PlayHandAnimation();
                        }
                        else
                        {
                            GoToCompletionPanel();
                            return; // Exit the function as there are no more words to process
                        }
                    }
                    else
                    {
                        currentCharIndex++;
                        isCorrect = true;
                        ShowFeedback(isCorrect);
                        Debug.Log("currentCharIndex updated to :" + currentCharIndex);
                        DisplayLetter();
                        PlayHandAnimation();
                        UpdateProgress();
                        Debug.Log("Letter displayed");
                    }

                    // Reset the timer after successful processing
                    timer = 0f;
                }

            }
            else
            {
                // Reset the timer if the hand sign is incorrect
                timer = 0f;
            }
        }
        else
        {
            // Reset the timer if there's no valid input
            timer = 0f;
        }
    }
    public override void GoToCompletionPanel()
    {
        exercisePanel.SetActive(false);
        completedPanel.SetActive(true);
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

}