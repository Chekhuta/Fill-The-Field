using UnityEngine;

public class LevelsParameters
{
    private string[] levels;
    public FigureSpawnInfo[] CurrentFigures { get; private set; }

    public LevelsParameters(string[] l)
    {
        levels = l;
        FillCurrentFigures();
    }

    public void FillCurrentFigures()
    {
        CurrentFigures = new FigureSpawnInfo[DataStorage.CountOfFigures];
        int level = 0;
        if (DataStorage.IsNextLevel())
        {
            level = DataStorage.GetCurrentLevel() - 1;
        }
        else
        {
            level = Random.Range(0, DataStorage.GetCurrentMaxLevel());
        }
        string[] figures = levels[level].Split('f');
        for (int i = 0; i < DataStorage.CountOfFigures; i++)
        {
            string[] parameters = figures[i].Split(',');
            int spawnCell = int.Parse(parameters[2]);
            int x = spawnCell / DataStorage.FieldSize.x;
            int y = spawnCell % DataStorage.FieldSize.x;
            CurrentFigures[i] = new FigureSpawnInfo(int.Parse(parameters[0]), int.Parse(parameters[1]), x, y);
        }
    }
}

[System.Serializable]
public class FigureSpawnInfo
{
    public int FigureId { get; }
    public int AngleId { get; }
    public int PositionX { get; }
    public int PositionY { get; }

    public FigureSpawnInfo(int f, int a, int x, int y)
    {
        FigureId = f;
        AngleId = a;
        PositionX = x;
        PositionY = y;
    }
}