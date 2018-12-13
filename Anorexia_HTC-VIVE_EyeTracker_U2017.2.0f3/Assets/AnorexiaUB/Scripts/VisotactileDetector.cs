using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisotactileDetector : MonoBehaviour {

    private SteamVR_TrackedObject trackedObj;
    private bool inZone = false;
    int touchingZones = 0;
    Vector3 initSpherePos = Vector3.zero;

    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }
    // Use this for initialization
    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
        initSpherePos = this.transform.position;
    }


    // Update is called once per frame
    void Update () {
        if (inZone)
        {
            Debug.Log("touching body: " + touchingZones);
        }
    }

    void OnCollisionStart(Collision other)
    {
        touchingZones++;
        this.transform.localPosition = other.transform.position;
        Debug.Log("starting collision");
    }

    void OnCollisionExit(Collision other)
    {
        touchingZones--;
        if (touchingZones <=0)
        {
            this.transform.localPosition = initSpherePos;
            this.GetComponent<Rigidbody>().velocity = Vector3.zero;
            //this.GetComponent<HingeJoint>().constantForce
        }
        //this.transform.localPosition = initSpherePos;
        Debug.Log("ending collision");
    }
   

}
