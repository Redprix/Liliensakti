using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerLife : MonoBehaviour
{
    public Slider hpSlider;
    public float Health = 100.0f;
    private Animator anim;
    private Rigidbody2D rb;

    [HideInInspector] public bool die;
    // Start is called before the first frame update
    void Start()
    {
        die = false;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        hpSlider.minValue = 0;
        hpSlider.maxValue = Health;
    }

    private void Update()
    {
        hpSlider.value = Health;
        if (Health <= 0 && !die)
        {
            Die();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("trap"))
        {
            Die();
        }
    }

    private void Die()
    {
        die = true;
        rb.bodyType = RigidbodyType2D.Static;
        anim.SetTrigger("death");
        SceneManager.LoadScene("TempMainMenu");
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene("TempMainMenu");
    }

    public void BackToStart()
    {
        SceneManager.LoadSceneAsync("TempLevel1");
    }
}
