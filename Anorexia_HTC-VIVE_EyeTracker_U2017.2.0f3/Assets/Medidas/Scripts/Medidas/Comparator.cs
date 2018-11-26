using MiniJSON;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;


public class Comparator : MonoBehaviour {

    //public Canvas measuresCanvas;
    public Canvas comparatorCanvas;
    public int maxModels;
    public GameObject maleModel;
    public GameObject weightSlider;
    public GameObject heightSlider;
    public GameObject subjectStatisticsField;
    public float sliderYOffset;
    public float slidersSeparation;
    public GameObject[] posPoints;
    public Color[] outLineColors;
    private int _modelsCount;
    public GameObject fPosPoint;
    public GameObject sPosPoint;
    private List<Model> _insertedModels;
    private List<Slider> _insertedWeightSliders;
    private List<Slider> _insertedHeightSliders;
    private List<InputField> _insertedStatisticsField;
    public InputField subjectIdField;
    private bool _overlaped;
    public Transform sideReference;
    public Transform frontReference;
    private CamView _camView;

    // Use this for initialization
    void Start () {
        _insertedModels = new List<Model>();
        _insertedWeightSliders = new List<Slider>();
        _insertedStatisticsField = new List<InputField>();
        _insertedHeightSliders = new List<Slider>();
        _camView = CamView.FRONT;
	}
	
	// Update is called once per frame
	void Update () {
        //Debug.DrawRay(Camera.main.transform.localPosition, posPoints[_currentPoint].transform.localPosition);
	}

    public void InsertModel() {
        if (_modelsCount >= maxModels || _camView == CamView.SIDE) return;
        GameObject m = (GameObject)GameObject.Instantiate(maleModel, posPoints[_modelsCount].transform.position, Quaternion.identity);
        _insertedModels.Add(m.GetComponent<Model>());
        //MakeOutline(m.GetComponentInChildren<SkinnedMeshRenderer>());
        m.GetComponent<ModelRenderers>().SetOutlineColor(outLineColors[_modelsCount]);
        CreateModelSlider();
        CreateStatisticsField();
        _modelsCount++;
    }

    /*public void MakeOutline(SkinnedMeshRenderer pRenderer){
        Material[] materials = pRenderer.materials;
        foreach (Material mat in materials)
        {
            mat.shader = Shader.Find("Outlined/Silhouette Only");
            mat.SetColor("_OutlineColor", outLineColors[_modelsCount]);
            mat.SetFloat("_Outline", 0.03f);
        }
        
    }*/

    public void EnableComparatorObjects(){
        comparatorCanvas.gameObject.SetActive(true);
        for (int i = 0; i < _insertedModels.Count; i++)
        {
            _insertedModels[i].gameObject.SetActive(true);
        }
    }

    public void DisableComparatorObjects(){
        comparatorCanvas.gameObject.SetActive(false);
        for (int i = 0; i < _insertedModels.Count; i++){
            _insertedModels[i].gameObject.SetActive(false);
        }
        _camView = CamView.FRONT;
        Camera.main.transform.position = frontReference.position;
        Camera.main.transform.rotation = frontReference.rotation;


    }

    public void LoadAvatar()
    {
        string filePath = Application.dataPath + "/Pictures/";
        string fileName = subjectIdField.text + ".txt";
        if (File.Exists(filePath + fileName))
        {
            string text = File.ReadAllText(filePath + fileName);
            Dictionary<string, object> data = (Dictionary<string, object>)Json.Deserialize(text);
            Dictionary<string, object> modelData = (Dictionary<string, object>)data["modelData"];
            _insertedModels[_modelsCount-1].SetModelData(modelData);

            Dictionary<string, object> auxData = (Dictionary<string, object>)data["imcData"];
            _insertedStatisticsField[_insertedStatisticsField.Count -1].text =("Weight: " + auxData["weight"] + "Kg  ") + 
                                                                            ("Height: " + (float.Parse("" + auxData["height"]) * 100) + "cm  ") +
                                                                            ("IMC: " + auxData["imc"] + "%" );

            Dictionary<string, object> blendShapesData = (Dictionary<string, object>)data["blendShapesData"];
            _insertedModels[_modelsCount-1].avatarComponents.DeserializeData(blendShapesData);

        }        
    }
    
    void ShowPercentage(int index)
    {
        InputField porcentaje = _insertedWeightSliders[index].transform.Find("Amount").GetComponent<InputField>();
        porcentaje.text = ((int)(_insertedWeightSliders[index].value)).ToString() + "%";
    }

