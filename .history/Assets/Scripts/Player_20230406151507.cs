using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

public class Player : MonoBehaviour
{
    [SerializeField] GameObject pauseUI;
    [SerializeField] GameObject defultProjectile;
    [SerializeField] GameObject fireProjectile;
    [SerializeField] GameObject waterProjectile;
    [SerializeField] GameObject utimateProjectile;
    [SerializeField] GameObject getHitEffect;
    [SerializeField] GameObject healingEffect;
    [SerializeField] GameObject duiHealingEffect;
    [SerializeField] GameObject genShield;
    private float invincibleTimer = 0f;
    public float invincibleTime = 2f;
    public bool isPaused = false;
    private Vector2 orientationVector = new Vector2(0, 1);
    private float orientationAngle = 90f;

    private float shotTimer = 0f;

    public float shotInterval;
    public int shotSpeed;

    private int MAX_HEALTH = 3;
    private int health = 3;
    private int MAX_QI = 8;
    private int qi = 0;
    private int maxShield = 0;
    private int shield = 0;
    private List<bool> yaoList = new List<bool>{}; // true = yang, false = yin
    private Dictionary<string, int> guaDict = new Dictionary<string, int> ()
    {
            {"Kun", 0},
            {"Zhen", 0},
            {"Kan", 0},
            {"Dui", 0},
            {"Gen", 0},
            {"Li", 0},
            {"Xun", 0},
            {"Qian", 0}
    };
    // Kun-earth Zhen-thunder Kan-water Dui-river Gen-mountain Li-fire Xun-wind Qian-heaven

    [System.Serializable]class SaveData
    {
        public int MAX_HEALTH_save;
        public int health_save;
        public int MAX_QI_save;
        public int qi_save;
        public int maxShield_save;
        public int shield_save;
        public List<bool> yaoList_save;
        public List<int> guaList_save;
    }

    void Start()
    {
        LoadFromPlayerPrefs();
        genShield.GetComponent<GenShield>().SetLayer(shield);
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        invincibleTimer += Time.deltaTime;

        if (health <= 0)
        {
            Die();
        }

        if (Input.touchCount == 0)
        {
            isPaused = true;

            if (!pauseUI.activeSelf)
            {
                SaveByPlayerPrefs();
            }

            pauseUI.SetActive(true);
            Time.timeScale = 0f;
        }        

        if (!isPaused)
        {
            if (Input.touchCount == 1)
            {
                Vector2 world_pos = ScreenToWorld(Input.GetTouch(0).position);
                transform.position = world_pos;
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                orientationVector = new Vector2(0, 1);
                orientationAngle = 90;
            }
            else if (Input.touchCount == 2)
            {   
                Vector2 world_pos0, world_pos1;
                if (Input.GetTouch(0).position[0] < Input.GetTouch(1).position[0])
                {
                    world_pos0 = ScreenToWorld(Input.GetTouch(0).position);
                    world_pos1 = ScreenToWorld(Input.GetTouch(1).position);
                }
                else
                {
                    world_pos1 = ScreenToWorld(Input.GetTouch(0).position);
                    world_pos0 = ScreenToWorld(Input.GetTouch(1).position);
                }

                Vector2 world_pos = (world_pos0 + world_pos1) / 2;
                
                orientationAngle = Mathf.Atan2(world_pos0[0]-world_pos1[0], world_pos0[1]-world_pos1[1]);
                orientationAngle = -180 * orientationAngle / Mathf.PI;

                transform.position = world_pos;
                transform.rotation = Quaternion.Euler(0f, 0f, orientationAngle - 90f);

                orientationVector = new Vector2(Mathf.Tan(Mathf.PI * (orientationAngle + 90) / -180), 1);
                orientationVector.Normalize();
            }
        }
    
        shotTimer += Time.deltaTime;

        if(shotTimer >= shotInterval)
        {
            Launch();
            shotTimer = 0f;
        }
    }

