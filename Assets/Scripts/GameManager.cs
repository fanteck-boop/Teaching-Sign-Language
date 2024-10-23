using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Singleton instance
    public static GameManager Instance { get; private set; }

    // Player progress
    private int currentExerciseCollection = 1; // Initialize the current exercise collection

    // Property for accessing the current exercise collection
    public int CurrentExerciseCollection 
    { 
        get => currentExerciseCollection; 
        set => currentExerciseCollection = value; // Set the current collection value
    }

    // Levels
    public Dictionary<string, bool> CurrentExercises { get; private set; }

    private void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
        else
        {
            Destroy(gameObject);
        }

        // Initialize the dictionary to track completed exercises
        CurrentExercises = new Dictionary<string, bool> 
        { 
            { "IntroductionCompleted", false }, 
            { "Spelling1Completed", false }, 
            { "Spelling2Completed", false }, 
            { "Reading1Completed", false }, 
            { "Reading2Completed", false } 
        };
    }
}
