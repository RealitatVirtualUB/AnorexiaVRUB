using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InGameData{
    private static float imcIncrement;
    private static int sn;
    private static bool isClinic;
    private static string pacientId;

    public static string PacientId { get { return pacientId; } set { pacientId = value; } }
    public static float ImcIncrement { get { return imcIncrement; } set { imcIncrement = value; } }
    public static int Sn { get { return sn; } set { sn = value; } }
    public static bool IsClinic { get { return isClinic; } set { isClinic = value; } }
}
