using UnityEngine;
using TwitchSDK;
using TwitchSDK.Interop;
using System.IO;
using System.Net.Sockets;
using System.Threading;

/// <summary>
/// TwitchManager which handles both Twitch SDK login and Twitch IRC chat via a custom connection.
/// </summary>
public class TwitchManager : MonoBehaviour
{
    [Header("Twitch SDK + IRC Settings")]
    public string twitchUsername = "your_bot_username";
    public string oauthToken = "oauth:your_token_from_tmi";
    public string channelName = "your_channel_name";

    private TcpClient twitchClient;
    private StreamReader reader;
    private StreamWriter writer;
    private Thread clientThread;

    private GameTask<AuthState> authTask;
    private GameTask<AuthenticationInfo> authInfoTask;
    private bool loginStarted = false;
    private bool loggedIn = false;

    void Start()
    {
        authTask = Twitch.API.GetAuthState();
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
            }
            else if (authState.Status == AuthStatus.LoggedIn)
            {
                Debug.Log("Twitch SDK login complete.");
                loggedIn = true;
                //ConnectToIRC();
            }
        }

        if (authInfoTask != null && authInfoTask.MaybeResult != null)
        {
            var authInfo = authInfoTask.MaybeResult;
            if (authInfo != null)
            {
                Debug.Log("Opening Twitch login browser...");
                Debug.Log("Open this URL in your browser:");
                Debug.Log(authInfo.Uri);
                Debug.Log("And enter this code:");
                Debug.Log(authInfo.UserCode);
                Application.OpenURL($"{authInfo.Uri}{authInfo.UserCode}");
                authInfoTask = null;
            }
        }
    }

    //void ConnectToIRC()
    //{
    //    clientThread = new Thread(() =>
    //    {
    //        try
    //        {
    //            twitchClient = new TcpClient("irc.chat.twitch.tv", 6667);
    //            reader = new StreamReader(twitchClient.GetStream());
    //            writer = new StreamWriter(twitchClient.GetStream());

    //            writer.WriteLine($"PASS {oauthToken}");
    //            writer.WriteLine($"NICK {twitchUsername}");
    //            writer.WriteLine($"JOIN #{channelName}");
    //            writer.Flush();

    //            Debug.Log("IRC: Connected to Twitch chat.");

    //            while (true)
    //            {
    //                if (twitchClient.Available > 0 || reader.Peek() >= 0)
    //                {
    //                    string message = reader.ReadLine();
    //                    if (message.Contains("PRIVMSG"))
    //                    {
    //                        int nameEnd = message.IndexOf("!");
    //                        string sender = message.Substring(1, nameEnd - 1);
    //                        int msgIndex = message.IndexOf(":", 1);
    //                        string msg = message.Substring(msgIndex + 1);

    //                        Debug.Log($"{sender}: {msg}");

    //                        if (msg.ToLower().Contains("!boost"))
    //                        {
    //                            UnityMainThreadDispatcher.Instance().Enqueue(() =>
    //                            {
    //                                Debug.Log("IRC: Boost triggered!");
    //                                PlayerManager player = FindObjectOfType<PlayerManager>();
    //                                if (player != null)
    //                                    player.OnBoost();
    //                            });
    //                        }
    //                    }
    //                }
    //            }
    //        }
    //        catch (System.Exception ex)
    //        {
    //            Debug.LogError("IRC Error: " + ex.Message);
    //        }
    //    });

    //    clientThread.IsBackground = true;
    //    clientThread.Start();
    //}

    void OnApplicationQuit()
    {
        if (clientThread != null && clientThread.IsAlive)
            clientThread.Abort();
    }
}