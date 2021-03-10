using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;

public static class SaveLoadManager
{


    public static void SaveGameState()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + "/gameState.gs", FileMode.Create);

        GameStateData data = new GameStateData();

        bf.Serialize(stream, data);
        stream.Close();
    }

    public static int LoadGameState()
    {
        if(File.Exists(Application.persistentDataPath + "/gameState.gs"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/gameState.gs", FileMode.Open);

            GameStateData data = bf.Deserialize(stream) as GameStateData;

            stream.Close();
            return data.levelIndex;
        }
        else
        {
            Debug.LogError("GS-File does not exist");
            return new int();
        }
    }

    public static void SaveSettings(Einstellungen einstellungen)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + "/settings.set", FileMode.Create);

        SettingsData data = new SettingsData(einstellungen);

        bf.Serialize(stream, data);
        stream.Close();
    }

    public static void LoadSettings(Einstellungen einstellungen)
    {
        if (File.Exists(Application.persistentDataPath + "/settings.set"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/settings.set", FileMode.Open);

            SettingsData data = bf.Deserialize(stream) as SettingsData;

            stream.Close();
            einstellungen.saving = true;
            einstellungen.currentResolutionIndex = data.resolutionIndex;
            einstellungen.currentFullscreen = data.fullscreenToggle;
            einstellungen.currentVolumeMusic = data.volumeMusic;
            einstellungen.currentVolumeMaster = data.volumeMaster;
            einstellungen.currentVolumeVoice = data.volumeVoice;
            einstellungen.currentSubtitle = data.subtitle;
            einstellungen.currentLanguage = data.language;
            MainMenu.Instance.hasSettings = true;
            //Debug.Log(einstellungen.currentResolutionIndex + "FS" + einstellungen.currentFullscreen + "V" + einstellungen.currentVolume);

        }
        else
        {
            MainMenu.Instance.hasSettings=false;
            MainMenu.Instance.SetFlags();
            Debug.LogError("Settings-File does not exist");

        }
    }

    public static void SaveInventarRene(InventoryRene rene)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + "/inventoryRene.inv", FileMode.Create);

        InventarReneStateData data = new InventarReneStateData(rene);

        bf.Serialize(stream, data);
        stream.Close();
    }

    public static void LoadInventarRene(InventoryRene rene)
    {
        if (File.Exists(Application.persistentDataPath + "/inventoryRene.inv"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/inventoryRene.inv", FileMode.Open);

            InventarReneStateData data = bf.Deserialize(stream) as InventarReneStateData;

            stream.Close();
            rene.isActive[0] = data.isActive[0];
            rene.isActive[1] = data.isActive[1];
            rene.isActive[2] = data.isActive[2];
            rene.isActive[3] = data.isActive[3];
            rene.isActive[4] = data.isActive[4];
            rene.isActive[5] = data.isActive[5];
            rene.isActive[6] = data.isActive[6];
            rene.isActive[7] = data.isActive[7];

            rene.spriteIndexRene[0] = data.spriteIndex[0];
            rene.spriteIndexRene[1] = data.spriteIndex[1];
            rene.spriteIndexRene[2] = data.spriteIndex[2];
            rene.spriteIndexRene[3] = data.spriteIndex[3];
            rene.spriteIndexRene[4] = data.spriteIndex[4];
            rene.spriteIndexRene[5] = data.spriteIndex[5];
            rene.spriteIndexRene[6] = data.spriteIndex[6];
            rene.spriteIndexRene[7] = data.spriteIndex[7];

            rene.tookNadelS2 = data.tookIn2;
        }
        else
        {
            Debug.LogError("InventoryRene-File does not exist");

        }
    }

    public static void SaveInventarElena(InventoryElena elena)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + "/inventoryElena.inv", FileMode.Create);

        InventarElenaStateData data = new InventarElenaStateData(elena);

        bf.Serialize(stream, data);
        stream.Close();
    }

    public static void LoadInventarElena(InventoryElena elena)
    {
        if (File.Exists(Application.persistentDataPath + "/inventoryElena.inv"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/inventoryElena.inv", FileMode.Open);

            InventarElenaStateData data = bf.Deserialize(stream) as InventarElenaStateData;

            stream.Close();
            elena.isActive[0] = data.isActive[0];
            elena.isActive[1] = data.isActive[1];
            elena.isActive[2] = data.isActive[2];
            elena.isActive[3] = data.isActive[3];
            elena.isActive[4] = data.isActive[4];
            elena.isActive[5] = data.isActive[5];
            elena.isActive[6] = data.isActive[6];
            elena.isActive[7] = data.isActive[7];


            elena.spriteIndexElena[0] = data.spriteIndex[0];
            elena.spriteIndexElena[1] = data.spriteIndex[1];
           
        }
        else
        {
            Debug.LogError("InventoryElena-File does not exist");
            
        }
    }


}

