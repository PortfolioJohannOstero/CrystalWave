using UnityEngine;
using UnityEngine.UI;

public class UiHandler : MonoBehaviour {

    public Text pointsLabel;
    public GameObject startGameCollection;

    [Header("GameOver")]
    public GameObject gameoverCollection;
    public Text creditLabel;
    public Text highscoreLabel;

    [Header("Device Specific")]
    public Text pressToStartLabel;
    public Text pressToRestartLabel;

    private void Start()
    {
        string input = ControlInput.GetInputName();
        string text = "\"" + input + "\" to ";

        pressToStartLabel.text = text + "start";
        pressToRestartLabel.text = text + "restart";
    }

    public void ShowGameOver(int score)
    {
        gameoverCollection.SetActive(true);
        creditLabel.gameObject.SetActive(true);

        highscoreLabel.text = score.ToString();
        highscoreLabel.gameObject.SetActive(true);
    }
}
