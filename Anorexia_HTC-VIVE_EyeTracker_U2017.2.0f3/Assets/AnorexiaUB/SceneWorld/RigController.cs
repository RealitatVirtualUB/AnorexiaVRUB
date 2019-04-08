using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigController : MonoBehaviour
{
    public SkinnedMeshRenderer meshRenderer;
    public RiggedArmature riggedArmature;

    // con esto se configura la altura
    public Transform upperBone;
    public Pivote pivote;
    public Transform r_controller;
    public Transform l_controller;
    public Transform headCamera;
    private Vector3 lastCamera;

    // Cabeza
    public Transform headBone;
    public Transform BaseHumanHead;

    // Cuello
    public Transform BaseHumanRibcage;
    public Transform neckBone;
    public float neckFactor = 0.6f;


    public Transform centerBone;
    public Transform BaseHumanSpine1;

    public Transform r_hombroController;
    public Transform r_codoController;
    public Transform r_claviculaController;
    public Transform BaseHumanRCollarbone;
    public Transform BaseHumanRUpperarm;
    public Transform BaseHumanRForearm;
    public float r_claviculaFactor = 0.6f;

    public Transform l_hombroController;
    public Transform l_codoController;
    public Transform l_claviculaController;
    public Transform BaseHumanLCollarbone;
    public Transform BaseHumanLUpperarm;
    public Transform BaseHumanLForearm;
    public float l_claviculaFactor = 0.6f;

    public Transform mirror_room;
    public RigController mirrorController;

    float r_clInitAngle;
    float l_clInitAngle;

    public Nodes nodes;

    public GameObject reference_1;
    public GameObject reference_2;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    public void update()
    {
        this.mirrorRoom();
        this.iterate();
        this.head();
        this.mirrorPlayer();
        this.mirrorController.iterate();
        this.mirrorController.head();
        this.mirrorRoom();
    }

    public void init()
    {
        this.configureScale();
        this.mirrorRoom();
        this.mirrorPlayer();
        this.configure();
        this.mirrorController.configure();
        this.mirrorRoom();
    }

    void configureScale()
    {
        Vector3 position = this.pivote.transform.position;
        position.x = this.headCamera.position.x;
        position.z = this.headCamera.position.z;
        this.pivote.transform.position = position;
        //Vector3 distanceVector = this.headCamera.position - this.upperBone.position;
        float scale = this.headCamera.position.y / this.upperBone.position.y;
        this.mirrorController.transform.localScale = scale * Vector3.one;
        this.transform.localScale = scale * Vector3.one;
        this.transform.position = this.pivote.spawn.transform.position;
    }

    void configure()
    {
        this.lastCamera = this.headCamera.position;
        
        this.r_hombroController.position = this.BaseHumanRUpperarm.position;
        this.r_hombroController.rotation = this.BaseHumanRUpperarm.rotation;
        this.r_codoController.position = this.BaseHumanRForearm.position;
        this.r_codoController.rotation = this.BaseHumanRForearm.rotation;

        this.r_claviculaController.position = this.BaseHumanRCollarbone.position;

        this.BaseHumanRCollarbone.parent = this.r_claviculaController;
        this.r_codoController.parent = this.r_controller.transform;
        this.r_hombroController.position = this.BaseHumanRUpperarm.position;
        this.r_hombroController.LookAt(this.BaseHumanRForearm);
        this.BaseHumanRUpperarm.parent = this.r_hombroController;

        this.l_hombroController.position = this.BaseHumanLUpperarm.position;
        this.l_hombroController.rotation = this.BaseHumanLUpperarm.rotation;
        this.l_codoController.position = this.BaseHumanLForearm.position;
        this.l_codoController.rotation = this.BaseHumanLForearm.rotation;
        this.l_claviculaController.position = this.BaseHumanLCollarbone.position;
        this.BaseHumanLCollarbone.parent = this.l_claviculaController;
        this.l_codoController.parent = this.l_controller.transform;
        this.l_hombroController.position = this.BaseHumanLUpperarm.position;
        this.l_hombroController.LookAt(this.BaseHumanLForearm);
        this.BaseHumanLUpperarm.parent = this.l_hombroController;
        
        // Cabeza
        this.headBone.position = this.BaseHumanHead.position;
        this.BaseHumanHead.parent = this.headBone;

        // Cuello
        this.neckBone.position = this.BaseHumanRibcage.position;
        this.BaseHumanRibcage.parent = this.neckBone;

        Transform neck = this.neckBone.GetChild(0);
        neck.parent = null;
        this.neckBone.LookAt(this.headBone);
        neck.parent = this.neckBone;

        ArrayList transforms = new ArrayList();
        foreach (Transform child in this.centerBone)
        {
            transforms.Add(child);
            child.parent = null;
        }

        Vector3 centerNeckVector = this.neckBone.position - this.centerBone.position;
        this.centerBone.up = centerNeckVector.normalized;
        this.BaseHumanSpine1.parent = this.centerBone;

        foreach (Transform child in transforms)
        {
            child.parent = this.centerBone;
        }

        this.r_clInitAngle = Vector3.Angle(this.r_hombroController.forward, this.centerBone.up);
        this.l_clInitAngle = Vector3.Angle(this.l_hombroController.forward, this.centerBone.up);
    }

    void mirrorRoom()
    {
        Vector3 scale = this.mirror_room.localScale;
        scale.z = -scale.z;
        this.mirror_room.localScale = scale;
    }

    void mirrorPlayer()
    {
        this.mirrorController.transform.position = this.transform.position;

        this.mirrorController.l_controller.position = this.l_controller.position;
        this.mirrorController.l_controller.rotation = this.l_controller.rotation;
        this.mirrorController.r_controller.position = this.r_controller.position;
        this.mirrorController.r_controller.rotation = this.r_controller.rotation;
        this.mirrorController.headCamera.position = this.headCamera.position;
        this.mirrorController.headCamera.rotation = this.headCamera.rotation;

        this.mirrorController.GetComponent<RiggedArmature>().Pelvis_D.position = this.GetComponent<RiggedArmature>().Pelvis_D.position;
        this.mirrorController.GetComponent<RiggedArmature>().Pelvis_I.position = this.GetComponent<RiggedArmature>().Pelvis_I.position;

        if (this.nodes.mimicController)
        {
            this.nodes.mimicController.mirrorModel.position = this.nodes.mimicController.model.transform.position;
            this.nodes.mimicController.mirrorModel.rotation = this.nodes.mimicController.model.transform.rotation;
        }
    }

    void iterate()
    {
        this.r_hombroController.LookAt(this.r_codoController);
        this.BaseHumanRForearm.rotation = this.r_codoController.rotation;

        this.l_hombroController.LookAt(this.l_codoController);
        this.BaseHumanLForearm.rotation = this.l_codoController.rotation;

        float l_armAngle = Vector3.Angle(this.l_hombroController.forward, this.centerBone.up);
        Vector3 l_claviculaEuler = this.l_claviculaController.eulerAngles;
        l_claviculaEuler.z = this.l_claviculaFactor * (l_armAngle - this.l_clInitAngle);
        this.l_claviculaController.eulerAngles = l_claviculaEuler;

        float r_armAngle = Vector3.Angle(this.r_hombroController.forward, this.centerBone.up);
        Vector3 r_claviculaEuler = this.r_claviculaController.eulerAngles;
        r_claviculaEuler.z = this.r_claviculaFactor * (this.r_clInitAngle - r_armAngle);
        this.r_claviculaController.eulerAngles = r_claviculaEuler;
    }

    void head()
    {
        Vector3 cameraDiff = this.headCamera.position - this.lastCamera;
        this.headBone.position += cameraDiff;
        this.headBone.rotation = this.headCamera.rotation;
        this.lastCamera = this.headCamera.position;

        Vector3 centerNeckVector = this.neckBone.position - this.centerBone.position;
        this.centerBone.up = centerNeckVector.normalized;
        this.neckBone.position += this.neckFactor * cameraDiff;
        this.neckBone.LookAt(this.headBone);

        Vector3 angles = this.neckBone.eulerAngles;
        angles.y = 0f;
        this.neckBone.eulerAngles = angles;
    }

    public void toggleReference() {
        this.reference_1.SetActive(!this.reference_1.activeSelf);
    }

    public void toggleReference_2()
    {
        this.reference_2.SetActive(!this.reference_2.activeSelf);
    }
}
