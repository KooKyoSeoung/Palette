using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSelection : MonoBehaviour
{
    [SerializeField] private QuestionType qType;
    [SerializeField] private TutorialScene tutorialScene;

    private Transform playerTransform;
    
    void Start()
    {
        playerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            float yPos = transform.position.y - playerTransform.position.y;

            tutorialScene.SelectQuestion();

            if (yPos < 0)
            {
                if (qType == QuestionType.SorN)
                {
                    // N
                    PlayerSelectData.SELECT_N++;
                    Debug.Log("N : " + PlayerSelectData.SELECT_N);
                    Debug.Log("S : " + PlayerSelectData.SELECT_S);
                    Debug.Log("T : " + PlayerSelectData.SELECT_T);
                    Debug.Log("F : " + PlayerSelectData.SELECT_F);
                }
                else
                {
                    // T
                    PlayerSelectData.SELECT_T++;
                    Debug.Log("N : " + PlayerSelectData.SELECT_N);
                    Debug.Log("S : " + PlayerSelectData.SELECT_S);
                    Debug.Log("T : " + PlayerSelectData.SELECT_T);
                    Debug.Log("F : " + PlayerSelectData.SELECT_F);
                }
            }
            else
            {
                if (qType == QuestionType.SorN)
                {
                    // S
                    PlayerSelectData.SELECT_S++;
                    Debug.Log("N : " + PlayerSelectData.SELECT_N);
                    Debug.Log("S : " + PlayerSelectData.SELECT_S);
                    Debug.Log("T : " + PlayerSelectData.SELECT_T);
                    Debug.Log("F : " + PlayerSelectData.SELECT_F);
                }
                else
                {
                    // F
                    PlayerSelectData.SELECT_F++;
                    Debug.Log("N : " + PlayerSelectData.SELECT_N);
                    Debug.Log("S : " + PlayerSelectData.SELECT_S);
                    Debug.Log("T : " + PlayerSelectData.SELECT_T);
                    Debug.Log("F : " + PlayerSelectData.SELECT_F);
                }
            }
        }
    }
}

public enum QuestionType
{
    SorN,
    ForT
}
