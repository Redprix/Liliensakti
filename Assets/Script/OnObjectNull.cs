using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnObjectNull : MonoBehaviour
{
    public Object referenceNull;
    public UnityEvent onNullThisFrame, onObjectNull;

    private bool wasNull;
    // Start is called before the first frame update
    void Start()
    {
        wasNull = !referenceNull;
    }

    // Update is called once per frame
    void Update()
    {
        if (referenceNull)
        {
            return;
        }

        if (wasNull)
        {
            onObjectNull.Invoke();
            return;
        }

        onNullThisFrame.Invoke();
        wasNull = true;
    }
}
