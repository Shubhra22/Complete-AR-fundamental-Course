using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class UDTManager : MonoBehaviour, IUserDefinedTargetEventHandler
{

    UserDefinedTargetBuildingBehaviour udt_targetBuildingBehaviour;

    ObjectTracker objectTracker;
    DataSet dataSet;

    ImageTargetBuilder.FrameQuality udt_FrameQuality;

    public ImageTargetBehaviour targetBehaviour;

    int targetCounter;
    bool didPress;
    void Start()
    {
        udt_targetBuildingBehaviour = GetComponent<UserDefinedTargetBuildingBehaviour>(); // get UserDefinedTargetBuildingBehaviour from currrent game object.
        if(udt_targetBuildingBehaviour) // UserDefinedTargetBuildingBehaviour has been found
        {
            udt_targetBuildingBehaviour.RegisterEventHandler(this);
        }
    }

    // This method updates the framequality
    public void OnFrameQualityChanged(ImageTargetBuilder.FrameQuality frameQuality)
    {
        udt_FrameQuality = frameQuality;
        print(udt_FrameQuality);
        //throw new System.NotImplementedException();
    }

    public void OnInitialized()
    {
        objectTracker = TrackerManager.Instance.GetTracker<ObjectTracker>();
        if(objectTracker!=null) // if objectTracker is not null
        {
            dataSet = objectTracker.CreateDataSet(); // creating a new dataset
            objectTracker.ActivateDataSet(dataSet);
        }
    }

    public void OnNewTrackableSource(TrackableSource trackableSource)
    {
        targetCounter++;
        objectTracker.DeactivateDataSet(dataSet);

        dataSet.CreateTrackable(trackableSource, targetBehaviour.gameObject);

        objectTracker.ActivateDataSet(dataSet);

        udt_targetBuildingBehaviour.StartScanning();

        //throw new System.NotImplementedException();
    }


    public void BuildTarget()
    {
        if(udt_FrameQuality == ImageTargetBuilder.FrameQuality.FRAME_QUALITY_HIGH)
        {

            //print(targetBehaviour.GetComponent<DefaultTrackableEventHandler>().didclick);
            didPress = !didPress;
            targetBehaviour.GetComponent<DefaultTrackableEventHandler>().HideorShowOnButtonPress(didPress);
            // I want to build a new target
            udt_targetBuildingBehaviour.BuildNewTarget(targetCounter.ToString(),targetBehaviour.GetSize().x);

        }
    }

}
