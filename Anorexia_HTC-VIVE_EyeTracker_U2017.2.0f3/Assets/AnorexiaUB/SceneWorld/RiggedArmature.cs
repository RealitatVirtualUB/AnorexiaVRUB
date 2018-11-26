using MiniJSON;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class RiggedArmature : MonoBehaviour {
    public RigController rigController;
    public float startBlendShape;

	public DeformP pies_def;
	public DeformP pantorrillas_def;
	public DeformP muslos_def;
	public DeformP panza_def;
	public DeformP pecho_def;
	public DeformP brazos_def;
	public DeformP antebrazos_def;
	public DeformP manos_def;
	public DeformP cuello_def;
	public DeformP hombrosSeparacion_def;
	public DeformP caderasSeparacion_def;

	public Transform Pie_D;
	public Transform Pie_I;
	public Transform Pantorrilla_D;
	public Transform Pantorrilla_I;
	public Transform Muslo_D;
	public Transform Muslo_I;
	public Transform Pelvis_D;
	public Transform Pelvis_I;
	public Transform Pelvis;
	public Transform Panza;
	public Transform Pecho;
	public Transform Cuello;
	public Transform Hombro_D;
	public Transform Hombro_I;
	public Transform Brazo_D;
	public Transform Brazo_I;
	public Transform AnteBrazo_D;
	public Transform AnteBrazo_I;
	public Transform Mano_D;
	public Transform Mano_I;

	private float pelvis_D_x;
	private float pelvis_I_x;
	private float hombro_D_x;
	private float hombro_I_x;
    
    public void configurarMedidas() {
		//this.pies_def.slider.onValueChanged.AddListener (delegate {piesChanged ();});
        
		this.pantorrillas_def.slider.onValueChanged.AddListener (delegate {pantorrillasChanged ();});
        this.muslos_def.slider.onValueChanged.AddListener (delegate {muslosChanged ();});
        this.panza_def.slider.onValueChanged.AddListener (delegate {panzaChanged ();});
        this.pecho_def.slider.onValueChanged.AddListener (delegate {pechoChanged ();});
        
        this.brazos_def.slider.onValueChanged.AddListener (delegate {brazosChanged ();});
		this.antebrazos_def.slider.onValueChanged.AddListener (delegate {antebrazosChanged ();});
		//this.manos_def.slider.onValueChanged.AddListener (delegate {manosChanged ();});
		//this.cuello_def.slider.onValueChanged.AddListener (delegate {cuelloChanged ();});
		this.hombrosSeparacion_def.slider.onValueChanged.AddListener (delegate {homborsChanged ();});
        this.caderasSeparacion_def.slider.onValueChanged.AddListener (delegate {caderasChanged ();});

		this.pelvis_I_x = this.Pelvis_I.localPosition.z;
		this.pelvis_D_x = this.Pelvis_D.localPosition.z;

		this.hombro_I_x = this.Hombro_I.localPosition.x;
		this.hombro_D_x = this.Hombro_D.localPosition.x;

        //string filePath = Application.dataPath + "/Pictures/";
        //string fileName = subjectIdField.text + ".txt";
        string FILE_NAME = "model.txt";
        if (File.Exists(FILE_NAME)) {
            string text = File.ReadAllText(FILE_NAME);
            Dictionary<string, object> data = (Dictionary<string, object>)Json.Deserialize(text);
            this.scaleParts(this.transform, data);
            this.startBlendShape = float.Parse("" + data["shape"]);
            this.rigController.meshRenderer.SetBlendShapeWeight(0, this.startBlendShape);
        }
    }

    void scaleParts(Transform parent, Dictionary<string, object> data)
    {
        foreach (Transform child in parent)
        {
            if (0 < child.childCount)
            {
                if (data.ContainsKey(child.name))
                {
                    Dictionary<string, object> objectData = (Dictionary<string, object>) data[child.name];
                    if ("Model" == child.GetChild(0).name && objectData.ContainsKey("scale"))
                    {
                        Dictionary<string, object> scale = (Dictionary<string, object>) objectData["scale"];
                        Transform modelTransform = child.GetChild(0);
                        modelTransform.localScale = new Vector3(float.Parse("" + scale["x"]), float.Parse("" + scale["y"]), float.Parse("" + scale["z"])); ;
                    }

                    if (objectData.ContainsKey("localPosition"))
                    {
                        Dictionary<string, object> localPosition = (Dictionary<string, object>) objectData["localPosition"];
                        child.localPosition = new Vector3(float.Parse("" + localPosition["x"]), float.Parse("" + localPosition["y"]), float.Parse("" + localPosition["z"]));
                    }
                }
                
                this.scaleParts(child, data);
            }
        }
    }

    void Update()
    {
        this.AnteBrazo_I.localScale = this.AnteBrazo_I.localScale;
        this.AnteBrazo_D.localScale = this.AnteBrazo_D.localScale;
    }

	private void change(DeformP deform, Transform part) {
		float x = deform.x();
		float z = deform.z();
        float y = deform.y();
        
        ArrayList transforms = new ArrayList();
        foreach (Transform child in part)
        {
            transforms.Add(child);
        }

        foreach (Transform child in transforms)
        {
            if (!child.gameObject.name.StartsWith("Mid_"))
            {
                child.parent = null;
            }
        }

        Vector3 partScale = part.localScale;
        partScale.x = (1f + x);
        partScale.y = (1f + y);
        partScale.z = (1f + z);
        part.localScale = partScale;

        this.StartCoroutine(dale(transforms, part));

       // if (deform.mid)
        {
            float mid = this.transform.localScale.y * (1f + z);// * deform.mid.getMid();
            deform.inputField.text = mid.ToString("0.00");
        }
    }

    IEnumerator dale(ArrayList transforms, Transform part)
    {
        yield return new WaitForSeconds(0.1f);
        foreach (Transform child in transforms)
        {
            child.parent = part;
        }
    }

	public void piesChanged() {
		this.change(this.pies_def, this.Pie_I);
        this.change(this.pies_def, this.Pie_D);
	}
	
	public void pantorrillasChanged() {
        this.change(this.pantorrillas_def, this.Pantorrilla_I);
        this.change(this.pantorrillas_def, this.Pantorrilla_D);
	}
	
	public void muslosChanged() {
        this.change(this.muslos_def, this.Muslo_I);
        this.change(this.muslos_def, this.Muslo_D);
	}
	
	public void panzaChanged() {
        this.change(this.panza_def, this.Panza);
	}
	
	public void pechoChanged() {
        this.change(this.pecho_def, this.Pecho);
	}
	
	public void brazosChanged() {
        this.change(this.brazos_def, this.Brazo_I);
        this.change(this.brazos_def, this.Brazo_D);
	}
	
	public void antebrazosChanged() {
        this.change(this.antebrazos_def, this.AnteBrazo_I);
        this.change(this.antebrazos_def, this.AnteBrazo_D);
	}
	
	public void manosChanged() {
        this.change(this.manos_def, this.Mano_I);
        this.change(this.manos_def, this.Mano_D);
	}
	
	public void cuelloChanged() {
		float x = this.cuello_def.x();
		float y = this.cuello_def.y();
		float z = this.cuello_def.z();

		Vector3 partScale = this.Cuello.localScale;
		partScale.x = (1f + x);
		partScale.y = (1f + y);
		partScale.z = (1f + z);
		this.Cuello.localScale = partScale;
	}
	
	public void homborsChanged() {
		float fvalue = this.hombrosSeparacion_def.x();

		Vector3 hombro_I_position = this.Hombro_I.localPosition;
		hombro_I_position.x = this.hombro_I_x * (1f + fvalue);
		this.Hombro_I.localPosition = hombro_I_position;

		Vector3 hombro_D_position = this.Hombro_D.localPosition;
		hombro_D_position.x = this.hombro_D_x * (1f + fvalue);
		this.Hombro_D.localPosition = hombro_D_position;

        float mid = this.transform.localScale.y /* this.hombrosSeparacion_def.mid.getMid() */ * this.hombrosSeparacion_def.az;
        this.hombrosSeparacion_def.inputField.text = mid.ToString("0.00");
    }
	
	public void caderasChanged() {
		float fvalue = this.caderasSeparacion_def.x();
		Vector3 pelvis_I_position = this.Pelvis_I.localPosition;
		pelvis_I_position.z = this.pelvis_I_x * (1f + fvalue);
		this.Pelvis_I.localPosition = pelvis_I_position;

		Vector3 pelvis_D_position = this.Pelvis_D.localPosition;
		pelvis_D_position.z = this.pelvis_D_x * (1f + fvalue);
		this.Pelvis_D.localPosition = pelvis_D_position;
	}
}
