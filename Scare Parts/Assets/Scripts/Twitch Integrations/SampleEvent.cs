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
        
    }

    //OnChatMessage Event 
    //pChatter is chatter username
    //pMessage is chatter's message
    public void OnChatMessage(string pChatter, string pMessage)
    {
        //check the command
        if (pMessage.Contains("!cmd") && cooldownObj.CanTest())
        {
            //change text
            m_TextMeshProUGUI1.text = pChatter + " sent a cmd, can use command? " + cooldownObj.CanTest();
        }
        else if (pMessage.Contains("!cmd") && !cooldownObj.CanTest())
        {
            m_TextMeshProUGUI1.text = pChatter + " sent a cmd, can use command? " + cooldownObj.CanTest();
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
