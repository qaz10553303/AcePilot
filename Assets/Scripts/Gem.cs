using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    public float moveSpeed;
    private AudioSource audioSource;
    public AudioClip gemSFX;
    private GameObject audioPlayer;

    // Start is called before the first frame update

    void Awake()
    {
        audioPlayer = transform.parent.gameObject;
        audioSource = audioPlayer.GetComponent<AudioSource>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = new Vector2(transform.position.x, transform.position.y - moveSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            PlayerController.bulletType = "PlayerUpgradedBullet";
            GameManager.score += 450;
            audioSource.Play();
            Destroy(gameObject);
        }
    }
}
