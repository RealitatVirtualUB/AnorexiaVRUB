using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRig : MonoBehaviour {
    public RayTracker rayTracker;

    public void pupilCalibrationStarted()
    {
        this.rayTracker.pupilCalibrationStarted();
    }

    public void pupilCalibrationTerminated()
    {
        this.rayTracker.pupilCalibrationTerminated();
    }

    public void recordPupilData()
    {
        this.rayTracker.recordPupilData();
    }

    public void stopRecordPupilData()
    {
        this.rayTracker.stopRecordPupilData();
    }
}
