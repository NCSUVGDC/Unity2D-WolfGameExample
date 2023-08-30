using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    [SerializeField] private AudioSource damageAudioSource;
    [SerializeField] private AudioSource deathAudioSource;
    [SerializeField] private AudioSource coinCollectAudioSource;
    [SerializeField] private AudioSource jumpAudioSource;


    // private void Start() {
    //     audioSource = GetComponent<AudioSource>();
    // }

    public void PlayDamageSound(){
        damageAudioSource.Play();
    }

    public void PlayCollectSound(){
        coinCollectAudioSource.Play();
    }

    public void PlayDeathSound(){
        deathAudioSource.Play();
    }

    public void PlayJumpSound(){
        jumpAudioSource.Play();
    }
}
