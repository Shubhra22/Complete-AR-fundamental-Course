using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recorder : MonoBehaviour {

    AndroidJavaClass plugin;
	// Use this for initialization
	void Start () 
    {
        plugin = new AndroidJavaClass("com.joysticklab.screenrecorder.Recorder");

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartRecording()
    {
        plugin.Call("initRecorder");
        plugin.Call("shareScreen");
    }
}
