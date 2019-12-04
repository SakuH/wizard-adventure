using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialLevelChange : MonoBehaviour
{
    // Start is called before the first frame update
    public int levelNumber;

   

   private void OnTriggerEnter(Collider other) {
        //SceneManager.LoadScene(levelNumber);
        GameObject.Find("LevelChanger").GetComponent<LevelChanger>().FadeToScene(levelNumber);
    }
}
