using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using RedScarf.EasyCSV;
using UnityEngine;
using UnityEngine.Networking;

public class DataManager : MonoBehaviour
{
    public bool DataFetched { get; private set; } = false;

    [SerializeField]
    private List<Data> Data = new List<Data>();

    [SerializeField]
    private bool test = true;

    private const string sheetKey = "1Wj75QfY2F8PkNCTMYvOsL-FxYia2mdGvQITVti1xHMk";

    public event Action<int> OnLoadedAssetsCountChange;
    public event Action<int> OnTableCreated;

    private void Start()
    {
        StartCoroutine(FetchData());
    }

    private IEnumerator FetchData()
    {
        DataFetched = false;
        UnityWebRequest request = new UnityWebRequest(
            string.Format(
                "https://docs.google.com/spreadsheets/d/{0}/export?gid={1}&format=csv",
                sheetKey,
                0
            )
        );
        request.downloadHandler = new DownloadHandlerBuffer();

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError(request.error);
            yield return null;
        }
        else
        {
            var csvData = request.downloadHandler.text;
            ReadData(csvData);
        }
    }

    private async void ReadData(string data)
    {
        DataFetched = false;
        CsvHelper.Init();
        ;
        var table = CsvHelper.Create("Test", data);
        string title = string.Empty;
        string previewLink = string.Empty;
        string textureLink = string.Empty;
        float size = 0;
        var maxValue = table.RowCount;
        OnTableCreated?.Invoke(maxValue);
        for (int i = 1; i < maxValue; i++)
        {
            title = table.Read(i, 0);
            previewLink = table.Read(i, 1);
            textureLink = table.Read(i, 2);
            var preview = await GetRemoteTexture(previewLink);
            var sizeText = table.Read(i, 3);
            size = float.Parse(sizeText.Replace('.', ','));
            Data.Add(new Data(title, preview, size, textureLink));
            OnLoadedAssetsCountChange?.Invoke(i);
        }
        DataFetched = true;
    }

    public static async Task<Texture2D> GetRemoteTexture(string url)
    {
        using (UnityWebRequest www = UnityWebRequestTexture.GetTexture(url))
        {
            var asyncOp = www.SendWebRequest();
            while (asyncOp.isDone == false)
                await Task.Delay(1000 / 30);

            if( www.result!=UnityWebRequest.Result.Success )
            {
#if DEBUG
                Debug.Log($"{www.error}, URL:{www.url}");
#endif
                return null;
            }
            else
            {
                return DownloadHandlerTexture.GetContent(www);
            }
        }
    }

    public List<Data> GetData() => Data;
}
