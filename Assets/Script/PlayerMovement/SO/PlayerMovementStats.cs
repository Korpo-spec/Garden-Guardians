using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerStats/PlayerMovementStats")]
public class PlayerMovementStats : ScriptableObject
{
    [SerializeField] public float speed;
    [SerializeField] public float sprintSpeed;


}
