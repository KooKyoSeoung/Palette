using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    [SerializeField] private SoundManager sound;

    public static bool canInput = true;

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
    public static SoundManager Sound { get { return Instance.sound; } }
    public static PoolManager Pool { get { return Instance.pool; } }

    InputManager input = new InputManager();
    PoolManager pool = new PoolManager();

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

            sInstance.pool.Init();
        }
    }

    public static void Clear()
    {
        Pool.Clear();
    }

    void Update()
    {
        if (canInput)
            input.OnUpdate();
    }
}
