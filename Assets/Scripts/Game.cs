using System.Collections;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private LevelInfoPanel levelInfoPanel;
    [SerializeField] private CompleteLines completeLines;
    [SerializeField] private BoardGrid boardGrid;
    [SerializeField] private FigureSpawner figureSpawner;
    private static Game instance;
    private LevelsParameters levelsParameters;
    private Tutorial tutorial;
    private LevelsFileLoader levelsFileLoader;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Input.multiTouchEnabled = false;
        DataStorage.LoadDataStorage();
        levelsFileLoader = GetComponent<LevelsFileLoader>();
        StartCoroutine(StartGame());
    }

    public static Game GetInstance()
    {
        return instance;
    }

    public GridCell GetFigureSpawnCell(int figureIndex)
    {
        FigureSpawnInfo figureSpawnInfo = levelsParameters.CurrentFigures[figureIndex];
        return boardGrid.GetGridCell(figureSpawnInfo.PositionX, figureSpawnInfo.PositionY);
    }

    public void SetLevelsParameters(LevelsParameters levels)
    {
        levelsParameters = levels;
    }

    public void ChangeGameMode()
    {
        StartCoroutine(ChangeMode());
    }

    public void ShowLevelAfterTutorial()
    {
        levelInfoPanel.SetLevelHeaderText("Level");
        levelInfoPanel.UpdateLevelValue(DataStorage.GetCurrentLevel());
        figureSpawner.SpawnFigures(levelsParameters.CurrentFigures, DataStorage.CountOfFigures);
        levelInfoPanel.ShowRestOfButtons();
    }

    public void NextLevel()
    {
        if (!DataStorage.IsTutorialCompleted)
        {
            tutorial.NextLevel();
        }
        else
        {
            levelInfoPanel.SetPanelEnabled(false);
            DataStorage.LevelCompleted();
            completeLines.ShowLines();
            StartCoroutine(ShowNextLevel(1));
        }
    }

    public void RestartLevel()
    {
        boardGrid.FillBoard(MarkType.Cross);
        if (!DataStorage.IsTutorialCompleted)
        {
            tutorial.RestartLevel();
        }
        else
        {
            figureSpawner.RespawnFigures(levelsParameters.CurrentFigures);
        }
    }

    private IEnumerator StartGame()
    {
        Logo logo = FindObjectOfType<Logo>();
        yield return StartCoroutine(levelsFileLoader.LoadLevelFile(DataStorage.CurrentGameMode));
        yield return StartCoroutine(logo.ShowLogo());
        Destroy(logo.gameObject);

        boardGrid.SpawnField();
        figureSpawner.InitializeFigureSpawner();
        if (!DataStorage.IsTutorialCompleted)
        {
            tutorial = levelInfoPanel.gameObject.AddComponent<Tutorial>();
            tutorial.StartTutorial(this, levelInfoPanel, figureSpawner, boardGrid);
        }
        else
        {
            figureSpawner.SpawnFigures(levelsParameters.CurrentFigures, DataStorage.CountOfFigures);
            boardGrid.ShowMarks(true);
            levelInfoPanel.UpdateLevelValue(DataStorage.GetCurrentLevel());
            levelInfoPanel.ShowPanelButtons();
        }
    }

    private IEnumerator ShowNextLevel(float delay)
    {
        yield return new WaitForSeconds(delay);
        AdManager.GetInstance().UpdateDelay();
        boardGrid.FillBoard(MarkType.Cross);
        levelsParameters.FillCurrentFigures();
        levelInfoPanel.UpdateLevelValue(DataStorage.GetCurrentLevel());
        figureSpawner.SpawnFigures(levelsParameters.CurrentFigures, DataStorage.CountOfFigures);
        levelInfoPanel.SetPanelEnabled(true);
        completeLines.HideLines();
    }

    private IEnumerator ChangeMode()
    {
        yield return StartCoroutine(levelsFileLoader.LoadLevelFile(DataStorage.CurrentGameMode));
        if (boardGrid.GetFieldSize().x != DataStorage.FieldSize.x)
        {
            boardGrid.RespawnField();
        }
        completeLines.InitializeCompleteLines();
        boardGrid.UpdateFieldScale();
        boardGrid.FillBoard(MarkType.Cross);
        figureSpawner.DestroyFigures();
        figureSpawner.SpawnFigures(levelsParameters.CurrentFigures, DataStorage.CountOfFigures);
        boardGrid.ShowMarks(false);
        levelInfoPanel.UpdateLevelValue(DataStorage.GetCurrentLevel());
    }
}