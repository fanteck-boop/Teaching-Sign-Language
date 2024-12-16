using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // For TextMeshPro UI components

public class SpellingExercise : Exercise
{
    [SerializeField] private TextMeshProUGUI currentWordText;

    public override void CheckInput()
    {
        if (udpSocket != null && udpSocket.handSignID > -1)
        {
            string currentWord = words[currentWordIndex];
            int currentHandID = ConvertCharToInt(currentWord[currentCharIndex]);

            if (currentHandID == udpSocket.handSignID)
            {
                timer += Time.deltaTime;

                if (timer >= holdTime)
                {
                    if (wordCompleted == true)
                    {
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
                        DisplayLetter();
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

    public override void StartExercise()
    {
        // Hide the start panel and show the exercise panel
        startPanel.SetActive(false);
        exercisePanel.SetActive(true);

        // Reset the exercise to the initial state
        ResetExercise();
        DisplayLetter();
        DisplayWord();
        state = State.Check;
    }
}
