using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;
using FMODUnity;

[CreateAssetMenu(menuName = "Audio/Interactables_Audio", order = 2)]

public class InteractablesAudio : ScriptableObject
{
   
    // public EventReference 

    public EventReference Apple_Drop;
    public EventReference Button;
    public EventReference Lever;
    public EventReference Page_Collect;
    public EventReference Pressure_Plate;
    public EventReference Shovel_Drop;
    public EventReference Alt_Button_Lever;
    public EventReference GoopShort;
    public EventReference GoopDrop;
    public EventReference RespawnAnchor;
    public EventReference WarpBlock;
    public EventReference Box1;
    public EventReference Box2;

    public void AppleDropAudio(GameObject appleObj)
    {
        RuntimeManager.PlayOneShotAttached(Apple_Drop, appleObj);
    }
    
    public void ButtonAudio(GameObject buttonObj)
    {
        RuntimeManager.PlayOneShotAttached(Button, buttonObj);
    }
    
    public void LeverAudio(GameObject leverObj)
    {
        RuntimeManager.PlayOneShotAttached(Lever, leverObj);
    }
    
    public void PageCollectAudio(GameObject pageObj)
    {
        RuntimeManager.PlayOneShotAttached(Page_Collect, pageObj);
    }
    
    public void PressurePlateAudio(GameObject plateObj)
    {
        RuntimeManager.PlayOneShotAttached(Pressure_Plate, plateObj);
    }
    
    public void ShovelDropAudio(GameObject shovelObj)
    {
        RuntimeManager.PlayOneShotAttached(Shovel_Drop, shovelObj);
    }
    
    public void AltLBAudio(GameObject altObj)
    {
        RuntimeManager.PlayOneShotAttached(Alt_Button_Lever, altObj);
    }

    public void GoopShortAudio(GameObject gooObj)
    {
        RuntimeManager.PlayOneShotAttached(GoopShort, gooObj);
    }

    public void GoopDropAudio(GameObject dropObj)
    {
        RuntimeManager.PlayOneShotAttached(GoopDrop, dropObj);
    }

    public void RespawnAnchorAudio(GameObject resObj)
    {
        RuntimeManager.PlayOneShotAttached(RespawnAnchor, resObj);
    }

    public void WarpBlockAudio(GameObject warpObj)
    {
        RuntimeManager.PlayOneShotAttached(WarpBlock, warpObj);
    }

    public void Box1Audio(GameObject b1Obj)
    {
        RuntimeManager.PlayOneShotAttached(Box1, b1Obj);
    }

    public void Box2Audio(GameObject b2Obj)
    {
        RuntimeManager.PlayOneShotAttached(Box2, b2Obj);
    }

    /* public void Audio(GameObject Obj)
     {
         RuntimeManager.PlayOneShotAttached(, Obj);
     }
     */




}
