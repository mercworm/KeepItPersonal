using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreetLights : MonoBehaviour {

    public GameObject lightPrefab;
    public Transform lightSpawn;

    public float spawnRate;

    private void OnEnable()
    {
        InvokeRepeating("SpawnLight", 0f, spawnRate);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    public void SpawnLight ()
    {
        Instantiate(lightPrefab, lightSpawn);
    }
}
