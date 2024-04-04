using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtendVectors 
{
    public static Vector3 Vector3WorldSpaceToIsometricSpace(this Vector3 vector3)
    {
        var matrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));
        return matrix.MultiplyPoint3x4(vector3);
    }
    public static Vector2 Vector2WorldSpaceToIsometricSpace(this Vector2 vector2)
    {
        var matrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));
        return matrix.MultiplyPoint3x4(vector2);
        
    }
}
