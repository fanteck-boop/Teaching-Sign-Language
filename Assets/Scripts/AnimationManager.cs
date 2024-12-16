using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    
    public Animator handAnimator; // Animator to control the hand animations
    private float displayDuration = 1.6f; // Duration for displaying each animation
    private Dictionary<char, string> letterAnimations; // Dictionary to map letters to corresponding animation state names
    private float crossFadeTime = 0.2f; // Duration for the smooth crossfade transition

    void Awake()
    {
        // Initialize the dictionary with mappings of letters to their animation state names
        letterAnimations = new Dictionary<char, string>
        {
            { 'A', "AnimateA" },
            { 'B', "AnimateB" },
            { 'C', "AnimateC" },
            { 'D', "AnimateD" },
            { 'E', "AnimateE" },
            { 'F', "AnimateF" },
            { 'G', "AnimateG" },
            { 'H', "AnimateH" },
            { 'I', "AnimateI" },
            { 'K', "AnimateK" },
            { 'L', "AnimateL" },
            { 'M', "AnimateM" } // Map more letters as needed
        };
    }

    public IEnumerator PlayLetterAnimation(char letter)
    {
        // Convert the letter to uppercase to ensure case-insensitive matching
        letter = char.ToUpper(letter); 

        // Wait for the specified duration before playing the animation
        yield return new WaitForSeconds(displayDuration);

        // Check if the letter exists in the dictionary and retrieve its animation state name
        if (letterAnimations.TryGetValue(letter, out string stateName))
        {
            // Play the animation using a smooth crossfade transition
            handAnimator.CrossFade(stateName, crossFadeTime);
        }
        else
        {
            // Log a warning if no animation is found for the given letter
            Debug.LogWarning($"No animation found for letter: {letter}");
        }
    }


    // Function to cancel animations and reset to Idle
    public void StopLetterAnimation()
    {
        //cancelAnimation = true;
        handAnimator.Play("Idle"); // Transition to idle smoothly
        //cancelAnimation = false;
        Debug.Log("Hand animation stopped and reset to idle");
    }

    // Reset the cancel flag when starting a new animation sequence
    /*public void ResetAnimationState()
    {
        cancelAnimation = false;
    }

    
    // Coroutine to play a letter animation (based on the state name)
    public IEnumerator RePlayLetterAnimation(char letter)
    {
        letter = char.ToUpper(letter); // Ensure the letter is uppercase
        yield return new WaitForSeconds(displayDuration);

        if (letterAnimations.TryGetValue(letter, out string stateName))
        {

            // Transition to the specific animation state using CrossFade
            handAnimator.CrossFade(stateName, crossFadeTime);  // Adjust the transition duration as needed
        }
        else
        {
            Debug.LogWarning($"No animation found for letter: {letter}");
        }
    }

    // Optional: Stop the letter animation and reset to a neutral pose (idle pose)

    
    public void ReplayAnimation()
    {
        GoToIdle();
        Debug.Log("Hand animation stopped");
    }*/
    
    public void GoToIdle()
    {
        handAnimator.CrossFade("Idle", crossFadeTime); // Transition to idle
    }
}
