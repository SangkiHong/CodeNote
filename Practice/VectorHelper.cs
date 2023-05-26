using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorHelper 
{
    public static void Vector3Swap(ref Vector3 _a, ref Vector3 _b)
    {
        Vector3 tmp = _a;
        _a = _b;
        _b = tmp;
    }
}
