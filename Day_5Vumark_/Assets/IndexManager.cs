using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class will be responsible for adding, removing or getting the list where I put the index of the vumarks
public class IndexManager : MonoBehaviour 
{

    List<int> trackedIndex;

    public static IndexManager instance;

	private void Awake()
	{
        instance = this;
	}
	// Use this for initialization
	void Start () 
    {
        trackedIndex = new List<int>();
		
	}
	

    public bool Contains(int index)
    {
        return trackedIndex.Contains(index);
    }


    public void Add(int value)
    {
        trackedIndex.Add(value);
    }

    public void Remove(int value)
    {

        trackedIndex.Remove(value);
    }

    public List<int> GetTrackableIndex()
    {
        return trackedIndex;
    }
 
}
