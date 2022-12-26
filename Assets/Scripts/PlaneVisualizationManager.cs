using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class PlaneVisualizationManager : MonoBehaviour
{
    [SerializeField] private ARRaycastManager arRaycastManager;
    [SerializeField] private ARPlaneManager arPlaneManager;
    [SerializeField] private Camera arCamera;

    public List<ARRaycastHit> GetAllRaycastHits()
    {
        var centre = arCamera.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        var hits = new List<ARRaycastHit>();
        arRaycastManager.Raycast(centre, hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes);
        return hits;
    }

    public ARPlane GetPlane()
    {
        var hits = GetAllRaycastHits();
        if(hits.Count <= 0) return null;

        var hit = hits[0];
        return arPlaneManager.GetPlane(hit.trackableId);
    }

    public Pose? GetPose()
    {
        var hits = GetAllRaycastHits();
        if(hits.Count<=0) return null;

        return hits[0].pose;
    }
}