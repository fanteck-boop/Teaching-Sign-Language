using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // For TextMeshPro UI components

public class ReadingExercise : Exercise
{
     // Placeholder text for the input field
    private string placeholderText;

    // UI element references (set in the inspector)
    [SerializeField] private TMP_InputField inputField; // Text input field
    [SerializeField] private Button checkAnswerButton; // Button to submit the input
    [SerializeField] private TextMeshProUGUI placeholder; // UI element for placeholder text

    //Correct wrong logic
    [SerializeField] private GameObject checkMark;
    [SerializeField] private GameObject wrongMark;
    [SerializeField] private AudioClip correctSound; // Sound for correct answer
    [SerializeField] private AudioClip wrongSound; // Sound for incorrect answer
    [SerializeField] private float feedbackDuration = 1.5f; // Duration for showing feedback
    [SerializeField] private AudioSource audioSource;



    // Property for the player's input
    public string PlayerInput { get; private set; } // Public getter, private setter

    //Bool
    private bool isCorrect;

    public void Start()
    {
        OrderPanels();
        checkMark.SetActive(false);
        wrongMark.SetActive(false);
    }

    // Function to check the player's input when they click the submit button
    public override void CheckInput()
    { 
        // Get the current input from the input field
        PlayerInput = inputField.text;

        // Compare the player's input with the current letter
        if (PlayerInput.ToLower() == words[currentWordIndex].ToLower())
        {
            isCorrect = true;
            Debug.Log("1" + isCorrect);
            ShowFeedback(isCorrect);
            // If correct, move to the next letter
            currentWordIndex++;

            // Check if there are more letters to display
            if (currentWordIndex < words.Count)
            {
                // Reset the input field and show the next letter
                ResetInputField();
                Debug.Log(words[currentWordIndex]);
                PlayHandAnimation();
            }
            else
            {
                // If all letters are completed, show the completion panel and reset the exercise
                GoToCompletionPanel();
            }
        }
        else //Show wrong
        {
            isCorrect = false;
            ShowFeedback(isCorrect);
        }
    }

    // Function to reset the input field
    private void ResetInputField()
    {
        inputField.text = placeholderText;
    }

    // Function to reset the exercise to the initial state
    public override void ResetExercise()
    {
        currentWordIndex = 0; // Reset to the first letter
        currentCharIndex = 0; // Reset to the first letter
        ResetInputField(); // Clear the input field
        animationManager.StopLetterAnimation();
    }

    public override void GoToCompletionPanel()
    {
        exercisePanel.SetActive(false);
        completedPanel.SetActive(true);
        ResetExercise();
    }

    public override void StartExercise()
    {
        // Hide the start panel and show the exercise panel
        startPanel.SetActive(false);
        exercisePanel.SetActive(true);
        ResetInputField();
        PlayHandAnimation();
    }

    public void ShowFeedback(bool isCorrect)
    {
        StartCoroutine(ShowFeedbackCoroutine(isCorrect));
    }

    private IEnumerator ShowFeedbackCoroutine(bool isCorrect)
    {
        // First, deactivate both marks to reset any previous feedback
        checkMark.SetActive(false);
        wrongMark.SetActive(false);

        Debug.Log("2" + isCorrect);

        if (isCorrect == true)
        {
            checkMark.SetActive(true);
            audioSource.clip = correctSound;
            audioSource.Play();
        }
        else if (isCorrect == false)
        {
            wrongMark.SetActive(true);
            audioSource.clip = wrongSound;
            audioSource.Play();
        }

        yield return new WaitForSeconds(feedbackDuration);

        checkMark.SetActive(false);
        wrongMark.SetActive(false);
        isCorrect = false;

        Debug.Log("3" + isCorrect);
    }

}

