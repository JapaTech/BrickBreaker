using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            GameManager.Instance.ChangeHealth(-1);
        }
        if (collision.gameObject.CompareTag("Item"))
        {
            collision.gameObject.SetActive(false);
        }
    }
}
