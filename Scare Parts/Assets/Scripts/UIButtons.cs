using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class UIButtons : MonoBehaviour
{
    private UIDocument document;
    private Button playAgainButton;
    private Button mainMenuButton;
    private Button quitButton;

    private string playAgain = "GameScene";
    private string mainMenu = "MainMenu";

    /// <summary>
    /// setting up the button to switch the image
    /// </summary>
    private void Awake()
    {
        //get button references
        document = GetComponent<UIDocument>();

        playAgainButton = document.rootVisualElement.Q("PlayAgainButton") as Button;
        playAgainButton.RegisterCallback<ClickEvent>(LoadGameScene);

        mainMenuButton = document.rootVisualElement.Q("MainMenuButton") as Button;
        mainMenuButton.RegisterCallback<ClickEvent>(LoadMainMenu);

        quitButton = document.rootVisualElement.Q("QuitButton") as Button;
        quitButton.RegisterCallback<ClickEvent>(QuitGame);
    }

    /// <summary>
    /// loads main menu
    /// </summary>
    /// <param name="evt"></param>
    private void LoadMainMenu(ClickEvent evt)
    {

        SceneManager.LoadScene("MainMenu");
    }

    /// <summary>
    /// loads game scene
    /// </summary>
    /// <param name="evt"></param>
    private void LoadGameScene(ClickEvent evt)
    {

        SceneManager.LoadScene("GameScene");
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
        playAgainButton.UnregisterCallback<ClickEvent>(LoadGameScene);

        mainMenuButton.UnregisterCallback<ClickEvent>(LoadMainMenu);

        quitButton.UnregisterCallback<ClickEvent>(QuitGame);
    }
}
