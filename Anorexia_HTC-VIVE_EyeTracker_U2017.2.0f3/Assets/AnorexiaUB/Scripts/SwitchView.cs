using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchView : MonoBehaviour {
    private Vector3 initPos = Vector3.zero;
    private Vector3 initSize = Vector3.one;
    public GameObject bigScreenPos;

    bool minimized = true;
	// Use this for initialization
	void Start () {
        initPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SwitchPosAndScale()
    {
        if (!this.GetComponent<RawImage>()) return;
        if (minimized)
        {
            this.GetComponent<RawImage>().transform.position = bigScreenPos.transform.position;
            this.GetComponent<RawImage>().transform.localScale = bigScreenPos.transform.localScale;
            minimized = false;
        }
        else
        {
            this.GetComponent<RawImage>().transform.position = initPos;
            this.GetComponent<RawImage>().transform.localScale = initSize;
            minimized = true;
        }
    }
}
