using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System;

public class RayTracker : MonoBehaviour {
    public Transform target;
    public Transform testigo;
    public GameObject calibrationBox;
    private StreamWriter streamWriter;
    private TrackArea lastArea;
    public float limitTime = 0.5f;
    private float startTime;
    private bool recording;
    private bool recordingData = false;

    public float rate = 30f;
    private float lastTime;

    public InputField idText;

    public Material referenceMaterial;
    public Material referenceMaterial_2;
    public Material normalMaterial;

    void OnEnable() {
        this.lastTime = Time.time;
    }

    void OnDestroy() {
        if (null != this.streamWriter)
        {
            this.streamWriter.WriteLine(";");
            this.streamWriter.Close();
        }
    }

    void OnDrawGizmos() {
        Gizmos.DrawLine(this.transform.position, this.target.position);
    }

	// Update is called once per frame
	void FixedUpdate () {
        Vector3 distanceVector = this.target.position - this.transform.position;
        //bool hitted = false;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, distanceVector.normalized, out hit)) {
            TrackArea trackArea = hit.collider.GetComponent<TrackArea>();
            if (null != trackArea) {
                this.testigo.position = hit.point;
                if ("Reference_1" == hit.collider.gameObject.name) {
                    this.testigo.gameObject.GetComponent<Renderer>().material = this.referenceMaterial;
                } else if ("Reference_2" == hit.collider.gameObject.name) {
                    this.testigo.gameObject.GetComponent<Renderer>().material = this.referenceMaterial_2;
                } else {
                    this.testigo.gameObject.GetComponent<Renderer>().material = this.normalMaterial;
                }

                if (this.recordingData) {
                    if (trackArea.recordable) {
                        if (!this.calibrationBox.activeSelf) {
                            float currentTime = Time.time;
                            if ((1 / this.rate) < (currentTime - this.lastTime)) {
                                this.lastTime = currentTime;
                                Vector3 localPoint = hit.point - hit.collider.transform.position;
                                Vector3 transformedPosition = hit.collider.transform.InverseTransformVector(localPoint);
                                this.streamWriter.WriteLine(trackArea.areaName + ";" + transformedPosition.x + ";" + transformedPosition.y + ";" + transformedPosition.z);
                            }
                        }
                    }
                }
            }
        }
    }

    public void pupilCalibrationStarted() {
        this.calibrationBox.SetActive(true);
    }

    public void pupilCalibrationTerminated() {
        this.calibrationBox.SetActive(false);
    } 


    public void recordPupilData() {
        if (this.streamWriter != null){
            this.streamWriter.Close();
        }

        DateTime now = DateTime.Now;
        string date = now.Day + "-" + now.Month + "-" + now.Year + "___" + now.Hour + "-" + now.Minute + "-" + now.Second;
        string path = "Datos\\" + this.idText.text;
        Directory.CreateDirectory(path);

        this.streamWriter = File.CreateText (path + "\\" + date + ".csv");
        this.recordingData = true;

        string FILE_NAME = "model.txt";
        if (File.Exists(FILE_NAME) && !File.Exists(path + "\\" + FILE_NAME))
        {
            string text = File.ReadAllText(FILE_NAME);
            StreamWriter sw = File.CreateText(path + "\\" + FILE_NAME);
            sw.Write("{0}", text);
            sw.Close();
        }
    }

    public void stopRecordPupilData() {
        this.streamWriter.WriteLine(";");
        this.streamWriter.Close();
        this.streamWriter = null;
        this.recordingData = false;
    }
}
