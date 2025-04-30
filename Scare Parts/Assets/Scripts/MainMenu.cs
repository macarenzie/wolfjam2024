using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // === Fields ===

    private UIDocument document;
    private Button startButton;
    private Button quitButton;
    private Button twitchButton;
    private VisualElement twitchMenu;

    private Button twitchMenuCloseButton;
    private TextField twitchMenuChannelNameTextField;
    private TextField twitchMenuOauthTokenTextField;
    private TextField twitchMenuClientIDTextField;
    private Button twitchConfirmButton;

    private string channelNameString;
    private string oauthTokenString;
    private string clientIDString;


    // === Functions ===

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

        twitchButton = document.rootVisualElement.Q("TwitchButton") as Button;
        twitchButton.RegisterCallback<ClickEvent>(OpenCloseTwitchMenu);

        twitchMenu = document.rootVisualElement.Q("TwitchMenuContainer") as VisualElement;

        twitchMenuCloseButton = document.rootVisualElement.Q("CloseButton") as Button;
        twitchMenuCloseButton.RegisterCallback<ClickEvent>(OpenCloseTwitchMenu);

        twitchMenuChannelNameTextField = document.rootVisualElement.Q("ChannelNameTextField") as TextField;

        twitchMenuOauthTokenTextField = document.rootVisualElement.Q("OauthTokenTextField") as TextField;

        twitchMenuClientIDTextField = document.rootVisualElement.Q("ClientIDTextField") as TextField;

        twitchConfirmButton = document.rootVisualElement.Q("ConfirmButton") as Button;
        twitchConfirmButton.RegisterCallback<ClickEvent>(GetTwitchData);
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

    /// <summary>
    /// Toggles the Twitch Menu open or closed
    /// </summary>
    /// <param name="evt"></param>
    private void OpenCloseTwitchMenu(ClickEvent evt)
    {
        DisplayStyle changedVisbility = DisplayStyle.Flex;

        if (twitchMenu.style.display == DisplayStyle.Flex)
        {
            changedVisbility = DisplayStyle.None;
        }

        twitchMenu.style.display = changedVisbility;
    }

    /// <summary>
    /// Saves data from text fields
    /// </summary>
    /// <param name="evt"></param>
    private void GetTwitchData(ClickEvent evt)
    {
        channelNameString = twitchMenuChannelNameTextField.value;
        oauthTokenString = twitchMenuOauthTokenTextField.value;
        clientIDString = twitchMenuClientIDTextField.value;

        // For debugging
        Debug.Log(channelNameString);
        Debug.Log(oauthTokenString);
        Debug.Log(clientIDString);
    }

    //unregister the events
    private void OnDisable()
    {
        startButton.UnregisterCallback<ClickEvent>(LoadScene);

        quitButton.UnregisterCallback<ClickEvent>(QuitGame);

        twitchButton.UnregisterCallback<ClickEvent>(OpenCloseTwitchMenu);

        twitchMenuCloseButton.UnregisterCallback<ClickEvent>(OpenCloseTwitchMenu);

        twitchConfirmButton.UnregisterCallback<ClickEvent>(GetTwitchData);
    }
}

