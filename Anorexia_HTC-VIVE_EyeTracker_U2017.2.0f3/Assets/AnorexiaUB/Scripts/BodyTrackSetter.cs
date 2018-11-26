using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyTrackSetter : MonoBehaviour {


    public float overlapRadius = 0.2f;
    private Color _currentColor = Color.grey;
    public Color missingTrackerColor;
    public Color correctTrackerColor;
    public Color conflictTrackerColor;
    //public GameObject 
    public BodyPartId choosenDummie;
    public bool trackReady;
    public BODYPARTS bodyPart;

    void Awake(){
        _currentColor = missingTrackerColor;
    }
    	
	
	void Update () {
        DetectTrackers();
	}

    void DetectTrackers(){
        Collider[] trackedObjects = Physics.OverlapSphere(transform.position, overlapRadius, 1<<Layers.TRACKERS);
        if(trackedObjects.Length > 1){
            _currentColor = conflictTrackerColor;
            trackReady = true;
        }else if (trackedObjects.Length > 0){
            _currentColor = correctTrackerColor;
            trackReady = true;
        }else{
            _currentColor = missingTrackerColor;
            trackReady = false;
        }
        //get closest tracker
        if(trackedObjects.Length > 0)
        {
            float distance = float.MaxValue;
            int index = 0;
            for (int i = 0; i < trackedObjects.Length; i++)
            {
                if(Vector3.Distance(transform.position, trackedObjects[i].transform.position) < distance){
                    index = i;
                    distance = Vector3.Distance(transform.position, trackedObjects[i].transform.position); 
                }
            }
            choosenDummie = trackedObjects[index].gameObject.GetComponent<BodyPartId>();
        }else
        {
            choosenDummie = null;
        }


    }

    void OnDrawGizmos()
    {
        Gizmos.color = _currentColor;
        Gizmos.DrawSphere(transform.position, overlapRadius);
      
    }

    
}
