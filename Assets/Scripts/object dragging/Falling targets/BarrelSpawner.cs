using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelSpawner : MonoBehaviour
{

    public Rigidbody barrelPrefab;
    public float globalClock = 5f;
    public float difficultyMultiplier = 1f;
    public float repeatingMin = 0.2f;
    public float repeatingMax = 1f;
    Vector3 position;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("BarrelSpawn", 0.1f * Random.Range(1f,1.5f), Random.Range(repeatingMin * difficultyMultiplier, repeatingMax * difficultyMultiplier));
    }
    void BarrelSpawn()
    {
        var position = new Vector3(Random.Range(-15f, 15f), 10, 10);
        Rigidbody instance = Instantiate(barrelPrefab, position, Quaternion.identity);
    }
}
