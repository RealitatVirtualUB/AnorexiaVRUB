using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneWorld : MonoBehaviour {
    public CameraRig cameraRig;
    public Room room;

    public void pupilCalibrationStarted()
    {
        this.cameraRig.pupilCalibrationStarted();
    }

    public void pupilCalibrationTerminated()
    {
        this.cameraRig.pupilCalibrationTerminated();
    }

    public void recordPupilData()
    {
        this.cameraRig.recordPupilData();
    }

    public void stopRecordPupilData()
    {
        this.cameraRig.stopRecordPupilData();
    }

    public void toggleReference()
    {
        this.room.toggleReference();
    }

    public void toggleReference_2()
    {
        this.room.toggleReference_2();
    }
}
