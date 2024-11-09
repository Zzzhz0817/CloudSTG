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
            Debug.Log(waveNum);

            if (waveNum % 5 == 1)
            {
                Spawn(enemyShooter, new Vector2(1.5f, 5.5f), 2f);
                Spawn(enemyShooter, new Vector2(-1.5f, 5.5f), 2f);
            }
            else if (waveNum % 5 == 2)
            {
                Spawn(enemyShooterElite, new Vector2(0f, 5.5f), 2f);
            }
            else if (waveNum % 5 == 3)
            {
                Spawn(enemyShooter, new Vector2(1.5f, 5.5f), 2f);
                Spawn(enemyShooter, new Vector2(0f, 6f), 2.5f);
                Spawn(enemyShooter, new Vector2(1.5f, 5.5f), 2f);
                
            }
            else if (waveNum % 5 == 4)
            {
                Spawn(enemyShooterElite, new Vector2(1.5f, 5.5f), 2f);
                Spawn(enemyShooterElite, new Vector2(-1.5f, 5.5f), 2f);
            }
            else if (waveNum % 5 == 0)
            {
                Spawn(enemyShooterElite, new Vector2(1f, 6f), 5f);
                Spawn(enemyShooterElite, new Vector2(-1f, 6f), 5f);
                Spawn(enemyShooter, new Vector2(1.6f, 5.5f), 2f);
                Spawn(enemyShooter, new Vector2(-1.6f, 5.5f), 2f);
            }
            
        }


    }

    private IEnumerator Spawn(GameObject enemy, Vector2 position, float delayTime)
    {
        this.enemyWave.Add(spawnedEnemy);
        yield return new WaitForSeconds(delayTime);
        GameObject spawnedEnemy = Instantiate(enemy, new Vector3(position[0], position[1], 0f), Quaternion.identity);
    }

}
