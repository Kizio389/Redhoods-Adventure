using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAnimals : MonoBehaviour
{
    [SerializeField] private GameObject Prefab_Oposum;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Invoke(nameof(SpawnAnimal), 4f);
    }

    void SpawnAnimal()
    {
        var randonSpawn = Random.Range(0, 4);
        var _Oposum = Prefab_Oposum;
        switch (randonSpawn)
        {
            case 0:
                Instantiate(_Oposum, new Vector3(55f,0.45f,0f), Quaternion.identity);
                break;
            case 1:
                Instantiate(_Oposum, new Vector3(6f, 0.45f, 0f), Quaternion.identity);
                break;
            case 2:
                Instantiate(_Oposum, new Vector3(-30f, 4.45f, 0f), Quaternion.identity);
                break;
            case 3:
                Instantiate(_Oposum, new Vector3(-30f, 19.45f, 0f), Quaternion.identity);
                break;
            default:
                break;
        }
        CancelInvoke(nameof(SpawnAnimal));
    }
}
