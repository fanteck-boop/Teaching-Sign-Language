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
    [SerializeField] private GameObject exerciseObject;
    [SerializeField] private GameObject exerciseCompletedPanel;
    //[SerializeField] private GameObject exerciseStartPanel;
    [SerializeField] private GameObject levelsObject;

    // The exercise collection number (set in the inspector or dynamically)
    public int exerciseCollectionNumber;


    public virtual void Start()
    {
        // Make sure the exercise UI is hidden and shows the correct panels
        exerciseCompletedPanel.SetActive(false);
        exerciseObject.SetActive(false);
        //exerciseStartPanel.SetActive(false);

        // Initialize the exercise by showing the first letter
        UpdateLetter();
        UpdateAnimation();

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
            UpdateAnimation();
        }
        else
        {
            // If all letters are completed, show the completion panel and reset the exercise
            exerciseCompletedPanel.SetActive(true);
            ResetExercise();
        }
    }

    public virtual void ResetExercise()
    {
        currentLetterIndex = 0; // Reset to the first letter
        UpdateLetter(); // Update the display to show the first letter
        UpdateAnimation();

    }

    public virtual void CompleteExercise()
    {
        // Check if the exerciseObject is active
        if (exerciseObject.activeSelf)
        {
            // Mark the exercise as completed in the GameManager's dictionary
            string key = exerciseObject.name + "Completed";

            //checks if the key-value pair exists and if isCompleted is false
            if (GameManager.Instance.CurrentExercises.TryGetValue(key, out bool isCompleted) && !isCompleted)
            {
                GameManager.Instance.CurrentExercises[key] = true; // Set exercise status as completed
            }
            // Hide the completion panel and the exercise, and go back to the level menu
            exerciseCompletedPanel.SetActive(false);
            exerciseObject.SetActive(false);
            //exerciseStartPanel.SetActive(false);
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

    public virtual void UpdateAnimation()
    {
        foreach (char letter in letters[currentLetterIndex])
        {
            StartCoroutine(animationManager.PlayLetterAnimation(letter));
        }
    }

}
