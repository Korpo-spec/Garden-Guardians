using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeasureScript : MonoBehaviour
{
    [SerializeField] private float speed;

    private float[] array = new float[1000];
    [SerializeField] int sum;
    [SerializeField] private int averageSpeed;
    
    private int newValue;
    public int movingAverageLength = 10;

    private int count;
    private float movingAverage;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //count++;

        //if (count > movingAverageLength)
        //{
            //movingAverage = movingAverage + (newValue - movingAverage) / (movingAverageLength + 1);
        //}
        //else
        //{
            //movingAverage += newValue;

            //if (count == movingAverageLength)
            //{
                //movingAverage = movingAverage / count;
            //}
        //}
        
        
        
        
        Vector3 lastPosition = Vector3.zero;
        speed = Mathf.RoundToInt(Vector3.Distance(transform.position, lastPosition) / Time.deltaTime) / 1000;
        lastPosition = transform.position;

        for (var i = 0; i < array.Length; i++)
        {
            sum += Mathf.RoundToInt(speed);
            
            if (i > 0)
            {
                //averageSpeed = Mathf.RoundToInt(sum / 1000);
                averageSpeed = sum / i;
            }
        }
    }
}
