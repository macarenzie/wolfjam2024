using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollisionManager : MonoBehaviour
{
    // ===  FIELDS ===
    [SerializeField] private BulletManager bulletManager;
    [SerializeField] private ObjectSpawner spawner;


    // === METHODS ===
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
        // THIS IS BROKEN! I KEEP GETTING AN OUT OF BOUNDS ERROR!!!
        //if (collision.gameObject.tag == "Enemy")
        //{
        //    Debug.Log("yasssss");
        //    Destroy(collision.gameObject);
        //
        //    for (int i = 0; i < spawner.Objects.Count; i++)
        //    {
        //        if (spawner.Objects[i] == gameObject)
        //        {
        //            Destroy(spawner.Objects[i]);
        //            spawner.Objects.RemoveAt(i);
        //            i--;
        //            continue;
        //        }
        //    }
        //
        //    for (int i = 0; i < bulletManager.bullets.Count; i++)
        //    {
        //        if (bulletManager.bullets[i] == gameObject)
        //        {
        //            Destroy(bulletManager.bullets[i]);
        //            bulletManager.bullets.RemoveAt(i);
        //            i--;
        //            continue;
        //        }
        //    }
        //}
        //
        //else if (collision.gameObject.tag != "Player")
        //{
        //    for (int i = 0; i < bulletManager.bullets.Count; i++)
        //    {
        //        if (bulletManager.bullets[i] == gameObject)
        //        {
        //            Destroy(bulletManager.bullets[i]);
        //            bulletManager.bullets.RemoveAt(i);
        //            i--;
        //            continue;
        //        }
        //    }
        //}
    }
}
