using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalFlagSounds : MonoBehaviour
{
    [SerializeField] private AudioSource gameWinSound;
    
    public void PlayWinSound(){
        gameWinSound.Play();
    }
}
