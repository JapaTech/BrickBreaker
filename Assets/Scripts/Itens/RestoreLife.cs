using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestoreLife : MonoBehaviour
{
    [SerializeField] private int amount =1;
    [SerializeField] private Vector2 fallSpeed;
    private Rigidbody2D rb;
    private AudioSource restoreAudio;
    private Vector3 playAudioAtPoint;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        restoreAudio = GetComponent<AudioSource>();
        playAudioAtPoint = new Vector3(transform.position.x, transform.position.y, Camera.main.transform.position.z);
    }

    private void FixedUpdate()
    {
        rb.velocity = fallSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            restoreAudio.pitch = UnityEngine.Random.Range(0.9f, 1.1f);
            AudioSource.PlayClipAtPoint(restoreAudio.clip, playAudioAtPoint, 1f);
            GameManager.Instance.ChangeHealth(amount);
            gameObject.SetActive(false);
        }
        if (collision.CompareTag("Death"))
        {
            gameObject.SetActive(false);
        }
    }
}
