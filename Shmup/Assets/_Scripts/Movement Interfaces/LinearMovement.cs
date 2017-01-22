using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearMovement : IMoveable
{
    private Vector3 position;
    private Vector3 direction;
    private float speed;

    public LinearMovement()
    {

    }

    public Vector3 Move(Vector3 position, Vector3 direction, float speed)
    {
        return position += direction * speed * Time.deltaTime;
    }
    
}
