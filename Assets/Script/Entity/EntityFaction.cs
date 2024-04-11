using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityFaction : MonoBehaviour
{
    public Faction faction;
}
public enum Faction
{
    Player,
    Enemy,
    Neutral
}
