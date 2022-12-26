using UnityEngine;
using UnityEngine.UI;

public class LoadingManager : MonoBehaviour
{
    [SerializeField] private GameObject laodingPanel;

    [SerializeField] private Slider loadingSlider;

    [SerializeField] private DataManager dataManager;

    private void Start()
    {
        dataManager.OnTableCreated += SetMaxValue;
        dataManager.OnLoadedAssetsCountChange += UpdateLoadingSlider;
    }

    private void UpdateLoadingSlider(int value)
    {
        loadingSlider.value = value;
        if(loadingSlider.value == loadingSlider.maxValue-1)
        Close();
    }

    private void SetMaxValue(int value)
    {
        loadingSlider.maxValue = value;
    }

    private void OnDestroy()
    {
        dataManager.OnTableCreated -= SetMaxValue;
        dataManager.OnLoadedAssetsCountChange -= UpdateLoadingSlider;
    }

    public void Close() => laodingPanel.SetActive(false);
}