[Serializable]
public class InventarReneStateData
{
    public bool[] isActive;
    public int[] spriteIndex;
    public bool tookIn2;

    public InventarReneStateData(InventoryRene rene)
    {
        tookIn2 = rene.tookNadelS2;

        isActive = new bool[8];
        spriteIndex = new int[8];

        isActive[0] = rene.isActive[0];
        isActive[1] = rene.isActive[1];
        isActive[2] = rene.isActive[2];
        isActive[3] = rene.isActive[3];
        isActive[4] = rene.isActive[4];
        isActive[5] = rene.isActive[5];
        isActive[6] = rene.isActive[6];
        isActive[7] = rene.isActive[7];

        spriteIndex[0] = rene.spriteIndexRene[0];
        spriteIndex[1] = rene.spriteIndexRene[1];
        spriteIndex[2] = rene.spriteIndexRene[2];
        spriteIndex[3] = rene.spriteIndexRene[3];
        spriteIndex[4] = rene.spriteIndexRene[4];
        spriteIndex[5] = rene.spriteIndexRene[5];
        spriteIndex[6] = rene.spriteIndexRene[6];
        spriteIndex[7] = rene.spriteIndexRene[7];
    }
}

[Serializable]
public class InventarElenaStateData
{
    public bool[] isActive;
    public int[] spriteIndex;

    public InventarElenaStateData(InventoryElena elena)
    {
        isActive = new bool[8];
        spriteIndex = new int[2];

        isActive[0] = elena.isActive[0];
        isActive[1] = elena.isActive[1];
        isActive[2] = elena.isActive[2];
        isActive[3] = elena.isActive[3];
        isActive[4] = elena.isActive[4];
        isActive[5] = elena.isActive[5];
        isActive[6] = elena.isActive[6];
        isActive[7] = elena.isActive[7];


        spriteIndex[0] = elena.spriteIndexElena[0];
        spriteIndex[1] = elena.spriteIndexElena[1];
        

    }
}

[Serializable]
public class GameStateData
{
    public int levelIndex;

    public GameStateData()
    {
        levelIndex = SceneManager.GetActiveScene().buildIndex;
    }
}

[Serializable]
public class SettingsData
{
    public bool saving;
    public int resolutionIndex;
    public bool fullscreenToggle;
    public float volumeMusic;
    public float volumeMaster;
    public float volumeVoice;
    public bool subtitle;
    public int language;

    public SettingsData(Einstellungen einstellungen)
    {
        saving = einstellungen.saving;
        resolutionIndex = einstellungen.currentResolutionIndex;
        fullscreenToggle = einstellungen.currentFullscreen;
        volumeMusic = einstellungen.currentVolumeMusic;
        volumeMaster = einstellungen.currentVolumeMaster;
        volumeVoice = einstellungen.currentVolumeVoice;
        subtitle = einstellungen.currentSubtitle;
        language = einstellungen.currentLanguage;
    }
}

    

    