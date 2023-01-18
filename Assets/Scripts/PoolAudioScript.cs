using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolAudioScript : MonoBehaviour
{
    public AudioSource BackgroundSound;
   
    void Start()
    {
        BackgroundSound.Play();
    }

}
