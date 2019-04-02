using MiniJSON;
using System.Collections;
using System.Collections.Generic;
using System.IO;

using UnityEngine;
using UnityEngine.UI;
using System.Xml;
using System.Xml.Serialization;

public class Medidas : MonoBehaviour {

    public Model model;

    public Slider rShoulderRotationSlider;
    public Slider lShoulderRotationSlider;
    public Slider rArmRotationSlider;
    public Slider lArmRotationSlider;
    public Slider rLegRotationSlider;
    public Slider lLegRotationSlider;

    public Slider hipsScaleSlider;
    public Slider lLegScaleSlider;
    public Slider rLegScaleSlider;
    public Slider lKneeScaleSlider;
    public Slider rKneeScaleSlider;
    public Slider spineScaleSlider;
    public Slider spine1ScaleSlider;
    public Slider spine2ScaleSlider;
    public Slider spine3ScaleSlider;
    public Slider lArmScaleSlider;
    public Slider rArmScaleSlider;
    public Slider lForeArmScaleSlider;
    public Slider rForeArmScaleSlider;

    public Slider shouldersSeparationSlider;
    public Slider hipsSeparationSlider;
    public Slider generalChangedSlider;

    public Dropdown webCamDropDown;
    public RenderWebCam renderWebCam;
    public InputField subjectIdField;
    public IMCCalculator imcCalculator;
    public Canvas measuresCanvas;
   
   
    void Start () {
        OnSwitchView();
        if(!imcCalculator)imcCalculator = gameObject.GetComponent<IMCCalculator>();
    }

    public void ShouldersSeparation(){
        model.ShouldersSeparation(shouldersSeparationSlider.value);
    }

    public void HipsSeparation(){
        model.HipsSeparation(hipsSeparationSlider.value);
    }
      
    public void rShoulderRotationChanged(){
        Vector3 angles = model.rShoulderDeform.transform.eulerAngles;
        angles.z = 30f * (0.5f - this.rShoulderRotationSlider.value);
        model.rShoulderDeform.transform.eulerAngles = angles;
    }

    public void lShoulderRotationChanged(){
        Vector3 angles = model.lShoulderDeform.transform.eulerAngles;
        angles.z = 30f * (0.5f - this.lShoulderRotationSlider.value);
        model.lShoulderDeform.transform.eulerAngles = angles;
    }

    public void rLegRotationChanged(){
        Vector3 angles = model.rLegDeform.transform.eulerAngles;
        angles.z = 30f * (0.5f - this.rLegRotationSlider.value);
        model.rLegDeform.transform.eulerAngles = angles;
    }

    public void lLegRotationChanged(){
        Vector3 angles = model.lLegDeform.transform.eulerAngles;
        angles.z = 30f * (0.5f - this.lLegRotationSlider.value);
        model.lLegDeform.transform.eulerAngles = angles;
    }

    public void modelOnOff(){
        this.model.gameObject.SetActive(!this.model.gameObject.activeSelf);
    }
    
    public void HipsScaleChanged(string pAxis)
    {
        model.hipsDeform.ScaleAxis(hipsScaleSlider.value, pAxis);
    }

    public void LLegScaleChanged(string pAxis){
        model.lThighDeform.ScaleAxis(lLegScaleSlider.value, pAxis);
    }
    
    public void RLegScaleChanged(string pAxis){
        model.rThighDeform.ScaleAxis(rLegScaleSlider.value, pAxis);
    }
    
    public void LKneeScaleChanged(string pAxis){
        model.lKneeDeform.ScaleAxis(lKneeScaleSlider.value, pAxis);
    }
    
    public void RKneeScaleChanged(string pAxis){
        model.rKneeDeform.ScaleAxis(rKneeScaleSlider.value, pAxis);
    }    

    public void SpineScaleChanged(string pAxis){
        model.spineDeform.ScaleAxis(spineScaleSlider.value, pAxis);
    }

    public void Spine1ScaleChanged(string pAxis){
        model.spine1Deform.ScaleAxis(spine1ScaleSlider.value, pAxis);
    }
    
    public void Spine2ScaleChanged(string pAxis){
        model.spine2Deform.ScaleAxis(spine2ScaleSlider.value, pAxis);
    }

    public void Spine3ScaleChanged(string pAxis)
    {
        model.spine3Deform.ScaleAxis(spine3ScaleSlider.value, pAxis);
    }

    public void RArmScaleChanged(string pAxis){
        model.rArmDeform.ScaleAxis(rArmScaleSlider.value, pAxis);
    }
    
