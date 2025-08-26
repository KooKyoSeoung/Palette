using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScene : MonoBehaviour
{
    [SerializeField] private GameObject mainCamera;

    [SerializeField] private List<Transform> rightEndTransform = new List<Transform>();
    [SerializeField] private List<GameObject> walls = new List<GameObject>();
    [SerializeField] private List<GameObject> questions = new List<GameObject>();

    private TutorialCamera tutorialCamera;

    private GameObject player;
    private Vector3 cameraPosition = new Vector3(0, 0, -10);

    private int answeredIndex;
    private bool isSubStageTransition = false;

    private float smoothCameraMoveSpeed = 5.0f;

    void Start()
    {
        tutorialCamera = mainCamera.GetComponent<TutorialCamera>();
        tutorialCamera.RightEndSize = rightEndTransform[0].position.x;
        answeredIndex = 0;

        Manager.Sound.PlayBGM("Tutorial");

        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        if (isSubStageTransition)
            SmoothCameraMove();
    }

    private void SmoothCameraMove()
    {
        tutorialCamera.isSubStageMove = true;

        cameraPosition.x = player.transform.position.x;
        mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, cameraPosition, Time.deltaTime * smoothCameraMoveSpeed);

        StartCoroutine(DelayTime());
    }

    private IEnumerator DelayTime()
    {
        yield return new WaitForSeconds(1.0f);
        tutorialCamera.isSubStageMove = false;
        isSubStageTransition = false;
        StopAllCoroutines();
    }

    public void SelectQuestion()
    {
        isSubStageTransition = true;

        answeredIndex++;
        tutorialCamera.RightEndSize = rightEndTransform[answeredIndex].position.x;
        questions[answeredIndex - 1].SetActive(false);
        walls[answeredIndex - 1].SetActive(false);
    }
}
