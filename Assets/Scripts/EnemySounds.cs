using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySounds : MonoBehaviour
{
    [SerializeField] private AudioSource damageAudioSource;
    [SerializeField] private AudioSource attackAudioSource;
    [SerializeField] private AudioSource dieAudioSource;
    public void PlayDamageSound(){
        damageAudioSource.Play();
    }
    public void PlayAttackSound(){
        attackAudioSource.Play();
    }
    public void PlayDieSound(){
        dieAudioSource.Play();
    }
}
