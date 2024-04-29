using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;
public class SoundTrigger : MonoBehaviour
{
    [SerializeField]
    public GameObject soundSource;


// Sound Code
private SoundManager _soundManager => SoundManager.Instance;
    /*
private void Start()
{
    _soundManager = SoundManager.Instance;
}
*/

public void GoopFall(GameObject obj) // for Goop Fall sound
{
    soundSource = obj;
    _soundManager.interactablesAudio.ButtonAudio(soundSource);
}

public void GoopLoop(GameObject obj) // for Goop Loop sound
{
    soundSource = obj;
    _soundManager.ambientAudio.GoopLoopAudio(soundSource);
}

public void Page_Collect(GameObject obj) // for Page Collect sound
{
    soundSource = obj;
    _soundManager.interactablesAudio.PageCollectAudio(soundSource);
}

public void Pressure_Plate(GameObject obj) // for Button press sound
{
    soundSource = obj;
    _soundManager.interactablesAudio.PressurePlateAudio(soundSource);
}

    public void Final_Gate(GameObject obj) // for Final Gate sound
    {
        soundSource = obj;
        _soundManager.ambientAudio.FinalGateAudio(soundSource);
    }

    public void Block(GameObject obj) // for Warp Block sound
{
    soundSource = obj;
    _soundManager.interactablesAudio.WarpBlockAudio(soundSource);
}

    public void Respawn(GameObject obj) // for Warp Block sound
    {
        soundSource = obj;
        _soundManager.interactablesAudio.RespawnAnchorAudio(soundSource);
    }
    
    public void Box1(GameObject obj) // for Box1 sound
    {
        soundSource = obj;
        _soundManager.interactablesAudio.Box1Audio(soundSource);
    }

    public void Box2(GameObject obj) // for Box2 sound
    {
        soundSource = obj;
        _soundManager.interactablesAudio.Box2Audio(soundSource);
    }

}
