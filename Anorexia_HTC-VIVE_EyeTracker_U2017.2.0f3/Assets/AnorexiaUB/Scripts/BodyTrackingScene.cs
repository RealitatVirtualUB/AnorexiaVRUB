using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyTrackingScene : MainScene {

    public GameObject bodyPartsButtons;
    private bool _visTouchActive;
    public GameObject therapistController;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ActivateVisTouch()
    {
        model.GetComponent<ModelColliders>().DisableAll();
        _visTouchActive = !_visTouchActive;
        bodyPartsButtons.SetActive(_visTouchActive);
        therapistController.SetActive(_visTouchActive);
    }
}
