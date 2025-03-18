using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.FilePathAttribute;

public class LevelLoader : MonoBehaviour
{
    // === FIELDS ===
    //[SerializeField] List<GameObject> objectTypes = new List<GameObject>();
    [SerializeField] private TextAsset level;
    [SerializeField] private float clock;

    private string originalText;
    private string[] lines;
    private List<List<char>> lanes = new List<List<char>>();
    private float timer = 1.0f;
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
        LevelToLane();

        // assign spawn locations
        spawnLocations[0] = new Vector2(-7.43f, 0);
        for (int i = 1; i < spawnLocations.Length; i++)
        {
            spawnLocations[i] = new Vector2(spawnLocations[0].x + (i * 3.71f), 15);
        }

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

    // Update is called once per frame
    void Update()
    {
        
    }

    void LevelToLane()
    {
        for (int i = 0; i < lines.Length; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                lanes[j].Add(lines[i][j]);
            }
        }
    }

    void Spawn()
    {
        if (timer < 0)
        {
            for(int i = 0; i < 5; i++)
            {
            }
            //location = new Vector2(Random.Range(lowerBound, upperBound), 15);
            //Objects.Add(Instantiate(objectTypes[Random.Range(0, objectTypes.Count)], location, Quaternion.identity));
            //timer = clock;
        }
    }
}
