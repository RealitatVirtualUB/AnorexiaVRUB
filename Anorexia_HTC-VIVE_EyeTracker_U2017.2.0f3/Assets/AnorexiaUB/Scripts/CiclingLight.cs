using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CiclingLight : MonoBehaviour {
    public float deltaAngle = 0.1f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
        this.transform.Rotate(deltaAngle, 0, deltaAngle, Space.Self);
       
	}
}
