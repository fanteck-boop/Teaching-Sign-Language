using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // For TextMeshPro UI components

public class Exercise : MonoBehaviour
{
    //Managers
    [SerializeField] protected NavigationManager navManager;
    [SerializeField] protected AnimationManager animationManager;

    //Word variables
    [SerializeField] protected List<string> words; // List of letters
    [SerializeField] protected int currentWordIndex = 0; // Index of the current word in the list
    [SerializeField] protected int currentCharIndex = 0; // Index of the current character in the current word
    [SerializeField] protected TextMeshProUGUI letterDisplay; // UI element to display the current letter

    // GameObject references for the exercise and the completion panel and the introduction panel
    protected GameObject exercisePanel;
    protected GameObject completedPanel;
    protected GameObject startPanel;
    [SerializeField] protected GameObject exercise;

    //Lock reference
    public Locks associatedLock;
    [SerializeField] private bool isCompleted = false;

    //Icons
    [SerializeField] public GameObject unlockedIcon;
    [SerializeField] private GameObject completedIcon;


    protected bool wordCompleted = false;

    public void OrderPanels()
    {
        // Assign UI elements by finding them in the scene
        exercisePanel = transform.Find("Canvas/Exercise").gameObject;
        completedPanel = transform.Find("Canvas/Completed").gameObject;
        startPanel = transform.Find("Canvas/Start").gameObject;

        // Ensure the exercise UI starts with the correct panels visible
        if (exercise != null) exercise.SetActive(false);
        if (completedPanel != null) completedPanel.SetActive(false);
        if (exercisePanel != null) exercisePanel.SetActive(false);
        if (startPanel != null) startPanel.SetActive(true);
    }

    public virtual void DisplayLetter()
    {
        // Get the current word
        Debug.Log(currentWordIndex);
        string currentWord = words[currentWordIndex];
        Debug.Log("current word:" + currentWord);
        Debug.Log("current char index:" + currentCharIndex);
        Debug.Log("current word length:" + currentWord.Length);

        // Display the current character in the word
        if (currentCharIndex < currentWord.Length)
        {
            char currentChar = currentWord[currentCharIndex];
            Debug.Log("current char:" + currentChar);
            letterDisplay.text = currentChar.ToString();
            currentCharIndex++; // Move to the next character
        }
        else
        {
            wordCompleted = true;
            currentCharIndex = 0;
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
            string currentWord = words[currentWordIndex]; //Finds the word
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
        }
        else
        {
            Debug.LogError($"currentWordIndex {currentWordIndex} is out of bounds for the words list (count: {words.Count}).");
        }
    }


    public void CompleteExercise()
    {
        // Check if the exerciseObject is active
        if (completedPanel.activeSelf)
        {
            if (isCompleted == false)
            {
                isCompleted = true;
                associatedLock.AddCompletedExercises();
            }
            // Hide the completion panel and the exercise, and go back to the level menu
            completedPanel.SetActive(false);
            exercisePanel.SetActive(false);
            startPanel.SetActive(true);
            exercise.SetActive(false);
            associatedLock.MarkIconAsCompleted(unlockedIcon, completedIcon);
            navManager.ToggleLevelMenu();
        }

    }

    public void UpdateWord()
    {
        currentWordIndex++;
        wordCompleted = false;
    }

    public virtual void CheckInput()
    {
    }

    public virtual void ResetExercise()
    {
        currentWordIndex = 0; // Index of the current word in the list
        currentCharIndex = 0;
    }

    public virtual void StartExercise()
    {
        // Hide the start panel and show the exercise panel
        startPanel.SetActive(false);
        exercisePanel.SetActive(true);

        // Reset the exercise to the initial state
        ResetExercise();
        PlayHandAnimation();
        DisplayLetter();
    }

    public virtual void GoToCompletionPanel()
    {

    }

}
