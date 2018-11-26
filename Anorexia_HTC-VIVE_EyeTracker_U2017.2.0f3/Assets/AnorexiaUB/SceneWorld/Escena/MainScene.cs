using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainScene : MonoBehaviour {

    public Model model;
    public bool activateSplashScreen;
    public GameObject splashImage;
    public GameObject bodyPartsButtons;
    private bool _visTouchActive;
    public GameObject therapistController;
    //public Toggle touch;
    

    
	// Use this for initialization
	void Start () {
        ActivateSplashImage();
	}

    void ActivateSplashImage(){
        if (activateSplashScreen) splashImage.SetActive(true); 
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ThinChanged(Slider s){
        model.avatarComponents.SetThin(s);
    }

    public void FatChanged(Slider s){
        model.avatarComponents.SetFat(s);
    }

    public void ActivateVisTouch(){
        model.GetComponent<ModelColliders>().DisableAll();
        _visTouchActive = !_visTouchActive;
        bodyPartsButtons.SetActive(_visTouchActive);
        therapistController.SetActive(_visTouchActive);
    }
}
