using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MimicController : MonoBehaviour {
    public SteamVR_TrackedController controller;
    public Transform plane;
    public Transform model;
    public Transform start;
    public Transform end;
    public Transform mirrorModel;

    public InputField delayInput;
    public Transform modelParent;
    public Slider shapesSlider;
    public float factor = 1f;

    public void setNodes(Transform start, Transform end)
    {
        this.start = start;
        this.end = end;
    }

    void Update()
    {
        this.plane.forward = (this.end.position - this.start.position).normalized;
        this.model.gameObject.SetActive(this.controller.triggerPressed);
        this.mirrorModel.gameObject.SetActive(this.controller.triggerPressed);
        this.StartCoroutine(this.moveDelayed(this.controller.transform.position, this.controller.transform.rotation));
    }

    IEnumerator moveDelayed(Vector3 position, Quaternion rotation)
    {
        float seconds = 0f;//
        try
        {
            seconds = float.Parse(this.delayInput.text);
        }
        catch (System.Exception e)
        {

        }

        yield return new WaitForSeconds(seconds);

        this.transform.position = position;
        this.model.rotation = rotation;

        Vector3 distanceVector = -this.transform.up;
        this.modelParent.localPosition = this.factor * this.modelParent.up * this.shapesSlider.value / 100f;
    }
}
