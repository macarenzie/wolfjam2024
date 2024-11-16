using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Scrolling : MonoBehaviour
{
    // fields
    private float length;
    [SerializeField] private Camera cam;

    void Start()
    {
        length = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float spriteTopY = GetComponent<SpriteRenderer>().bounds.max.y;
        float camBottomY = cam.transform.position.y - cam.orthographicSize;

        // figure out if the top of the sprite has reached the bottom of the cam
        if (spriteTopY < camBottomY)
        {
            transform.position = new Vector3(
                transform.position.x, 
                transform.position.y + length * 2, 
                transform.position.z);
        }



        // calculate distance moved based on cam movement
        //float distance = car.transform.position.y * speed;
        //float movement = car.transform.position.y * (1 - speed);
        //
        //transform.position = new Vector3(
        //    transform.position.x, 
        //    startPos + distance, 
        //    transform.position.z);
        //
        //if (movement > startPos + length)
        //{
        //    startPos += length;
        //}
        //else if (movement < startPos - length)
        //{
        //    startPos -= length;
        //}
    }
}
