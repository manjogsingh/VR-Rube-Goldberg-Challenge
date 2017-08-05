using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    public GameObject platform;
    Scene scene;
    public bool exit=false;
    // Use this for initialization
    void Start()
    {
        scene = SceneManager.GetActiveScene();
    }
    /// <summary>
    /// OnCollisionEnter is called when this collider/rigidbody has begun
    /// touching another rigidbody/collider.
    /// </summary>
    /// <param name="other">The Collision data associated with this collision.</param>
    void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Throwable"))
        {
            if (other.gameObject.GetComponent<BallReset>().areAllCollected && platform.GetComponent<AntiCheat>().onPlatform)
            {
                Debug.Log("Yes Goal");
                if (scene.name.Equals("Intro"))
                {
                    SteamVR_LoadLevel.Begin("Scene1");
                }
                else if (scene.name.Equals("Scene1"))
                {
                    SteamVR_LoadLevel.Begin("Scene2");
                }
                else if (scene.name.Equals("Scene2"))
                {
                    SteamVR_LoadLevel.Begin("Scene3");
                }
                else if (scene.name.Equals("Scene3"))
                {
                    SteamVR_LoadLevel.Begin("Scene4");
                }
                else if(scene.name.Equals("Scene4"))
                {
                    exit=true;
                }
            }
            else
            {
                Debug.Log("No Goal");
            }
        }
    }
}
