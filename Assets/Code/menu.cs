using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI WinAmount;
    private void Start()
    {
        if (!PlayerPrefs.HasKey("Wins"))
        {
            PlayerPrefs.SetInt("Wins",0);
        }
     if(WinAmount)WinAmount.text = "Number of Times won :" + PlayerPrefs.GetInt("Wins");
    }
    public void play()
    {
        SceneManager.LoadScene(1);
    }
    public void Menu() {
        SceneManager.LoadScene(0);
    }

}
