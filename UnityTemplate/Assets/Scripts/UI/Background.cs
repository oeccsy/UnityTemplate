using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Background : MonoBehaviour
{
    [SerializeField]
    private string resourcePath;
    [SerializeField]
    private Image backgroundImage;
    
    public void InitBackground(string path)
    {
        backgroundImage = GetComponent<Image>();

        Sprite backgroundSprite = ResourceManager.Instance.FindAsset<Sprite>(path);
        backgroundImage.sprite = backgroundSprite;
    }
    
    public void InitBackground()
    {
        InitBackground(resourcePath);
    }
}
