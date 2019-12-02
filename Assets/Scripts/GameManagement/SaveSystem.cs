using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem
{
    public static void SaveSettings(Settings settings) 
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Path.Combine(Application.persistentDataPath, "gamesettings.sav");
        FileStream stream = new FileStream(path, FileMode.Create);

        SettingsData data = new SettingsData(settings);

        formatter.Serialize(stream, data);
        stream.Close();

    }

    public static SettingsData LoadSettings()
    {
        string path = Path.Combine(Application.persistentDataPath, "gamesettings.sav");
        Debug.Log("settings file in" + path);
        try
        {
            if (File.Exists(path))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open);

                SettingsData data = formatter.Deserialize(stream) as SettingsData;
                stream.Close();

                return data;
            }
            else
            {
                Debug.LogError("Settings file not found in " + path);
                return null;
            }
        }
        catch (System.Exception)
        {
            Debug.LogError("Settings file not found in " + path);
            return null;
        }

    }


    public static void SavePlayer(GameObject player)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Path.Combine(Application.persistentDataPath, "playerprogress.sav");
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(player);

        formatter.Serialize(stream, data);
        stream.Close();

    }

    public static PlayerData LoadPlayer()
    {
        string path = Path.Combine(Application.persistentDataPath, "playerprogress.sav");
        try
        {
            if (File.Exists(path))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open);

                PlayerData data = formatter.Deserialize(stream) as PlayerData;
                stream.Close();

                return data;
            }
            else
            {
                Debug.LogError("Player progress file not found in " + path);
                return null;
            }
        }
        catch (System.Exception)
        {
            Debug.LogError("Player progress file not found in " + path);
            return null;
        }
    }

    public static void DeletePlayer()
    {
        string path = Path.Combine(Application.persistentDataPath, "playerprogress.sav");
        try
        {
            if (File.Exists(path))
            {
                File.Delete(path);

                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open);

                PlayerData data = formatter.Deserialize(stream) as PlayerData;
                stream.Close();

                
            }
            else
            {
                Debug.LogError("Player progress file not found in " + path);
                
            }
        }
        catch (System.Exception)
        {
            Debug.LogError("Could not delete player progress file in " + path);
            
        }
    }

    public static void SaveGameStats(GameObject player)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Path.Combine(Application.persistentDataPath, "gamestats.sav");
        FileStream stream = new FileStream(path, FileMode.Create);

        GameStatsData data = new GameStatsData(player);

        formatter.Serialize(stream, data);
        stream.Close();

    }

    public static GameStatsData LoadStats()
    {
        string path = Path.Combine(Application.persistentDataPath, "gamestats.sav");
        try
        {
            if (File.Exists(path))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open);

                GameStatsData data = formatter.Deserialize(stream) as GameStatsData;
                stream.Close();

                return data;
            }
            else
            {
                Debug.LogError("Game stats file not found in " + path);
                return null;
            }
        }
        catch (System.Exception)
        {
            Debug.LogError("Game stats file not found in " + path);
            return null;
        }
    }
}
