using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Net.Sockets;
using System.IO;
using System.Globalization;
/// <summary>
/// Name: Twitch Connect
/// Purpose: Connect to specfied streamer's twitch chat and read chatlogs to parge commands
/// Author(s): Katie Hellmann, PabloMakes Twitch Integration Tutorial
/// </summary>
public class TwitchConnect : MonoBehaviour
{
    //call this event to check for commands
    public UnityEvent<string, string> OnChatMessage;
    TcpClient TwitchTV;
    StreamReader Reader;
    StreamWriter Writer;

    //internet things
    private float pingCounter = 0;
    const string URL = "irc.chat.twitch.tv";
    const int PORT = 6667;

    private string user;
    //from https://twitchtokengenerator.com 
    private string oauth;
    private string channel;

    /// <summary>
    /// A function to Connect to Twitch
    /// </summary>
    private void ConnectToTwitch()
    {
        TwitchTV = new TcpClient(URL, PORT);
        Reader = new StreamReader(TwitchTV.GetStream());
        Writer = new StreamWriter(TwitchTV.GetStream());

        Writer.WriteLine("PASS " + oauth);
        Writer.WriteLine("NICK " + user.ToLower());
        Writer.WriteLine("JOIN #" + channel.ToLower());
        Writer.Flush();
    }

    private void Awake()
    {
        ConnectToTwitch();
    }
    // Update is called once per frame
    void Update()
    {
        pingCounter += Time.deltaTime; //count time
        //constantly reconnect
        if (pingCounter > 60)
        {
            Writer.WriteLine("PING " + URL);
            Writer.Flush();
            pingCounter = 0;
        }
        //if not connected, connect to twitch
        if (!TwitchTV.Connected)
        {
            ConnectToTwitch();
        }
        //if available parse commands
        if (TwitchTV.Available > 0)
        {
            string message = Reader.ReadLine();
            if (message.Contains("PRIVMSG"))
            {
                int splitPoint = message.IndexOf("!");
                string chatter = message.Substring(1, splitPoint - 1);
                splitPoint = message.IndexOf(":", 1);
                string msg = message.Substring(splitPoint + 1);

                Debug.Log($"[Twitch Message] {chatter}: {msg}");
                if (OnChatMessage != null)
                {
                    Debug.Log("OnChatMessage is NOT null. Invoking...");
                    OnChatMessage.Invoke(chatter, msg);
                }
                else
                {
                    Debug.LogWarning("OnChatMessage is NULL. Nothing to invoke.");
                }


            }
            //print to unity console for debugging purposes
            print(message);
        }
    }
}