    public void LArmScaleChanged(string pAxis){
        model.lArmDeform.ScaleAxis(lArmScaleSlider.value, pAxis);
    }
    
    public void RForeArmScaleChanged(string pAxis){
        model.rForeArmDeform.ScaleAxis(rForeArmScaleSlider.value, pAxis);
    }
   
    public void LForeArmScaleChanged(string pAxis){
        model.lForeArmDeform.ScaleAxis(lForeArmScaleSlider.value, pAxis);
    }

    public void OnSwitchView(){
        if(ViewState.VIEW == CamView.FRONT){
            ChangeAxis("x");           
        }else{
            ChangeAxis("z");
        }
    }
        
    public void ChangeAxis(string pAxis){
        hipsScaleSlider.onValueChanged = new Slider.SliderEvent();
        lLegScaleSlider.onValueChanged = new Slider.SliderEvent(); 
        rLegScaleSlider.onValueChanged = new Slider.SliderEvent(); 
        lKneeScaleSlider.onValueChanged = new Slider.SliderEvent(); 
        rKneeScaleSlider.onValueChanged = new Slider.SliderEvent(); 
        spineScaleSlider.onValueChanged = new Slider.SliderEvent(); 
        spine1ScaleSlider.onValueChanged = new Slider.SliderEvent();
        spine2ScaleSlider.onValueChanged = new Slider.SliderEvent();
        spine3ScaleSlider.onValueChanged = new Slider.SliderEvent();
        lArmScaleSlider.onValueChanged = new Slider.SliderEvent(); 
        rArmScaleSlider.onValueChanged = new Slider.SliderEvent(); 
        lForeArmScaleSlider.onValueChanged = new Slider.SliderEvent(); 
        rForeArmScaleSlider.onValueChanged = new Slider.SliderEvent();

        hipsScaleSlider.onValueChanged.AddListener(delegate { HipsScaleChanged(pAxis); });
        lLegScaleSlider.onValueChanged.AddListener(delegate { LLegScaleChanged(pAxis); });
        rLegScaleSlider.onValueChanged.AddListener(delegate { RLegScaleChanged(pAxis); });
        lKneeScaleSlider.onValueChanged.AddListener(delegate { LKneeScaleChanged(pAxis); });
        rKneeScaleSlider.onValueChanged.AddListener(delegate { RKneeScaleChanged(pAxis); });
        spineScaleSlider.onValueChanged.AddListener(delegate { SpineScaleChanged(pAxis); });
        spine1ScaleSlider.onValueChanged.AddListener(delegate { Spine1ScaleChanged(pAxis); });
        spine2ScaleSlider.onValueChanged.AddListener(delegate { Spine2ScaleChanged(pAxis); });
        spine3ScaleSlider.onValueChanged.AddListener(delegate { Spine3ScaleChanged(pAxis); });
        lArmScaleSlider.onValueChanged.AddListener(delegate { LArmScaleChanged(pAxis); });
        rArmScaleSlider.onValueChanged.AddListener(delegate { RArmScaleChanged(pAxis); });
        lForeArmScaleSlider.onValueChanged.AddListener(delegate { LForeArmScaleChanged(pAxis); });
        rForeArmScaleSlider.onValueChanged.AddListener(delegate { RForeArmScaleChanged(pAxis); });
    }

    
    public void okClicked()
    {/*
        Hashtable modelData = this.models.serializableData();
#if UNITY_EDITOR
        string FILE_NAME = "..\\Anorexia_UB_U5.5_HTC-VIVE\\model.txt";
#else
        string FILE_NAME = "model.txt";
#endif
        StreamWriter sw = File.CreateText(FILE_NAME);
        sw.Write("{0}", Json.Serialize(modelData));
        sw.Close();
        Application.Quit();*/
    }
    
