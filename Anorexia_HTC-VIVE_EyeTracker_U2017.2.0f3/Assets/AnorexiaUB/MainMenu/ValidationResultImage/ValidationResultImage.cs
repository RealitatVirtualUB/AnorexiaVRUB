using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ValidationResultImage : MonoBehaviour {
    public Canvas canvas;
    public PupilValidation pupilValidation;
    public Transform[] referencias;
    public Transform[] averages;
    public Text[] angles;
    public Gradient gradient;
    public float min;
    public float max;
    public Text angleAngleText;
    public Text[] rowAverages;

    public void startPositions() {
        for (int i = 0; i < this.pupilValidation.referencias.Length; i++) {
            Transform pupilReference = this.pupilValidation.referencias[i];
            this.setPosition(this.referencias, i, pupilReference.localPosition, 0);
        }
    }

    // 569.4
    void setPosition(Transform[] referencias, int index, Vector3 localPosition, float angle) {
        RectTransform canvasRectTransform = this.canvas.GetComponent<RectTransform>();
        RectTransform imageRectTransform = this.GetComponent<RectTransform>();

        float canvasWidth = canvasRectTransform.rect.width;
        float width = imageRectTransform.rect.width;
        float canvasHeight = canvasRectTransform.rect.height;
        float height = imageRectTransform.rect.height;
        
        Transform reference = referencias[index];
        RectTransform rectTransform = reference.gameObject.GetComponent<RectTransform>();

        if (0 < angle) {
            float clampedAngle = Mathf.Clamp(angle, this.min, this.max);
            float step = (clampedAngle - this.min) / this.max;
            Image image = reference.GetComponent<Image>();
            image.color = this.gradient.Evaluate(step);
        }

        float x = width * localPosition.x / canvasWidth;
        float y = height * localPosition.y / canvasHeight;

        rectTransform.localPosition = new Vector3(x, y, 0);

        this.angles[index].text = "" + angle;

        float pos = index + 1;
        this.angleAngleText.text = "" + ((index * float.Parse(this.angleAngleText.text) + angle) / pos) ;
    }

    public void setAverage(int index, Vector3 localPosition, float angle) {
        this.setPosition(this.averages, index, localPosition, angle);
    }
}
