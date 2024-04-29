using UnityEngine;

public sealed class AudioManFinder : MonoBehaviour
{
    [SerializeField]
    private GameObject audioManagerObject;

    private void Awake()
    {
        /*
        if (FindObjectOfType<AudioManager>() == null)
        {
            Instantiate(audioManagerObject);
        }
        */
        if (AudioManager.Instance == null)
        {
            Instantiate(audioManagerObject);
        }
    }
}
