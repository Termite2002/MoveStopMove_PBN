using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGameplay : UICanvas
{
    public Text aliveText;
    public GameObject instruction;

    private void OnEnable()
    {
        instruction.SetActive(true);
    }
    public void SettingButton()
    {
        UIManager.Instance.OpenUI<UISetting>();
    }
    public void ChangeAlive(int numAlive)
    {
        aliveText.text = "Alive:" + numAlive.ToString();
    }
    private void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            instruction.SetActive(false);
        }
    }
}
