using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryController : MonoBehaviour
{
    [SerializeField] private float _SpeedCamera;

    [SerializeField] GameObject _Camera;
    // Start is called before the first frame update
    void Start()
    {
        _Camera.transform.localPosition = new Vector3(-60,11,-10);
    }

    // Update is called once per frame
    void Update()
    {
        MoveCamera();
    }

    void MoveCamera()
    {
        if(_Camera.transform.position.x < 53)
        {
            _Camera.transform.position += new Vector3(_SpeedCamera * Time.deltaTime, 0, 0);
        }
        else
        {
            _Camera.transform.localPosition = new Vector3(-60, 11, -10);
        }
    }
}
