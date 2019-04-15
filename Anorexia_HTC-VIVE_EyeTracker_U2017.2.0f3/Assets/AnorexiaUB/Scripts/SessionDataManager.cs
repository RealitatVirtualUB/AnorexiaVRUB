using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;


public class SessionDataManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //CreateExel();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CreateExel()
    {
        IWorkbook workbook = new XSSFWorkbook();

        ISheet sheet1 = workbook.CreateSheet("Test");

        sheet1.CreateRow(0).CreateCell(0).SetCellValue("This is a Sample");

        int x = 1;

        for (int i = 1; i <= 15; i++)
        {
            IRow row = sheet1.CreateRow(i);

            for (int j = 0; j < 15; j++)
            {
                row.CreateCell(j).SetCellValue(x++);
            }
        }

        FileStream sw = File.Create("test.xlsx");

        workbook.Write(sw);

        sw.Close();

    }
}
