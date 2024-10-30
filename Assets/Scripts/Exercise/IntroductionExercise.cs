using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // For TextMeshPro UI components

public class IntroductionExercise : MonoBehaviour
{
    //managers
    public NavigationManager navManager;
    public AnimationManager animationManager;

    //
    [SerializeField] protected List<string> letters; // List of letters
    protected int currentLetterIndex = 0;
    [SerializeField] private TextMeshProUGUI letterDisplay; // UI element to display the current letter

    // GameObject references for the exercise and the completion panel and the introduction panel
    [SerializeField] private GameObject exercisePanel;
    [SerializeField] private GameObject completedPanel;
    [SerializeField] private GameObject startPanel;
    [SerializeField] private GameObject levelMenu;

    // The exercise collection number (set in the inspector)
    public int exerciseCollectionNumber;

    public virtual void Start()
    {
        // Make sure the exercise UI is hidden and shows the correct panels
        completedPanel.SetActive(false);
        exercisePanel.SetActive(false);
        startPanel.SetActive(false);

        // Initialize the exercise by showing the first letter
        UpdateLetter();
        PlayHandAnimation();
    }

    public virtual void CheckInput()
    {
        //if handgesture matches current letter, do this:
        currentLetterIndex++;

        //Here goes the code for checking the input the player gives against the actual answer
        if (currentLetterIndex < letters.Count)
        {
            // Reset the input field and show the next letter
            UpdateLetter();
            PlayHandAnimation();
        }
        else
        {
            // If all letters are completed, show the completion panel and reset the exercise
            completedPanel.SetActive(true);
            ResetExercise();
        }
    }

    public virtual void ResetExercise()
    {
        currentLetterIndex = 0; // Reset to the first letter
        UpdateLetter(); // Update the display to show the first letter
        PlayHandAnimation(); //TEST OM DETTE ER NØDVENDIGT

    }

    public virtual void CompleteExercise()
    {
        // Check if the exerciseObject is active
        if (exercisePanel.activeSelf)
        {
            // Mark the exercise as completed in the GameManager's dictionary
            string key = exercisePanel.name + "Completed";

            //checks if the key-value pair exists and if isCompleted is false
            if (GameManager.Instance.CurrentExercises.TryGetValue(key, out bool isCompleted) && !isCompleted)
            {
                GameManager.Instance.CurrentExercises[key] = true; // Set exercise status as completed
            }

            // Hide the completion panel and the exercise, and go back to the level menu
            completedPanel.SetActive(false);
            exercisePanel.SetActive(false);
            startPanel.SetActive(false);
            navManager.ToggleLevelMenu();
        }

    }

    public virtual void UpdateLetter()
    {
        if (currentLetterIndex < letters.Count)
        {
            letterDisplay.text = letters[currentLetterIndex];
        }
    }

    public virtual void PlayHandAnimation()
    {
        StartCoroutine(PlayLetterSequence());
    }

    private IEnumerator PlayLetterSequence()
    {
        foreach (char letter in letters[currentLetterIndex])
        {
            yield return StartCoroutine(animationManager.PlayLetterAnimation(letter));
        }
    }

}
