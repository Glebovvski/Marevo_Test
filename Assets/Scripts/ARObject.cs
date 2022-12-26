using Lean.Touch;
using UnityEngine;

public class ARObject : MonoBehaviour
{
    [SerializeField] private LeanDragTranslate leanDrag;
    private Material materal;

    private void Start()
    {
        leanDrag.Camera = Camera.main;
    }

    public void InitData(float width, float length, float depth, Texture2D texture)
    {
        this.transform.localScale = new Vector3(width, length, depth);
        materal = GetComponent<MeshRenderer>().material;
        materal.mainTexture = texture;
    }
}
