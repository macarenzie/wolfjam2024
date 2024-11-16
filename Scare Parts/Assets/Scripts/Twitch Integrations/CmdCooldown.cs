using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Name: Cmd Coldown
/// Purpose: Set twitch command cooldowns to prevent event spamming and overloading the api
/// Author(s): Katie Hellmann, GucioDevs Unity3D Quick Tips: Cooldown Timers
/// </summary>

public class CmdCooldown : MonoBehaviour
{
    private float shootCooldownTime;
    private float switchCooldownTime;
    private float testCooldownTime;
    [SerializeField] private float _timeTilNextShoot = 5;
    [SerializeField] private float _timeTilNextSwitch = 5;
    [SerializeField] private float _timeTilNextTest = 1000;

    private bool canUseShoot;
    private bool canUseSwitch;
    private bool canUseTest;


    private void Start()
    {
        shootCooldownTime = 1;
        switchCooldownTime = 1;
        testCooldownTime = 1;

        canUseShoot = true;
        canUseTest = true;
        canUseSwitch = true;
    }
    private void Update()
    {
        //this is redundant, would refactor later but just to ensure it works:
        print(canUseTest);
        if (shootCooldownTime > 0)
        {
            shootCooldownTime -= Time.deltaTime;
        }
        if (shootCooldownTime < 0)
        {
            shootCooldownTime = 0;
        }

        if (switchCooldownTime > 0)
        {
            switchCooldownTime -= Time.deltaTime;
        }
        if (switchCooldownTime < 0)
        {
            switchCooldownTime = 0;
        }


        if (testCooldownTime > 0)
        {
            testCooldownTime -= Time.deltaTime;
        }
        if (testCooldownTime < 0)
        {
            testCooldownTime = 0;
        }



        if (shootCooldownTime <= 0)
        {
            canUseShoot = true;
        }
        if (testCooldownTime <= 0)
        {
            canUseTest = true;
        }
        if (switchCooldownTime <= 0)
        {
            canUseSwitch = true;
        }


    }

    public void OnChatMessage(string pChatter, string pMessage)
    {
        //check the command
        if (pMessage.Contains("!cmd") && canUseTest)
        {
            canUseTest = false;
            testCooldownTime = _timeTilNextTest;
        }
        else if (pMessage.Contains("!cmd") && !canUseTest)
        {
            print("cant use");
        }
        //check the command
        else if (pMessage.Contains("!shoot") && canUseShoot)
        {
            shootCooldownTime = _timeTilNextShoot;
            canUseShoot = false;
        }
        //check the command
        else if (pMessage.Contains("!switch") && canUseSwitch)
        {
            switchCooldownTime = _timeTilNextSwitch;
            canUseSwitch = false;
        }
       

    }


    public bool CanShoot()
    {
        return canUseShoot;
    }
    public bool CanSwitch()
    {
        return canUseSwitch;
    }
    public bool CanTest()
    {
        return canUseTest;
    }

}
