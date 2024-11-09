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
    [SerializeField] GameObject obstacle;
    [SerializeField] GameObject obstacleVertex;

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
            Debug.Log("Current Wave: "+waveNum);

            if (waveNum % 15 == 1)
            {
                Spawn(enemyShooter, new Vector2(1.5f, 5.5f));
                Spawn(enemyShooter, new Vector2(-1.5f, 5.5f));
            }
            else if (waveNum % 15 == 2)
            {
                SpawnObstacle(new Vector2(-2f, 5.5f), new Vector2(2f, 15.5f));
            }
            else if (waveNum % 15 == 3)
            {
                Spawn(enemyShieldShooter, new Vector2(0f, 7f));
            }
            else if (waveNum % 15 == 4)
            {
                Spawn(enemyShooter, new Vector2(-1.5f, 5.5f));
                Spawn(enemyRusherRight, new Vector2(-1.2f, 7f), 500);
                Spawn(enemyShooter, new Vector2(1.5f, 5.5f));
                
            }
            else if (waveNum % 15 == 5)
            {
                Spawn(enemyFlyerRight, new Vector2(-0.85f, 6f), 0);
                Spawn(enemyFlyerRight, new Vector2(-0.85f, 6f), 400);
                Spawn(enemyFlyerRight, new Vector2(-0.85f, 6f), 800);
                Spawn(enemyFlyerLeft, new Vector2(-0.85f, 6f), 0);
                Spawn(enemyFlyerLeft, new Vector2(-0.85f, 6f), 400);
                Spawn(enemyFlyerLeft, new Vector2(-0.85f, 6f), 800);
            }
            else if (waveNum % 15 == 6)
            {
                SpawnObstacle(new Vector2(-2f, 8.5f), new Vector2(2f, 8.5f));
                SpawnObstacle(new Vector2(-2f, 5.5f), new Vector2(2f, 11.5f));
            }
            else if (waveNum % 15 == 7)
            {
                Spawn(enemyShooterElite, new Vector2(1.5f, 5.5f));
                Spawn(enemyRusherRight, new Vector2(-1.2f, 7f), 500);
                Spawn(enemyRusherLeft, new Vector2(1.2f, 7f), 500);
            }
            else if (waveNum % 15 == 8)
            {
                Spawn(enemyFlyerRight, new Vector2(-0.85f, 6f), 0);
                Spawn(enemyFlyerRight, new Vector2(-0.85f, 6f), 400);
                Spawn(enemyShooter, new Vector2(0f, 5.5f));
                Spawn(enemyFlyerLeft, new Vector2(0.85f, 6f), 0);
                Spawn(enemyFlyerLeft, new Vector2(0.85f, 6f), 400);

            }
            else if (waveNum % 15 == 9)
            {
                Spawn(enemyShooterElite, new Vector2(1.5f, 5.5f));
                Spawn(enemyShooterElite, new Vector2(-1.5f, 5.5f));
            }
            else if (waveNum % 15 == 10)
            {
                SpawnObstacle(new Vector2(-2f, 5.5f), new Vector2(2f, 5.5f));
                SpawnObstacle(new Vector2(-2f, 5.5f), new Vector2(2f, 10.5f));
            }
            else if (waveNum % 15 == 11)
            {
                Spawn(enemyShooterElite, new Vector2(0f, 7f));
                Spawn(enemyRusherRight, new Vector2(-1.2f, 7f), 500);
                Spawn(enemyRusherLeft, new Vector2(1.2f, 7f), 500);
            }
            else if (waveNum % 15 == 12)
            {
                Spawn(enemyShooter, new Vector2(1.5f, 5.5f));
                Spawn(enemyShooter, new Vector2(-1.5f, 5.5f));
                Spawn(enemyShieldShooter, new Vector2(0.5f, 7f));
                Spawn(enemyShieldShooter, new Vector2(-0.5f, 7f));
                
            }
            else if (waveNum % 15 == 13)
            {
                SpawnObstacle(new Vector2(-2f, 5.5f), new Vector2(2f, 5.5f));
                SpawnObstacle(new Vector2(-2f, 10.5f), new Vector2(2f, 10.5f));
                SpawnObstacle(new Vector2(-2f, 15.5f), new Vector2(2f, 15.5f));
            }
            else if (waveNum % 15 == 14)
            {
                Spawn(enemyShieldShooter, new Vector2(1.5f, 5.5f));
                Spawn(enemyFlyerRight, new Vector2(-0.85f, 6f), 0);
                Spawn(enemyFlyerRight, new Vector2(-0.85f, 6f), 400);
                Spawn(enemyFlyerRight, new Vector2(-0.85f, 6f), 800);
                Spawn(enemyFlyerRight, new Vector2(-0.85f, 6f), 1200);
                Spawn(enemyFlyerRight, new Vector2(-0.85f, 6f), 1600);
                Spawn(enemyFlyerLeft, new Vector2(-0.85f, 6f), 0);
                Spawn(enemyFlyerLeft, new Vector2(-0.85f, 6f), 400);
                Spawn(enemyFlyerLeft, new Vector2(-0.85f, 6f), 800);
                Spawn(enemyFlyerLeft, new Vector2(-0.85f, 6f), 1200);
                Spawn(enemyFlyerLeft, new Vector2(-0.85f, 6f), 1600);
            }
            else if (waveNum % 15 == 0)
            {
                Spawn(enemyShieldShooterElite, new Vector2(0f, 7f));
                Spawn(enemyRusherRight, new Vector2(-1.2f, 7f), 500);
                Spawn(enemyRusherLeft, new Vector2(1.2f, 7f), 500);
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

    private async void SpawnObstacle(Vector2 position1, Vector2 position2, int delayTime = 0)
    {
        GameObject obstacleVertex1 = Instantiate(obstacleVertex, new Vector3(position1[0], position1[1], 0f), Quaternion.identity);
        GameObject obstacleVertex2 = Instantiate(obstacleVertex, new Vector3(position2[0], position2[1], 0f), Quaternion.identity);
        var angle = Mathf.Atan2(position1[0]-position2[0], position1[1]-position2[1]);
        angle = -180 * angle / Mathf.PI + 90;
        GameObject spawnedObstacle = Instantiate(obstacle, (new Vector3(position1[0], position1[1], 0f) + new Vector3(position2[0], position2[1], 0f))/2f, Quaternion.Euler(0f,0f,angle));
        spawnedObstacle.transform.localScale = new Vector3(spawnedObstacle.transform.localScale.x*(new Vector3(position1[0], position1[1], 0f) - new Vector3(position2[0], position2[1], 0f)).magnitude*0.3f, spawnedObstacle.transform.localScale.y, spawnedObstacle.transform.localScale.z);  

        enemyWave.Add(obstacleVertex1);
        enemyWave.Add(obstacleVertex2);
        enemyWave.Add(spawnedObstacle);
    }

}
