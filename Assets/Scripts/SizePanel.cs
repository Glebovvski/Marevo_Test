using UnityEngine;
using UnityEngine.EventSystems;

public class SizePanel : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        Close();
    }

    private void Close() => this.gameObject.SetActive(false);
}
