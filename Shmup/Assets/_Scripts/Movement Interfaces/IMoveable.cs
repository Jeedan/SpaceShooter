using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMoveable
{
    Vector3 Move(Vector3 position, Vector3 direction, float speed);
}
