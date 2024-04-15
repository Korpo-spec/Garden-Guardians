using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    [SerializeField]private List<Effect> activeEffects = new List<Effect>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = activeEffects.Count-1; i >= 0 ; i--)
        {
            activeEffects[i].UpdateEffect();
            if (activeEffects[i].Timer())
            {
                RemoveEffect(activeEffects[i]);
            }
        }
    }

    public void AddEffect(Effect effect)
    {

        for (int i = 0; i < activeEffects.Count; i++)
        {
            if (activeEffects[i].name == effect.name)
            {
                activeEffects[i].OnReapply();
                return;
            }
        }

        effect = Instantiate(effect);
        effect.SetDuration();
        effect.Enter(this);
        activeEffects.Add(effect);
        
    }

    private void RemoveEffect(Effect effect)
    {
        effect.Exit();
        activeEffects.Remove(effect);
    }
}
