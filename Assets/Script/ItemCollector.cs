using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ItemCollector : MonoBehaviour
{
    private int item = 0;
    private float AmountToGive = 40;

    private PlayerMovement player;


    [SerializeField] private Text PowerUpText;

    private void Awake()
    {
        player = FindObjectOfType<PlayerMovement>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Item"))
        {
            item++;
            PowerUpText.text = "Korek : " + item;

            Destroy(collision.gameObject);
        }

       
    }
}