    private Vector2 ScreenToWorld(Vector2 screen_pos)
    {   
        Camera camera = Camera.main;
        float halfHeight = camera.orthographicSize;
        float halfWidth = camera.aspect * halfHeight;

        float y = 2 * halfHeight * screen_pos[1] / (float) Screen.height - halfHeight;
        float x = 2 * halfWidth * screen_pos[0] / (float) Screen.width - halfWidth;
        Vector2 world_pos = new Vector2(x, y);
        return world_pos;
    }

    private async void Launch()
    {
        orientationVector = new Vector2(Mathf.Tan(Mathf.PI * (orientationAngle + 90) / -180), 1);
        orientationVector.Normalize();
        Vector2 tempOrientationVector;

        for (int i = 0; i <= guaDict["Qian"]; i++)
        {
            for (int j = 0; j <= guaDict["Kun"]; j++)
            {
                float deltaAngle = 15f * j;
        
                if (guaDict["Li"] > 0 && guaDict["Kan"] > 0)
                {
                    GameObject projectileObject1 = Instantiate(utimateProjectile, transform.position, Quaternion.Euler(0f, 0f, orientationAngle-90-deltaAngle));
                    ProjectileUltimate projectile1 = projectileObject1.GetComponent<ProjectileUltimate>();
                    tempOrientationVector = new Vector2(Mathf.Tan(Mathf.PI * (orientationAngle+90-deltaAngle) / -180), 1);
                    tempOrientationVector.Normalize();
                    projectile1.SetLevel(guaDict["Li"], guaDict["Kan"]);
                    projectile1.Launch(tempOrientationVector, shotSpeed);
                }
                else if (guaDict["Li"] > 0)
                {
                    GameObject projectileObject1 = Instantiate(fireProjectile, transform.position, Quaternion.Euler(0f, 0f, orientationAngle-90-deltaAngle));
                    ProjectileFire projectile1 = projectileObject1.GetComponent<ProjectileFire>();
                    tempOrientationVector = new Vector2(Mathf.Tan(Mathf.PI * (orientationAngle+90-deltaAngle) / -180), 1);
                    tempOrientationVector.Normalize();
                    projectile1.SetLevel(guaDict["Li"], guaDict["Kan"]);
                    projectile1.Launch(tempOrientationVector, shotSpeed);
                }
                else if (guaDict["Kan"] > 0)
                {
                    GameObject projectileObject1 = Instantiate(waterProjectile, transform.position, Quaternion.Euler(0f, 0f, orientationAngle-90-deltaAngle));
                    ProjectileWater projectile1 = projectileObject1.GetComponent<ProjectileWater>();
                    tempOrientationVector = new Vector2(Mathf.Tan(Mathf.PI * (orientationAngle+90-deltaAngle) / -180), 1);
                    tempOrientationVector.Normalize();
                    projectile1.SetLevel(guaDict["Li"], guaDict["Kan"]);
                    projectile1.Launch(tempOrientationVector, shotSpeed);
                }
                else
                {
                    GameObject projectileObject1 = Instantiate(defultProjectile, transform.position, Quaternion.Euler(0f, 0f, orientationAngle-90-deltaAngle));
                    Projectile projectile1 = projectileObject1.GetComponent<Projectile>();
                    tempOrientationVector = new Vector2(Mathf.Tan(Mathf.PI * (orientationAngle+90-deltaAngle) / -180), 1);
                    tempOrientationVector.Normalize();
                    projectile1.SetLevel(guaDict["Li"], guaDict["Kan"]);
                    projectile1.Launch(tempOrientationVector, shotSpeed);
                }

                if (j > 0)
                {
                    if (guaDict["Li"] > 0 && guaDict["Kan"] > 0)
                    {
                        GameObject projectileObject2 = Instantiate(utimateProjectile, transform.position, Quaternion.Euler(0f, 0f, orientationAngle-90+deltaAngle));
                        ProjectileUltimate projectile2 = projectileObject2.GetComponent<ProjectileUltimate>();
                        tempOrientationVector = new Vector2(Mathf.Tan(Mathf.PI * (orientationAngle+90+deltaAngle) / -180), 1);
                        tempOrientationVector.Normalize();
                        projectile2.SetLevel(guaDict["Li"], guaDict["Kan"]);
                        projectile2.Launch(tempOrientationVector, shotSpeed);
                    }
                    else if (guaDict["Li"] > 0)
                    {
                        GameObject projectileObject2 = Instantiate(fireProjectile, transform.position, Quaternion.Euler(0f, 0f, orientationAngle-90+deltaAngle));
                        ProjectileFire projectile2 = projectileObject2.GetComponent<ProjectileFire>();
                        tempOrientationVector = new Vector2(Mathf.Tan(Mathf.PI * (orientationAngle+90+deltaAngle) / -180), 1);
                        tempOrientationVector.Normalize();
                        projectile2.SetLevel(guaDict["Li"], guaDict["Kan"]);
                        projectile2.Launch(tempOrientationVector, shotSpeed);
                    }
                    else if (guaDict["Kan"] > 0)
                    {
                        GameObject projectileObject2 = Instantiate(waterProjectile, transform.position, Quaternion.Euler(0f, 0f, orientationAngle-90+deltaAngle));
                        ProjectileWater projectile2 = projectileObject2.GetComponent<ProjectileWater>();
                        tempOrientationVector = new Vector2(Mathf.Tan(Mathf.PI * (orientationAngle+90+deltaAngle) / -180), 1);
                        tempOrientationVector.Normalize();
                        projectile2.SetLevel(guaDict["Li"], guaDict["Kan"]);
                        projectile2.Launch(tempOrientationVector, shotSpeed);
                    }
                    else
                    {
                        GameObject projectileObject2 = Instantiate(defultProjectile, transform.position, Quaternion.Euler(0f, 0f, orientationAngle-90+deltaAngle));
                        Projectile projectile2 = projectileObject2.GetComponent<Projectile>();
                        tempOrientationVector = new Vector2(Mathf.Tan(Mathf.PI * (orientationAngle+90+deltaAngle) / -180), 1);
                        tempOrientationVector.Normalize();
                        projectile2.SetLevel(guaDict["Li"], guaDict["Kan"]);
                        projectile2.Launch(tempOrientationVector, shotSpeed);
                    }
                }
            }

            await Task.Run(() =>
            {
                Task.Delay(50).Wait();
            });
        }
    }

