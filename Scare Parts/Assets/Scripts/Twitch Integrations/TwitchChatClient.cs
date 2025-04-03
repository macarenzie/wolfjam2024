using UnityEngine;
using System.IO;
using System.Net.Sockets;
using System.Threading;

public class TwitchChatClient : MonoBehaviour
{
    [Header("Twitch IRC Settings")]
    public string twitchUsername = "bot_username";
    public string oauthToken = "oauth:your_token_here"; //via https://antiscuff.com/oauth/
    public string channelName = "channel_name";

    private TcpClient twitchClient;
    private StreamReader reader;
    private StreamWriter writer;
    private Thread clientThread;

    void Start()
    {
        Debug.Log("TwitchChatClient: Start() called.");
        Connect();
    }

    void OnApplicationQuit()
    {
        if (clientThread != null && clientThread.IsAlive)
            clientThread.Abort();
    }

    void Connect()
    {
        Debug.Log("TwitchChatClient: Connect() called.");
        clientThread = new Thread(() =>
        {
            try
            {
                twitchClient = new TcpClient("irc.chat.twitch.tv", 6667);
                reader = new StreamReader(twitchClient.GetStream());
                writer = new StreamWriter(twitchClient.GetStream());

                writer.WriteLine($"PASS {oauthToken}");
                writer.WriteLine($"NICK {twitchUsername.ToLower()}");
                writer.WriteLine("CAP REQ :twitch.tv/tags twitch.tv/commands twitch.tv/membership");
                writer.WriteLine($"JOIN #{channelName.ToLower()}");
                writer.Flush();

                UnityMainThreadDispatcher.Instance().Enqueue(() =>
                    Debug.Log("IRC: Connected to Twitch chat.")
                );

                while (true)
                {
                    while ((twitchClient != null && twitchClient.Connected))
                    {
                        string message = reader.ReadLine();

                        UnityMainThreadDispatcher.Instance().Enqueue(() =>
                            Debug.Log("IRC RAW: " + message)
                        );

                        UnityMainThreadDispatcher.Instance().Enqueue(() =>
                            Debug.Log("IRC RAW: " + message)
                        );

                        if (message.Contains("PRIVMSG"))
                        {
                            int nameEnd = message.IndexOf("!");
                            string sender = message.Substring(1, nameEnd - 1);
                            int msgIndex = message.IndexOf(":", 1);
                            string msg = message.Substring(msgIndex + 1);

                            UnityMainThreadDispatcher.Instance().Enqueue(() =>
                            {
                                Debug.Log($"{sender}: {msg}");

                                if (msg.ToLower().Contains("!boost"))
                                {
                                    Debug.Log("IRC: Boost triggered!");
                                    PlayerManager player = FindObjectOfType<PlayerManager>();
                                    if (player != null)
                                        player.OnBoost();
                                    else
                                        Debug.LogWarning("No PlayerManager found in scene.");
                                }
                            });
                        }
                    }
                }
            }
            catch (System.Exception e)
            {
                UnityMainThreadDispatcher.Instance().Enqueue(() =>
                {
                    Debug.LogError("IRC Exception: " + e.Message);
                    Debug.LogError("Stack Trace: " + e.StackTrace);
                });
            }
        });

        clientThread.IsBackground = true;
        clientThread.Start();
    }
}