using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.FilePathAttribute;

public class LevelLoader : MonoBehaviour
{
    // === FIELDS ===
    [SerializeField] List<GameObject> objectTypes = new List<GameObject>();
    [SerializeField] private TextAsset level;

    private string originalText;
    private int count = 0;
    private string[] lines;
    private GameObject[,] lanes;
    private float timer = 1.5f;
    private Vector2[] spawnLocations = new Vector2[5]; // [0] is lane 0, [1] is lane 1, etc.
    private GameObject obj;


    // === PROPERTIES ===
    public List<GameObject> Objects
    {
        get { return _objects; }
    }
    private List<GameObject> _objects = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        originalText = level.text;
        lines = level.text.Split('\n');
        lanes = new GameObject[lines.Length, 5];
        LevelToLane();

        // assign spawn locations
        for (int i = 0; i < spawnLocations.Length; i++)
        {
            spawnLocations[i] = new Vector2(-7.43f + (i * 3.71f), 15);
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if(count < lines.Length)
        {
            Spawn();
        }
        else if (Objects.Count == 0)
        {
            SceneManager.LoadScene("WinScene");
        }

        // destroy of bounds obstacles
        for (int i = 0; i < Objects.Count; i++)
        {
            if (Objects[i].GetComponent<SpriteRenderer>().bounds.center.y < -10)
            {
                Destroy(Objects[i]);
                Objects.RemoveAt(i);
                i--;
                continue;
            }
        }
    }

    void LevelToLane()
    {
        // assign obstacles to their lanes
        for (int i = 0; i < lines.Length; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                switch (lines[i][j])
                {
                    // obstacle
                    case '0':
                        lanes[i,j] = objectTypes[0];
                        //lanes[i].Add(objectTypes[0]);
                        break;
                    // enemy
                    case '1':
                        lanes[i,j] = objectTypes[1];
                        //lanes[i].Add(objectTypes[1]);
                        break;
                    // null
                    default:
                        lanes[i,j] = null;
                        //lanes[i].Add(null);
                        break;
                }
            }
        }
    }

    void Spawn()
    {
        if (timer < 0)
        {
            for(int i = 0; i < 5; i++)
            {
               // if there is an object in the lane, spawn it in
               if (lanes[count,i] != null)
               {
                   Objects.Add(Instantiate(lanes[count,i], spawnLocations[i], Quaternion.identity));
               }
            }

            timer = 1.5f;
            count++;
            Debug.Log(count);
        }
    }
}