    public void GetQi()
    {
        qi += 1;
        if (qi >= MAX_QI)
        {
            Debug.Log("POWER UP!");
            qi = 0;
        }
    }

    public void GetYao(bool isYang)
    {
        yaoList.Add(isYang);
        Debug.Log(yaoList.Count);

        if (guaDict["Dui"] > 0)
        {
            GetHealed(guaDict["Dui"]);
            var healEffect = Instantiate(duiHealingEffect, transform.position, Quaternion.identity);
            healEffect.transform.parent = gameObject.transform;
        }

        if (yaoList.Count >= 3)
        {
            string key = guaDict.ElementAt((yaoList[0] ? 1 : 0)*4 + (yaoList[1] ? 1 : 0)*2 + (yaoList[2] ? 1 : 0)).Key;
            guaDict[key] ++;
            yaoList = new List<bool>{};
            if (key == "Dui")
            {
                GetHealed(MAX_HEALTH);
                var healEffect = Instantiate(duiHealingEffect, transform.position, Quaternion.identity);
                healEffect.transform.parent = gameObject.transform;
            }
            if (key == "Gen")
            {
                maxShield = guaDict["Gen"];
            }
        }
        
        shield = maxShield;
        genShield.GetComponent<GenShield>().SetLayer(shield);

    }

