using UnityEngine;
using TwitchSDK;
using TwitchSDK.Interop;
using System.Threading;

public class TwitchManager : MonoBehaviour
{
    [Header("Twitch SDK Settings")]
    public string twitchUsername = "your_bot_username";
    public string oauthToken = "oauth:your_token_here";
    public string channelName = "your_channel_name";

    private GameTask<AuthState> authTask;
    private GameTask<AuthenticationInfo> authInfoTask;
    private bool loginStarted = false;
    private bool loggedIn = false;

    void Start()
    {
        authTask = Twitch.API.GetAuthState();
        Debug.Log("TwitchManager: Started auth check...");
    }

    void Update()
    {
        HandleLogin();
    }

    void HandleLogin()
    {
        if (!loggedIn && authTask != null && authTask.MaybeResult != null)
        {
            var authState = authTask.MaybeResult;

            if (authState.Status == AuthStatus.LoggedOut && !loginStarted)
            {
                loginStarted = true;
                TwitchOAuthScope scopes = new TwitchOAuthScope("chat:read chat:edit");
                authInfoTask = Twitch.API.GetAuthenticationInfo(scopes);
                Debug.Log("TwitchManager: Requested authentication info...");
            }
            else if (authState.Status == AuthStatus.LoggedIn)
            {
                Debug.Log("Twitch SDK login complete.");
                loggedIn = true;
                // Chat is now handled by TwitchChatClient.cs
            }
        }

        if (authInfoTask != null && authInfoTask.MaybeResult != null)
        {
            var authInfo = authInfoTask.MaybeResult;
            if (authInfo != null)
            {
                Debug.Log("Twitch Login Info:");
                Debug.Log("Open this URL: " + authInfo.Uri);
                Debug.Log("Enter this code: " + authInfo.UserCode);

                Application.OpenURL(authInfo.Uri);
                authInfoTask = null;
            }
        }
    }
}