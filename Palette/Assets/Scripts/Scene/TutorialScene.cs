using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScene : MonoBehaviour
{
    [SerializeField] private GameObject mainCamera;

    private TutorialCamera tutorialCamera;

    private GameObject player;
    private Vector3 cameraPosition = new Vector3(0, 0, -10);

    private bool[] isAnswered = new bool[6];
    private int answeredIndex = 0;

    private float smoothCameraMoveSpeed = 5.0f;

    void Start()
    {
        tutorialCamera = mainCamera.GetComponent<TutorialCamera>();
        tutorialCamera.RightEndSize = 80.0f;

        for (int i = 0; i < isAnswered.Length; i++)
            isAnswered[i] = false;

        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        
    }
}
