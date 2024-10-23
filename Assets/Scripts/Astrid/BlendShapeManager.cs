using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlendShapeManager : MonoBehaviour
{

    public SkinnedMeshRenderer handRenderer;   // Reference to the hand's SkinnedMeshRenderer
    public Animator handAnimator;              // Reference to the hand's Animator
    public float displayDuration = 0.1f;       // Display duration for the blendshapes

    // Dictionary to map each letter to its blendshape index or Animator flag
    private Dictionary<char, (int blendShapeIndex, bool isAnimated)> letterData;


    void Awake()
    {
        // Upon awake, initialize the dictionary mapping letters to their blendshape index / animation flag
        letterData = new Dictionary<char, (int, bool)>
        {
            { 'A', (0, false) },
            { 'B', (1, false) },
            { 'C', (2, false) },
            { 'D', (3, false) },
            { 'E', (4, false) },
            { 'F', (5, false) },
            { 'G', (6, false) },
            { 'H', (7, false) },
            { 'I', (8, false) },
            { 'J', (0, true) },  // 'J' requires an animation
            { 'K', (10, false) },
            { 'L', (11, false) },
            { 'M', (12, false) },
            { 'N', (13, false) },
            { 'O', (14, false) },
            { 'P', (15, false) },
            { 'Q', (16, false) },
            { 'R', (17, false) },
            { 'S', (18, false) },
            { 'T', (19, false) },
            { 'U', (20, false) },
            { 'V', (21, false) },
            { 'W', (22, false) },
            { 'X', (23, false) },
            { 'Y', (24, false) },
            { 'Z', (25, true) }, // 'Z' requires animation
            { 'Æ', (26, false) },
            { 'Ø', (27, true) }, // 'OE' requires animation
            { 'Å', (28, true) } // 'AA' requires animation
            
        };
    }


    // Since we want there to be "pauses" between each letter when spelling a letter
    // We make use of an Enumerator and couroutine. 
    public IEnumerator SpellWord(string word)
    {
        word = word.ToUpper(); // Convert to uppercase to match dictionary keys

        foreach (char letter in word)
        {
            if (letterData.ContainsKey(letter))
            {
                var (blendShapeIndex, isAnimated) = letterData[letter];

                if (isAnimated) 
                {
                    PlayAnimationForLetter(letter); // Trigger the Animator for animated letters like 'J' / 'Z'
                }
                else
                {  
                    SetBlendshape(blendShapeIndex, 100);  // Use blendshape for static letters
                }

                yield return new WaitForSeconds(displayDuration);
                ResetBlendshapes();  // Reset between letters
            }
            else
            {
                Debug.LogWarning($"No blendshape or animation found for letter: {letter}"); // Error code
            }
        }
    }
    

    // Function to reset all blendshapes
    public void ResetBlendshapes()
    {
        foreach (var key in letterData.Keys)
        {
            handRenderer.SetBlendShapeWeight(letterData[key].blendShapeIndex, 0);
        }
    }


    // Set a specific blendshape
    public void SetBlendshape(int blendShapeIndex, float weight)
    {
        handRenderer.SetBlendShapeWeight(blendShapeIndex, Mathf.Clamp(weight, 0, 100));
    }


    // Play animation for animated letters like 'J' and 'Z

    private void PlayAnimationForLetter(char letter)
    {
        string triggerName = "Animate" + letter; // Example trigger: "AnimateJ"

        if (letter == 'J')
        {
            handAnimator.SetFloat("AnimationIndex", 0);  // Set index for 'J' in the blend tree
        }
        else if (letter == 'Z')
        {
            handAnimator.SetFloat("AnimationIndex", 1);  // Set index for 'Z' in the blend tree
        }
        else if (letter == 'Ø')
        {
            handAnimator.SetFloat("AnimationIndex", 2);  // Set index for 'Z' in the blend tree
        }
        else if (letter == 'Å')
        {
            handAnimator.SetFloat("AnimationIndex", 3);  // Set index for 'Z' in the blend tree
        }

        handAnimator.SetTrigger(triggerName);
    }

}

// Examples of how it will be called in the level
// StartCoroutine(blendshapeManager.SpellWord("KÆRØ"));
