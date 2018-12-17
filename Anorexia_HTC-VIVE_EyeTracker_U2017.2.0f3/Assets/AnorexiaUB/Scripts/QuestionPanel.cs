using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionPanel : MonoBehaviour {

    public Slider stressBar;
    public Text valueText;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void UpdateStressValue()
    {
        float value = stressBar.value;
        valueText.text = value.ToString() + "%";
    }
}
