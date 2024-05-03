using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EntityFaction : MonoBehaviour
{
    public Faction faction;

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
public enum Faction
{
    Player,
    Enemy,
    Neutral
}
