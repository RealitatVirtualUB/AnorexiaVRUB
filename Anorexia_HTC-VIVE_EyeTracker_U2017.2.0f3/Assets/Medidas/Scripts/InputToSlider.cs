using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputToSlider : MonoBehaviour {

    public Slider slider;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetSlider(){
        slider.value = float.Parse(gameObject.GetComponent<InputField>().text);
        
    }
}
