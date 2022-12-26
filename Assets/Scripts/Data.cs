using System;
using UnityEngine;

[Serializable]
public class Data
{
    [SerializeField] private string title;

    [SerializeField] private Texture2D preview;

    [SerializeField] private Texture2D texture;

    [SerializeField] private float size;

    public string Title => title;
    public Texture2D Preview => preview;
    public Texture2D Texture => texture;
    public float Size => size;
    public string TextureLink { get; set; }

    public Data(string title, Texture2D preview, float size, string textureLink)
    {
        this.title = title;
        this.preview = preview;
        this.size = size;
        TextureLink = textureLink;
    }

    public void SetTexture(Texture2D texture)
    {
        this.texture = texture;
    }
}
