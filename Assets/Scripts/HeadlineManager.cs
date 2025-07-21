using UnityEngine;
using TMPro;
using System.Collections.Generic;

[System.Serializable]
public class HeadlineData {
    public string text;
    public bool isFactual;
}

public class HeadlineManager : MonoBehaviour {
    public List<HeadlineData> headlineList;

    public GameObject headlinePrefab;
    public Transform spawnPoint;
    public TMP_Text feedbackText;
    public TMP_Text scoreText;

    private int currentIndex = 0;
    private int score = 0;

    void Start() {
        SpawnNextHeadline();
    }

    public void SpawnNextHeadline() {
        if (currentIndex >= headlineList.Count) {
            feedbackText.text = "Permainan selesai!";
            return;
        }

        GameObject go = Instantiate(headlinePrefab, spawnPoint);
        var card = go.GetComponent<HeadlineCard>();
        card.Init(headlineList[currentIndex]);
        currentIndex++;
    }

    public void CheckAnswer(bool isCorrect) {
        feedbackText.text = isCorrect ? "Benar!" : "Salah!";
        score += isCorrect ? 1 : -1;
        scoreText.text = "Skor: " + score;
    }
}
