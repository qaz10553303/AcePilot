using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class Bullet : MonoBehaviour
{
    private bool isPlayerBullet;
    public float bulletSpeed;
    public float missleSpeed;
    private float bulletDamage;
    private GameObject player;
    private Vector3 targetPos;

    public Sprite enemyBulletSp;
    public Sprite bossBulletSp;
    public Sprite playerBulletSp;
    public Sprite playerUpgradedBulletSp;
    public Sprite MissleSp;

    // Start is called before the first frame update
    void Start()
    {
        SetBulletSprite();
        Destroy(gameObject, 10f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        BulletMove();
    }

    void BulletMove()
    {
        if (isPlayerBullet == true)//player bullet
        {
            transform.position = new Vector2(transform.position.x,transform.position.y+bulletSpeed);
        }
        else if (transform.tag == "Missle")//boss missle
        {
            //transform.Translate(transform.forward * missleSpeed);
            transform.position = new Vector2(transform.position.x, transform.position.y - missleSpeed);
        }
        else//normal bullet
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - bulletSpeed);
        }
    }

    void TargetPlayer()
    {
        //player = GameObject.FindGameObjectWithTag("Player");
        //targetPos = player.transform.position;
        
        //Debug.Log("playerPos:"+targetPos);
        //Debug.Log("misslePos:"+transform.position);
        //Debug.Log("MovingPos:" + (transform.position - targetPos).normalized);

        //double angleOfLine = Math.Atan2((targetPos.y - transform.position.y), (targetPos.x - transform.position.x)) * 180 / Math.PI;
        //Debug.Log(angleOfLine);
        //float dist = Vector3.Distance(targetPos, transform.position);
        //if (dist > 100)
        //{
        //    transform.Translate(Vector3.right * missleSpeed * Time.deltaTime);
        //}


        //Vector2 direction = targetPos - transform.position;
        //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    

    void SetBulletSprite()
    {
        //set bullet sprite
        if (transform.tag == "PlayerBullet")
        {
            GetComponent<Image>().sprite = playerBulletSp;
            bulletDamage = 10;
            isPlayerBullet = true;
        }
        else if (transform.tag == "PlayerUpgradedBullet")
        {
            GetComponent<Image>().sprite = playerUpgradedBulletSp;
            bulletDamage = 20;
            isPlayerBullet = true;
        }
        else if (transform.tag == "EnemyBullet")
        {
            GetComponent<Image>().sprite = enemyBulletSp;
            transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, -180f, transform.rotation.w);
            isPlayerBullet = false;
        }
        else if (transform.tag == "BossBullet")
        {
            GetComponent<Image>().sprite = bossBulletSp;
            transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, -180f, transform.rotation.w);
            isPlayerBullet = false;
        }
        else if (transform.tag == "Missle")
        {
            GetComponent<Image>().sprite = MissleSp;
            isPlayerBullet = false;
            TargetPlayer();
            Debug.Log("missle");
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag=="Enemy"|| collision.collider.tag == "Boss")
        {
            if (isPlayerBullet == true)
            {
                collision.collider.GetComponent<Enemy>().hp -= bulletDamage;
                Destroy(gameObject);
            }
        }
        else if (collision.collider.tag == "Player")
        {
            if (isPlayerBullet == false)
            {
                collision.collider.GetComponent<PlayerController>().Die();
                Destroy(gameObject);
            }
        }
    }
}
