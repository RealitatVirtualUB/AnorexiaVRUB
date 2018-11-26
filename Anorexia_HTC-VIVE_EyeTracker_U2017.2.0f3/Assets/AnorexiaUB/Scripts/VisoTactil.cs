using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisoTactil : MonoBehaviour {

    private SteamVR_TrackedObject trackedObj;
    private GameObject trackedCollisionObject;
    public GameObject therapistController;
    public float distanceToReact;
    public float controllerZOffset;
    public float originOffset;
    private bool _visTouchActive;
    private bool _unused;


    private SteamVR_Controller.Device Controller{
        get {return SteamVR_Controller.Input((int)trackedObj.index);}
    }

    void Awake(){
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    void Start(){
        //trackedCollisionObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        //Destroy(trackedCollisionObject.GetComponent<SphereCollider>());
        //trackedCollisionObject.transform.localScale = Vector3.one * 0.01f;
        therapistController = (GameObject)GameObject.Instantiate(therapistController);
        therapistController.SetActive(false);
        
        //c.transform.position = r.point;
    }
    public void ActivateVisTouch(){
        _visTouchActive = !_visTouchActive;
        int layer = (_visTouchActive) ? Layers.THERAPIST_VIEW : Layers.DEFAULT;
        gameObject.SetLayer(layer, true);
    }

        // Update is called once per frame
    void Update () {
        CheckSurfaceByProximity();
        CheckUnusedControllers();
	}

    private void CheckUnusedControllers(){
        if (_unused) return;
        if (Controller.GetHairTrigger()){
            _unused = true;
            Destroy(therapistController.gameObject);
            Destroy(gameObject.GetComponent<VisoTactil>());
        }
            
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Vector3 direction = transform.forward * 10;
        Gizmos.DrawRay(transform.position - transform.forward * originOffset, direction);
        //Debug.DrawRay(mirrorPoint, direction, Color.red);
    }

    public void CheckSurfaceByProximity(){
        RaycastHit r;
        Ray ray = new Ray();
        Vector3 origin = transform.position - transform.forward * originOffset;
        ray.origin = origin;
        ray.direction = transform.forward;
        if (Physics.Raycast(ray, out r, 1 << Layers.MODEL))
        {
            //Debug.Log(r.collider.name + " Trigger Press");
            //trackedCollisionObject.transform.position = r.point;
            if (Vector3.Distance(origin, r.transform.position) < distanceToReact){
                therapistController.SetActive(true);
                therapistController.transform.position = r.point;
                therapistController.transform.position -= transform.forward * controllerZOffset;
                therapistController.transform.rotation = transform.rotation;
            }else{
                therapistController.SetActive(false);
            }
        }else{
            therapistController.SetActive(false);

        }
    }
            

    public void CheckSurface()
    {
        
        if (Controller.GetHairTrigger())
        {
            RaycastHit r;
            Ray ray = new Ray();
            ray.origin = transform.position - transform.forward * originOffset;
            ray.direction = transform.forward;
            if (Physics.Raycast(ray, out r)){
                Debug.Log(r.collider.name + " Trigger Press");
                //trackedCollisionObject.transform.position = r.point;

                therapistController.SetActive(true);
               
                therapistController.transform.position = r.point;
                therapistController.transform.position -= transform.forward * controllerZOffset;
                therapistController.transform.rotation = transform.rotation;

            }

        }
        if (Controller.GetHairTriggerUp())
        {
            Debug.Log(gameObject.name + " Trigger Release");
            therapistController.SetActive(false);
        }
/*

        // 1
        if (Controller.GetAxis() != Vector2.zero)
        {
            Debug.Log(gameObject.name + Controller.GetAxis());
        }
        // 3

        // 4
        if (Controller.GetPressDown(SteamVR_Controller.ButtonMask.Grip))
        {
            Debug.Log(gameObject.name + " Grip Press");
        }

        // 5
        if (Controller.GetPressUp(SteamVR_Controller.ButtonMask.Grip))
        {
            Debug.Log(gameObject.name + " Grip Release");
        }
*/
    }
}
