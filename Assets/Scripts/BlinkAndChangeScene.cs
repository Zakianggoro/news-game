using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionWithBlinkPanel : MonoBehaviour
{
    public int sceneIndex; // Index of the next scene to load
    [SerializeField] private GameObject[] panelsToDeactivate; // Panels to deactivate before scene transition
    [SerializeField] private GameObject blinkPanel; // Panel to use for fade-in/out effect
    [SerializeField] private float deactivateDelay = 2f; // Delay before activating the blink panel
    [SerializeField] private float fadeDuration = 1f; // Duration of the fade-in/out effect
    [SerializeField] private float waitBeforeSceneChange = 1.5f; // Time to wait before changing the scene

    [Header("Optional Sound Settings")]
    [SerializeField] private bool playSoundOnClick = false; // Toggle sound on/off
    [SerializeField] private AudioSource clickSound; // Reference to the audio source for the sound

    private CanvasGroup blinkPanelCanvasGroup;

    void Start()
    {
        // Ensure the blink panel is active and has a CanvasGroup for controlling opacity
        blinkPanelCanvasGroup = blinkPanel.GetComponent<CanvasGroup>();
        if (blinkPanelCanvasGroup == null)
        {
            blinkPanelCanvasGroup = blinkPanel.AddComponent<CanvasGroup>();
        }

        // Start with the blink panel visible (fade-in effect at scene start)
        blinkPanelCanvasGroup.alpha = 1f;
        StartCoroutine(FadeOutOnSceneLoad());
    }

    public void DeactivatePanelsAndChangeScene()
    {
        // Check if sound should be played
        if (playSoundOnClick && clickSound != null)
        {
            clickSound.Play();  // Play the sound
        }

        StartCoroutine(DeactivatePanels());
    }

    private IEnumerator DeactivatePanels()
    {
        // Deactivate specified panels with animation
        foreach (GameObject panel in panelsToDeactivate)
        {
            yield return StartCoroutine(AnimatePanelOut(panel));
        }

        // Wait for the specified delay after deactivating the panels
        yield return new WaitForSeconds(deactivateDelay);

        // Start the fade-in effect before changing scenes
        yield return StartCoroutine(FadeInBeforeSceneChange());

        // Change the scene
        SceneManager.LoadScene(sceneIndex);
    }

    private IEnumerator AnimatePanelOut(GameObject panel)
    {
        RectTransform panelRectTransform = panel.GetComponent<RectTransform>();
        Vector3 initialScale = panelRectTransform.localScale;

        // Animate the panel out (scale down)
        float animationDuration = 0.5f;
        float elapsedTime = 0f;

        while (elapsedTime < animationDuration)
        {
            panelRectTransform.localScale = Vector3.Lerp(initialScale, Vector3.zero, elapsedTime / animationDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the panel is fully hidden
        panelRectTransform.localScale = Vector3.zero;
        panel.SetActive(false); // Deactivate the panel
    }

    private IEnumerator FadeOutOnSceneLoad()
    {
        // Fade the blink panel out at the start of the scene
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            blinkPanelCanvasGroup.alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the blink panel is fully transparent and deactivate it
        blinkPanelCanvasGroup.alpha = 0f;
        blinkPanel.SetActive(false);
    }

    private IEnumerator FadeInBeforeSceneChange()
    {
        // Reactivate the blink panel and fade it in before scene change
        blinkPanel.SetActive(true);
        blinkPanelCanvasGroup.alpha = 0f;

        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            blinkPanelCanvasGroup.alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the blink panel is fully opaque
        blinkPanelCanvasGroup.alpha = 1f;

        // Wait for a brief moment before changing the scene
        yield return new WaitForSeconds(waitBeforeSceneChange);
    }
}