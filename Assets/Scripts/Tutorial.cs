using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class Tutorial : MonoBehaviour
{
    private int tutorialLevel = 1;
    private Game game;
    private LevelInfoPanel levelInfoPanel;
    private FigureSpawner figureSpawner;
    private BoardGrid boardGrid;

    public void StartTutorial(Game game, LevelInfoPanel levelInfoPanel, FigureSpawner figureSpawner, BoardGrid boardGrid)
    {
        this.game = game;
        this.levelInfoPanel = levelInfoPanel;
        this.figureSpawner = figureSpawner;
        this.boardGrid = boardGrid;
        levelInfoPanel.SetLevelHeaderText("Training");
        levelInfoPanel.UpdateLevelValue(tutorialLevel);
        figureSpawner.SpawnFigures(GetFiguresSpawnInfo(), GetCountOfFigures());
        boardGrid.ShowMarks(false);
        levelInfoPanel.ShowTutorialButtons();
        Addressables.LoadAssetAsync<GameObject>("Assets/Prefabs/Rules Window.prefab").Completed += OnResultWindowInstantiate;
    }

    private void OnResultWindowInstantiate(UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<GameObject> rulesWindow)
    {
        Instantiate(rulesWindow.Result, GetComponent<Transform>()).GetComponent<RulesWindow>().OpenWindow();
    }

    public void NextLevel()
    {
        tutorialLevel++;
        if (tutorialLevel > 2)
        {
            StartCoroutine(CompleteTutorial());
        }
        else
        {
            StartCoroutine(ShowNextTrainingLevel(GetFiguresSpawnInfo(), tutorialLevel, GetCountOfFigures()));
        }
    }

    private IEnumerator CompleteTutorial()
    {
        DataStorage.CompleteTutorial();
        yield return new WaitForSeconds(1);
        game.ShowLevelAfterTutorial();
        Destroy(this);
    }

    private IEnumerator ShowNextTrainingLevel(FigureSpawnInfo[] figureSpawnInfo, int level, int countOfFigures)
    {
        yield return new WaitForSeconds(1);
        boardGrid.FillBoard(MarkType.Cross);
        figureSpawner.SpawnFigures(figureSpawnInfo, countOfFigures);
        levelInfoPanel.UpdateLevelValue(level);
    }

    public void RestartLevel()
    {
        figureSpawner.RespawnFigures(GetFiguresSpawnInfo());
    }

    public int GetCountOfFigures()
    {
        switch (tutorialLevel)
        {
            case 1:
                return 1;
            case 2:
                return 2;
            default:
                return 3;
        }
    }

    public FigureSpawnInfo[] GetFiguresSpawnInfo()
    {
        switch (tutorialLevel)
        {
            case 1:
                return new FigureSpawnInfo[] { new FigureSpawnInfo(2, 0, 1, 1) };
            case 2:
                return new FigureSpawnInfo[] { new FigureSpawnInfo(3, 1, 1, 0), new FigureSpawnInfo(1, 0, 1, 1) };
            default:
                return null;
        }
    }
}
