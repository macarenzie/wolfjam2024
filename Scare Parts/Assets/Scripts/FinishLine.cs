using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    public PlayerManager pm;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(pm.transform.position.y > transform.position.y)
        {
            SceneManager.LoadScene("WinScene");
        }
    }
}
