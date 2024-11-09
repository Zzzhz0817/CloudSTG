using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject enemyShooter;
    [SerializeField] GameObject enemyShooterElite;

    private List<GameObject> enemyWave = new List<GameObject>();
    private int waveNum = 0;
    private float waveTimer = 0f;
    private bool waveEndFlag; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        waveEndFlag = true;
        foreach (GameObject enemy in enemyWave)
        {
            if (enemy)
            {
                waveEndFlag = false;
            }
        }
        
        if (waveEndFlag) 
        {
            enemyWave = new List<GameObject>();
            waveNum ++;

            if (waveNum % 5 == 1)
            {
                GameObject enemy1 = Instantiate(enemyShooter, new Vector3(-1.5f, 5.5f, 0f), Quaternion.identity);
                enemyWave.Add(enemy1);
                GameObject enemy2 = Instantiate(enemyShooter, new Vector3(1.5f, 5.5f, 0f), Quaternion.identity);
                enemyWave.Add(enemy2);
            }
            
        }


    }

    private void SpawnEnemy(GameObject enemy, Vector2 position)
    {
        GameObject spawnedEnemy = Instantiate(enemy, new Vector3(position[0], position[1], 0f), Quaternion.identity);
        this.enemyWave.Add(spawnedEnemy);
    }

}
