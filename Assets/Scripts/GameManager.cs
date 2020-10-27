using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Sprite mapSprite;
    public GameObject map1,map2;

    public GameObject normalEnemy;
    public GameObject bossEnemy;
    public Transform spawnPointRight;
    public Transform spawnPointLeft;
    public Transform spawnPointUp;
    public Transform enemyList;
    public GameObject gameOverScreen;

    public AudioClip battleBGM;
    public AudioClip bossBGM;
    public AudioSource audioSource;


    private bool isBossSpawned;

    public Text scoreText;
    public static int score;

    public static int enemyDefeatNum;

    private float lastSpawnTime;
    public static float timePast;
    public static int lives = 3;

    private void Awake()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        map1.GetComponent<Image>().sprite = mapSprite;
        map1.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.safeArea.width, Screen.height);
        map1.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        map2.GetComponent<Image>().sprite = mapSprite;
        map2.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.safeArea.width, Screen.height);
        map2.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, Screen.height);
    }
    void Start()
    {
        isBossSpawned = false;
        Time.timeScale = 1;
        score = 0;
        enemyDefeatNum = 0;
        lives = 3;
        timePast = 0;
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: "+score;
        timePast += Time.deltaTime;
        lastSpawnTime += Time.deltaTime;
        SpawnOrder();
    }

    void SpawnOrder()
    {
        if (lastSpawnTime>5)
        {
            GameObject enemy = Instantiate(normalEnemy, spawnPointRight.position, Quaternion.identity, enemyList);
            enemy.GetComponent<Enemy>().moveDir = -1;
            Invoke("SpawnEnemy", 2.5f);
            lastSpawnTime = 0;
        }
        if (enemyDefeatNum == 5&& ! isBossSpawned)
        {
            GameObject enemy = Instantiate(bossEnemy, spawnPointUp.position, Quaternion.identity, enemyList);
            isBossSpawned = true;
            audioSource.clip = bossBGM;
            audioSource.Play();
        }
    }

    void SpawnEnemy()
    {
        GameObject enemy = Instantiate(normalEnemy, spawnPointLeft.position, Quaternion.identity, enemyList);
        enemy.GetComponent<Enemy>().moveDir = 1;
    }



    void ShowResult()
    {

    }
}
