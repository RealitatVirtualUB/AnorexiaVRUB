using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Room : MonoBehaviour {
    public PrincipalSlider principalSlider;

    public Pivote pivote;
    public GameObject malePrefab;
    public GameObject femalePrefab;

    public GameObject blockMirror;

    public Transform mirrorRoom;
    public RigController rigController;
    public RigController mirrorRigController;

    public Transform cameraHead;
    public Transform r_Controller;
    public Transform l_Controller;

    public Transform cameraFollow;
    public Transform r_ControllerFollow;
    public Transform l_ControllerFollow;

    public float delay;

    public Transform mirrorCameraHead;
    public Transform r_mirror_Controller;
    public Transform l_mirror_Controller;

    public InputField delayField;

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

    private bool side = true;

    public MimicController mimicController;
    public Toggle visoMotorToggle;

    public Toggle brazoDerechoToggle;
    public Toggle brazoIzquierdoToggle;
    public Toggle panzaToggle;
    public Toggle piernaDerechaToggle;
    public Toggle piernaIzquierdaToggle;

    void Update()
    {
        Vector3 rcp = this.side ? this.r_Controller.position : this.l_Controller.position;
        Vector3 lcp = this.side ? this.l_Controller.position : this.r_Controller.position;
        Quaternion rcr = this.side ? this.r_Controller.rotation : this.l_Controller.rotation;
        Quaternion lcr = this.side ? this.l_Controller.rotation : this.r_Controller.rotation;

        float seconds = 0f;//
        try
        {
            seconds = float.Parse(this.delayField.text);
        }
        catch (System.Exception e)
        {

        }


        this.StartCoroutine(this.moveDelayed(this.cameraHead.transform.position, rcp, lcp, this.cameraHead.transform.rotation, rcr, lcr));

        if (null != this.rigController)
        {
            this.rigController.update();
        }
    }

    IEnumerator moveDelayed(Vector3 hPosition, Vector3 rPosition, Vector3 lPosition, Quaternion hRotation, Quaternion rRotation, Quaternion lRotation)
    {

        float seconds = 0f;//
        try {
            seconds = float.Parse(this.delayField.text);
        } catch(System.Exception e)
        {

        }
        
        yield return new WaitForSeconds(seconds);

        this.cameraFollow.transform.position = hPosition;
        this.cameraFollow.transform.rotation = hRotation;

        bool viso = this.visoMotorToggle.isOn;
        bool rArm = this.brazoDerechoToggle.isOn;
        bool lArm = this.brazoIzquierdoToggle.isOn;
        if (viso)
        {
            this.r_ControllerFollow.transform.position = rPosition;
            this.r_ControllerFollow.transform.rotation = rRotation;
            this.l_ControllerFollow.transform.position = lPosition;
            this.l_ControllerFollow.transform.rotation = lRotation;
        }
        else if (rArm)
        {
            this.r_ControllerFollow.transform.position = lPosition;
            this.r_ControllerFollow.transform.rotation = lRotation;
        }
        else if (lArm)
        {
            this.l_ControllerFollow.transform.position = lPosition;
            this.l_ControllerFollow.transform.rotation = lRotation;
        }
    }

    public void changeControls()
    {
        this.side = !this.side;
    }

    public void insertMale()
    {
        this.insertHuman(this.malePrefab);
    }

    public void insertFemale()
    {
        this.insertHuman(this.femalePrefab);
    }

    public void insertHuman(GameObject prefab)
    {
        if (null != this.rigController) {
            GameObject.Destroy(this.rigController.gameObject);
            GameObject.Destroy(this.mirrorRigController.gameObject);
        } else
        {
            this.visionBox.SetActive(true);
        }
        
        GameObject rigControllerObject = (GameObject)GameObject.Instantiate(prefab);
        rigControllerObject.transform.parent = this.transform;
        this.rigController = rigControllerObject.GetComponent<RigController>();

        this.rigController.pivote = this.pivote;
        this.rigController.headCamera = this.cameraFollow;
        this.rigController.r_controller = this.r_ControllerFollow;
        this.rigController.l_controller = this.l_ControllerFollow;
        this.rigController.mirror_room = this.mirrorRoom;
        this.rigController.mirrorController = this.mirrorRigController;
        this.rigController.headBone.localScale = Vector3.zero;

        GameObject mirrorRigControllerObject = (GameObject)GameObject.Instantiate(prefab);
        mirrorRigControllerObject.transform.parent = this.mirrorRoom;
        Vector3 scale = mirrorRigControllerObject.transform.localScale;
        scale.z = 1f;
        mirrorRigControllerObject.transform.localScale = scale;

        this.mirrorRigController = mirrorRigControllerObject.GetComponent<RigController>();
        this.mirrorRigController.enabled = false;

        this.mirrorRigController.headCamera = this.mirrorCameraHead;
        this.mirrorRigController.r_controller = this.r_mirror_Controller;
        this.mirrorRigController.l_controller = this.l_mirror_Controller;
        this.mirrorRigController.mirrorController = this.mirrorRigController;

        this.rigController.mirrorController = mirrorRigController;

        this.rigController.upperBone = this.pivote.upperBone;
        mirrorRigControllerObject.transform.localPosition = rigControllerObject.transform.localPosition;

        this.principalSlider.setRigControllers(this.rigController, this.mirrorRigController);

        RiggedArmature riggedArmature = rigControllerObject.GetComponent<RiggedArmature>();
        RiggedArmature mirrorRiggedArmature = mirrorRigControllerObject.GetComponent<RiggedArmature>();
        riggedArmature.pies_def = mirrorRiggedArmature.pies_def = this.pies_def;
        riggedArmature.pantorrillas_def = mirrorRiggedArmature.pantorrillas_def = this.pantorrillas_def;
        riggedArmature.muslos_def = mirrorRiggedArmature.muslos_def = this.muslos_def;
        riggedArmature.panza_def = mirrorRiggedArmature.panza_def = this.panza_def;
        riggedArmature.pecho_def = mirrorRiggedArmature.pecho_def = this.pecho_def;
        riggedArmature.brazos_def = mirrorRiggedArmature.brazos_def = this.brazos_def;
        riggedArmature.antebrazos_def = mirrorRiggedArmature.antebrazos_def = this.antebrazos_def;
        riggedArmature.manos_def = mirrorRiggedArmature.manos_def = this.manos_def;
        riggedArmature.cuello_def = mirrorRiggedArmature.cuello_def = this.cuello_def;
        riggedArmature.hombrosSeparacion_def = mirrorRiggedArmature.hombrosSeparacion_def = this.hombrosSeparacion_def;
        riggedArmature.caderasSeparacion_def = mirrorRiggedArmature.caderasSeparacion_def = this.caderasSeparacion_def;

        riggedArmature.configurarMedidas();
        mirrorRiggedArmature.configurarMedidas();

        this.rigController.init();
        this.rigController.nodes.mimicController = this.mimicController;
        this.mimicController.gameObject.SetActive(!this.visoMotorToggle.isOn);
        this.mimicController.mirrorModel.gameObject.SetActive(!this.visoMotorToggle.isOn);

        if (!this.visoMotorToggle.isOn)
        {
            if (brazoDerechoToggle.isOn)
            {
                this.enableBrazoDerechoTactil(true);
            }
            else if (brazoIzquierdoToggle.isOn)
            {
                this.enableBrazoIzquierdoTactil(true);
            }
            else if (panzaToggle.isOn)
            {
                this.enablePanzaTactil(true);
            }
            else if (piernaDerechaToggle.isOn)
            {
                this.enablePiernaDerechaTactil(true);
            }
            else if (piernaIzquierdaToggle.isOn)
            {
                this.enablePiernaIzquierdaTactil(true);
            }
        }

        this.principalSlider.sliderChanged();
    }

    public GameObject visionBox;

    public void toggleVision()
    {
        this.visionBox.SetActive(!this.visionBox.activeSelf);
    }

    public void toggleMirror()
    {
        this.blockMirror.SetActive(!this.blockMirror.activeSelf);
    }

    public void enableBrazoIzquierdoTactil(bool enabled)
    {
        if (this.brazoIzquierdoToggle.isOn)
        {
            this.enableTactil(this.rigController.nodes.group1, true);
            this.enableTactil(this.rigController.nodes.group2, false);
            this.enableTactil(this.rigController.nodes.group3, false);
            this.enableTactil(this.rigController.nodes.group4, false);
            this.enableTactil(this.rigController.nodes.group5, false);
            this.mimicController.setNodes(this.rigController.nodes.group1[0].transform, this.rigController.nodes.group1[this.rigController.nodes.group1.Length - 1].transform);
        }
    }

    public void enableBrazoDerechoTactil(bool enabled)
    {
        if (this.brazoDerechoToggle.isOn)
        {
            this.enableTactil(this.rigController.nodes.group1, false);
            this.enableTactil(this.rigController.nodes.group2, false);
            this.enableTactil(this.rigController.nodes.group3, false);
            this.enableTactil(this.rigController.nodes.group4, false);
            this.enableTactil(this.rigController.nodes.group5, true);
            this.mimicController.setNodes(this.rigController.nodes.group5[0].transform, this.rigController.nodes.group5[this.rigController.nodes.group5.Length - 1].transform);
        }
    }

    public void enablePanzaTactil(bool enabled)
    {
        if (this.panzaToggle.isOn)
        {
            this.enableTactil(this.rigController.nodes.group1, false);
            this.enableTactil(this.rigController.nodes.group2, true);
            this.enableTactil(this.rigController.nodes.group3, false);
            this.enableTactil(this.rigController.nodes.group4, false);
            this.enableTactil(this.rigController.nodes.group5, false);
            this.mimicController.setNodes(this.rigController.nodes.group2[0].transform, this.rigController.nodes.group2[this.rigController.nodes.group2.Length - 1].transform);
        }
    }

    public void enablePiernaDerechaTactil(bool enabled)
    {
        if (this.piernaDerechaToggle.isOn)
        {
            this.enableTactil(this.rigController.nodes.group1, false);
            this.enableTactil(this.rigController.nodes.group2, false);
            this.enableTactil(this.rigController.nodes.group3, true);
            this.enableTactil(this.rigController.nodes.group4, false);
            this.enableTactil(this.rigController.nodes.group5, false);
            this.mimicController.setNodes(this.rigController.nodes.group3[0].transform, this.rigController.nodes.group3[this.rigController.nodes.group3.Length - 1].transform);
        }
    }

    public void enablePiernaIzquierdaTactil(bool enabled)
    {
        if (this.piernaIzquierdaToggle.isOn)
        {
            this.enableTactil(this.rigController.nodes.group1, false);
            this.enableTactil(this.rigController.nodes.group2, false);
            this.enableTactil(this.rigController.nodes.group3, false);
            this.enableTactil(this.rigController.nodes.group4, true);
            this.enableTactil(this.rigController.nodes.group5, false);
            this.mimicController.setNodes(this.rigController.nodes.group4[0].transform, this.rigController.nodes.group4[this.rigController.nodes.group4.Length - 1].transform);
        }
    }

    public void enableTactil(GameObject[] group, bool enabled)
    {
        foreach (GameObject go in group)
        {
            go.SetActive(enabled);
        }
    }

    public void toggleReference()
    {
        if (null != this.mirrorRigController)
        {
            this.mirrorRigController.toggleReference();
        }
    }

    public void toggleReference_2()
    {
        if (null != this.mirrorRigController)
        {
            this.mirrorRigController.toggleReference_2();
        }
    }
}
