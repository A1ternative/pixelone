using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField] private InputField nameField;
    [SerializeField] private InputField soundOption;

    private void Start()
    {
        if (PlayerPrefs.HasKey("Player_Name"))
            nameField.text = PlayerPrefs.GetString("Player_Name");
        if (PlayerPrefs.HasKey("Sound_Option"))
            soundOption.text = PlayerPrefs.GetInt("Sound_Option", soundOption.);
    }

    public void OnClickPlay()
    {
        SceneManager.LoadScene(1);
    }

    public void OnClickExit()
    {
        Application.Quit();
    }

    public void OnEndEditName()
    {
        PlayerPrefs.SetString("Player_Name", nameField.text);
    }

    public void OnClickSound()
    {
        if (PlayerPrefs.GetInt("Sound_Option", 1))
            PlayerPrefs.SetInt("Sound_Option", 0) ;
        else
            PlayerPrefs.SetInt("Sound_Option", 1);
    }

}
