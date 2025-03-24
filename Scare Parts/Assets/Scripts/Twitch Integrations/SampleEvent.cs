using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
/// <summary>
/// Name: Sample Event
/// Purpose: to test and demonstrate that TwitchConnect is working, 
/// changes text to reflect the user who sent the '!cmd' command 
/// Author(s): Katie Hellmann, PabloMakes Twitch Integration Tutorial
/// </summary>
public class SampleEvent : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI m_TextMeshProUGUI1;

    [SerializeField] CmdCooldown cooldownObj;

    // Start is called before the first frame update
    void Start()
    {
        TwitchConnect twitch = FindObjectOfType<TwitchConnect>();
        if (twitch != null)
        {
            twitch.OnChatMessage.AddListener(OnChatMessage);
            Debug.Log("SampleEvent registered for OnChatMessage.");
        }
        else
        {
            Debug.LogError("TwitchConnect not found in the scene.");
        }


    }

    //OnChatMessage Event 
    //pChatter is chatter username
    //pMessage is chatter's message
    public void OnChatMessage(string pChatter, string pMessage)
    {
        Debug.Log($"[Twitch Chat] {pChatter} said: {pMessage}"); //can be commented out after testing
        if (pMessage == "!boost")
        {
            PlayerManager playerManager = FindObjectOfType<PlayerManager>();
            if (playerManager != null)
            {
                playerManager.OnBoost();
                Debug.Log($"Boost triggered by {pChatter}!");
            }
            else
            {
                Debug.LogWarning("PlayerManager not found!");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}

