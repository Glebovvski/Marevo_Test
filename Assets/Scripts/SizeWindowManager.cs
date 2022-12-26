using UnityEngine;
using TMPro;
using System;
using System.Globalization;

public class SizeWindowManager : MonoBehaviour
{
    [SerializeField]
    private GameObject sizePanel;

    [SerializeField]
    private TMP_InputField widthInput;

    [SerializeField]
    private TMP_InputField lengthInput;

    [SerializeField]
    private TMP_InputField depthInput;

    [SerializeField]
    private ARPanelManager arPanelManager;

    [SerializeField]
    private Camera mainCamera;

    [SerializeField]
    private Camera arCamera;

    public float Width { get; private set; }
    public float Length { get; private set; }
    public float Depth { get; private set; }

    public Data Data { get; private set; }

    public event Action OnStartAR;
    public event Action<float, float, float, Texture2D> OnDataSet;

    private void Start()
    {
        arPanelManager.OnClose += Activate;
        widthInput.onEndEdit.AddListener(SetWidth);
        lengthInput.onEndEdit.AddListener(SetLength);
        depthInput.onEndEdit.AddListener(SetDepth);
    }

    private void SetWidth(string value)
    {
        if (
            float.TryParse(
                value,
                NumberStyles.Any,
                CultureInfo.InvariantCulture,
                out var widthValue
            )
        )
        {
            Width = widthValue;
        }
    }

    private void SetLength(string value)
    {
        if (
            float.TryParse(
                value,
                NumberStyles.Any,
                CultureInfo.InvariantCulture,
                out var lengthValue
            )
        )
        {
            Length = lengthValue;
        }
    }

    private void SetDepth(string value)
    {
        if (
            float.TryParse(
                value,
                NumberStyles.Any,
                CultureInfo.InvariantCulture,
                out var depthValue
            )
        )
        {
            Depth = depthValue;
        }
    }

    public void Ok()
    {
        mainCamera.gameObject.SetActive(false);
        arCamera.gameObject.SetActive(true);

        Close();
        OnDataSet?.Invoke(Width, Length, Depth, Data.Texture);
        OnStartAR?.Invoke();
    }

    public async void Open(Data data)
    {
        Data = data;
        Data.SetTexture(await DataManager.GetRemoteTexture(Data.TextureLink));
        sizePanel.SetActive(true);
    }

    private void Activate()
    {
        arCamera.gameObject.SetActive(false);
        mainCamera.gameObject.SetActive(true);
    }

    public void Close() => sizePanel.SetActive(false);

    private void OnDestroy()
    {
        arPanelManager.OnClose -= Activate;
    }
}
