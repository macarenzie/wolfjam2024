using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // fields
    public delegate void ParallaxCameraDelegate(float deltaMovement);
    public ParallaxCameraDelegate onCameraTranslate;
    private float oldPos;

    void Start()
    {
        oldPos = transform.position.y;
    }

    
    void Update()
    {
        if (transform.position.y != oldPos)
        {
            if (onCameraTranslate != null)
            {
                float delta = oldPos - transform.position.y;
                onCameraTranslate(delta);
            }
            oldPos = transform.position.y;
        }
    }
}
