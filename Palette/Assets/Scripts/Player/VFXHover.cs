using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXHover : MonoBehaviour
{
    public float floatAmplitude = 0.5f;
    public float floatFrequency = 2f;

    private Vector3 offset;

    void Start()
    {
        offset = transform.localPosition;
    }

    void Update()
    {
        float floatY = Mathf.Sin(Time.time * floatFrequency) * floatAmplitude;
        Vector3 floatingPos = (Vector3.up * floatY) + offset;

        transform.localPosition = floatingPos;
    }
}
