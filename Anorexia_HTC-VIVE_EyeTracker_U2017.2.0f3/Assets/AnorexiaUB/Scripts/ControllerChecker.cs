using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ControllerChecker : MonoBehaviour {

    private SteamVR_TrackedObject trackedObj;
    private SteamVR_Controller.Device Controller{
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    public GraphicRaycaster m_Raycaster;
    PointerEventData m_PointerEventData;
    public EventSystem m_EventSystem;

    void Awake(){
        trackedObj = GetComponent<SteamVR_TrackedObject>();
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
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && (hit.collider.gameObject.tag == "QuestionButton"))
            {
                Debug.Log("quest slider hited");
                Debug.DrawLine(hit.transform.position, hit.transform.position + transform.forward * 100);

            }
            else
            {
                Debug.Log("fuck off, doesn't work yet");
                Debug.DrawLine(hit.transform.position, hit.transform.position + transform.forward * 100);

            }
        }
        if (Controller.GetHairTriggerUp()){
            Debug.Log(" Trigger Release");
        }
    }
}
