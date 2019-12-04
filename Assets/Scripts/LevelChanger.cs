using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{

    public Animator animator;

    private int sceneNumberToLoad = -1;
    private string sceneNameToLoad;
    // Update is called once per frame
    void Update()
    {
        
    }

    public void FadeToNextScene()
    {
        FadeToScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void FadeToScene(int sceneIndex)
    {
        sceneNumberToLoad = sceneIndex;
        animator.SetTrigger("FadeOut");
    }

    public void FadeToScene(string sceneName)
    {
        sceneNameToLoad = sceneName;
        animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        if (sceneNumberToLoad == -1)
        {
            SceneManager.LoadScene(sceneNameToLoad);
        }
        else
        {
            SceneManager.LoadScene(sceneNumberToLoad);
        }
    }
}
