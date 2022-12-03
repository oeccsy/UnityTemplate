using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    private int _order = -20; // 생성되는 UI의 depth를 결정하는 변수

    private Stack<GameObject> _popupStack = new Stack<GameObject>();
    private Dictionary<int, GameObject> _sceneUIDict = new Dictionary<int, GameObject>();

    [Header("Parent Transforms")] private Transform _backgroundCanvas;
    private Transform _sceneUIRoot;
    private Transform _popupRoot;

    [Header("UI Component")] private Camera _mainCamera;
    private Background _background;

    #region Parent Transforms
    public Transform BackgroundCanvas
    {
        get
        {
            if (_backgroundCanvas == null)
                _backgroundCanvas = GameObject.Find("BackgroundCanvas").transform;

            return _backgroundCanvas;
        }
    }

    public Transform SceneUIRoot
    {
        get
        {
            if (_sceneUIRoot == null)
                _sceneUIRoot = GameObject.Find("SceneUIRoot").transform;

            return _sceneUIRoot;
        }
    }

    public Transform PopupRoot
    {
        get
        {
            if (_popupRoot == null)
                _popupRoot = GameObject.Find("PopupRoot").transform;

            return _popupRoot;
        }
    }
    #endregion
    
    #region UI Component
    public Camera MainCamera
    {
        get
        {
            if (_mainCamera == null)
                _mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();

            return _mainCamera;
        }
    }

    public Background Background
    {
        get
        {
            if (_background == null)
                _background = BackgroundCanvas.GetComponent<Background>();

            return _background;
        }
    }
    #endregion

    #region Popup
    public GameObject ShowPopup<T>(string resourcePath, bool useSort = true) where T : class
    {
        // Instantiate
        GameObject popupPrefab;
        GameObject popupInstance;
        
        popupPrefab = ResourceManager.Instance.FindAsset<GameObject>(resourcePath);
        popupInstance = Object.Instantiate(popupPrefab, PopupRoot);
        popupInstance.SetActive(true);

        _popupStack.Push(popupInstance);

        // Set Canvas Order
        SetCanvas(popupInstance);

        if (useSort)
            SetNextOrder(popupInstance);
        else
            SetOrderZero(popupInstance);

        return popupInstance;
    }
    
    public void HideTopPopup()
    {
        if (_popupStack.Count == 0)
            return;

        GameObject topObj = _popupStack.Pop();
        Object.Destroy(topObj);
    }
    
    //
    // public void HidePopup<T>(string name = null) where T : MonoBehaviour
    // {
    //     var targetObj = _popupStack.Where(obj => obj.name == typeof(T).Name);
    //     
    //     Destroy(targetObj.GetEnumerator().Current);
    // }
    //
    #endregion
    
    #region SceneUI
    public GameObject ShowSceneUI<T>(string resourcePath, int key, bool useSort = true) where T : class
    {
        // Instantiate
        GameObject sceneUIInstance;
        
        sceneUIInstance = ResourceManager.Instance.InstantiateObject(resourcePath, SceneUIRoot);
        
        // Store
        _sceneUIDict.Add(key, sceneUIInstance);
        
        // Set Canvas Order
        SetCanvas(sceneUIInstance);

        if (useSort)
            SetNextOrder(sceneUIInstance);
        else
            SetOrderZero(sceneUIInstance);

        return sceneUIInstance;
    }
    #endregion

    #region Canvas Settings
    public void SetCanvas(GameObject targetObj)
    {
        Canvas canvas = targetObj.GetComponent<Canvas>();
        if (canvas == null)
        {
            Debug.LogWarning($"{canvas.gameObject.name} has no canvas");
            canvas = targetObj.AddComponent<Canvas>();
        }
        
        canvas.renderMode = RenderMode.ScreenSpaceCamera;
        canvas.worldCamera = MainCamera;
        canvas.overrideSorting = true;
    }

    public void SetNextOrder(GameObject targetObj)
    {
        Canvas targetCanvas = targetObj.GetComponent<Canvas>();

        targetCanvas.sortingOrder = _order;
        _order++;
    }

    public void SetOrderZero(GameObject targetObj)
    {
        Canvas targetCanvas = targetObj.GetComponent<Canvas>();

        targetCanvas.sortingOrder = 0;
    }
    
    #endregion
    
}