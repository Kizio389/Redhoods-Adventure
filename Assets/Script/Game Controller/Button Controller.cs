
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Inventory.Model;

public class ButtonController : MonoBehaviour
{
    [SerializeField] private GameObject panel_Pause;
    [SerializeField] private GameObject panel_Inventory;

    [SerializeField] private Button button_Pause;

    [SerializeField] SceneConfig sceneConfig;
    DataPlayer dataPlayer;

    private void Awake()
    {
        dataPlayer = DataPlayer.Instance;
    }
    //Skip
    public void SkipButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    //Main Menu
    void ResetAllData()
    {
        dataPlayer.ResetData();
        sceneConfig.ResetToDefault();
    }

    public void NewGame()
    {
        ResetAllData();
        SceneManager.LoadScene(2);
    }

    public void ContinuesGame()
    {
        SceneManager.LoadScene(sceneConfig.thisScene);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    //Pause menu
    public void ButtonPause()
    {
        Time.timeScale = 0;
        panel_Pause.SetActive(true);
        //panel_Inventory.SetActive(false);
        button_Pause.enabled = false;
    }

    public void Continues()
    {
        Time.timeScale = 1;
        panel_Pause.SetActive(false);
        button_Pause.enabled = true;
    }

    public void Setting()
    {

    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Main Menu");
    }

    //Open Inventory
    public void OpenInventory()
    {
        panel_Inventory.SetActive(true);
        panel_Pause.SetActive(false);
        button_Pause.enabled = false;
        Time.timeScale = 0;
    }

    public void CloseInventory()
    {
        Time.timeScale = 1;
        button_Pause.enabled = true;
        panel_Inventory.SetActive(false);
    }
}
