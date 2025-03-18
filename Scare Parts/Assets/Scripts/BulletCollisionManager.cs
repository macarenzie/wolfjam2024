using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollisionManager : MonoBehaviour
{
    // ===  FIELDS ===
    private BulletManager bulletManager;
    private LevelLoader spawner;
    private List <GameObject> bulletList;
    private List <GameObject> enemyList;
    private GameObject tempBullet;
    private GameObject tempEnemy;


    // === METHODS ===
    // Start is called before the first frame update
    void Start()
    {
        // IF THE NAME OF BULLET MANAGER IS CHANGED, YOU NEED TO CHANGE IT HERE TOO
        bulletManager = GameObject.Find("Manager").GetComponent<BulletManager>();
        spawner = GameObject.Find("Manager").GetComponent<LevelLoader>();

        bulletList = bulletManager.bullets;
        enemyList = spawner.Objects;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // if bullet collides with enemy: destroy bullet AND enemy
        if (collision.gameObject.tag == "Enemy")
        {
            // destroy bullet
            for (int i = 0; i < bulletList.Count; i++)
            {
                if (bulletList[i] == gameObject)
                {
                    Destroy(bulletList[i]);
                    bulletList.RemoveAt(i);
                    i--;
                }
            }
            
            // destroy enemy
            for (int i = 0; i < enemyList.Count; i++)
            {
                if (enemyList[i] == collision.gameObject)
                {
                    Destroy(enemyList[i]);
                    enemyList.RemoveAt(i);
                    i--;
                }
            }
        }
        
        // if colliding with anything else: destroy bullet
        else if (collision.gameObject.tag != "Player")
        {
            // destroy bullet
            for (int i = 0; i < bulletList.Count; i++)
            {
                if (bulletList[i] == gameObject)
                {
                    Destroy(bulletList[i]);
                    bulletList.RemoveAt(i);
                    i--;
                }
            }
        }
    }
}
