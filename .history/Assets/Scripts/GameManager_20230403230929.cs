using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject enemyShooter;
    [SerializeField] GameObject enemyShooterElite;

    private List<GameObject> enemyWave = new List<GameObject>();
    private int waveCount = 0;
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
            waveCount ++;
            SpawnNewWave(waveCount);
        }
    }

    private void SpawnNewWave(int waveCount)
    {
        float waveTime = 0f;
        
        if (waveCount == 1)
        {
            GameObject enemy1 = Instantiate(enemyShooter, new Vector3(-1.5f, 5.5f, 0f), Quaternion.identity);
            enemyWave.Add(enemy1);
            GameObject enemy2 = Instantiate(enemyShooter, new Vector3(1.5f, 5.5f, 0f), Quaternion.identity);
            enemyWave.Add(enemy2);

        }
    }

}
