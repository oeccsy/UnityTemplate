using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusbarSetterButton : MonoBehaviour
{
    private enum ButtonType
    {
        ShowButton,
        HideButton
    }

    [SerializeField]
    private ButtonType _buttonType = ButtonType.HideButton;
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();

        ButtonInit();
        
    }

    private void ButtonInit()
    {
        if (_buttonType == ButtonType.HideButton)
        {
            _button.onClick.AddListener(() => HideNavigationbar() );
            _button.onClick.AddListener(() => HideStatusbar() ); 
            
        }
        else
        {
            _button.onClick.AddListener(() => ShowStatusbar() ); 
            _button.onClick.AddListener(() => ShowNavigationbar() );
        }
    }

    private void ShowStatusbar()
    {
        Debug.Log("show");
        ApplicationChrome.statusBarState = ApplicationChrome.States.Visible;
    }

    private void HideStatusbar()
    {
        Debug.Log("hide");
        ApplicationChrome.statusBarState = ApplicationChrome.States.Hidden;   
    }

    private void ShowNavigationbar()
    {
        ApplicationChrome.navigationBarState = ApplicationChrome.States.Visible;
    }
    
    private void HideNavigationbar()
    {
        ApplicationChrome.navigationBarState = ApplicationChrome.States.Hidden;
    }
}
