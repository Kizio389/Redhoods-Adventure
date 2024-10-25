using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplatSceneController : MonoBehaviour
{
    [SerializeField] SceneConfig sceneConfig;
    [SerializeField] ScriptableObject[] SO;

    [SerializeField] GameObject Player;
    Vector3 PosPlayer;

    private void Awake()
    {
        PosPlayer = Player.transform.localPosition;
    }

    private void Start()
    {
        foreach (ScriptableObject so in SO)
        {
            continue;
        }

    }
    private void Update()
    {
        Player.transform.position += new Vector3(2,0,0)* 2 * Time.deltaTime;
        if (Player.transform.position.x >= 10f)
        {
            Debug.Log("Load Scene");
            Invoke(nameof(LoadNextScene), 3f);
        }
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene(sceneConfig.WhatIsLoadScene);
    }
}
