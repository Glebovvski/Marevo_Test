using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Product : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private Image preview;
    public Data Data {get;private set;}

    public event Action<Data> OnSelect;

    public void SetData(Data data)
    {
        Data = data;
        title.text = data.Title;
        preview.sprite = Sprite.Create(data.Preview, new Rect(0,0,data.Preview.width, data.Preview.height), Vector2.zero);
    }

    public void SelectProduct()
    {
        OnSelect?.Invoke(Data);
    }
}
