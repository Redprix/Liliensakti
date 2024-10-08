using System.Collections;
using UnityEngine;

public class SpriteFlasher : MonoBehaviour
{
    [SerializeField] int numberOfFlashes = 2;
    [SerializeField] float durationBetweenFlashes = 0.05f;
    [SerializeField] GameObject objectToFlash;

    private SpriteRenderer spriteRenderer;
    private IEnumerator flashingCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = objectToFlash.GetComponent<SpriteRenderer>();
    }

    public void Flash()
    {
        if (spriteRenderer)
        {
            if (flashingCoroutine != null)
            {
                StopCoroutine(flashingCoroutine);
            }

            flashingCoroutine = InternalFlash();
            StartCoroutine(flashingCoroutine);
        }
    }

    private IEnumerator InternalFlash()
    {
        bool makeSpriteWhite = true;

        // Iterate twice the length of times - that way numberOfFlashes is
        // "how many times is it turned on", not "how many times does it flip"
        for (int i = 0; i < numberOfFlashes * 2; i++)
        {
            spriteRenderer.material.SetFloat("_FlashAmount", makeSpriteWhite ? 1f : 0f);
            makeSpriteWhite = !makeSpriteWhite;
            yield return new WaitForSeconds(durationBetweenFlashes);
        }
    }
}