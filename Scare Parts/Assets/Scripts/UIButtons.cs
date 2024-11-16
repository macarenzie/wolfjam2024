using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class UIButtons : MonoBehaviour
{
    private List<Button> menuButtons = new List<Button>();
    private UIDocument document;
    private Button startButton;

    private AudioSource audioSource;

    /// <summary>
    /// setting up the button to switch the image
    /// </summary>
    private void Awake()
    {
        //get audio
        audioSource = GetComponent<AudioSource>();

        //get button references
        document = GetComponent<UIDocument>();

        startButton = document.rootVisualElement.Q("StartButton") as Button;
        startButton.RegisterCallback<ClickEvent>(StartGame);
        
        //all buttons will play a sound effect
        menuButtons = document.rootVisualElement.Query<Button>().ToList();
        for (int i = 0; i < menuButtons.Count; i++)
        {
            menuButtons[i].RegisterCallback<ClickEvent>(OnAllButtonClick);
        }
    }

    private void StartGame(ClickEvent evt)
    {

        SceneManager.LoadScene("Cutscene");
    }

    private void OnAllButtonClick(ClickEvent evt)
    {
        audioSource.Play();
    }

    //unregister the events
    private void OnDisable()
    {
        startButton.UnregisterCallback<ClickEvent>(StartGame);

        for (int i = 0; i < menuButtons.Count; i++)
        {
            menuButtons[i].UnregisterCallback<ClickEvent>(OnAllButtonClick);
        }
    }
}
