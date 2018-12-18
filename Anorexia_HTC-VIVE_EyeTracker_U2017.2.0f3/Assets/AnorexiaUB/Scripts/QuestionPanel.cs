using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionPanel : MonoBehaviour {

    public Slider stressBar;
    public Text valueText;
    public Text questionText;
    public int updateValue = 1;
	// Use this for initialization

    public void UpdateStressValue()
    {
        float value = stressBar.value;
        valueText.text = value.ToString() + "%";
    }

    public void ResetStressBarValue()
    {
        stressBar.value = 50.0f;
        float value = stressBar.value;
        valueText.text = value.ToString() + "%";
    }

    public void SetQuestion(string q)
    {
        questionText.text = q;
    }

    public void MoveSlider(float v)
    {
        if(v > 0)stressBar.value += updateValue;
        else stressBar.value -= updateValue;
        Debug.Log("moving intensity slider");
    }
}
