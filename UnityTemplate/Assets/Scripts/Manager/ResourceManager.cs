using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class ResourceManager : Singleton<ResourceManager>
{
    /// <summary>
    /// Resource의 관리 로직을 Load, Find, Instantiate, Destroy으로 구분하여 Wrapping 하는 클래스
    /// </summary>

    public ResourceManager() {}
    
    /// <summary>
    /// Load Asset (Addressable Only) 
    /// </summary>
    
    public IEnumerator LoadAsset<T>(string resourcePath, System.Action successAction = null, System.Action failAction = null) where T : class
    {
        var handle = Addressables.LoadAssetAsync<T>(resourcePath);
        yield return handle;

        if (handle.IsDone)
        {
            Debug.Log($"load Done : {resourcePath}");
            if (successAction != null) successAction();
        }
        else
        {
            Debug.LogError($"load Fail : {resourcePath}");
            if (failAction != null) failAction();
        }
    }

    /// <summary>
    /// Find Asset
    /// string에 해당하는 Asset이 있는지 확인하여 존재하는 경우 해당 Prefab을 Return
    /// 존재하지 않는 경우 null return
    /// </summary>

    public T FindAsset<T>(string resourcePath) where T : Object
    {
        // Resources 경로에서 탐색
        T target = Resources.Load<T>($"{resourcePath}");
        
        // Addressable 경로에서 탐색
        if (target == null)
        {
            var handle = Addressables.LoadAssetAsync<T>(resourcePath);
            if (handle.IsDone)
            {
                target = handle.Result;
            }
        }
        
        // 예외
        if (target == null)
        {
            Debug.Log($"Failed to load prefab : {resourcePath}");
            return null;
        }

        return target;
    }

    /// <summary>
    /// Built-in Resource인지 Addressable Resource인지 판별 후 Instantiate
    /// 추후 로직 확장성을 위해 Wrapping
    /// </summary>

    public GameObject InstantiateObject(string resourcePath, Transform parent = null)
    {
        GameObject prefab = Resources.Load<GameObject>($"{resourcePath}");
        GameObject instance;
        
        if (prefab != null)
        {
            instance = Object.Instantiate(prefab, parent);
            return instance;
        }
        
        var handle = Addressables.LoadAssetAsync<GameObject>(resourcePath);
        if (handle.IsDone)
        {
            instance = Addressables.InstantiateAsync(resourcePath, parent).Result;
            return instance;
        }
        
        // 예외
        Debug.Log($"Failed to instantiate : {resourcePath}");
        return null;
    }

    public GameObject InstantiateObject(GameObject prefab, Transform parent = null)
    {
        GameObject instance = Object.Instantiate(prefab, parent);
        return instance;
    }
    
    /// <summary>
    /// Destroy
    /// 추후 로직 확장성을 위해 Wrapping
    /// </summary>
    
    private void DestroyObject(GameObject targetObject)
    {
        if (targetObject == null) return;
        
        Object.Destroy(targetObject);
    }
}
