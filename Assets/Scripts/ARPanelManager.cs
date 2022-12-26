using System;
using UnityEngine;

public class ARPanelManager : MonoBehaviour
{
    [SerializeField] private SizeWindowManager sizeWindowManager;
    [SerializeField] private GameObject arPanel;

    public event Action OnClose;
    private void Start()
    {
        sizeWindowManager.OnStartAR += Open;
    }

    private void Open()
    {
        arPanel.SetActive(true);
    }

    public void Close()
    {
        arPanel.SetActive(false);
        OnClose?.Invoke();
    }

    private void OnDestroy()
    {
        sizeWindowManager.OnStartAR -= Open;
    }
}