    void CreateModelSlider(){
        Vector3 sliderPos = Vector3.zero;       
        RaycastHit r;
        Ray ray = new Ray();
        ray.origin = Camera.main.transform.position;
        ray.direction = posPoints[_modelsCount].transform.position - Camera.main.transform.position;
        if (Physics.Raycast(ray, out r)){
            sliderPos = r.point;
            sliderPos.y -= sliderYOffset;
        }
        
        GameObject w = (GameObject)GameObject.Instantiate(weightSlider, sliderPos, weightSlider.transform.rotation, weightSlider.transform.parent);
        w.SetActive(true);
        _insertedWeightSliders.Add(w.GetComponent<Slider>());
        _insertedWeightSliders[_insertedWeightSliders.Count - 1].onValueChanged = new Slider.SliderEvent();
        int auxIndex = _insertedModels.Count - 1;
        _insertedWeightSliders[_insertedWeightSliders.Count - 1].onValueChanged.AddListener(delegate {GeneralChanged(auxIndex);});
        _insertedWeightSliders[_insertedWeightSliders.Count - 1].onValueChanged.AddListener(delegate {ShowPercentage(auxIndex);});

        sliderPos.y -= slidersSeparation;
        GameObject h = (GameObject)GameObject.Instantiate(heightSlider, sliderPos, heightSlider.transform.rotation, heightSlider.transform.parent);
        h.SetActive(true);
        _insertedHeightSliders.Add(h.GetComponent<Slider>());
        _insertedHeightSliders[_insertedHeightSliders.Count - 1].onValueChanged = new Slider.SliderEvent();
        _insertedHeightSliders[_insertedHeightSliders.Count - 1].onValueChanged.AddListener(delegate { HeightChanged(auxIndex); });
    }


    //takes reference position from weightSlider;
    void CreateStatisticsField()
    {
        Vector3 stPos = _insertedWeightSliders[_insertedWeightSliders.Count - 1].transform.position;
        stPos.y -= sliderYOffset;
        GameObject s = (GameObject)GameObject.Instantiate(subjectStatisticsField, stPos, subjectStatisticsField.transform.rotation, subjectStatisticsField.transform.parent);
        s.SetActive(true);
        _insertedStatisticsField.Add(s.GetComponent<InputField>());
    }

    /*void SetAvatar(Transform parent, Dictionary<string, object> data)
    {
        foreach (Transform child in parent)
        {
            if (0 < child.childCount)
            {
                if (data.ContainsKey(child.name))
                {
                    Dictionary<string, object> objectData = (Dictionary<string, object>)data[child.name];
                    if ("Model" == child.GetChild(0).name && objectData.ContainsKey("scale"))
                    {
                        Dictionary<string, object> scale = (Dictionary<string, object>)objectData["scale"];
                        Transform modelTransform = child.GetChild(0);
                        modelTransform.localScale = new Vector3(float.Parse("" + scale["x"]), float.Parse("" + scale["y"]), float.Parse("" + scale["z"])); ;
                    }

                    if (objectData.ContainsKey("localPosition"))
                    {
                        Dictionary<string, object> localPosition = (Dictionary<string, object>)objectData["localPosition"];
                        child.localPosition = new Vector3(float.Parse("" + localPosition["x"]), float.Parse("" + localPosition["y"]), float.Parse("" + localPosition["z"]));
                    }
                }

                this.SetAvatar(child, data);
            }
        }
    }*/
    public void HeightChanged(int modelIndex){
        _insertedModels[modelIndex].avatarComponents.SetHeight(_insertedHeightSliders[modelIndex].value);
    }

    public void GeneralChanged(int modelIndex){
        _insertedModels[modelIndex].avatarComponents.SetFat(_insertedWeightSliders[modelIndex].value);        
    }

    public void OverlapModels(){
        if (_insertedModels.Count <= 1) return;
        if (!_overlaped){
            for (int i = 0; i < _insertedModels.Count; i++){
                Vector3 modelPos = _insertedModels[i].transform.position;
                modelPos.x = 0;
                _insertedModels[i].transform.position = modelPos;
            }
            _overlaped = true;
        }else{
            for (int i = 0; i < _insertedModels.Count; i++)
            {
                _insertedModels[i].transform.position = posPoints[i].transform.position;
            }
            _overlaped = false;
        }
    }

    public void SwitchViews(){
        if (_camView == CamView.FRONT){
            _camView = CamView.SIDE;
            Camera.main.transform.position = sideReference.position;
            Camera.main.transform.rotation = sideReference.rotation;
        }else{
            _camView = CamView.FRONT;
            Camera.main.transform.position = frontReference.position;
            Camera.main.transform.rotation = frontReference.rotation;
            
        }
    }

    public void ResetModels(){
        _modelsCount = 0;
        for (int i = 0; i < _insertedModels.Count; i++)
        {
            GameObject.Destroy(_insertedModels[i].gameObject);
            GameObject.Destroy(_insertedWeightSliders[i].gameObject);
            GameObject.Destroy(_insertedHeightSliders[i].gameObject);
            GameObject.Destroy(_insertedStatisticsField[i].gameObject);
        }

        _insertedModels = new List<Model>();
        _insertedWeightSliders = new List<Slider>();
        _insertedHeightSliders = new List<Slider>();
        _insertedStatisticsField = new List<InputField>();
    }
}
