using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSound : MonoBehaviour
{
    // Start is called before the first frame update
  

 
     void Start()
    {
     
    }
 
   public void ChangeVol(float newValue) {
        float newVol = AudioListener.volume;
        newVol = newValue;
        AudioListener.volume = newVol;
    }

 
}
