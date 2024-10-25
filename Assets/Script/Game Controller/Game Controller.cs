using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private SceneConfig sceneConfig;
    private void Start()
    {
        sceneConfig.GetIndexScene();
    }
}
