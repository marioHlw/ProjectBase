using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.IO.Compression;

public class TestEditor 
{
    [MenuItem("Tools/测试")]
    public static void Init()
    {
        //Debug.Log(Language.GetTimekeepingFormatBySecond(100));

        //string _str = ("{&quot;cmd&quot;}").Replace("&quot;", "\"");

        //string[] _files = Directory.GetFiles(@"G:\whknmyhe\SVN\Hunter\Client\Assets\ExtraTools\DynamicUI\Resources\Atlas\ItemIcon\Atlas");
        //string _str = "string[] _atlasNames = new string[]\n{\n";
        //for (int i = 0; i < _files.Length; i++)
        //{
        //    if (Path.GetFileName(_files[i]).EndsWith("_Alpha.png"))
        //    {
        //        string _temp = Path.GetFileNameWithoutExtension(_files[i]);
        //        _str += string.Format("\"{0}\",", _temp.Substring(0, _temp.Length - 6));
        //        _str += "\n";
        //    }
        //}
        //_str += "};";
        //Debug.Log(_str);

        //using (FileStream zipToOpen = new FileStream(@"c:\users\exampleuser\release.zip", FileMode.Open))
        //{
            //using (ZipArchive archive = new ZipArchive(zipToOpen, ZipArchiveMode.Update))
            //{
            //    ZipArchiveEntry readmeEntry = archive.CreateEntry("Readme.txt");
            //    using (StreamWriter writer = new StreamWriter(readmeEntry.Open()))
            //    {
            //        writer.WriteLine("Information about this package.");
            //        writer.WriteLine("========================");
            //    }
            //}
        //}
    }
}