using System.Collections;
using UnityEngine;

public class ContentManager : MonoBehaviour
{
    [SerializeField] private DataManager dataManager;
    [SerializeField] private SizeWindowManager sizeWindowManager;
    [SerializeField] private ARPanelManager arPanelManager;
    [SerializeField] private Product productPrefab;
    [SerializeField] private GameObject content;
    [SerializeField] private GameObject catalogPanel;

    private void Start()
    {
        sizeWindowManager.OnStartAR += Close;
        arPanelManager.OnClose += Open;
        StartCoroutine(FillCatalog());
    }

    private IEnumerator FillCatalog()
    {
        yield return new WaitUntil(() => dataManager.DataFetched);
        var data = dataManager.GetData();
        foreach(var productData in data)
        {
            var product = Instantiate(productPrefab, content.transform);
            product.SetData(productData);
            product.OnSelect+=sizeWindowManager.Open;
        }
    }

    private void Open() => catalogPanel.SetActive(true);

    private void OnDestroy()
    {
        sizeWindowManager.OnStartAR -= Close;
        arPanelManager.OnClose -= Open;
    }

    private void Close() => catalogPanel.SetActive(false);
}
