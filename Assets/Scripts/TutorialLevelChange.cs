using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialLevelChange : MonoBehaviour
{
    public int levelNumber;

   

   private void OnTriggerEnter(Collider other) {
        GameObject.Find("Player").GetComponent<PlayerMovement>().SavePlayerData(levelNumber);
        GameObject.Find("LevelChanger").GetComponent<LevelChanger>().FadeToScene(levelNumber);
    }
}