    public void SaveAvatar(){
        Hashtable data = new Hashtable();
        data.Add("modelData", model.SerializeModelData());
        data.Add("imcData", imcCalculator.SerializeData());
        data.Add("blendShapesData", model.avatarComponents.SerializeData());
        //model.SerializeModelData();
        //modelData = imcCalculator.SerializeData();

        //set save avatar
#if UNITY_EDITOR
        //string filePath = Application.persistentDataPath + "/Pictures/";
        //if (!Directory.Exists(filePath)) Directory.CreateDirectory(filePath); // returns a DirectoryInfo object
        string filePath = System.IO.Directory.GetCurrentDirectory() + "/Pictures/";
        if(!Directory.Exists(filePath)) Directory.CreateDirectory(filePath); // returns a DirectoryInfo object
        Debug.Log(filePath);
        //change id of the avatar
        //string fileName = subjectIdField.text + ".txt";
        string fileName = InGameData.PacientId +".txt";
#else
        //string fileName = "model.txt";
        //string filePath = Application.persistentDataPath + "/Pictures/";
        string filePath = System.IO.Directory.GetCurrentDirectory() + "/Pictures/";
        if (!Directory.Exists(filePath))Directory.CreateDirectory(filePath); // returns a DirectoryInfo object
        //change id of the avatar
        //string fileName = subjectIdField.text + ".txt";
        string fileName = InGameData.PacientId +".txt";
#endif
        StreamWriter sw = File.CreateText(filePath + fileName);
        sw.Write("{0}", Json.Serialize(data));
        sw.Close();
    }   

    public void LoadAvatar(){
        //string filePath = Application.persistentDataPath + "/Pictures/";
        string filePath = System.IO.Directory.GetCurrentDirectory() + "/Pictures/";
        string fileName = subjectIdField.text + ".txt";
        Debug.Log(filePath + fileName);
        if (File.Exists(filePath + fileName)){
            string text = File.ReadAllText(filePath + fileName);
            Dictionary<string, object> data = (Dictionary<string, object>)Json.Deserialize(text);
            Dictionary<string, object> modelData = (Dictionary<string, object>)data["modelData"];
            model.SetModelData(modelData);
            Dictionary<string, object> imcData = (Dictionary<string, object>)data["imcData"];
            imcCalculator.LoadData(float.Parse("" + imcData["weight"]), float.Parse("" + imcData["height"]), float.Parse("" + imcData["imc"]));
            Dictionary<string, object> blendShapesData = (Dictionary<string, object>)data["blendShapesData"];
            model.avatarComponents.DeserializeData(blendShapesData);
        }
    }

    public void DeleteAvatar()
    {
        string filePath = Application.dataPath + "/Pictures/";
        string fileName = subjectIdField.text + ".txt";
        if (File.Exists(filePath + fileName)){
            File.Delete(filePath + fileName);
        }
    }


    //To Delete
    public void configurarMedidas()
    {
        //this.pies_def.slider.onValueChanged.AddListener (delegate {piesChanged ();});

        //this.pantorrillas_def.slider.onValueChanged.AddListener(delegate { pantorrillasChanged(); });
       // this.muslos_def.slider.onValueChanged.AddListener(delegate { muslosChanged(); });
       // this.panza_def.slider.onValueChanged.AddListener(delegate { panzaChanged(); });
      //  this.pecho_def.slider.onValueChanged.AddListener(delegate { pechoChanged(); });

       // this.brazos_def.slider.onValueChanged.AddListener(delegate { brazosChanged(); });
      //  this.antebrazos_def.slider.onValueChanged.AddListener(delegate { antebrazosChanged(); });
        //this.manos_def.slider.onValueChanged.AddListener (delegate {manosChanged ();});
        //this.cuello_def.slider.onValueChanged.AddListener (delegate {cuelloChanged ();});
       // this.hombrosSeparacion_def.slider.onValueChanged.AddListener(delegate { homborsChanged(); });
       // this.caderasSeparacion_def.slider.onValueChanged.AddListener(delegate { caderasChanged(); });

        //this.pelvis_I_x = this.Pelvis_I.localPosition.z;
        //this.pelvis_D_x = this.Pelvis_D.localPosition.z;

       // this.hombro_I_x = this.Hombro_I.localPosition.x;
      //  this.hombro_D_x = this.Hombro_D.localPosition.x;

        //string filePath = Application.dataPath + "/Pictures/";
        //string fileName = subjectIdField.text + ".txt";
        string FILE_NAME = "model.txt";
        if (File.Exists(FILE_NAME))
        {
            string text = File.ReadAllText(FILE_NAME);
            Dictionary<string, object> data = (Dictionary<string, object>)Json.Deserialize(text);
            //this.SetAvatar(this.transform, data);
            //this.startBlendShape = float.Parse("" + data["shape"]);
            //this.rigController.meshRenderer.SetBlendShapeWeight(0, this.startBlendShape);
        }
    }
    
    
    public void DisableMeasuresObjects(){
        model.gameObject.SetActive(false);
        measuresCanvas.gameObject.SetActive(false);       
    }
    
    public void EnableMeasuresObjects(){
        OnSwitchView();
        model.gameObject.SetActive(true);
        measuresCanvas.gameObject.SetActive(true);
    }
}

