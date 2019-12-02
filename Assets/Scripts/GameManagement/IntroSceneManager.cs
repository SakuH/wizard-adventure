using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroSceneManager : MonoBehaviour
{
    public GameObject teleportPrefab;
    public int nextScene;
    public GameObject teleportObject;
    void Start()
    {
        
    }

   
    void Update()
    {
        
    }
    public void loadLevelOne()
    {
        SceneManager.LoadScene(nextScene);
    }
    public void instantiateTeleportAnimation()
    {
        Vector3 position = transform.position;
        
        Instantiate(teleportPrefab, teleportObject.transform.position, transform.rotation);
        
    }
}
