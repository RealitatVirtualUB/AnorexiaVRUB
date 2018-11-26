using MiniJSON;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class AvatarLoader : MonoBehaviour
{

    public InputField subjectIdField;
    public Model model;
    //public IMCCalculator imcCalculator;
    public GameObject background;
    public delegate void OnCompleted();
    public OnCompleted OnAvatarLoaded;
    public InputField statisticsField;
    // Use this for initialization
    void Start(){
        OnAvatarLoaded += HideBackground;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadAvatar(){        
        string filePath = Application.dataPath + "/Pictures/";
        string fileName = subjectIdField.text + ".txt";
        if (File.Exists(filePath + fileName)){
            string text = File.ReadAllText(filePath + fileName);
            Dictionary<string, object> data = (Dictionary<string, object>)Json.Deserialize(text);
            Dictionary<string, object> modelData = (Dictionary<string, object>)data["modelData"];
            model.SetModelData(modelData);
            //Dictionary<string, object> imcData = (Dictionary<string, object>)data["imcData"];
            //imcCalculator.LoadData(float.Parse("" + imcData["weight"]), float.Parse("" + imcData["height"]), float.Parse("" + imcData["imc"]));
            Dictionary<string, object> blendShapesData = (Dictionary<string, object>)data["blendShapesData"];
            model.avatarComponents.DeserializeData(blendShapesData);

            Dictionary<string, object> imcData = (Dictionary<string, object>)data["imcData"];
            statisticsField.text =  ("Weight: " + imcData["weight"] + "Kg  ") +
                                    ("Height: " + (float.Parse("" + imcData["height"]) * 100) + "cm  ") +
                                    ("IMC: " + imcData["imc"] + "%");
            OnAvatarLoaded();
        }
    }

    public void HideBackground(){
        background.SetActive(false);
    }
}

