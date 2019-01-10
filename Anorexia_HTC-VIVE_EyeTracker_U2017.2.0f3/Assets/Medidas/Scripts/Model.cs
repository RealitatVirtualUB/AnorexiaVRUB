using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Model : MonoBehaviour {
    public ModelRenderers avatarComponents;

    public DeformT spineDeform;
    public DeformT spine1Deform;
    public DeformT spine2Deform;
    public DeformT spine3Deform;

    public DeformT hipsDeform;
    public DeformT lLegDeform;
    public DeformT rLegDeform;
    public DeformT lThighDeform;
    public DeformT rThighDeform;                 
    public DeformT lKneeDeform;
    public DeformT rKneeDeform;
                 
    public DeformT lShoulderDeform;
    public DeformT rShoulderDeform;
    public DeformT lArmDeform;
    public DeformT rArmDeform;
    public DeformT lForeArmDeform;
    public DeformT rForeArmDeform;
    /*
    public Transform Hombro_D;
    public Transform Hombro_I;
    public Transform Pelvis_D;
    public Transform Pelvis_I;
    */
    //private Vector3 hmiddle;
    //private float hdistance;
    //private Vector3 cmiddle;
    //private float cdistance;

    //public DeformT rCollarbone;
    //public DeformT lCollarbone;

    //public float hombrosFactor = 1f;
    //public float caderasFactor = 1f;
    private float shouldersSeparation;
    private float hipsSeparation;
    private Vector3 lShoulderInitPos;
    private Vector3 rShoulderInitPos;
    private Vector3 lLegInitPos;
    private Vector3 rLegInitPos;

    //weight
    private float weight;
    private float imcBase;
    private float imcIncremented;
    private int numOfSession;
    

    //the weight bar is the represantation of the weight, not the IMC only for men
    public float minW = 40;
    public float midW = 80;
    public float maxW = 200;


    //height
    private float height;

    //heigh - collider fix
    public Transform bonesReference;

    public void Awake()
    {
        if(!avatarComponents) avatarComponents = GetComponent<ModelRenderers>();
        lShoulderInitPos = lShoulderDeform.transform.localPosition;
        rShoulderInitPos = rShoulderDeform.transform.localPosition;
        lLegInitPos = lLegDeform.transform.localPosition;
        rLegInitPos = rLegDeform.transform.localPosition;
    }

    public void Start(){
        //FixModelCollidersPivotIssue();
    }

    public void ShouldersSeparation(float fValue){
        Vector3 lShoulderPos = lShoulderInitPos;
        Vector3 rShoulderPos = rShoulderInitPos;
        lShoulderPos.x -= fValue;
        rShoulderPos.x += fValue;
        lShoulderDeform.transform.localPosition = lShoulderPos;
        rShoulderDeform.transform.localPosition = rShoulderPos;
        shouldersSeparation = fValue;
    }

    public void HipsSeparation(float fValue){
        Vector3 lLegPos = lLegInitPos;
        Vector3 rLegPos = rLegInitPos;
        lLegPos.x -= fValue;
        rLegPos.x += fValue;
        lLegDeform.transform.localPosition = lLegPos;
        rLegDeform.transform.localPosition = rLegPos;
        hipsSeparation = fValue;
    }

    public void FixModelCollidersPivotIssue(){
        Vector3 localScale = transform.localScale;
        transform.localScale = Vector3.one;
        bonesReference.transform.localScale = localScale; 
    } 

    public Hashtable SerializeModelData(){
        /*spineDeform;
        spine1Deform;
        spine2Deform;
        spine3Deform;

        hipsDeform;
        lLegDeform;
        rLegDeform;
        lThighDeform;
        rThighDeform;
        lKneeDeform;
        rKneeDeform;

        lShoulderDeform;
        rShoulderDeform;
        lArmDeform;
        rArmDeform;
        lForeArmDeform;
        rForeArmDeform;*/
        Hashtable data = new Hashtable();
        data[spineDeform.gameObject.name] =spineDeform.SerializeDeformData();
        data[spine1Deform.gameObject.name] =spine1Deform.SerializeDeformData();
        data[spine2Deform.gameObject.name] =spine2Deform.SerializeDeformData();
        data[spine3Deform.gameObject.name] =spine3Deform.SerializeDeformData();

        data[hipsDeform.gameObject.name] = hipsDeform.SerializeDeformData();
        data[lLegDeform.gameObject.name] = lLegDeform.SerializeDeformData();
        data[rLegDeform.gameObject.name] = rLegDeform.SerializeDeformData();
        data[lThighDeform.gameObject.name] =lThighDeform.SerializeDeformData();
        data[rThighDeform.gameObject.name] =rThighDeform.SerializeDeformData();
        data[lKneeDeform.gameObject.name] =lKneeDeform.SerializeDeformData();
        data[rKneeDeform.gameObject.name] =rKneeDeform.SerializeDeformData();
        data["shouldersSeparation"] = shouldersSeparation;
        data["hipsSeparation"] = hipsSeparation;

        //data[rCollarbone.gameObject.name] =rCollarbone.SerializeDeformData();
        //data[lCollarbone.gameObject.name] =lCollarbone.SerializeDeformData();

        //TODO ReDo leg serialization
        //data[this.rLeg.gameObject.name] = this.rLeg.SerializeDeformData();
        //data[this.lLeg.gameObject.name] = this.lLeg.SerializeDeformData();

        data[lShoulderDeform.gameObject.name] = lShoulderDeform.SerializeDeformData();
        data[rShoulderDeform.gameObject.name] = rShoulderDeform.SerializeDeformData();
        data[lArmDeform.gameObject.name] = lArmDeform.SerializeDeformData();
        data[rArmDeform.gameObject.name] = rArmDeform.SerializeDeformData();
        data[lForeArmDeform.gameObject.name] = lForeArmDeform.SerializeDeformData();
        data[rForeArmDeform.gameObject.name] = rForeArmDeform.SerializeDeformData();

        return data;
    }

    public void DeserializeModelData(Transform parent, Dictionary<string, object> data){
        foreach (Transform child in parent){
           
            if (data.ContainsKey(child.name))
            {
                Dictionary<string, object> objectData = (Dictionary<string, object>)data[child.name];
                //Debug.Log(child.name);
                if (/*"Model" == child.GetChild(0).name && */objectData.ContainsKey("scale"))
                {
                    Dictionary<string, object> scale = (Dictionary<string, object>)objectData["scale"];
                    Transform modelTransform = child;//child.GetChild(0);
                    modelTransform.localScale = new Vector3(float.Parse("" + scale["x"]), float.Parse("" + scale["y"]), float.Parse("" + scale["z"])); ;
                }

                if (objectData.ContainsKey("localPosition"))
                {
                    Dictionary<string, object> localPosition = (Dictionary<string, object>)objectData["localPosition"];
                    child.localPosition = new Vector3(float.Parse("" + localPosition["x"]), float.Parse("" + localPosition["y"]), float.Parse("" + localPosition["z"]));
                }
            }

            this.DeserializeModelData(child, data);
           
        }
        shouldersSeparation = float.Parse(data["shouldersSeparation"].ToString());
        ShouldersSeparation(shouldersSeparation);
        hipsSeparation = float.Parse(data["hipsSeparation"].ToString());
        HipsSeparation(hipsSeparation);
    }

    public void DeserializeImcData(Dictionary<string, object> data)
    {
        //parse weight value
        if (float.TryParse(data["weight"].ToString(), out weight)) Debug.Log("succed to parse weight: " + weight);
        else Debug.Log("error to parse weight value");
        //parse height value
        if (float.TryParse(data["height"].ToString(), out height))Debug.Log("succed to parse height: " + height);
        else Debug.Log("error to parse height value");
        //parse imc value
        if (float.TryParse(data["imc"].ToString(), out imcBase)) Debug.Log("succed to parse imc: " + imcBase);
        else Debug.Log("error to parse imc base value");
        //parse imc incremented value 
        //if (float.TryParse(data["imcIncremented"].ToString(), out imcIncremented)) Debug.Log("succed to parse imcincremented: " + imcIncremented);
        //else Debug.Log("error to parse imc incremented value");
        ////parse imc incremented value 
        //if (int.TryParse(data["sessionNumber"].ToString(), out numOfSession))
        //{
        //    Debug.Log("succed to parse session number: " + numOfSession);
        //    numOfSession++;
        //}

        //else Debug.Log("error to parse number of session value");

    }

    public void SetModelData(Dictionary<string, object> data)
    {
        DeserializeModelData(transform, data);
    }

    public bool TryInterpolateIMC(ref float imc,ref float h)
    {
        if (imcBase != 0 && height != 0)
        {
            imc = imcBase;
            h = height;
            return true;
        }
        else return false;
    }
}
