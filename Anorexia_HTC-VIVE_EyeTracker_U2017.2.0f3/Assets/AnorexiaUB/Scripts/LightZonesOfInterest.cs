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
    public float parseDuration = 20;

    int currentInterestZone = 0;
    List<int> currentInterestZones = new List<int>();
    

    private float currentSliderValue = 0;
    private float objectiveSliderValue = 0;
    private bool parseZOI = false;
    private float interval = 0;

    float currentIntensity = 0;
    float currentMaxIntensity = 0;
    float currentMinIntensity = 0;
    

    //private float desiredIntensity = 0;
    //private bool parseIntensity = false;

	// Use this for initialization
	void Start() {
        //ChangeAffectionZone(11);
        AddAffectionZone(11);
        SetIntensity();
	}
	
	// Update is called once per frame
	void Update () {
        //ChangeWorldPosMaterial();
        ChangeMultipleWorldPosMaterials();
        if (parseZOI)
        {
            currentSliderValue += interval;
            ChangeIntensitySlider(currentSliderValue);
            if ((Mathf.Round(currentSliderValue * 100f) / 100f) == (Mathf.Round(objectiveSliderValue * 100f) / 100f))
            {
                parseZOI = false;
                Debug.Log("end parse intensity");
            }
        }
        //Debug.Log("weight factor is: "+ weightFactor );
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
                    this.GetComponent<CsvReadWrite>().SetupBodyPartData(id.ToString() + " body part id");
                    Debug.Log("setting body part");
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

    public void AddAffectionZone(int id)
    {
        if (zonesOfinterest != null)
        {
            currentInterestZone = id;
            if (id == (int)interestId.NONE)
            {
                currentInterestZones.Clear();
                foreach (InterestZone zone in zonesOfinterest) connectedButtons[(int)zone.zoneID].GetComponent<Image>().color = disabledColor;
            }
            else if(currentInterestZones.Count == 2 || currentInterestZones.Count == 0)
            {
                currentInterestZones.Clear();
                foreach (InterestZone zone in zonesOfinterest) connectedButtons[(int)zone.zoneID].GetComponent<Image>().color = disabledColor;
                currentInterestZones.Add(id);
                currentIntensity = zonesOfinterest[id].maxIntensityOfInterest;
                SetIntensity();
            }else{
                currentInterestZones.Add(id);
                currentIntensity = zonesOfinterest[id].maxIntensityOfInterest;
                SetIntensity();
            }
            connectedButtons[id].GetComponent<Image>().color = enabledColor;
            //{
            //    if ((int)zone.zoneID == id)
            //    {
            //        connectedButtons[i].GetComponent<Image>().color = disabledColor;
            //        currentInterestZone = id;
            //        currentIntensity = zone.maxIntensityOfInterest;
            //        i++;
            //    }
            //    else
            //    {
            //        connectedButtons[i].GetComponent<Image>().color = enabledColor;
            //        i++;
            //    }
            //}
            //SetIntensity();
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

    public void ChangeObjectiveIntensity(float value)
    {
        currentSliderValue = sliderRef.value;
        objectiveSliderValue = value;
        interval = (objectiveSliderValue - currentSliderValue) / parseDuration;
        parseZOI = true;
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

    private void ChangeMultipleWorldPosMaterials()
    {
        int activatedNumber = 0;
        Vector3 newpos = Vector3.zero;
        Vector3 newPos2 = Vector3.zero;
        foreach (int id in currentInterestZones)
        {
            activatedNumber++;
            if(activatedNumber == 1)
            {
                if (zonesOfinterest[id].positionZone != null) newpos = zonesOfinterest[id].positionZone.position;
            }
            else if(activatedNumber == 2)
            {
                if (zonesOfinterest[id].positionZone != null) newPos2 = zonesOfinterest[id].positionZone.position;
            }
            else Debug.Log("i have to much ZOIs");
        }
        foreach (Material m in bodyMaterials)
        {
            m.SetVector("_Pos", newpos);
            m.SetVector("_Pos2", newPos2);
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
                                                -weightFactor); 
             currentMinIntensity = Mathf.Lerp(   zonesOfinterest[currentInterestZone].minIntensityOfInterest,
                                                zonesOfinterest[currentInterestZone].minThinIntensityOfInterest,
                                                -weightFactor); 
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
