using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class UIButtons : MonoBehaviour
{
    private UIDocument document;
    private Button playAgainButton;
    private Button quitButton;

    /// <summary>
    /// setting up the button to switch the image
    /// </summary>
    private void Awake()
    {
        //get button references
        document = GetComponent<UIDocument>();

        playAgainButton = document.rootVisualElement.Q("PlayAgainButton") as Button;
        playAgainButton.RegisterCallback<ClickEvent>(LoadScene);

        quitButton = document.rootVisualElement.Q("QuitButton") as Button;
        quitButton.RegisterCallback<ClickEvent>(QuitGame);
    }

    /// <summary>
    /// loads a scene
    /// </summary>
    /// <param name="evt"></param>
    private void LoadScene(ClickEvent evt)
    {

        SceneManager.LoadScene("MainMenu");
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
        playAgainButton.UnregisterCallback<ClickEvent>(LoadScene);

        quitButton.UnregisterCallback<ClickEvent>(QuitGame);
    }
}
