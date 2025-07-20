using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class QTEManager : MonoBehaviour
{
    public Button readyButton;
    public Button[] optionButtons;
    public TMP_Text questionText;
    public TMP_Text feedbackText;
    public TMP_Text timerText;
    public GameObject optionPanel;

    public string nextSceneName = "NextScene"; // set this in Inspector
    public float countdownTime = 10f;

    private string question = "Which question is the most appropriate to ask?";
    private int correctIndex;
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
            int index = i; // prevent closure issue
            optionButtons[i].onClick.AddListener(() => OnOptionSelected(index));
        }
    }

    void StartMiniGame()
    {
        gameStarted = true;
        answered = false;
        timer = countdownTime;

        readyButton.gameObject.SetActive(false);
        optionPanel.SetActive(true);
        questionText.text = question;
        feedbackText.text = "";

        correctIndex = Random.Range(0, optionButtons.Length);
        SetButtonTexts();
    }

    void Update()
    {
        if (!gameStarted || answered) return;

        timer -= Time.deltaTime;
        timerText.text = Mathf.CeilToInt(timer).ToString();

        if (timer <= 0f)
        {
            gameStarted = false;
            timerText.text = "0";
            feedbackText.text = "Time's up!";
            // optionally: disable buttons
        }
    }

    void OnOptionSelected(int index)
    {
        if (!gameStarted || answered) return;

        answered = true;
        gameStarted = false;

        if (index == correctIndex)
        {
            feedbackText.text = "You picked the right question. The witness gives a satisfied answer.";
            Invoke("LoadNextScene", 2f); // Wait 2 seconds before changing scene
        }
        else
        {
            feedbackText.text = "Wrong.";
        }
    }

    void SetButtonTexts()
    {
        string[] dummyQuestions = {
            "Where were you last night?",
            "Do you like pizza?",
            "Can I go home?",
            "Did you see the suspect?"
        };

        // Shuffle if you like, for now set as is
        for (int i = 0; i < optionButtons.Length; i++)
        {
            optionButtons[i].GetComponentInChildren<TMP_Text>().text = dummyQuestions[i];
        }
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
