using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using Excel;
using System.Data;
public class MyEditor : MonoBehaviour
{
#if UNITY_EDITOR
    // Start is called before the first frame update
    [MenuItem("我的工具/excel转成txt")]
#endif
    public static void ExportExcelToTxt()
    {
        string assetPath = Application.dataPath + "/_Excel";
        Debug.Log(assetPath);
        string[] files = Directory.GetFiles(assetPath,"*.xlsx");
        for(int i = 0; i < files.Length; i++)
        {
            Debug.Log(files[i]);
            files[i] = files[i].Replace('\\', '/');
            Debug.Log(files[i]);
            using(FileStream fs = File.Open(files[i], FileMode.Open, FileAccess.Read))
            {
                var excelReader = ExcelReaderFactory.CreateOpenXmlReader(fs);
                var dataSet = excelReader.AsDataSet();
                var dataTable = dataSet.Tables[0];
                readTableToTxt(files[i], dataTable);
            }
        }
#if UNITY_EDITOR
        AssetDatabase.Refresh();
#endif
    }
    private static void readTableToTxt(string filePath,DataTable table)
    {
        string fileName = Path.GetFileNameWithoutExtension(filePath);
       
        string path = Application.dataPath + "/Resources/Data/" + fileName + ".txt";
        if (File.Exists(path))
        {
            File.Delete(path);
        }
        using (FileStream fs = new FileStream(path,FileMode.Create))
        {
            using(StreamWriter wr = new StreamWriter(fs))
            {
                for(int rowNum =0; rowNum < table.Rows.Count; rowNum++)
                {
                   var row = table.Rows[rowNum];
                    string str = "";
                    for(int columnNum = 0; columnNum < table.Columns.Count; columnNum++)
                    {
                        
                        str = str + row[table.Columns[columnNum]] + "\t";
                    }
                    wr.Write(str);
                    if (rowNum != table.Rows.Count - 1)
                    {
                        wr.WriteLine();
                    }
                }
            }
        }

    }
}
