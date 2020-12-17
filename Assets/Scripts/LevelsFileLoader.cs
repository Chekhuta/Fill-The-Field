using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class LevelsFileLoader : MonoBehaviour
{
    private readonly AssetReference[] _levelsFileReference = new AssetReference[] {
        new AssetReference("Assets/Levels/l3f3.txt"),
        new AssetReference("Assets/Levels/l3f4.txt"),
        new AssetReference("Assets/Levels/l3f5.txt"),
        new AssetReference("Assets/Levels/l3f6.txt"),
        new AssetReference("Assets/Levels/l4f3.txt"),
        new AssetReference("Assets/Levels/l4f4.txt"),
        new AssetReference("Assets/Levels/l4f5.txt"),
        new AssetReference("Assets/Levels/l4f6.txt"),
        new AssetReference("Assets/Levels/l5f3.txt"),
        new AssetReference("Assets/Levels/l5f4.txt"),
        new AssetReference("Assets/Levels/l5f5.txt"),
        new AssetReference("Assets/Levels/l5f6.txt")
    };
    private AsyncOperationHandle _currentLevelsFileOperationHandle;

    public IEnumerator LoadLevelFile(int fileIndex)
    {
        if (_currentLevelsFileOperationHandle.IsValid())
        {
            Addressables.Release(_currentLevelsFileOperationHandle);
        }
        _currentLevelsFileOperationHandle = _levelsFileReference[fileIndex].LoadAssetAsync<TextAsset>();
        yield return _currentLevelsFileOperationHandle;
        GetComponent<Game>().SetLevelsParameters(new LevelsParameters(((TextAsset)_currentLevelsFileOperationHandle.Result).text.Split('\n')));
    }
}
