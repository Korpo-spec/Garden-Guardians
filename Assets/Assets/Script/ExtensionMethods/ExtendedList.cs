using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public static class ExtendedList 
{
    
    public static T GetRandom<T>(this List<T> list)
    {
        return list[Random.Range(0, list.Count)];
    }
    
    public static void Swap<T>(this List<T> list, int index1, int index2)
    {
        T temp = list[index1];
        list[index1] = list[index2];
        list[index2] = temp;
    }
    public static void Swap<T>(this List<T> list,List<T> To, int fromIndex, int ToIndex)
    {
        T temp = list[fromIndex];
        list[fromIndex] = To[ToIndex];
        To[ToIndex] = temp;
    }
}
