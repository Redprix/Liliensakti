using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjectDetector : MonoBehaviour
{
    public bool tagAll = false;
    public string objectTag;
    public UnityEvent<Collider2D> onTriggerEnter2D, onTriggerStay2D, onTriggerExit2D;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (tagAll)
        {
            onTriggerEnter2D?.Invoke(other);
            return;
        }

        if (other.gameObject.CompareTag(objectTag))
        {
            onTriggerEnter2D?.Invoke(other);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (tagAll)
        {
            onTriggerStay2D?.Invoke(other);
            return;
        }
        if (other.gameObject.CompareTag(objectTag))
        {
            onTriggerStay2D?.Invoke(other);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (tagAll)
        {
            onTriggerExit2D?.Invoke(other);
            return;
        }
        if (other.gameObject.CompareTag(objectTag))
        {
            onTriggerExit2D?.Invoke(other);
        }
    }
}
