using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUITrace : MonoBehaviour
{
    [SerializeField] private Transform bossTransform;
    private Vector3 offset = new Vector3(0, 4, 0);

    private Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        Vector3 screenPos = cam.WorldToScreenPoint(bossTransform.position + offset);
        gameObject.transform.position = screenPos;
    }
}
