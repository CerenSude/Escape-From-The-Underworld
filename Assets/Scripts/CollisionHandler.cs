using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    AudioSource Audio; //reference example
    
    bool isTransitioning = false;
    bool collisionDisabled = false;
    
    [SerializeField] float levelLoadDelay = 3f; // parameter example

    [SerializeField] AudioClip Acrash; //cache example
    [SerializeField] AudioClip Asuccess;

    [SerializeField] ParticleSystem pCrash;
    [SerializeField] ParticleSystem pSuccess;


    void Start()
    {
        Audio = GetComponent<AudioSource>();
    }

    void Update() 
    {
        //RespondToDebugKeys();    
    }

    void RespondToDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            loadNextLevel();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            collisionDisabled = !collisionDisabled;  // toggle collision
        } 
    }

    void OnCollisionEnter(Collision other) //other thing we collided with
    {
        if (isTransitioning || collisionDisabled)
        {
            return; 
            
        }
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("This thing is friendly!");
                break;
            case "Finish":
                startSuccessSeq();
                break;
            default: //anything else other then ones above
                startCrashSequence();
                break;
        }
    }

    void startCrashSequence()
    {   
        isTransitioning = true;
        Audio.Stop();
        Audio.PlayOneShot(Acrash);
        pCrash.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("reloadScene", levelLoadDelay);
    }

    void startSuccessSeq()
    {
        isTransitioning = true;
        Audio.Stop();
        Audio.PlayOneShot(Asuccess);
        pSuccess.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("loadNextLevel", levelLoadDelay);
    }
    
    void reloadScene()
    {
        int levelIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(levelIndex);
    }

    public void loadNextLevel()
    {
        int levelindex = SceneManager.GetActiveScene().buildIndex;
        if (levelindex == SceneManager.sceneCountInBuildSettings-1)
        {
            levelindex = 0;
        }
        else
        {
            levelindex++;
        }
        SceneManager.LoadScene(levelindex);
    }
}
