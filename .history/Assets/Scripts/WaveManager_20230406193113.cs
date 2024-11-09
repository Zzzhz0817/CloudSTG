using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.Threading.Tasks;

public class WaveManager : MonoBehaviour
{
    [SerializeField] GameObject enemyShooter;
    [SerializeField] GameObject enemyShooterElite;
    [SerializeField] GameObject enemyShieldShooter;
    [SerializeField] GameObject enemyShieldShooterElite;
    [SerializeField] GameObject enemyRusherRight;
    [SerializeField] GameObject enemyRusherLeft;
    [SerializeField] GameObject enemyFlyerRight;
    [SerializeField] GameObject enemyFlyerLeft;

    private List<GameObject> enemyWave = new List<GameObject>();
    public int waveNum = 0;
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

        if (waveTimer > waveInterval) 
        {
            waveTimer = 0f;
            enemyWave = new List<GameObject>();
            waveNum ++;

            if (waveNum % 10 == 1)
            {
                Spawn(enemyShooter, new Vector2(1.5f, 5.5f));
                Spawn(enemyShooter, new Vector2(-1.5f, 5.5f));
            }
            else if (waveNum % 10 == 2)
            {
                Spawn(enemyShieldShooter, new Vector2(0f, 7f));
            }
            else if (waveNum % 10 == 3)
            {
                Spawn(enemyShooter, new Vector2(-1.5f, 5.5f));
                Spawn(enemyRusherRight, new Vector2(-1.2f, 7f), 500);
                Spawn(enemyShooter, new Vector2(1.5f, 5.5f));
                
            }
            else if (waveNum % 10 == 4)
            {
                Spawn(enemyShooterElite, new Vector2(1.5f, 5.5f));
                Spawn(enemyRusherRight, new Vector2(-1.2f, 7f), 500);
                Spawn(enemyRusherLeft, new Vector2(1.2f, 7f), 500);
            }
            else if (waveNum % 10 == 5)
            {
                Spawn(enemyFlyerRight, new Vector2(-0.85f, 6f), 0);
                Spawn(enemyFlyerRight, new Vector2(-0.85f, 6f), 400);
                Spawn(enemyShooter, new Vector2(0f, 5.5f));
                Spawn(enemyFlyerLeft, new Vector2(0.85f, 6f), 0);
                Spawn(enemyFlyerLeft, new Vector2(0.85f, 6f), 400);

            }
            else if (waveNum % 10 == 6)
            {
                Spawn(enemyShooterElite, new Vector2(1.5f, 5.5f));
                Spawn(enemyShooterElite, new Vector2(-1.5f, 5.5f));
            }
            else if (waveNum % 10 == 7)
            {
                Spawn(enemyShooterElite, new Vector2(0f, 7f));
                Spawn(enemyRusherRight, new Vector2(-1.2f, 7f), 500);
                Spawn(enemyRusherLeft, new Vector2(1.2f, 7f), 500);
            }
            else if (waveNum % 10 == 8)
            {
                Spawn(enemyShooter, new Vector2(1.5f, 5.5f));
                Spawn(enemyShooter, new Vector2(-1.5f, 5.5f));
                Spawn(enemyShieldShooter, new Vector2(0.5f, 7f));
                Spawn(enemyShieldShooter, new Vector2(-0.5f, 7f));
                
            }
            else if (waveNum % 10 == 9)
            {
                Spawn(enemyShieldShooter, new Vector2(1.5f, 5.5f));
                Spawn(enemyFlyerRight, new Vector2(-0.85f, 6f), 0);
                Spawn(enemyFlyerRight, new Vector2(-0.85f, 6f), 400);
                Spawn(enemyFlyerRight, new Vector2(-0.85f, 6f), 800);
                Spawn(enemyFlyerLeft, new Vector2(-0.85f, 6f), 0);
                Spawn(enemyFlyerLeft, new Vector2(-0.85f, 6f), 400);
                Spawn(enemyFlyerLeft, new Vector2(-0.85f, 6f), 800);
            }
            else if (waveNum % 10 == 0)
            {
                Spawn(enemyFlyerRight, new Vector2(-0.85f, 6f), 0);
                Spawn(enemyFlyerRight, new Vector2(-0.85f, 6f), 400);
                Spawn(enemyFlyerRight, new Vector2(-0.85f, 6f), 800);
                Spawn(enemyShooter, new Vector2(0f, 5.5f));
                Spawn(enemyFlyerLeft, new Vector2(0.85f, 6f), 0);
                Spawn(enemyFlyerLeft, new Vector2(0.85f, 6f), 400);
                Spawn(enemyFlyerLeft, new Vector2(0.85f, 6f), 800);
                Spawn(enemyFlyerLeft, new Vector2(0.85f, 6f), 1200);
                Spawn(enemyFlyerLeft, new Vector2(0.85f, 6f), 1600);
                Spawn(enemyFlyerLeft, new Vector2(0.85f, 6f), 2000);
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
