using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIShop : UICanvas
{
    public GameObject buttonPrefab;
    public Transform content;
    public void CloseButton()
    {
        UIManager.Instance.OpenUI<UIMainMenu>();
        CloseDirectly();

        CameraFollow.Instance.StartZoomOut();

        if (UIManager.Instance.IsOpened<UIHat>())
        {
            UIManager.Instance.CloseUI<UIHat>();
        }
        if (UIManager.Instance.IsOpened<UIPant>())
        {
            UIManager.Instance.CloseUI<UIPant>();
        }
        if (UIManager.Instance.IsOpened<UIMuscle>())
        {
            UIManager.Instance.CloseUI<UIMuscle>();
        }
        if (UIManager.Instance.IsOpened<UISkin>())
        {
            UIManager.Instance.CloseUI<UISkin>();
        }
    }
    public void HatButton()
    {
        for (int i = 0; i < 8; i++)
        {
            Instantiate(buttonPrefab, content.position, content.rotation,content);
        }
        if(UIManager.Instance.IsOpened<UIPant>())
        {
            UIManager.Instance.CloseUI<UIPant>();
        }
        if (UIManager.Instance.IsOpened<UIMuscle>())
        {
            UIManager.Instance.CloseUI<UIMuscle>();
        }
        if (UIManager.Instance.IsOpened<UISkin>())
        {
            UIManager.Instance.CloseUI<UISkin>();
        }


        UIManager.Instance.OpenUI<UIHat>();
    }
    public void PantButton()
    {
        if (UIManager.Instance.IsOpened<UIHat>())
        {
            UIManager.Instance.CloseUI<UIHat>();
        }
        if (UIManager.Instance.IsOpened<UIMuscle>())
        {
            UIManager.Instance.CloseUI<UIMuscle>();
        }
        if (UIManager.Instance.IsOpened<UISkin>())
        {
            UIManager.Instance.CloseUI<UISkin>();
        }


        UIManager.Instance.OpenUI<UIPant>();
    }
    public void MuscleButton()
    {
        if (UIManager.Instance.IsOpened<UIPant>())
        {
            UIManager.Instance.CloseUI<UIPant>();
        }
        if (UIManager.Instance.IsOpened<UIHat>())
        {
            UIManager.Instance.CloseUI<UIHat>();
        }
        if (UIManager.Instance.IsOpened<UISkin>())
        {
            UIManager.Instance.CloseUI<UISkin>();
        }

        UIManager.Instance.OpenUI<UIMuscle>();
    }
    public void SkinButton()
    {
        if (UIManager.Instance.IsOpened<UIPant>())
        {
            UIManager.Instance.CloseUI<UIPant>();
        }
        if (UIManager.Instance.IsOpened<UIHat>())
        {
            UIManager.Instance.CloseUI<UIHat>();
        }
        if (UIManager.Instance.IsOpened<UIMuscle>())
        {
            UIManager.Instance.CloseUI<UIMuscle>();
        }

        UIManager.Instance.OpenUI<UISkin>();
    }
}
