﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class IMCCalculator : MonoBehaviour {

    public InputField weightField;
    public InputField heightField;
    public InputField ageField;
    public InputField imcField;
    public Slider heightSlider;
    public Slider weightSlider;
    private Color placeHolderInitialColor;
    public IMCData imcData;
    int gender = 0;
    //public List<IMCData> data;

    void Awake(){
        if(heightField != null)placeHolderInitialColor = heightField.placeholder.color;
        imcData = new IMCData();
    }

    public void CheckRange(GameObject field){
        InputField f = field.GetComponent<InputField>();
        if (string.IsNullOrEmpty(f.text)) return;
        float n = float.Parse(f.text);
        if(n < 1 || n > 300){
            f.text = "";
        }
    }

    public void SetGender(int g)
    {
        if (g != 0 || g != 1) return;
        else gender = g;
    }

    public void CalculateIMC(){
        if (!CheckFields()) return;
        float weight = float.Parse(weightField.text);
        float height = float.Parse(heightField.text);
        int age = int.Parse(ageField.text);
        //set height model
        Debug.Log("height is " + height);
        heightSlider.value = Mathf.Clamp01(height/200);
        Debug.Log("height relation is " + heightSlider.value);
        Debug.Log("age is: " + age);

        height /= 100;
        imcData.height = height;
        imcData.weight = weight;
        height *= height;
        imcData.imc = (float)System.Math.Round((weight / height),2);
        imcData.imcIncremented = imcData.imc;
        imcData.sessionNumber = 1;

        //set weight mode
        this.GetComponent<MainScene>().model.avatarComponents.SetAge(age);
        //if (gender == 1) gender *= 100;
        //this.GetComponent<MainScene>().model.avatarComponents.SetGender(gender);
        this.GetComponent<MainScene>().InterpolateIMC(imcData.imc,imcData.height, weightSlider, age, gender);

        imcField.text = imcData.imc.ToString();
        weightField.placeholder.color = placeHolderInitialColor;
        heightField.placeholder.color = placeHolderInitialColor;

    }

    public bool CheckFields(){
        bool weightFilled;
        bool heightFilled;
        bool ageFilled;
        if (string.IsNullOrEmpty(weightField.text)){
            weightField.placeholder.color = Color.red;
            weightFilled = false;
        }else{
            weightFilled = true;
        }

        if (string.IsNullOrEmpty(heightField.text))
        {
            heightField.placeholder.color = Color.red;
            heightFilled = false;
        }else{
            heightFilled = true;
        }

        if (string.IsNullOrEmpty(ageField.text))
        {
            ageField.placeholder.color = Color.red;
            ageFilled = false;
        }
        else
        {
            ageFilled = true;
        }

        if (!weightFilled || !heightFilled || !ageFilled){
            imcField.text = "";
            return false;
        }else{
            return true;
        }
        
    }

    public void LoadData(float pWeight, float pHeight, float pImc)
    {
        weightField.text = "" + pWeight;
        heightField.text = "" + (pHeight * 100);
        imcField.text = "" + pImc;
    }
    
    public void Deserialize()
    {

    } 

    public Hashtable SerializeData()
    {
        Hashtable data = new Hashtable();

        //imcData["IMCData"] = new Hashtable();
        //Hashtable statistics = (Hashtable)modelData["IMCData"];
        data["weight"] = imcData.weight;
        data["height"] = imcData.height;
        data["imc"] = imcData.imc;
        data["imcIncremented"] = imcData.imcIncremented;
        data["sessionNumber"] = imcData.sessionNumber;
        data["isClinic"] = InGameData.IsClinic;
        return data;
    }
}
