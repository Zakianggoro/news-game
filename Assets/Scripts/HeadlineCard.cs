using UnityEngine;
using TMPro;

public class HeadlineCard : MonoBehaviour {
    public HeadlineData data;
    public TextMeshProUGUI headlineText;

    public void Init(HeadlineData headline) {
        data = headline;
        headlineText.text = headline.text;
    }
}
