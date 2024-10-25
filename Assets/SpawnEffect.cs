using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEffect : MonoBehaviour
{
    [SerializeField] GameObject effect;
    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(DestroyGameObject), 10f);
        
    }
    void DestroyGameObject()
    {
        Instantiate(effect);
        Destroy(gameObject);
    }
}
