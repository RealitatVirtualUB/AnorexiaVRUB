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
    public QuestionController qc;

    public bool inputActivated = false;
    private bool parseValues = false;
    public float timeBTWupdates = 0.5f;

    private float timer = 0;

    void Awake(){
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }
	
	// Update is called once per frame
	void Update () {
        if (inputActivated)
        {
            timer += Time.deltaTime;
            TakeInput();
        }
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

    void TakeInput()
    {
        #region trigger
        //down
        if (Controller.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            qc.EndQuestion();
            //add data
            Debug.Log("trigger down");
        }
        //up
        if (Controller.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
        {
            //Debug.Log("trigger up");
        }
        //value
        //Vector2 triggerValue = Controller.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger);
        //Debug.Log("trigger value " + triggerValue);
        #endregion

        #region touchpad
        //down
        if (Controller.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad))
        {
            //Debug.Log("Touchpad down");
            parseValues = true;
            //Debug.Log("touchpad touch detected");
        }
        //up
        if (Controller.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad))
        {
            //Debug.Log("Touchpad up");
            parseValues = false;
            Debug.Log("ending touchs");
        }

        //value
        Vector2 touchpadValue = Controller.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad);
        if (parseValues && timer > timeBTWupdates)
        {
            qc.questionPanel.GetComponent<QuestionPanel>().MoveSlider(touchpadValue.x);
            timer = 0;
        }

        //Debug.Log("Touchpad value " + touchpadValue);
        #endregion
    }
}
