using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // For TextMeshPro UI components

public class Exercise : MonoBehaviour
{
    //managers
    [SerializeField] protected NavigationManager navManager;
    [SerializeField] protected AnimationManager animationManager;

    //
    [SerializeField] protected List<string> words; // List of letters
    protected int currentWordIndex = 0; // Index of the current word in the list
    protected int currentCharIndex = 0; // Index of the current character in the current word
    [SerializeField] protected TextMeshProUGUI letterDisplay; // UI element to display the current letter

    // GameObject references for the exercise and the completion panel and the introduction panel
    protected GameObject exercisePanel;
    protected GameObject completedPanel;
    protected GameObject startPanel;
    [SerializeField] protected GameObject exercise;

    // The exercise collection number (set in the inspector)
    public int exerciseCollectionNumber;

    //Lock reference
    public Locks associatedLock;

    public void OrderPanels()
    {
        // Assign UI elements by finding them in the scene
        exercisePanel = transform.Find("Canvas/Exercise").gameObject;
        completedPanel = transform.Find("Canvas/Completed").gameObject;
        startPanel = transform.Find("Canvas/Start").gameObject;

        // Ensure the exercise UI starts with the correct panels visible
        if (completedPanel != null) completedPanel.SetActive(false);
        if (exercisePanel != null) exercisePanel.SetActive(false);
        if (startPanel != null) startPanel.SetActive(true);
    }

    public virtual void UpdateLetter()
    {
        // Get the current word
        string currentWord = words[currentWordIndex];

        // Display the current character in the word
        if (currentCharIndex < currentWord.Length)
        {
            char currentChar = currentWord[currentCharIndex];
            letterDisplay.text = currentChar.ToString();
            currentCharIndex++; // Move to the next character
        }
        else
        {
            // Move to the next word and reset the character index
            currentCharIndex = 0;
            currentWordIndex++;
            CheckInput();
        }
    }

    public void PlayHandAnimation()
    {
        StartCoroutine(PlayLetterSequence());
    }

    private IEnumerator PlayLetterSequence()
    {
        Debug.Log($"Starting PlayLetterSequence for word index {currentWordIndex}");

        // Check if there are words left to animate
        if (currentWordIndex < words.Count)
        {
            string currentWord = words[currentWordIndex];
            int currentCharIndexAni = 0;

            // Iterate using currentCharIndexAni to track each character in the word
            while (currentCharIndexAni < currentWord.Length)
            {
                char currentChar = currentWord[currentCharIndexAni];  // Corrected to use currentCharIndexAni

                // Play the animation for the current character using the index
                yield return StartCoroutine(animationManager.PlayLetterAnimation(currentChar));

                // Increment the character index to move to the next character
                currentCharIndexAni++;
               
            }

            // Reset currentCharIndexAni for the next word
            //currentCharIndexAni = 0;

            // After finishing all characters in the word, return to idle state
            //animationManager.StopLetterAnimation();
            Debug.Log("Stopped letter animation, transitioning to idle state.");
        }
        else
        {
            Debug.LogError($"currentWordIndex {currentWordIndex} is out of bounds for the words list (count: {words.Count}).");
        }
    }


    public void CompleteExercise()
    {
        // Check if the exerciseObject is active
        if (exercisePanel.activeSelf)
        {
            // Mark the exercise as completed in the GameManager's dictionary
            string key = gameObject.name + "Completed";

            //checks if the key-value pair exists and if isCompleted is false
            if (GameManager.Instance.CurrentExercises.TryGetValue(key, out bool isCompleted) && !isCompleted)
            {
                GameManager.Instance.CurrentExercises[key] = true; // Set exercise status as completed
                associatedLock.AddCompletedExercises();
            }
            // Hide the completion panel and the exercise, and go back to the level menu
            completedPanel.SetActive(false);
            exercisePanel.SetActive(false);
            startPanel.SetActive(true);
            exercise.SetActive(false);
            navManager.ToggleLevelMenu();
        }

    }

    public virtual void CheckInput()
    {}

    public virtual void ResetExercise()
    {}

}
