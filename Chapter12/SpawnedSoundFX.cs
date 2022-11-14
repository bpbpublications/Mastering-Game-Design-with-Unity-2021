using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnedSoundFX : MonoBehaviour
{
    public AudioSource _audioSource;

    public static void Spawn( Vector3 pos, AudioClip clip = null )
    {
        // spawn soundFX object
        GameObject prefab = Resources.Load<GameObject>("Prefabs/SpawnedSoundFX");
        GameObject newObj = Instantiate( prefab, pos, Quaternion.identity);

        // add randomness to pitch
        float rand = Random.Range(0.95f, 1.05f);
        SpawnedSoundFX soundScript = newObj.GetComponent<SpawnedSoundFX>();
        soundScript._audioSource.pitch = rand;

        // swap audio clip
        if (clip)
        {
            soundScript._audioSource.clip = clip;
            soundScript._audioSource.Play();
        }
    }
}
