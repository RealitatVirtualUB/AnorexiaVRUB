using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Windows.Forms; // this is unity mono forms
using UnityEngine.UI;
using System;

public class ImagePlotting : MonoBehaviour
{
    public GameObject pointPrefab;
    private StreamReader streamReader;
    public GameObject humanFigure;
    public GameObject femaleFigure;
    public GameObject maleFigure;
    public ArrayList points = new ArrayList();
    public Material referenceMaterial;
    public Material referenceMaterial_2;
    public Material normalMaterial;

    Vector3 startWorldPosition;
    Transform picked;

    public Slider slider;
    public InputField percentageField;

    void Start()
    {
        this.humanFigure = this.maleFigure;        
    }

    void reset()
    {
        for (int i = 0; i < this.points.Count; i++)
        {
            GameObject point = (GameObject)this.points[i];
            GameObject.Destroy(point);
        }
        this.points.Clear();
    }

    public void toggleModel()
    {
        this.humanFigure.SetActive(false);
        this.humanFigure = (this.humanFigure == this.maleFigure) ? this.femaleFigure : this.maleFigure;
        this.humanFigure.SetActive(true);
    }

    public void OpenFile()
    {
        OpenFileDialog open = new OpenFileDialog();
        DialogResult result = open.ShowDialog();
        if (DialogResult.OK == result)
        {
            this.reset();
            this.streamReader = new StreamReader(open.OpenFile());
            string line = null;
            while (null != (line = this.streamReader.ReadLine()))
            {
                string[] splitted = line.Split(';');
                GameObject pointObject = (GameObject)GameObject.Instantiate(this.pointPrefab);

                string partName = splitted[0];
                GameObject currentObject = GameObject.Find(splitted[0]);

                if (null != currentObject)
                {
                    float x = float.Parse(splitted[1]);
                    float y = float.Parse(splitted[2]);
                    float z = float.Parse(splitted[3]);

                    pointObject.name = partName;
                    pointObject.transform.SetParent(currentObject.transform);
                    pointObject.transform.localPosition = new Vector3(x, y, z);


                    if ("Reference_1" == partName)
                    {
                        pointObject.GetComponent<Renderer>().material = this.referenceMaterial;
                    }
                    else if ("Reference_2" == partName)
                    {
                        pointObject.GetComponent<Renderer>().material = this.referenceMaterial_2;
                    }
                    else
                    {
                        pointObject.GetComponent<Renderer>().material = this.normalMaterial;
                    }

                    points.Add(pointObject);
                }
            }

            this.streamReader.Close();
        }
    }

    public void OpenMedidas()
    {
        if (null != this.humanFigure)
        {
            Scaler scaler = this.humanFigure.GetComponent<Scaler>();
            OpenFileDialog open = new OpenFileDialog();
            DialogResult result = open.ShowDialog();
            if (DialogResult.OK == result)
            {
                scaler.load(open.FileName);
            }
        }
    }

    public void generateFiles()
    {
        SaveFileDialog saveFileDialog1 = new SaveFileDialog();
        saveFileDialog1.ShowDialog();
        if (saveFileDialog1.FileName != "")
        {
            string path = Path.GetFullPath(saveFileDialog1.FileName);

            StreamWriter streamWriter = new StreamWriter(saveFileDialog1.OpenFile());
            for (int i = 0; i < this.points.Count; i++)
            {
                GameObject point = (GameObject)this.points[i];
                point.SetActive(false);
                streamWriter.WriteLine(point.name + ";" + point.transform.position.x + ";" + point.transform.position.y + ";" + point.transform.position.z);
            }
            streamWriter.Close();

            RenderTexture renderTexture = new RenderTexture(1024, 1024, 24, RenderTextureFormat.ARGB32);
            RenderTexture.active = renderTexture;
            Camera.main.targetTexture = renderTexture;
            Camera.main.Render();

            Texture2D _tex = new Texture2D(1024, 1024, TextureFormat.RGB24, false);
            _tex.ReadPixels(new Rect(0.0f, 0.0f, 1024, 1024), 0, 0);
            _tex.Apply();

            RenderTexture.active = null;
            Camera.main.targetTexture = null;

            var jpg = _tex.EncodeToJPG();

            System.IO.File.WriteAllBytes(path + ".jpg", jpg);

            for (int i = 0; i < this.points.Count; i++)
            {
                GameObject point = (GameObject)this.points[i];
                point.SetActive(true);
            }
        }
    }

    public void generarImagen()
    {

    }

    void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                PhotoPlane photoPlane = hit.collider.GetComponent<PhotoPlane>();
                if (photoPlane)
                {
                    this.startWorldPosition = hit.point;
                    this.picked = hit.collider.transform;
                }
            }
            else
            {
                this.picked = null;
            }
        }
        else if (Input.GetMouseButton(0) && this.picked)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
               PhotoPlane photoPlane = hit.collider.GetComponent<PhotoPlane>();
                if (photoPlane)
                {
                    Vector3 diff = (hit.point - this.startWorldPosition);
                    diff.z = 0f;
                    // this.picked.position += diff;
                    for (int i = 0; i < this.points.Count; i++)
                    {
                        GameObject point = (GameObject)this.points[i];
                        Vector3 pointPosition = point.transform.position;
                        pointPosition += diff;
                        point.transform.position = pointPosition;
                    }

                    this.startWorldPosition = hit.point;
                }
            }
        }
        else
        {
            this.picked = null;
        }
    }

    public void sliderChanged()
    {
        if (null != this.humanFigure)
        {
           Scaler scaler = this.humanFigure.GetComponent<Scaler>();
            float v = scaler.startBlendShape + (100f - scaler.startBlendShape) * this.slider.value;
            scaler.meshRenderer.SetBlendShapeWeight(0, v);
        }

        this.percentageField.text = (100f * this.slider.value).ToString("0.0");
    }

    public void inputEdited()
    {
        try
        {
            this.slider.value = float.Parse(this.percentageField.text) / 100f;
            this.sliderChanged();
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }
    
}

public class Scaler//: SkinnedMeshRenderer//: UnityEngine.Object
{
    public SkinnedMeshRenderer meshRenderer;
    public float startBlendShape;
    public void load(string s) { }
    //public float startBlendShape() { return 0f; }
    

}

