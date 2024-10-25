using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalsController : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void MoveAnimal(ref bool _IsRight, float _Speed, float Limit_Left, float Limit_Right)
    {
        if (_IsRight == true)
        {
            gameObject.transform.position += new Vector3(_Speed * Time.deltaTime, 0, 0);
            if (Limit_Right <= gameObject.transform.localPosition.x)
            {
                Flip(ref _IsRight);
            }
        }
        else
        {
            gameObject.transform.position += new Vector3(-_Speed * Time.deltaTime, 0, 0);
            if (Limit_Left >= gameObject.transform.localPosition.x)
            {
                Flip(ref _IsRight);
            }
        }
    }

    public void Flip(ref bool _IsRight)
    {
        _IsRight = !_IsRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    
    public void DestroyAnimals(GameObject Animals_gameObject)
    {
        Destroy(Animals_gameObject, 20f);
    }
}
