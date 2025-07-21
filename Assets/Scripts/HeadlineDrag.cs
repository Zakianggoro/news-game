using UnityEngine;
using UnityEngine.EventSystems;

public class HeadlineDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
    public Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    void Awake() {
    rectTransform = GetComponent<RectTransform>();
    canvasGroup = GetComponent<CanvasGroup>();

    // Auto-assign canvas kalau lupa isi di Inspector
    if (canvas == null)
        canvas = GetComponentInParent<Canvas>();
    }

    public void OnBeginDrag(PointerEventData eventData) {
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData) {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData) {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }
}
