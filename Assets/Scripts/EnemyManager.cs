using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    public PlayerHealth playerHealth;
    public GameObject enemy;
    public float spawnDelay = 1;
    public float spawnInterval = 3;
    public Transform[] spawnPoint;

	// Use this for initialization
	void Start () {
        InvokeRepeating("Spawn", spawnDelay, spawnInterval);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //怪物生成
    void Spawn()
    {
        if (playerHealth.curHP <= 0)
            return;

        int index = Random.Range(0, spawnPoint.Length);
        Instantiate(enemy, spawnPoint[index].position, spawnPoint[index].rotation);
    }
}
