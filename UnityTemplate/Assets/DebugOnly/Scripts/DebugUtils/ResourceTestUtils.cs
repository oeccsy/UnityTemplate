using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

public class ResourceTestUtils : Singleton<ResourceTestUtils>
{
    public void CheckAddressableAssetLoadDone(string resourcePath)
    {
        var handle = Addressables.LoadAssetAsync<Sprite>(resourcePath);

        if (handle.IsDone)
        {
            Debug.Log($"load Done : {resourcePath}");
        }
        else
        {
            Debug.LogError($"load Fail : {resourcePath}");
        }
    }
    public void LoadImage(string path, Image target)
    {
        Addressables.LoadAssetAsync<Sprite>($"{path}").Completed +=
            (AsyncOperationHandle<Sprite> handler) =>
            {
                target.sprite = handler.Result;
            };
    }
}