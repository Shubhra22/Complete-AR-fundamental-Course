using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using UnityEngine.Video;
public class VbuttonManager : MonoBehaviour, IVirtualButtonEventHandler 
{

    public VideoPlayer player;
    public GameObject playButton;
    // Use this for initialization
	void Start () 
    {
        GetComponent<VirtualButtonBehaviour>().RegisterEventHandler(this);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnButtonPressed(VirtualButtonBehaviour vb)
    {
        player.Play();
        playButton.GetComponent<Renderer>().enabled = false;
    }

    public void OnButtonReleased(VirtualButtonBehaviour vb)
    {
        player.Pause();
        playButton.GetComponent<Renderer>().enabled = true;
    }
}
