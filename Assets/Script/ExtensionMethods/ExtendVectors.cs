using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtendVectors 
{
    public static Vector3 Vector3WorldSpaceToIsometricSpace(this Vector3 vector3,Vector3 targetrotation)
    {
        var matrix = Matrix4x4.Rotate(Quaternion.Euler(new Vector3(0,targetrotation.y,0)));
        return matrix.MultiplyPoint3x4(vector3);
    }
    public static Vector2 Vector2WorldSpaceToIsometricSpace(this Vector2 vector2,Vector3 targetrotation)
    {
        var matrix = Matrix4x4.Rotate(Quaternion.Euler(0, targetrotation.z, 0));
        return matrix.MultiplyPoint3x4(vector2);
        
    }
    
    public static Vector3 RemoveY(this Vector3 vector3)
    {
        return new Vector3(vector3.x,0,vector3.z);
    }
}
