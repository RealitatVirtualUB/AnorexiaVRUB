using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightZonesOfInterest : MonoBehaviour {

    public enum interestId
    {
       RIGHTUPPERARM,
       RIGHTLOWERARM,
       LEFTUPPERARM,
       LEFTLOWERARM,
       STOMACH,
       RIGHTHIP,
       LEFTHIP,
       RIGHTUPPERLEG,
       RIGHTLOWERLEG,
       LEFTUPPERLEG,
       LEFTLOWERLEG
    }

    public List<InterestZone> zonesOfinterest;
    public List<Material> bodyMaterials;
    public List<Button> connectedButtons;

    public Color enabledColor;
    public Color disabledColor;

    public Slider sliderRef;

    int currentInterestZone = 0;
    float currentIntensity = 0;

	// Use this for initialization
	void Start() {
        ChangeAffectionZone(0);
        SetIntensity();
	}
	
	// Update is called once per frame
	void Update () {
        ChangeWorldPosMaterial();
    }

    public void ChangeAffectionZone(int id)
    {
        if (zonesOfinterest != null)
        {
            int i = 0; 
            foreach (InterestZone zone in zonesOfinterest)
            {
                if ((int)zone.zoneID == id)
                {
                    connectedButtons[i].GetComponent<Image>().color = disabledColor;
                    currentInterestZone = id;
                    currentIntensity = zone.maxIntensityOfInterest;
                    i++;
                }
                else
                {
                    connectedButtons[i].GetComponent<Image>().color = enabledColor;
                    i++;
                }
            }
            SetIntensity();
        }
        else Debug.Log("doesn't exist anyone zone of interest");
        
    }

    private void SetIntensity()
    {
        currentIntensity = zonesOfinterest[currentInterestZone].maxIntensityOfInterest;
        sliderRef.value = 1;
    }

    public void ChangeIntensity()
    {
        currentIntensity = Mathf.Lerp(0, zonesOfinterest[currentInterestZone].maxIntensityOfInterest, sliderRef.value);
    }

    private void ChangeWorldPosMaterial()
    {
        //es pot optimitzar si avans de setejar el valor, identifiquem quina es la part que es necesita canviar
        foreach(Material m in bodyMaterials)
        {
            //Vector3 newpos =zonesOfinterest[currentInterestZone].positionZone.InverseTransformPoint(zonesOfinterest[currentInterestZone].positionZone.position);
            Vector3 newpos = zonesOfinterest[currentInterestZone].positionZone.position;
            m.SetVector("_Pos", newpos);
            m.SetFloat("_Dist", currentIntensity);
        }
    }

    [System.Serializable]
    public struct InterestZone
    {
        public interestId zoneID;
        public Transform positionZone;
        public float maxIntensityOfInterest;
    }
}
