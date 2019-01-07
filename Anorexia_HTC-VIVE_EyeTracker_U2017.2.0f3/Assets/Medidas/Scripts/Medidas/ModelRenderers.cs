using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModelRenderers : MonoBehaviour {

    public SkinnedMeshRenderer skin;
    public SkinnedMeshRenderer shoes;
    public SkinnedMeshRenderer shirt;
    public SkinnedMeshRenderer pants;
    public SkinnedMeshRenderer hair;
    public SkinnedMeshRenderer bonnet;

    public TransformColliderController colliderSetter;
    private int gender;
    private float age = 100;
    private float height = 1;
    private float fat; //deprecated
    private float thin; //deprecated
    private float weight;
    /*
    public BlendShape blendShape;
    [Range(100,0)]
    public float amount;
    [Range(0.5f, 1)]
    public float height;
    */
    /*
    void Update(){
        skin.SetBlendShapeWeight((int)blendShape, amount);
        shoes.SetBlendShapeWeight((int)blendShape, amount);
        shirt.SetBlendShapeWeight((int)blendShape, amount);
        pants.SetBlendShapeWeight((int)blendShape, amount);
        hair.SetBlendShapeWeight((int)blendShape, amount);
        bonnet.SetBlendShapeWeight((int)blendShape, amount);

        transform.localScale = Vector3.one * height;
    }
    */
    public void SetGender(int amount){
        gender = amount;
        SetBlendShape(BlendShape.GENDER, gender); 
    }
        
    public void SetAge(Slider slider){
        age = slider.value;
        SetBlendShape(BlendShape.AGE, 100 - age);
    }

    public void SetHeight(Slider slider){
        height = slider.value;
        transform.localScale = Vector3.one * height;
    }

    public void SetFat(Slider slider){
        fat = slider.value; 
        SetBlendShape(BlendShape.FAT, fat);
        colliderSetter.SetColliderSize(fat/100);
        
        colliderSetter.SaveInterpolationValue(fat / 100);
    }

    public void SetThin(Slider slider){
        thin = slider.value;
        SetBlendShape(BlendShape.THIN, thin);
    }

    public void SetWeightPorcentage(float value, BlendShape change)
    {
        weight = value;
        //Debug.Log("type " + change + " value of the slider " + value);
        SetBlendShape(change,weight);
        if(colliderSetter != null)
        {
            if (change == BlendShape.FAT) colliderSetter.SaveInterpolationValue(value / 100);
            else colliderSetter.SaveInterpolationValue(-(value / 100));
        }
        
    }

    public void SetAge(float value)
    {
        age = value;
        SetBlendShape(BlendShape.AGE, 100 - age);
    }

    public void SetHeight(float value)
    {
        height = value;
        transform.localScale = Vector3.one * height;
    }

    public void SetFat(float value)
    {
        fat = value;
        SetBlendShape(BlendShape.FAT, fat);
    }

    public void SetThin(float value)
    {
        thin = value;
        SetBlendShape(BlendShape.THIN, thin);
    }

    void SetBlendShape(BlendShape bShape, float amount){
        skin.SetBlendShapeWeight((int)bShape, amount);
        shoes.SetBlendShapeWeight((int)bShape, amount);
        shirt.SetBlendShapeWeight((int)bShape, amount);
        pants.SetBlendShapeWeight((int)bShape, amount);
        hair.SetBlendShapeWeight((int)bShape, amount);
        bonnet.SetBlendShapeWeight((int)bShape, amount);
    }

    public Hashtable SerializeData()
    {
        Hashtable blendShapesData = new Hashtable();
        blendShapesData[BlendShape.AGE] = age;
        blendShapesData[BlendShape.GENDER] = gender;
        blendShapesData[BlendShape.THIN] = thin;
        blendShapesData[BlendShape.FAT] = fat;
        blendShapesData["HEIGHT"] = height;
        return blendShapesData;

    }

    public void DeserializeData(Dictionary<string, object> blendShapesData){
        /*
        foreach (string blendShapeKey in blendShapesData.Keys){
            SetBlendShape(((BlendShape)System.Enum.Parse(typeof(BlendShape), blendShapeKey)), (float)blendShapesData[blendShapeKey]);
        }*/
        Debug.Log(blendShapesData[BlendShape.AGE.ToString()]);
        age = float.Parse(((blendShapesData[BlendShape.AGE.ToString()]).ToString()));
        SetBlendShape(BlendShape.AGE, age);
        SetAge(age);
        gender = int.Parse(((blendShapesData[BlendShape.GENDER.ToString()]).ToString()));
        SetBlendShape(BlendShape.GENDER, gender);
        SetGender(gender);
        thin = float.Parse(((blendShapesData[BlendShape.THIN.ToString()]).ToString()));
        SetBlendShape(BlendShape.THIN, thin);
        SetThin(thin);
        fat = float.Parse(((blendShapesData[BlendShape.FAT.ToString()]).ToString()));
        SetBlendShape(BlendShape.FAT, fat);
        SetFat(fat);
        height = float.Parse(blendShapesData["HEIGHT"].ToString());
        SetHeight(height);
    }

    public void SetOutlineColor(Color pOutlineColor){
        ChangeOutlineColor(skin, pOutlineColor);
        ChangeOutlineColor(shoes, pOutlineColor);
        ChangeOutlineColor(shirt, pOutlineColor);
        ChangeOutlineColor(pants, pOutlineColor);
        ChangeOutlineColor(hair, pOutlineColor);
        ChangeOutlineColor(bonnet, pOutlineColor);
    }

    void ChangeOutlineColor(SkinnedMeshRenderer pRenderer, Color pOutlineColor)
    {
        Material[] materials = pRenderer.materials;
        foreach (Material mat in materials)
        {
            //mat.shader = Shader.Find("Outlined/Silhouette Only");
            mat.SetColor("_OutlineColor", pOutlineColor);
            mat.SetFloat("_Outline", 0.03f);
        }

    }
}
   


public enum BlendShape
{
    AGE,
    GENDER,
    THIN,
    FAT
}
