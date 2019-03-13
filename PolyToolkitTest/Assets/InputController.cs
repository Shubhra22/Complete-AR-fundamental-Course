using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;

public class InputController : MonoBehaviour
{

    public static InputController instance;

    TrackableHit hit;

    GameObject targetModel;
	// Use this for initialization
	void Start ()
    {
        instance = this;
	}

	private void Update()
	{
        Touch touch;
        if (Input.touchCount < 1 || (touch = Input.GetTouch(0)).phase != TouchPhase.Began)
        {
            return;
        }

        TrackableHitFlags raycastFilter = TrackableHitFlags.PlaneWithinPolygon |
        TrackableHitFlags.FeaturePointWithSurfaceNormal;

        if(Frame.Raycast(touch.position.x, touch.position.y,raycastFilter, out hit))
        {
            // Create the ARCore anchor.
            var anchor = hit.Trackable.CreateAnchor(hit.Pose);
            // Reparent the object to the anchor.
            targetModel.transform.SetParent(anchor.transform, false);
            // Now we're ready to show it!
            targetModel.SetActive(true);

        }
    }
	public void ControlModel(GameObject Model, string modelName)
    {
        targetModel = Model;
        targetModel.name = modelName;
    }


}
