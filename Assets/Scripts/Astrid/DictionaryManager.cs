using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class DictionaryManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject dictionaryPanel;
    public VideoPlayer videoPlayer;
    public VideoClip[] letterClips; // Assign your video clips for each letter in the Inspector
    public Button[] letterButtons;

    private void Start()
    {
        dictionaryPanel.SetActive(false);

        // Assign each button click to play the corresponding video
        for (int i = 0; i < letterButtons.Length; i++)
        {
            int index = i; // Avoid closure issue
            letterButtons[i].onClick.AddListener(() => PlayLetterVideo(index));
        }
    }

    public void ToggleDictionary()
    {
        dictionaryPanel.SetActive(!dictionaryPanel.activeSelf);
    }

    private void PlayLetterVideo(int letterIndex)
    {
        if (letterIndex >= 0 && letterIndex < letterClips.Length)
        {
            videoPlayer.clip = letterClips[letterIndex];
            videoPlayer.Play();
            videoPlayer.isLooping = true;
        }
    }
}

