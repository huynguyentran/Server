using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MountainSoundSwap : MonoBehaviour
{
    public AudioClip newTrack;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            MountainSoundManager.instance.SwapTrack(newTrack);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            MountainSoundManager.instance.ReturnToDefault();
        }
    }

}
