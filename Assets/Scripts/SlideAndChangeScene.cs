using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SlideAndChangeScene : MonoBehaviour
{
    public Slider slider; // Assign your slider in the Inspector
    public float requiredValue = 100f; // The value the player must reach to avoid reset
    public float resetSpeed = 2f; // Speed at which the slider returns to 0

    private bool dragging = false; // To track if the player is dragging the slider

    void Start()
    {
        // Ensure the slider starts at 0
        slider.value = 0;
    }

    void Update()
    {
        // Check if the player is dragging the slider
        if (Input.GetMouseButton(0))
        {
            dragging = true;
        }
        else if (dragging && !Input.GetMouseButton(0))
        {
            // Player released the drag
            dragging = false;

            // If the slider wasn't dragged fully to the required value, reset it
            if (slider.value < requiredValue)
            {
                StartCoroutine(ResetSlider());
            }
        }
    }

    // Coroutine to smoothly reset the slider back to 0
    IEnumerator ResetSlider()
    {
        while (slider.value > 0)
        {
            slider.value = Mathf.MoveTowards(slider.value, 0, resetSpeed * Time.deltaTime);
            yield return null; // Wait for the next frame
        }
    }
}