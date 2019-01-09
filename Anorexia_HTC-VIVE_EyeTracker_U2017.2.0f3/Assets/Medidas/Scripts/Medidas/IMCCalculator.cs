using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class IMCCalculator : MonoBehaviour {

    public InputField weightField;
    public InputField heightField;
    public InputField imcField;
    public Slider heightSlider;
    public Slider weightSlider;
    private Color placeHolderInitialColor;
    public IMCData imcData;
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

    public void CalculateIMC(){
        if (!CheckFields()) return;
        float weight = float.Parse(weightField.text);
        float height = float.Parse(heightField.text);
        //set height model
        Debug.Log("height is " + height);
        heightSlider.value = Mathf.Clamp01(height/200);
        Debug.Log("height relation is " + heightSlider.value);

        height /= 100;
        imcData.height = height;
        imcData.weight = weight;
        height *= height;
        imcData.imc = (float)System.Math.Round((weight / height),2);
        imcData.imcIncremented = imcData.imc;
        imcData.sessionNumber = 1;

        //set weight model
        this.GetComponent<MainScene>().InterpolateIMC(imcData.imc,imcData.height, weightSlider);

        imcField.text = imcData.imc.ToString();
        weightField.placeholder.color = placeHolderInitialColor;
        heightField.placeholder.color = placeHolderInitialColor;

    }

    public bool CheckFields(){
        bool weightFilled;
        bool heightFilled;
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

        if(!weightFilled || !heightFilled){
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
        return data;
    }
}
