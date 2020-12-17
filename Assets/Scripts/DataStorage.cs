using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class DataStorage : MonoBehaviour
{
    public static int[] CurrentLevel { get; set; } = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
    public static int[] MaxLevel { get; set; } = { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 };
    public static bool IsTutorialCompleted { get; set; } = false;
    public static bool IsSoundOn { get; set; } = true;
    public static int HintsCount { get; set; } = 10;
    public static int CurrentGameMode { get; set; } = 0;
    public static Vector2Int FieldSize { get; set; } = new Vector2Int(3, 3);
    public static int CountOfFigures { get; set; } = 3;

    public static void UpdateHintCount(int hints)
    {
        HintsCount = hints;
        SaveDataStorage();
    }

    public static void UseHint()
    {
        HintsCount--;
        SaveDataStorage();
    }

    public static int GetCurrentLevel()
    {
        return CurrentLevel[CurrentGameMode];
    }

    public static int GetCurrentMaxLevel()
    {
        return MaxLevel[CurrentGameMode];
    }

    public static bool IsNextLevel()
    {
        if (CurrentLevel[CurrentGameMode] > MaxLevel[CurrentGameMode])
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public static void LevelCompleted()
    {
        if (!IsNextLevel())
        {
            return;
        }
        CurrentLevel[CurrentGameMode]++;
        SaveDataStorage();
    }

    public static void SwitchSound()
    {
        IsSoundOn = !IsSoundOn;
        SaveDataStorage();
    }

    public static void CompleteTutorial()
    {
        IsTutorialCompleted = true;
        SaveDataStorage();
    }

    public static void SaveDataStorage()
    {
        DataStorageSer data = new DataStorageSer();
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/dscbc.bin";
        FileStream fileStream = new FileStream(path, FileMode.Create);
        formatter.Serialize(fileStream, data);
        fileStream.Close();
    }

    public static void LoadDataStorage()
    {
        string path = Application.persistentDataPath + "/dscbc.bin";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream fileStream = new FileStream(path, FileMode.Open);
            DataStorageSer data = formatter.Deserialize(fileStream) as DataStorageSer;
            fileStream.Close();
            IsTutorialCompleted = data.IsTutorialCompletedSer;
            CurrentLevel = data.CurrentLevelSer;
            IsSoundOn = data.IsSoundOnSer;
            HintsCount = data.HintsCountSer;
        }
    }
}

[System.Serializable]
class DataStorageSer
{
    public int[] CurrentLevelSer;
    public bool IsTutorialCompletedSer;
    public bool IsSoundOnSer;
    public int HintsCountSer;

    public DataStorageSer()
    {
        CurrentLevelSer = DataStorage.CurrentLevel;
        IsTutorialCompletedSer = DataStorage.IsTutorialCompleted;
        IsSoundOnSer = DataStorage.IsSoundOn;
        HintsCountSer = DataStorage.HintsCount;
    }
}
