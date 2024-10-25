using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oposum : AnimalsController
{
    [SerializeField] private float _speed;
    private float Limit_Move_Left;
    private float Limit_Move_Right;
    private bool is_right = true;

    private Vector3 Position_Oposum;
    private Vector3 localScale_Oposum;

    // Start is called before the first frame update
    void Start()
    {
        Position_Oposum = transform.localPosition;
        Limit_Move_Left += transform.localPosition.x - 7;
        Limit_Move_Right += transform.localPosition.x + 7;
    }

    // Update is called once per frame
    void Update()
    {
        DestroyAnimals(gameObject);
        MoveAnimal(ref is_right, _speed, Limit_Move_Left, Limit_Move_Right);
    } 
}
