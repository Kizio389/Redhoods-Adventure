using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "SceneConfig", menuName = "GameConfiguration/Scene", order = 1)]
public class SceneConfig : ScriptableObject
{
    [SerializeField] public int thisScene;
    [SerializeField] public int previousScene;
    [SerializeField] public int nextScene;
    [SerializeField] public int WhatIsLoadScene;
    [SerializeField] public int lastScene;

    private static SceneConfig instance;

    public static SceneConfig Instance
    {
        get
        {
            // Nếu instance chưa được tạo ra, tạo mới
            if (instance == null)
            {
                instance = ScriptableObject.CreateInstance<SceneConfig>();
            }
            return instance;
        }
    }

    public void GetIndexScene()
    {
        lastScene = thisScene;
        thisScene = SceneManager.GetActiveScene().buildIndex;
        previousScene = thisScene - 1;
        nextScene = thisScene + 1;
    }

    public void ResetToDefault()
    {
        thisScene = 3;
        previousScene = 3;
        nextScene = 3;
        WhatIsLoadScene = 3;
        lastScene = 3;
    }
}

