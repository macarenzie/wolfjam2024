using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DylanCollisions : MonoBehaviour
{
    // === FIELDS ===




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnCollisionEnter2D(Collision2D collision)

    {

        // Access information about the colliding object using 'collision' [2, 5, 12]

        Debug.Log("Collision with: " + collision.gameObject.name);



        // Implement your collision response logic here, like playing a sound, applying force, etc [5, 12, 16]. 

    }
}
