using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeformT : MonoBehaviour {

    //public float x = 1f;
    //public float y = 1f;
    //public float z = 1f;
    public bool switchAxisXToY;
    //public Transform model;

    public bool saveScale;
    public bool savePosition;

    private void Start(){
        //ScaleAxis(10, "x");
    }

    public void ScaleAxis(float factor, string pAxis)
    {
        Vector3 localScale = transform.localScale;
        switch (pAxis)
        {
            case "x":
                if(!switchAxisXToY) localScale.x = 1f + (factor - 0.5f);// * this.x;
                else localScale.y = 1f + (factor - 0.5f);// * this.y;
                break;
            case "y":
                localScale.y = 1f + (factor - 0.5f);// * this.y;
                break;
            case "z":
                localScale.z = 1f + (factor - 0.5f);// * this.z;
                break;
            case "xyz":
                localScale.x = 1f + (factor - 0.5f);// * this.x;
                localScale.y = 1f + (factor - 0.5f);// * this.y;
                localScale.z = 1f + (factor - 0.5f);// * this.z;
                break;
            case "yz":
                localScale.y = 1f + (factor - 0.5f);// * this.y;
                localScale.z = 1f + (factor - 0.5f);// * this.z;
                break;
            default:
                break;
        }
        print(localScale);
        transform.localScale = localScale;
    }

    /*
      public void ScaleAxis(float factor, string pAxis)
    {
        Vector3 localScale = transform.localScale;
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
        transform.localScale = localScale;
    }
     * */

    public Hashtable SerializeDeformData()
    {
        Hashtable data = new Hashtable();

        if (this.saveScale)
        {
            data["scale"] = new Hashtable();
            Hashtable scale = (Hashtable)data["scale"];
            scale["x"] = transform.localScale.x;
            scale["y"] = transform.localScale.y;
            scale["z"] = transform.localScale.z;
        }                           

        if (this.savePosition)
        {
            data["localPosition"] = new Hashtable();
            Hashtable localPosition = (Hashtable)data["localPosition"];
            localPosition["x"] = transform.localPosition.x;
            localPosition["y"] = transform.localPosition.y;
            localPosition["z"] = transform.localPosition.z;
        }

        return data;
    }
}