    public void GetMedicine()
    {
        GetHealed(1);
    }

    public void GetHealed(int heal)
    {
        if (health < MAX_HEALTH)
        {
            if (health + heal <= MAX_HEALTH)
            {
                health += heal;
            }
            else
            {
                health = MAX_HEALTH;
            }
            
            Debug.Log("healed");
            var healEffect = Instantiate(healingEffect, new Vector3 (orientationVector.x, orientationVector.y, 0f)*0.36f + transform.position, Quaternion.identity);
            healEffect.transform.parent = gameObject.transform;
        }
    }

    public void GetHit()
    {   

        if (invincibleTimer > invincibleTime)
        {
            if (shield > 0)
            {
                shield -= 1;
                genShield.GetComponent<GenShield>().SetLayer(shield);
                invincibleTimer = 0f;
            }
            else
            {
                invincibleTimer = 0f;
                health -= 1;
                Debug.Log("Health:"+health);
                var hitEffect = Instantiate (getHitEffect, new Vector3 (orientationVector.x, orientationVector.y, 0f)*0.36f + transform.position, Quaternion.identity);
                hitEffect.transform.parent = gameObject.transform;
                Time.timeScale = 0f;
                TimeFlowAfter(400);
            }           
        }        
    }

    private async void TimeFlowAfter(int delayTime = 0)
    {
        await Task.Run(() =>
        {
            Task.Delay(delayTime).Wait();
        });
        Time.timeScale = 1f;
    }

    private void Die()
    {
        PlayerPrefs.DeleteKey("Save");
        Debug.Log("Player Dies!!!!!!");
    }

    public void GameResume()
    {
        Debug.Log("resume");
        isPaused = false;
        pauseUI.SetActive(false);
        Time.timeScale = 1f;
    }

    private void SaveByPlayerPrefs()
    {
        var saveData = new SaveData();
        saveData.MAX_HEALTH_save = MAX_HEALTH;
        saveData.health_save = health;
        saveData.MAX_QI_save = MAX_QI;
        saveData.qi_save = qi;
        saveData.maxShield_save = maxShield;
        saveData.shield_save = shield;
        saveData.yaoList_save = yaoList;
        List<int> tempList = new List<int>();
        for (int i = 0; i < 8; i++)
        {
            tempList.Add(guaDict.ElementAt(i).Value);
        }
        saveData.guaList_save = tempList;

        var json = JsonUtility.ToJson(saveData);
        PlayerPrefs.SetString("Save", json);
        Debug.Log("saved"+json);
        PlayerPrefs.Save();
    }

    private void LoadFromPlayerPrefs()
    {
        var defaultSaveData = new SaveData();
        defaultSaveData.MAX_HEALTH_save = 3;
        defaultSaveData.health_save = 3;
        defaultSaveData.MAX_QI_save = 8;
        defaultSaveData.qi_save = 0;
        defaultSaveData.maxShield_save = 0;
        defaultSaveData.shield_save = 5;
        defaultSaveData.yaoList_save = new List<bool>();
        defaultSaveData.guaList_save = new List<int>{0,0,0,0,5,0,0,0};
        var defaultJson = JsonUtility.ToJson(defaultSaveData);

        var json = PlayerPrefs.GetString("Save", defaultJson);
        var saveData = JsonUtility.FromJson<SaveData>(json);
        Debug.Log("loaded"+json);

        MAX_HEALTH = saveData.MAX_HEALTH_save;
        health = saveData.health_save;
        MAX_QI = saveData.MAX_QI_save;
        qi = saveData.qi_save;
        maxShield = saveData.maxShield_save;
        shield = saveData.shield_save;
        yaoList = saveData.yaoList_save;
        for (int i = 0; i < 8; i++)
        {
            string key = guaDict.ElementAt(i).Key;
            guaDict[key] = saveData.guaList_save[i];
        }
    }


}
