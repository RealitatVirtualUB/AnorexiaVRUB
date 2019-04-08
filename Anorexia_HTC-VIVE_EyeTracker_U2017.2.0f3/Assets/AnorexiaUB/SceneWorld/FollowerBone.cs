using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerBone : MonoBehaviour {
    public Transform followed;
    private Vector3 lastLocalPosition;

    public bool trackPosition = true;
    public bool trackRotation = true;

	// Use this for initialization
	void Start () {
        this.lastLocalPosition = this.followed.localPosition;
	}
	
	// Update is called once per frame
	void Update () {
        if (this.trackPosition)
        {
            //Vector3 delta = this.followed.localPosition - this.lastLocalPosition;
            this.lastLocalPosition = this.followed.localPosition;
            this.transform.localPosition = this.followed.localPosition;//+= delta;
        }

        if (this.trackRotation)
        {
            this.transform.localRotation = this.followed.localRotation;
        }
    }
}
