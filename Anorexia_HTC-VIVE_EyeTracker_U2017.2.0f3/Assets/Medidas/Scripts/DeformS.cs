using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeformS : MonoBehaviour {
    public float x = 1f;
    public float y = 1f;
    public float z = 1f;
    public Transform model;

    public bool saveScale;
    public bool savePosition;

    public void scale(float factor)
    {
        Vector3 localScale = this.model.transform.localScale;
        localScale.x = 1f + (factor - 0.5f) * this.x;
        localScale.y = 1f + (factor - 0.5f) * this.y;
        localScale.z = 1f + (factor - 0.5f) * this.z;
        this.model.transform.localScale = localScale;
    }

    public void ScaleAxis(float factor, string pAxis)
    {
        Vector3 localScale = this.model.transform.localScale;
        switch (pAxis)
        {
            case "x":
            localScale.x = 1f + (factor - 0.5f) * this.x;
                break;
            case "y":
            localScale.y = 1f + (factor - 0.5f) * this.y;
                break;
            case "z":
            localScale.z = 1f + (factor - 0.5f) * this.z;
                break;
            case "xyz":
                localScale.x = 1f + (factor - 0.5f) * this.x;
                localScale.y = 1f + (factor - 0.5f) * this.y;
                localScale.z = 1f + (factor - 0.5f) * this.z;
                break;
            case "yz":
                localScale.y = 1f + (factor - 0.5f) * this.y;
                localScale.z = 1f + (factor - 0.5f) * this.z;
                break;
            default:
                break;
        }
        print(localScale);
        this.model.transform.localScale = localScale;
    }

    public Hashtable SerializeDeformData()
    {
        Hashtable data = new Hashtable();

        if (this.saveScale)
        {
            data["scale"] = new Hashtable();
            Hashtable scale = (Hashtable)data["scale"];
            scale["x"] = this.model.transform.localScale.x;
            scale["y"] = this.model.transform.localScale.y;
            scale["z"] = this.model.transform.localScale.z;
        }

        if (this.savePosition)
        {
            data["localPosition"] = new Hashtable();
            Hashtable localPosition = (Hashtable)data["localPosition"];
            localPosition["x"] = this.transform.localPosition.x;
            localPosition["y"] = this.transform.localPosition.y;
            localPosition["z"] = this.transform.localPosition.z;
        }

        return data;
    }
}
