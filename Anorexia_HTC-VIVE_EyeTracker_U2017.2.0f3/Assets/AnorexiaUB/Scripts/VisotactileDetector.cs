using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisotactileDetector : MonoBehaviour {

    private SteamVR_TrackedObject trackedObj;
    private bool inZone = false;
    int touchingZones = 0;
    public Vector3 initSpherePos = Vector3.zero;
    //public Vector3 desiredPos = Vector3.zero;
    public float factor = 0.05f;

    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }
    // Use this for initialization
    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
        //initSpherePos = this.transform.position;
        //desiredPos = initSpherePos;


    }

    //void Start()
    //{
    //    initSpherePos = this.transform.position;
    //    desiredPos = initSpherePos;
    //}

    // Update is called once per frame
    void Update () {
        Vector3 objective = Vector3.zero;
        objective = initSpherePos - this.transform.localPosition;

        if (!inZone && objective.magnitude > factor)
        {
            Debug.Log("ending touching body");
            this.transform.localPosition += (objective.normalized)/1000;
        }
        else if (!inZone) this.transform.localPosition = initSpherePos;
            //objective = objective.normalized;
    }

    void OnCollisionEnter(Collision other)
    {
        
        touchingZones++;
        //this.transform.localPosition = other.transform.position;
        inZone = true;
        Debug.Log("starting collision " + inZone);
    }

    void OnCollisionExit(Collision other)
    {

        Debug.Log("ending collision " + inZone + touchingZones + " tag " + other.gameObject.tag);
        touchingZones--;

        if (touchingZones <=0)
        {
            //desiredPos = initSpherePos;
            this.GetComponent<Rigidbody>().velocity = Vector3.zero;
            //this.GetComponent<HingeJoint>().constantForce
            inZone = false;
        }
        //this.transform.localPosition = initSpherePos;
    }
   

}
