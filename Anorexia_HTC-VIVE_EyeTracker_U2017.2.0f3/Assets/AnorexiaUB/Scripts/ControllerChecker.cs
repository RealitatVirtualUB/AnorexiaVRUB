using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerChecker : MonoBehaviour {

    private SteamVR_TrackedObject trackedObj;
    public GameObject flag;


    private SteamVR_Controller.Device Controller{
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    void Awake(){
        trackedObj = GetComponent<SteamVR_TrackedObject>();
        flag.SetActive(false);
    }

    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        TryController();
	}

    void TryController(){
        if (Controller.GetHairTriggerDown()){
            Debug.Log(" Trigger Press");
            flag.SetActive(true);
        }
        if (Controller.GetHairTriggerUp()){
            Debug.Log(" Trigger Release");
            flag.SetActive(false);
        }
    }
}
