using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.Threading.Tasks;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject enemyShooter;
    [SerializeField] GameObject enemyShooterElite;

    private List<GameObject> enemyWave = new List<GameObject>();
    private int waveNum = 0;
    private float waveTimer = 0f;
    private float waveInterval = 2f;
    private bool waveEndFlag = false; 

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

        if (waveEndFlag) {waveTimer += Time.deltaTime;}
        Debug.Log(waveTimer);

        if (waveTimer > waveInterval) 
        {
            waveTimer = 0f;
            enemyWave = new List<GameObject>();
            waveNum ++;
            Debug.Log(waveNum);

            if (waveNum % 5 == 1)
            {
                Spawn(enemyShooter, new Vector2(1.5f, 5.5f));
                Spawn(enemyShooter, new Vector2(-1.5f, 5.5f));
            }
            else if (waveNum % 5 == 2)
            {
                Spawn(enemyShooterElite, new Vector2(0f, 5.5f));
            }
            else if (waveNum % 5 == 3)
            {
                Spawn(enemyShooter, new Vector2(-1.5f, 5.5f));
                Spawn(enemyShooter, new Vector2(0f, 6f), 500);
                Spawn(enemyShooter, new Vector2(1.5f, 5.5f));
                
            }
            else if (waveNum % 5 == 4)
            {
                Spawn(enemyShooterElite, new Vector2(1.5f, 5.5f));
                Spawn(enemyShooterElite, new Vector2(-1.5f, 5.5f));
            }
            else if (waveNum % 5 == 0)
            {
                Spawn(enemyShooterElite, new Vector2(1f, 6f), 3000);
                Spawn(enemyShooterElite, new Vector2(-1f, 6f), 3000);
                Spawn(enemyShooter, new Vector2(1.6f, 5.5f));
                Spawn(enemyShooter, new Vector2(-1.6f, 5.5f));
            }
            
        }


    }


    private async void Spawn(GameObject enemy, Vector2 position, int delayTime = 0)
    {
        await Task.Run(() =>
        {
            Task.Delay(delayTime).Wait();
        });
        GameObject spawnedEnemy = Instantiate(enemy, new Vector3(position[0], position[1], 0f), Quaternion.identity);
        enemyWave.Add(spawnedEnemy);
    }

}
