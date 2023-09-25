using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trapspawner : MonoBehaviour
{
    [SerializeField, Tooltip("the object to be spawned")]private GameObject trapSpawnPiece;
    [SerializeField, Tooltip("how long to wait before spawning the object")]private float idleTime = .7f;
    [SerializeField, Tooltip("how long the object should be spawned")]private float spawnedTime = .3f;
    [SerializeField, Tooltip("if marked true, this will try to play a sound when it spawns the trap piece")]private bool playAudioOnSpawn = true;
    [SerializeField, Tooltip("the audio source to play on spawning")]private AudioSource audioSource;
    [SerializeField, Tooltip("if marked true, the object will start the game spawned")]private bool startSpawned = false;
    private bool spawned;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        trapSpawnPiece.SetActive(startSpawned);
        if(startSpawned){
            timer = spawnedTime;
        }
        else{
            timer = idleTime;
        }
        if(!audioSource){
            audioSource = GetComponent<AudioSource>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(timer < 0){
            if(spawned){
                trapSpawnPiece.SetActive(false);
                spawned = false;
                timer = idleTime;
            }
            else{
                trapSpawnPiece.SetActive(true);
                spawned = true;
                timer = spawnedTime;
                if(audioSource && playAudioOnSpawn){
                    audioSource.Play();
                }
            }
        }
        timer -= Time.deltaTime;
    }
}
