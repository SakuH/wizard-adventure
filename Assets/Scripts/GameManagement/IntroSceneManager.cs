using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroSceneManager : MonoBehaviour
{
    public GameObject teleportPrefab;
    public int nextScene;
    public GameObject teleportObject;

    public AudioClip itsLate, forgotKeys;
    static AudioSource audioSrc;
    void Start()
    {
        
    }

   
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {

            loadLevelOne();
        }
    }
    public void loadLevelOne()
    {
        //SceneManager.LoadScene("Tutorial Level");
        GameObject.Find("LevelChanger").GetComponent<LevelChanger>().FadeToScene("Tutorial Level");
    }
    public void instantiateTeleportAnimation()
    {
        Vector3 position = transform.position;
        
        Instantiate(teleportPrefab, teleportObject.transform.position, transform.rotation);
        
    }
    
}
