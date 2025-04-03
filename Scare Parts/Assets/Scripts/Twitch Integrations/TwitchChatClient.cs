using UnityEngine;
using System.IO;
using System.Net.Sockets;
using System.Threading;

public class TwitchChatClient : MonoBehaviour
{
    [Header("Twitch Settings")]
    public string twitchUsername = "username";
    public string oauthToken = "oauth:token_here";
    public string channelName = "channel_name";

    private TcpClient twitchClient;
    private StreamReader reader;
    private StreamWriter writer;

    private Thread clientThread;

    void Start()
    {
        Connect();
    }

    void OnApplicationQuit()
    {
        if (clientThread != null && clientThread.IsAlive)
            clientThread.Abort();
    }

    void Connect()
    {
        clientThread = new Thread(() =>
        {
            try
            {
                twitchClient = new TcpClient("irc.chat.twitch.tv", 6667);
                reader = new StreamReader(twitchClient.GetStream());
                writer = new StreamWriter(twitchClient.GetStream());

                writer.WriteLine($"PASS {oauthToken}");
                writer.WriteLine($"NICK {twitchUsername.ToLower()}");
                writer.WriteLine($"JOIN #{channelName.ToLower()}");
                writer.Flush();

                Debug.Log("Connected to Twitch chat!");

                while (true)
                {
                    if (twitchClient.Available > 0 || reader.Peek() >= 0)
                    {
                        UnityMainThreadDispatcher.Instance().Enqueue(() =>
                            Debug.Log("IRC is actively polling chat..."));

                        string message = reader.ReadLine();
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
                                   Debug.LogWarning("No PlayerManager found.");
                               }
                            });
                        }
                    }
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError("Twitch IRC Error: " + e.Message);
            }
        });

        clientThread.IsBackground = true;
        clientThread.Start();
    }
}
