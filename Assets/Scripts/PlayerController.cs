using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public GameObject bulletPrefab;
    public Transform firePos;
    public Transform playerBullets;
    public GameObject canvas;

    private float lastFireTime;
    public static string bulletType;
    public GameObject liveIcon1;
    public GameObject liveIcon2;
    public GameObject liveIcon3;
    public GameObject playerPrefab;
    private Transform rebornPos;
    private GameObject gameOverScreen;
    public AudioSource audioSource;
    private AudioSource canvasAudioSource;
    public AudioClip playerFireSFX;

    void Awake()
    {
        canvasAudioSource = transform.parent.gameObject.GetComponent<AudioSource>();
        Debug.Log(transform.parent.gameObject.name);
    }


    void Start()
    {
        bulletType = "PlayerBullet";
        playerBullets = GameObject.FindGameObjectWithTag("PlayerBulletList").transform;
        canvas = GameObject.FindGameObjectWithTag("Canvas");
        rebornPos= GameObject.FindGameObjectWithTag("RebornPos").transform;
        gameOverScreen = canvas.transform.Find("GameOverScreen").gameObject;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
        Fire();

    }

    void Move()
    {
        if (Input.touchCount > 0)
        {
            Touch myTouch = Input.GetTouch(Input.touchCount-1);//save last touch infomation
            if (myTouch.phase == TouchPhase.Stationary|| myTouch.phase == TouchPhase.Moved)//if user continues the touch
            {
                if (myTouch.position.x > Screen.width / 2)//if touch on the right side
                {
                    transform.position = new Vector2(transform.position.x + moveSpeed, transform.position.y);//move to the right
                }
                else if (myTouch.position.x < Screen.width / 2)//if touch on the left side
                {
                    transform.position = new Vector2(transform.position.x - moveSpeed, transform.position.y);//move to the left
                }
                
            }
        }
        if (transform.position.x < GetComponent<RectTransform>().rect.width/2)//left bound check
        {
            transform.position = new Vector2(GetComponent<RectTransform>().rect.width/2, transform.position.y);
        }
        if (transform.position.x > Screen.width-GetComponent<RectTransform>().rect.width/2)//right bound check
        {
            transform.position = new Vector2(Screen.width - GetComponent<RectTransform>().rect.width/2, transform.position.y);
        }
    }

    void Fire()
    {

        if (GameManager.timePast-lastFireTime>0.3f)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePos.position, Quaternion.identity, playerBullets);
            bullet.transform.tag = bulletType;
            lastFireTime = GameManager.timePast;
            audioSource.clip = playerFireSFX;
            audioSource.Play();
        }
    }

    public void Die()
    {
        switch (GameManager.lives)
        {
            case 0:
                GameOver();
                break;
            case 1:
                Destroy(liveIcon1);
                break;
            case 2:
                Destroy(liveIcon2);
                break;
            case 3:
                Destroy(liveIcon3);
                break;
        }
        GameManager.lives -= 1;

        canvasAudioSource.Play();

        GameObject player = Instantiate(playerPrefab, rebornPos.position, Quaternion.identity, canvas.transform);

        Destroy(gameObject);
    }

    void GameOver()
    {
        Time.timeScale = 0;
        gameOverScreen.SetActive(true);
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Gem")
        {
            bulletType = "PlayerUpgradedBullet";
            Destroy(collision.collider.gameObject);
        }
    }


}
