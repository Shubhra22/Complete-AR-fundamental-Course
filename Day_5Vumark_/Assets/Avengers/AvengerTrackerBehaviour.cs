using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class AvengerTrackerBehaviour : MonoBehaviour, ITrackableEventHandler
{

    TrackableBehaviour avenger_TrackableBehaviour;

    VuMarkManager avenger_Vumark;

	// Use this for initialization
	void Start () 
    {
        avenger_Vumark = TrackerManager.Instance.GetStateManager().GetVuMarkManager();
        avenger_TrackableBehaviour = GetComponent<TrackableBehaviour>();
        if(avenger_TrackableBehaviour!=null)
        {
            avenger_TrackableBehaviour.RegisterTrackableEventHandler(this);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus)
    {
        if (newStatus == TrackableBehaviour.Status.DETECTED || newStatus == TrackableBehaviour.Status.TRACKED || newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            OnTrackerFound();
        }
        else if (previousStatus == TrackableBehaviour.Status.TRACKED && newStatus == TrackableBehaviour.Status.NOT_FOUND)
        {
            OnTrackerLost();
        }

        else if (previousStatus == TrackableBehaviour.Status.TRACKED && newStatus == TrackableBehaviour.Status.NOT_FOUND)
        {
            OnTrackerLost();
        }
    }

    void OnTrackerFound()
    {
        
        foreach (var item in avenger_Vumark.GetActiveBehaviours())
        {
            int targetObj = System.Convert.ToInt32(item.VuMarkTarget.InstanceId.NumericValue -1);
            if(IndexManager.instance.Contains(targetObj))
            {
                continue;
            }
            transform.GetChild(targetObj).gameObject.SetActive(true);
            DeactiveOthers();
            IndexManager.instance.Add(targetObj);
        }
    }

    void OnTrackerLost()
    {        
        foreach (var item in avenger_Vumark.GetActiveBehaviours())
        {
            if(item.transform.name == transform.name) 
            {
                int targetObj = System.Convert.ToInt32(item.VuMarkTarget.InstanceId.NumericValue) - 1;
                transform.GetChild(targetObj).gameObject.SetActive(false);
                IndexManager.instance.Remove(targetObj);
            }


        }
    }

    void DeactiveOthers()
    {
        foreach (var item in IndexManager.instance.GetTrackableIndex())
        {
            transform.GetChild(item).gameObject.SetActive(false);
        }
    }
}
