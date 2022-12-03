using UnityEngine;
public class AddressableAssetKeyCodeTest : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            ResourceTestUtils.Instance.CheckAddressableAssetLoadDone(
                "Assets/Resources_Addressable/GameType_04/Background/04_working manny.png");
            Debug.Log("Press A Done");   
        }
        
        if (Input.GetKeyDown(KeyCode.S))
        {
            ResourceTestUtils.Instance.CheckAddressableAssetLoadDone(
                "Assets/Resources_Addressable/GameType_10/Effect/10_ufo light.png");

            Debug.Log("Press S Done");
        }
    }
}