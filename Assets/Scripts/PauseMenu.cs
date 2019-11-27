using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Text soundTextField;
    [SerializeField] private string soundOnText;
    [SerializeField] private string soundOffText;

    private int soundStateValue;
    public bool SoundState
    {
        get => soundStateValue == 1 ? true : false;
        set
        {
            if (value == true)
            {
                soundTextField.text = soundOnText;
                soundStateValue = 1;
            }

            else
            {
                soundTextField.text = soundOffText;
                soundStateValue = 0;
            }
        }
    }

    private void Start()
    {
        SoundState = PlayerPrefs.GetInt("SoundState", soundStateValue) == 1 ? true : false;
    }

    public void OnClickContinue()
    {
        if (Time.timeScale > 0)
            Time.timeScale = 0;
        else Time.timeScale = 1;

        gameObject.SetActive(false);
    }

    public void OnClickSound()
    {
        SoundState = !SoundState;
        PlayerPrefs.SetInt("SoundState", soundStateValue);
    }

    public void OnClickQuitToMenu()
    {
        SceneManager.LoadScene("GameMenu");
    }

    public void OnClickExitGame()
    {
        Application.Quit();
    }

     
}
