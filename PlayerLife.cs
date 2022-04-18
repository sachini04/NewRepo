using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    private Rigidbody2D rbd;
    private Animator Anime;

    [SerializeField] private AudioSource DeathSound;

    private void Start()
    {
        Anime = GetComponent<Animator>();
        rbd = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Traps"))
        {
            Die();
        }
    }

    private void Die()
    {
        rbd.bodyType = RigidbodyType2D.Static;
        Anime.SetTrigger("Death");
        DeathSound.Play();
    }

    private void RestartLvl()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
