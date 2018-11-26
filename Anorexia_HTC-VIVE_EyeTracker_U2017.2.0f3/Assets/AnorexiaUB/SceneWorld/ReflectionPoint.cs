using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectionPoint : MonoBehaviour {

    private Camera _cam;
    private Vector3 mirrorPoint;


    void Awake(){
        if (!_cam) _cam = GetComponent<Camera>();
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        GetReflectionPoint();
	}

    void GetReflectionPoint()
    {
        RaycastHit r;
        Ray ray = new Ray();
        ray.origin = _cam.transform.position;
        ray.direction = _cam.transform.forward;
        if (Physics.Raycast(ray, out r, 1<<11))
        {
            //Debug.Log(r.collider.name);
            mirrorPoint = r.point;
        }

    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 direction = Vector3.forward * -10;
        Gizmos.DrawRay(mirrorPoint, direction);
        Debug.DrawRay(mirrorPoint, direction, Color.red);
    }
}
