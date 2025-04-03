using TMPro;

using UnityEngine;

/// <summary>
/// SampleEvent is no longer needed when Twitch API is hooked directly via TwitchInitializer.cs.
/// This is a basic placeholder for separate event handling.
/// </summary>
public class SampleEvent : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI chatDisplay;

    public void DisplayMessage(string chatter, string message)
    {
        if (chatDisplay != null)
        {
            chatDisplay.text = $"{chatter}: {message}";
        }
    }
}