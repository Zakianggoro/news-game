using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

[System.Serializable]
public class QTEOption
{
    [TextArea]
    public string questionText;
    [TextArea]
    public string feedbackText;
    public bool isCorrect;
}

public class QTEManager : MonoBehaviour
{
    [Header("UI Elements")]
    public Button readyButton;
    public Button[] optionButtons;
    public TMP_Text questionText;
    public TMP_Text feedbackText;
    public TMP_Text timerText;
    public GameObject optionPanel;

    [Header("Game Settings")]
    public string question = "Which question is the most appropriate to ask?";
    public float countdownTime = 10f;
    public string nextSceneName = "NextScene";

    [Header("QTE Options")]
    public QTEOption[] options; 

    private float timer;
    private bool gameStarted = false;
    private bool answered = false;

    void Start()
    {
        optionPanel.SetActive(false);
        feedbackText.text = "";
        questionText.text = "";
        timerText.text = "";
        readyButton.onClick.AddListener(StartMiniGame);

        for (int i = 0; i < optionButtons.Length; i++)
        {
            int index = i;
            optionButtons[i].onClick.AddListener(() => OnOptionSelected(index));
        }
    }

    void StartMiniGame()
    {
        if (options.Length == 0 || optionButtons.Length == 0) return;

        gameStarted = true;
        answered = false;
        timer = countdownTime;

        readyButton.gameObject.SetActive(false);
        optionPanel.SetActive(true);
        questionText.text = question;
        feedbackText.text = "";
        timerText.text = Mathf.CeilToInt(timer).ToString();

        SetButtonTexts();
    }

    void Update()
    {
        if (!gameStarted || answered) return;

        timer -= Time.deltaTime;
        timerText.text = Mathf.CeilToInt(timer).ToString();

        if (timer <= 0f)
        {
            timer = 0f;
            timerText.text = "0";
            answered = true;
            gameStarted = false;
            feedbackText.text = "Time's up!";
        }
    }

    void OnOptionSelected(int index)
    {
        if (!gameStarted || answered) return;

        answered = true;
        gameStarted = false;

        if (index < options.Length && options[index].isCorrect)
        {
            feedbackText.text = options[index].feedbackText;
            Invoke("LoadNextScene", 2f);
        }
        else if (index < options.Length)
        {
            feedbackText.text = options[index].feedbackText;
        }
        else
        {
            feedbackText.text = "";
        }
    }

    void SetButtonTexts()
    {
        for (int i = 0; i < optionButtons.Length; i++)
        {
            if (i < options.Length)
            {
                optionButtons[i].gameObject.SetActive(true);
                optionButtons[i].GetComponentInChildren<TMP_Text>().text = options[i].questionText;
            }
            else
            {
                optionButtons[i].gameObject.SetActive(false); // Hide unused buttons
            }
        }
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
