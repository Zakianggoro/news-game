using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler {
    public bool isFactualZone;
    public HeadlineManager manager;

    public void OnDrop(PointerEventData eventData) {
        var headline = eventData.pointerDrag.GetComponent<HeadlineCard>();
        if (headline != null) {
            bool isCorrect = headline.data.isFactual == isFactualZone;
            manager.CheckAnswer(isCorrect);
            Destroy(eventData.pointerDrag);
            manager.SpawnNextHeadline();
        }
    }
}
