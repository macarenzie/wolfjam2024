using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;

public class SampleEvent : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI m_TextMeshProUGUI1;



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
        if (pMessage.Contains("!cmd"))
        {
            //change text
            m_TextMeshProUGUI1.text = pChatter + " sent a cmd";
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
