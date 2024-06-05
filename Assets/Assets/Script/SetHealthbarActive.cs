using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetHealthbarActive : MonoBehaviour
{
   public GameObject healthbar;

   public void SetHEalthbar()
   {
      healthbar.SetActive(true);
   }
}
