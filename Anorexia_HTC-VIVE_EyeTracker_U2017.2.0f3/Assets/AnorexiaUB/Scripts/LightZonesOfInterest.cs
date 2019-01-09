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
       LEFTLOWERLEG,
       NONE
    }

    public List<InterestZone> zonesOfinterest;
    public List<Material> bodyMaterials;
    public List<Button> connectedButtons;

    public Color enabledColor;
    public Color disabledColor;

    public Slider sliderRef;
    public float weightFactor = 0;

    int currentInterestZone = 0;

    float currentIntensity = 0;
    float currentMaxIntensity = 0;
    float currentMinIntensity = 0;
    
    //private float desiredIntensity = 0;
    //private bool parseIntensity = false;

	// Use this for initialization
	void Start() {
        ChangeAffectionZone(11);
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
        SetCurrentMaxMinIntensity();
        //currentIntensity = zonesOfinterest[currentInterestZone].maxIntensityOfInterest;
        currentIntensity = currentMaxIntensity;
        sliderRef.value = 1;
    }

    public void ChangeIntensity()
    {
        //currentIntensity = Mathf.Lerp(zonesOfinterest[currentInterestZone].minIntensityOfInterest, zonesOfinterest[currentInterestZone].maxIntensityOfInterest, sliderRef.value);
        currentIntensity = Mathf.Lerp(currentMinIntensity,currentMaxIntensity, sliderRef.value);
    }
    
    public void ChangeIntensitySlider(float value)
    {
        sliderRef.value = value;
    }

    private void ChangeWorldPosMaterial()
    {
        //es pot optimitzar si avans de setejar el valor, identifiquem quina es la part que es necesita canviar
        foreach(Material m in bodyMaterials)
        {
            Vector3 newpos = Vector3.zero;
            //Vector3 newpos =zonesOfinterest[currentInterestZone].positionZone.InverseTransformPoint(zonesOfinterest[currentInterestZone].positionZone.position);
            if (zonesOfinterest[currentInterestZone].positionZone != null)newpos = zonesOfinterest[currentInterestZone].positionZone.position;
            m.SetVector("_Pos", newpos);
            m.SetFloat("_Dist", currentIntensity);
        }
    }

    public void SetWeightFactor(float w)
    {
        weightFactor = w;
        Debug.Log("weight factor is: " + weightFactor);
        SetCurrentMaxMinIntensity();
    }

    public void SetCurrentMaxMinIntensity()
    {
        if (weightFactor >= 0) {
            currentMaxIntensity = Mathf.Lerp(   zonesOfinterest[currentInterestZone].maxIntensityOfInterest, 
                                                zonesOfinterest[currentInterestZone].maxFatIntensityOfInterest, 
                                                weightFactor);
            currentMinIntensity = Mathf.Lerp(   zonesOfinterest[currentInterestZone].minIntensityOfInterest,
                                                zonesOfinterest[currentInterestZone].minFatIntensityOfInterest,
                                                weightFactor);
           
        }
        else
        {
            currentMaxIntensity = Mathf.Lerp(   zonesOfinterest[currentInterestZone].maxIntensityOfInterest,
                                                zonesOfinterest[currentInterestZone].maxThinIntensityOfInterest,
                                                weightFactor);
            currentMinIntensity = Mathf.Lerp(   zonesOfinterest[currentInterestZone].minIntensityOfInterest,
                                                zonesOfinterest[currentInterestZone].minThinIntensityOfInterest,
                                                weightFactor);
        }
        //Debug.Log("current max intensity: " + currentMaxIntensity + " & current min intensity: " + currentMinIntensity);
    }

    [System.Serializable]
    public struct InterestZone
    {
        public interestId zoneID;
        public Transform positionZone;
        public float maxIntensityOfInterest;
        public float minIntensityOfInterest;
        public float maxFatIntensityOfInterest;
        public float minFatIntensityOfInterest;
        public float maxThinIntensityOfInterest;
        public float minThinIntensityOfInterest;
    }
}
