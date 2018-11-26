using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PupilValidation : MonoBehaviour {
    public AnorexiaUB anorexiaUB;
    public RectTransform rightTarget;
    public RectTransform leftTarget;
    public RectTransform centerTarget;
    public RectTransform pointTarget;
    public float duration = 3f;
    public Transform cameraTransform;
    public ValidationResultImage validationResultImage;

    public Transform[] referencias;
    private int currentReference = -1;

    private float startTime;
    ArrayList muestras = new ArrayList();

    public void validate() {
        this.enabled = true;
        this.currentReference = -1;
        this.pointTarget.gameObject.SetActive(true);
        this.changeReference();

        this.validationResultImage.startPositions();
    }

    void changeReference() {
        this.muestras.Clear();
        this.startTime = Time.time;
        this.currentReference += 1;
        this.pointTarget.localPosition = this.referencias[this.currentReference].localPosition;
    }

    void calculate(bool end) {
        int N = this.muestras.Count;
        Vector3 average = Vector3.zero;
        foreach (Vector3 muestra in this.muestras) {
            average += muestra;
        }

        average /= N;

        Vector3 pointRespectoCamara = pointTarget.position - this.cameraTransform.transform.position;
        Vector3 averageRespectoCamara = pointRespectoCamara + (average - pointTarget.localPosition);

        float angle = Vector3.Angle(averageRespectoCamara, pointRespectoCamara);
        Vector3 reference = this.referencias[this.currentReference].localPosition;

        if (!File.Exists("validaciones.csv")) {
            StreamWriter streamWriter_1 = File.AppendText("validaciones.csv");
            string columns = "x ref; y ref; x avg; y avg; angle; ;";
            for (int i = 0; i < this.referencias.Length; i++) {
                streamWriter_1.Write(columns);
            }
            streamWriter_1.Write("\n");
            streamWriter_1.Close();
        }

        StreamWriter streamWriter = File.AppendText("validaciones.csv");
        streamWriter.Write(reference.x + ";" + reference.y + ";" + average.x + ";" + average.y + ";" + angle + ";;" + (end ? "\n" : ""));
        //Debug.Log(average + " " + pointTarget.position + " " + Vector3.Angle(averageRespectoCamara, pointRespectoCamara));
        streamWriter.Close();

        this.validationResultImage.setAverage(this.currentReference, average, angle);
    }

    // Update is called once per frame
    void FixedUpdate () {
        float elapsed = (Time.time - this.startTime);
        if (duration > elapsed) {
            if (1f <= elapsed) {
                Vector3 centerPosition = this.centerTarget.localPosition;
                muestras.Add(centerPosition);
            }
        } else if (this.currentReference < (this.referencias.Length - 1)) {
            this.calculate(false);
            this.changeReference();
        } else {
            this.calculate(true);
            this.pointTarget.gameObject.SetActive(false);
            this.enabled = false;
            this.anorexiaUB.pupilCalibrationTerminated();
        }
    }
}
