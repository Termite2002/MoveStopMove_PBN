using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveLoadManager : Singleton<SaveLoadManager>
{
    public static void SaveData(object data, string filename)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/" + filename;
        FileStream stream = new FileStream(path, FileMode.Create);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static object LoadData(string filename)
    {
        string path = Application.persistentDataPath + "/" + filename;
        Debug.Log(path);
        if (File.Exists(filename))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            object data = formatter.Deserialize(stream);
            stream.Close();
            return data;
        }
        else
        {
            CreateNewFile(filename);
            return null;
        }
    }
    private static void CreateNewFile(string filename)
    {
        string path = Application.persistentDataPath + "/" + filename;
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream file = File.Create(path);
        Data data = new Data();
        data.gold = 8999;
        formatter.Serialize(file, data);
        file.Close();
    }
}
