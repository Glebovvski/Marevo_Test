using System.Collections;
using UnityEngine;

public class ARSpawnManager : MonoBehaviour
{
    [SerializeField] private PlaneVisualizationManager planeVisualizationManager;
    [SerializeField] private SizeWindowManager sizeWindowManager;

    [SerializeField] private ARPanelManager panelManager;

    [SerializeField] private ARObject prefab;

    public Pose? Pose { get; private set; }
    private bool IsSpawned { get; set; } = false;
    private bool IsARStarted { get; set; } = false;
    private ARObject ARObject {get;set;}

    private void Start()
    {
        sizeWindowManager.OnStartAR += StartAfterFrame;
        panelManager.OnClose += DestroyObject;
        IsSpawned = false;
    }

    private void StartAfterFrame()
    {
        StartCoroutine(StartARSpawner());
    }

    IEnumerator StartARSpawner()
    {
        yield return new WaitForEndOfFrame();
        IsARStarted = true;
    }

    public void DestroyObject()
    {
        Destroy(ARObject.gameObject);
        IsARStarted = false;
        IsSpawned = false;
    }

    private void Update()
    {
        if (!IsARStarted)
            return;
        if (IsSpawned)
            return;

#if UNITY_EDITOR
        var btn = Input.GetMouseButtonUp(0);
        if (!btn)
            return;
        ARObject = Instantiate(prefab, null);
        ARObject.transform.position = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        ARObject.InitData(
            sizeWindowManager.Width,
            sizeWindowManager.Length,
            sizeWindowManager.Depth,
            sizeWindowManager.Data.Texture
        );
        IsSpawned = true;
#else
        if (Input.touchCount == 0)
            return;
        Pose = planeVisualizationManager.GetPose();
        var touch = Input.GetTouch(0);
        ARObject = Instantiate(prefab, Pose.Value.position, Pose.Value.rotation);
        ARObject.InitData(
            sizeWindowManager.Width,
            sizeWindowManager.Length,
            sizeWindowManager.Depth,
            sizeWindowManager.Data.Texture
        );
        IsSpawned = true;
#endif
    }

    private void OnDestroy()
    {
        sizeWindowManager.OnStartAR -= StartAfterFrame;
        panelManager.OnClose -= DestroyObject;
    }
}
