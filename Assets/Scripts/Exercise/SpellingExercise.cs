using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // For TextMeshPro UI components

public class SpellingExercise : Exercise
{
    [SerializeField] private TextMeshProUGUI currentWordText;

    private float timer = 0f; // Timer to track how long the correct hand sign is held
    private const float holdTime = 1f; // Time required to hold the correct sign

    enum State { NoCheck, Check}
    private State state = State.NoCheck;

    public void LateUpdate()
    {
        switch (state)
        {
            case State.NoCheck:
                // Start the introduction animation
                break;
            case State.Check:
                CheckInput();
                break;
        }
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

            Debug.Log("The correct handsign is "+ currentHandID);

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
                            DisplayWord();
                            UpdateProgress();
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
    public override void ResetExercise()
    {
        currentWordIndex = 0; // Reset to the first word
        currentCharIndex = 0; // Reset to the first character
        UpdateProgress();
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
        state = State.NoCheck;
    }

    public override void StartExercise()
    {
        // Hide the start panel and show the exercise panel
        startPanel.SetActive(false);
        exercisePanel.SetActive(true);

        // Reset the exercise to the initial state
        ResetExercise();
        DisplayLetter();
        DisplayWord();
        UpdateProgress();
        state = State.Check;
    }
    
    public override void Exit()
    {
        // Start the reset process
        ResetExercise();

        // Now hide the exercise panel and reset the UI.
        exercisePanel.SetActive(false);
        startPanel.SetActive(true);
        exercise.SetActive(false);

        // Navigate back to the level menu
        navManager.ToggleLevelMenu();
        state = State.NoCheck;
        
    }
}
