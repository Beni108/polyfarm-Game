using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using Newtonsoft.Json;
public static class SaveSystem 
{
    private static readonly string SAVE_FOLDER = Application.dataPath + "/Saves";

    private static readonly string STAT_FOLDER = Application.dataPath;
    public static void init()
    {
        if (!Directory.Exists(SAVE_FOLDER))
        {
            Directory.CreateDirectory(SAVE_FOLDER);
        }
        if (!Directory.Exists(STAT_FOLDER))
        {
            Directory.CreateDirectory(STAT_FOLDER);
        }
    }
    public static string saveLevel(LevelInfo lv)
    {

        string lvlJson = JsonConvert.SerializeObject(lv);
        
        lvlJson = encryptSaved(lvlJson);
        string levelname = "CustomLevel" + System.DateTime.Now.Day + System.DateTime.Now.Month + System.DateTime.Now.Hour + System.DateTime.Now.Minute;
        File.WriteAllText(Application.dataPath+"/"+levelname+".json",lvlJson);
        Debug.Log("saved level");
        return levelname+".json";
    }
    public static LevelInfo loadLevel(string path)
    {
        if(File.Exists(path))
        {
            string lvlJson = File.ReadAllText(path);
            
            lvlJson = decryptSaved(lvlJson);
            return JsonConvert.DeserializeObject<LevelInfo>(lvlJson);
        }
        return null;
    }
    public static  void saveStatistics()
    {
        string[] stats = new string[4];
        stats[0] = PlayerPrefs.GetInt("UndoCounter").ToString();
        stats[1] = PlayerPrefs.GetInt("GameOverCounter").ToString();
        stats[2] = PlayerPrefs.GetInt("PlayedCounter").ToString();
        stats[3] = PlayerPrefs.GetString("LevelCounter");
        string jsonStats = JsonConvert.SerializeObject(stats);
        jsonStats = encryptSaved(jsonStats);
        File.WriteAllText(Application.dataPath + "/" +"PlayerData.json", jsonStats);

    }

    public static string[] readPlayerstatistics(string path)
    {
        if (File.Exists(path))
        {
            string JsonStats = File.ReadAllText(path);

            JsonStats = decryptSaved(JsonStats);
            return JsonConvert.DeserializeObject<string[]>(JsonStats);
        }
        return null;
    }


    private static string encryptSaved(string js)
    {
        js = Rot13(js);
        return js;
    }
    private static string decryptSaved(string js)
    {
        js = Rot13(js);
        return js;
    }
    public static string Base64Encode(string value)
    {
        byte[] bytestoEncode = Encoding.UTF8.GetBytes(value);
        return System.Convert.ToBase64String(bytestoEncode);
    }
    public static string Base64Decode(string value)
    {
        byte[] decodedBytes = System.Convert.FromBase64String(value);
        return Encoding.UTF8.GetString(decodedBytes);
    }
    public static string Rot13(string value)
        {
            char[] array = value.ToCharArray();
            for (int i = 0; i < array.Length; i++)
            {
                int number = (int)array[i];

                if (number >= 'a' && number <= 'z')
                {
                    if (number > 'm')
                    {
                        number -= 13;
                    }
                    else
                    {
                        number += 13;
                    }
                }
                else if (number >= 'A' && number <= 'Z')
                {
                    if (number > 'M')
                    {
                        number -= 13;
                    }
                    else
                    {
                        number += 13;
                    }
                }
                array[i] = (char)number;
            }
            return new string(array);
        }
    
}
