using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectDestroy : MonoBehaviour
{
    public enum DestroyType
    {
        gameObjectOnly,
        root,
        parentChild,
        rootChild
    }
    public DestroyType destroyType;
    public void DestroyOnDetect(Collider2D col)
    {
        switch (destroyType)
        {
            case DestroyType.gameObjectOnly:
                Destroy(col.gameObject);
                break;

            case DestroyType.root:
                Destroy(col.transform.root.gameObject);
                break;

            case DestroyType.parentChild:
                foreach (Transform child in col.transform.parent)
                {
                    Destroy(child.gameObject);
                }
                break;

            case DestroyType.rootChild:
                foreach (Transform child in col.transform.root)
                {
                    Destroy(child.gameObject);
                }
                break;
        }
    }
}
