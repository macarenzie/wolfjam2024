using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private UIDocument document;
    private Button startButton;
    private Button quitButton;

    /// <summary>
    /// setting up the button to switch the image
    /// </summary>
    private void Awake()
    {
        //get button references
        document = GetComponent<UIDocument>();

        startButton = document.rootVisualElement.Q("StartButton") as Button;
        startButton.RegisterCallback<ClickEvent>(LoadScene);

        quitButton = document.rootVisualElement.Q("QuitButton") as Button;
        quitButton.RegisterCallback<ClickEvent>(QuitGame);
    }

    /// <summary>
    /// loads a scene
    /// </summary>
    /// <param name="evt"></param>
    private void LoadScene(ClickEvent evt)
    {

        SceneManager.LoadScene("Cutscene");
        Debug.Log("fjdisl");
    }

    /// <summary>
    /// quits game
    /// </summary>
    /// <param name="evt"></param>
    private void QuitGame(ClickEvent evt)
    {
        Application.Quit();
    }

    //unregister the events
    private void OnDisable()
    {
        startButton.UnregisterCallback<ClickEvent>(LoadScene);

        quitButton.UnregisterCallback<ClickEvent>(QuitGame);
    }
}

