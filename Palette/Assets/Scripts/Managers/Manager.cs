using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    static Manager sInstance;
    static Manager Instance
    {
        get
        {
            if (sInstance == null)
                Init();
            return sInstance;
        }
    }

    public static InputManager Input { get { return Instance.input; } }

    InputManager input = new InputManager();

    static void Init()
    {
        if (sInstance == null)
        {
            GameObject go = GameObject.FindWithTag("Manager");

            if (go == null)
            {
                go = new GameObject { name = "@Managers", tag = "Manager" };
                go.AddComponent<Manager>();
            }

            DontDestroyOnLoad(go);
            sInstance = go.GetComponent<Manager>();
        }
    }

    void Update()
    {
        input.OnUpdate();
    }
}
