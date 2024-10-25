using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PanelEndGame : MonoBehaviour
{
    [TextArea(3, 10)]
    public string Text;
    [SerializeField] TextMeshProUGUI TextUI;
    [SerializeField] GameObject Button_EndGame;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void ButtonEndGameClick()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void StoryEndGame()
    {
        Instantiate(gameObject);
        Button_EndGame.SetActive(false);

        foreach (char letter in Text)
        {
            TextUI.text += letter;
        }
        Button_EndGame.SetActive(true);
    }
}
