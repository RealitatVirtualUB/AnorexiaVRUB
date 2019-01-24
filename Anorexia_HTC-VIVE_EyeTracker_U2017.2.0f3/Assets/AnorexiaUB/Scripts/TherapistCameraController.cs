using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TherapistCameraController : MonoBehaviour {

    public float speed = 20f;
    public float speedH = 2.0f;
    public float speedV = 2.0f;

    private float yaw = 0.0f;
    private float pitch = 0.0f;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 pos = transform.position;
        Vector3 forward = Vector3.zero;
        Vector3 right = Vector3.zero;
        yaw += speedH * Input.GetAxis("Mouse X");
        pitch -= speedV * Input.GetAxis("Mouse Y");

        if (Input.GetKey("w")) forward = transform.forward;
        if (Input.GetKey("s"))forward = -transform.forward;
        if (Input.GetKey("d"))right = transform.right;
        if (Input.GetKey("a")) right = -transform.right;
        

        pos = (forward + right) * (speed * Time.deltaTime);
        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);

        transform.position += pos;
    }
}
