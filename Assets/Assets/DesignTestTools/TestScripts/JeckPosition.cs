using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JeckPosition : MonoBehaviour
{
    // Start position
    [SerializeField] private float PosX;
    [SerializeField] private float PosZ;

    [SerializeField] private float rotationX;
    [SerializeField] private float rotationY;
    [SerializeField] private float rotationZ;

    [SerializeField] private int timeToWait;
    public void ChangePositionJeck()
    {
        StartCoroutine(changePositionJeckEnumerator());
    }

    private IEnumerator changePositionJeckEnumerator()
    {
        yield return new WaitForSeconds(timeToWait);
        
        gameObject.transform.SetLocalPositionAndRotation(new Vector3(PosX, transform.position.y, PosZ), new Quaternion(rotationX,rotationY,rotationZ,0f));
    }
}
