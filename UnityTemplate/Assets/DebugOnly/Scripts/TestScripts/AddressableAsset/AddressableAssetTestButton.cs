using UnityEngine;
using UnityEngine.UI;

    public class AddressableAssetTestButton : MonoBehaviour
    {
        [SerializeField]
        private Image targetImage;

        [SerializeField]
        private Button button;
        
        private void Awake()
        {
            //Init
            button = GetComponent<Button>();
            button.onClick.AddListener(() => Test() ); 
        }

        private void Test()
        {
            ResourceTestUtils.Instance.LoadImage("Assets/Resources_Addressable/Common/InfoText/10_ingame_right.png", targetImage);
        }
    }