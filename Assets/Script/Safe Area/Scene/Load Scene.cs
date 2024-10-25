using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    //Scene
    [SerializeField] SceneConfig sceneConfig;

    private void Start()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(gameObject.CompareTag("Next Scene"))
            {
                sceneConfig.lastScene = sceneConfig.thisScene;
                sceneConfig.WhatIsLoadScene = sceneConfig.nextScene;
            }
            else if(gameObject.CompareTag("Previous Scene"))
            {
                sceneConfig.lastScene = sceneConfig.thisScene;
                sceneConfig.WhatIsLoadScene = sceneConfig.previousScene;
            }
            LoadSplatScene();
        }
    }

    void LoadSplatScene()
    {
        SceneManager.LoadScene("Splat Scene");
    }
}
