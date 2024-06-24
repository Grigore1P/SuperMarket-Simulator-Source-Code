using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelMovement : MonoBehaviour
{
    [SerializeField] private float speed = 550f;
    [SerializeField] CarMovement carMovement;


    void Update()
    {
        if (carMovement.Move)
        {
            transform.Rotate(speed * Time.deltaTime, 0, 0);
        }
    }
}
