using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public void OnClickLevel1()
    {
        SceneManager.LoadScene("level_1");
        Time.timeScale = 1;
    }

    public void OnClickLevel2()
    {
        SceneManager.LoadScene("level_2");
        Time.timeScale = 1;
    }

    public void OnClickLevel3()
    {
        SceneManager.LoadScene("level_3");
        Time.timeScale = 1;
    }
}
