using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnInOneScene : MonoBehaviour
{
    [SerializeField] private GameObject previous_ScenePrefabs;
    [SerializeField] private GameObject next_ScenePrefabs;

    [SerializeField] PlayerController playerController;

    [SerializeField] SceneConfig sceneConfig;
    private void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        if(sceneConfig.lastScene - sceneConfig.WhatIsLoadScene > 0) //1 - 2 < 0
        {
            if(next_ScenePrefabs == null)  return;
            //Nếu người chơi từ màn chơi sau chuyển về màn chơi trc
            gameObject.transform.localPosition = next_ScenePrefabs.transform.position - new Vector3(5, 0, 0);
        }
        else if( sceneConfig.lastScene - sceneConfig.WhatIsLoadScene < 0) //2 - 1 > 0
        {
            if(previous_ScenePrefabs == null) return;
            //Nếu người chơi từ màn chơi trc chuyển đến màn chơi sau
            gameObject.transform.localPosition = previous_ScenePrefabs.transform.position + new Vector3(5, 0, 0);
        }
    }
}
