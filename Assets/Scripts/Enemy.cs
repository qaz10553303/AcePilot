using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    public float hp;
    public float moveSpeed;
    private float lastFireTime;
    private float missleLastFireTime;
    public int moveDir;
    private int bossMoveDir;

    public GameObject bulletPrefab;
    public GameObject gemPrefab;
    public GameObject canvas;
    public Transform firePos;
    public Transform gemUI;
    public Transform misslePos1;
    public Transform misslePos2;
    public Transform enemyBullets;
    private GameObject gameOverScreen;
    private GameObject enemyAudioPlayer;
    private AudioSource audioSource;
    public AudioClip enemyDieSFX;

    private string bulletType;

    void Awake()
    {
        enemyAudioPlayer = transform.parent.gameObject;
        audioSource =enemyAudioPlayer.GetComponent<AudioSource>();
    }

    void Start()
    {
        gemUI = GameObject.FindGameObjectWithTag("Items").transform;
        enemyBullets = GameObject.FindGameObjectWithTag("EnemyBulletList").transform;
        canvas = GameObject.FindGameObjectWithTag("Canvas");
        gameOverScreen = canvas.transform.Find("GameOverScreen").gameObject;

        if (this.tag == "Boss")
        {
            hp = 1000;
        }
        else if(this.tag=="Enemy")
        {
            hp = 20;
            Destroy(gameObject, 20f);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        DeathCheck();
        Fire();
        Move();
    }

    void DeathCheck()
    {
        if (hp <= 0&&tag=="Enemy")
        {
            GameManager.score += 100;
            GameManager.enemyDefeatNum++;
            int temp = Random.Range(0, 3);
            if(temp==0){
                GameObject gem = Instantiate(gemPrefab, transform.position, Quaternion.identity, gemUI);
            }
            audioSource.clip = enemyDieSFX;
            audioSource.Play();
            Destroy(gameObject);
        }
        if (hp <= 0 && tag == "Boss")
        {
            GameManager.score += 3000;
            audioSource.clip = enemyDieSFX;
            audioSource.Play();
            GameOver();
            Destroy(gameObject);
        }
    }

    void Fire()
    {
        if (tag=="Enemy")//normal enmey bullet
        {
            if (GameManager.timePast - lastFireTime > 1f)
            {
                GameObject bullet = Instantiate(bulletPrefab, firePos.position, Quaternion.identity, enemyBullets);
                bulletType = "EnemyBullet";
                bullet.transform.tag = bulletType;
                lastFireTime = GameManager.timePast;
            }
        }
        else if (tag=="Boss")//boss bullet
        {
            if (GameManager.timePast - lastFireTime > 2f)
            {
                GameObject bullet = Instantiate(bulletPrefab, firePos.position, Quaternion.identity, enemyBullets);
                bulletType = "BossBullet";
                bullet.transform.tag = bulletType;
                lastFireTime = GameManager.timePast;
            }
            else if (tag == "Boss")//luanch a missle
            {
                if (GameManager.timePast - missleLastFireTime > 5f)
                {
                    GameObject bullet = Instantiate(bulletPrefab, misslePos1.position, Quaternion.identity, enemyBullets);
                    bulletType = "Missle";
                    bullet.transform.tag = bulletType;
                    missleLastFireTime = GameManager.timePast;

                    bullet = Instantiate(bulletPrefab, misslePos2.position, Quaternion.identity, enemyBullets);
                    bulletType = "Missle";
                    bullet.transform.tag = bulletType;
                    missleLastFireTime = GameManager.timePast;
                }
            }
        }
    }

    void GameOver()
    {
        Time.timeScale = 0;
        gameOverScreen.SetActive(true);
    }

    public void Move()
    {
        if (moveDir == 0)//boss
        {
            if (transform.localPosition.y>850f)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - moveSpeed);
            }
            else if(transform.localPosition.y<=850f)
            {
                if (transform.position.x < GetComponent<RectTransform>().rect.width / 2)//left bound check
                {
                    bossMoveDir = 1;
                }
                if (transform.position.x > Screen.width - GetComponent<RectTransform>().rect.width / 2)//right bound check
                {
                    bossMoveDir = -1;
                }
                if (bossMoveDir==1|| bossMoveDir == 0)
                {
                    transform.position = new Vector3(transform.position.x + moveSpeed / 2, transform.position.y);
                }
                else if (bossMoveDir == -1)
                {
                    transform.position = new Vector3(transform.position.x - moveSpeed / 2, transform.position.y);
                }
            }
        }
        if (moveDir ==1)
        {
            transform.position = new Vector3(transform.position.x + moveSpeed, transform.position.y - moveSpeed/3);
        }
        else if (moveDir == -1)
        {
            transform.position = new Vector3(transform.position.x - moveSpeed, transform.position.y - moveSpeed/3);
        }
    }
}
