using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerStats/General")]
public class PlayerStats : ScriptableObject
{
    public PlayerMovementStats playerMovementStats;
    public PlayerCombatStats playerCombatStats;
}


