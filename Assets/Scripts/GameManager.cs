using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    // Singleton instance
    public static GameManager Instance { get; private set; }

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
    }

    // Player progress
    private int exercisesCompleted = 0;

    public void AddExercisesCompleted()
    {
        exercisesCompleted++;
        Debug.Log(exercisesCompleted);
    }
      

}
