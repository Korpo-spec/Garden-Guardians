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

    public static Vector3 RandomVector3(this Vector3 vector3,float xmin,float xmax,float ymin,float ymax, float zmin, float zmax)
    {
        return new Vector3(Random.Range(xmin, xmax), Random.Range(ymin, ymax), Random.Range(zmin, zmax));
    }public static Vector2 RandomVector2(this Vector3 vector3,float xmin,float xmax,float ymin,float ymax)
    {
        return new Vector2(Random.Range(xmin, xmax), Random.Range(ymin, ymax));
    }
    public static Vector2 RotateVector2(this Vector2 v, float delta) {
        return new Vector2(
            v.x * Mathf.Cos(delta) - v.y * Mathf.Sin(delta),
            v.x * Mathf.Sin(delta) + v.y * Mathf.Cos(delta)
        );
    }
}
