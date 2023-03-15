using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveLoadManager : Singleton<SaveLoadManager>
{
    public static void SaveData(object data, string filename)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(filename, FileMode.Create);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static object LoadData(string filename)
    {
        if (File.Exists(filename))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(filename, FileMode.Open);
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
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream file = File.Create(filename);
        Data data = new Data();
        data.gold = 0;
        formatter.Serialize(file, data);
        file.Close();
    }
}
