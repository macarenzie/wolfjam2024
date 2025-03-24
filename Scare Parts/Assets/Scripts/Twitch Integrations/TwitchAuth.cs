/// <summary>
/// Name: Twitch Auth
/// Purpose: Secure credential holding for duration of session
/// Author(s): Katie Hellmann Gator Flack
/// </summary>
using UnityEngine;

public class TwitchAuth : MonoBehaviour
{
    public static string ClientID => System.Environment.GetEnvironmentVariable("TWITCH_CLIENT_ID");
    public static string OAuthToken => System.Environment.GetEnvironmentVariable("TWITCH_OAUTH_TOKEN");
    public static string ChannelName => System.Environment.GetEnvironmentVariable("TWITCH_CHANNEL_NAME");
}

//these enviorment variables need to be set up in unity