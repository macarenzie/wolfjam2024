using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // fields
    [SerializeField] private Transform transform;
    [SerializeField] private float speed = 0.5f;

    void Start()
    {
        //transform.position = new Vector3(0, -4, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
            if (Input.GetKey(KeyCode.RightArrow) && transform.localPosition.x < 3)
            {
                transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
            }
            if (Input.GetKey(KeyCode.LeftArrow) && transform.localPosition.x > -3)
            {
                transform.position += new Vector3(-speed * Time.deltaTime, 0, 0);
            }
        
        
    }
}
