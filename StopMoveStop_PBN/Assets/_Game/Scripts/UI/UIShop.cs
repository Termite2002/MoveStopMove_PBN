using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIShop : UICanvas
{
    public GameObject buttonPrefab, buyButton, equipButton, unequipButton;
    public Transform content;
    public Text priceText;
    public Text coinText;

    [SerializeField] private List<Button> buttonsDisplay = new List<Button>();
    [SerializeField] private int currentDisplay;
    private int currentItemIndex;
    private int typeSkin;

    [SerializeField] private List<Image> iconButton = new List<Image>();
    //Pant
    [SerializeField] private List<Image> pantImage = new List<Image>();
    //Hat
    [SerializeField] private List<Image> hatImage = new List<Image>();
    //Shield 
    [SerializeField] private List<Image> shiledImage = new List<Image>();
    //Skin
    [SerializeField] private List<Image> skinImage = new List<Image>();

    private void OnEnable()
    {
        ChangeAnim(Constant.ANIM_OPEN_SHOP);
        coinText.text = SaveLoadController.Instance.gold.ToString();

        UIManager.Instance.player.ChangeAnim(Constant.ANIM_DANCE);
    }
    private void Start()
    {
        for (int i = 0; i < 20; i++)
        {
            Button button = Instantiate(buttonPrefab, content.position, content.rotation, content).GetComponent<Button>();
            buttonsDisplay.Add(button);
            button.gameObject.SetActive(false);
        }
        currentDisplay = 0;
        HatButton();
    }
    public void CloseButton()
    {
        ChangeAnim(Constant.ANIM_CLOSE_SHOP);
        Close(1f);

        if (SaveLoadController.Instance.currentSkin != -1)
        {
            SaveLoadController.Instance.currentHat = -1;
            SaveLoadController.Instance.currentPant = -1;
            SaveLoadController.Instance.currentShield = -1;

            WeaponShopController.Instance.ChooseSkin(SaveLoadController.Instance.currentSkin);
        }
        else
        {
            WeaponShopController.Instance.BackToNormalSkin();
            WeaponShopController.Instance.ResetPartOfSkin();
            if (SaveLoadController.Instance.currentHat != -1)
            {
                WeaponShopController.Instance.ChooseHatToWear(SaveLoadController.Instance.currentHat);
            }
            else
            {
                WeaponShopController.Instance.RefreshHatToDefault();
            }
            if (SaveLoadController.Instance.currentPant != -1)
            {
                WeaponShopController.Instance.ChoosePantToWear(SaveLoadController.Instance.currentPant);
            }
            else
            {
                WeaponShopController.Instance.RefreshSkinPantToDefault();
                WeaponShopController.Instance.pantSkin.material = WeaponShopController.Instance.pant;
            }
            if (SaveLoadController.Instance.currentShield != -1)
            {
                WeaponShopController.Instance.ChooseShieldToHold(SaveLoadController.Instance.currentShield);
            }
            else
            {
                WeaponShopController.Instance.RefreshShieldToDefault();
            }

        }

        UIManager.Instance.OpenUI<UIMainMenu>();
        CameraFollow.Instance.StartZoomOut();

        SaveLoadController.Instance.SaveData(UIManager.Instance.player);
        UIManager.Instance.player.ChangeAnim(Constant.ANIM_IDLE);
    }
    public void HatButton()
    {
        ModifyStatusOfHatItem(0);
        if (typeSkin == 3)
        {
            WeaponShopController.Instance.BackToNormalSkin();
        }
        ClearButtonItemHighlight();
        ClearButtonIconHighlight();
        typeSkin = 0;

        // icon dang mac thu
        buttonsDisplay[0].transform.GetChild(0).GetComponent<Image>().enabled = true;
        // doi mau icon mu, quan, khien, skin
        iconButton[0].color = Color.yellow;

        WeaponShopController.Instance.ResetPartOfSkin();
        WeaponShopController.Instance.ChooseHatToWear(0);
        DeleteListenerOnClick(currentDisplay);
        ChangeButtonDisplay(7);
        priceText.text = WeaponShopController.Instance.priceHat[0].ToString();
        for (int i = 0; i < 7; i++)
        {
            buttonsDisplay[i].GetComponentInChildren<Image>().sprite = hatImage[i].sprite;
        }
        for (int i = 0; i < 7; i++)
        {
            int index = i;
            buttonsDisplay[i].onClick.AddListener(() =>
            {
                ClearButtonItemHighlight();
                currentItemIndex = index;
                buttonsDisplay[index].transform.GetChild(0).GetComponent<Image>().enabled = true;
                WeaponShopController.Instance.ChooseHatToWear(index);

                ModifyStatusOfHatItem(index);
            });
        }
    }
    public void ModifyStatusOfHatItem(int index) 
    {
        if (SaveLoadController.Instance.hatOwner.Contains(index))
        {
            if (SaveLoadController.Instance.currentHat != index)
            {
                ShowEquipButton();
            }
            else
            {
                ShowUnequipButton();
            }
        }
        else
        {
            ShowBuyButton();
            priceText.text = WeaponShopController.Instance.priceHat[index].ToString();
        }
    }
    public void PantButton()
    {
        ModifyStatusOfPantItem(0);
        if (typeSkin == 3)
        {
            WeaponShopController.Instance.BackToNormalSkin();
        }
        ClearButtonItemHighlight();
        ClearButtonIconHighlight();
        typeSkin = 1;

        // icon dang mac thu
        buttonsDisplay[0].transform.GetChild(0).GetComponent<Image>().enabled = true;
        // doi mau icon mu, quan, khien, skin
        iconButton[1].color = Color.yellow;


        WeaponShopController.Instance.ResetPartOfSkin();
        WeaponShopController.Instance.ChoosePantToWear(0);
        DeleteListenerOnClick(currentDisplay);
        ChangeButtonDisplay(9);
        priceText.text = WeaponShopController.Instance.pricePant[0].ToString();
        for (int i = 0; i < 9; i++)
        {
            buttonsDisplay[i].GetComponentInChildren<Image>().sprite = pantImage[i].sprite;
        }
        for (int i = 0; i < 9; i++)
        {
            int index = i;
            buttonsDisplay[i].onClick.AddListener(() =>
            {
                ClearButtonItemHighlight();
                currentItemIndex = index;
                buttonsDisplay[index].transform.GetChild(0).GetComponent<Image>().enabled = true;
                WeaponShopController.Instance.ChoosePantToWear(index);

                ModifyStatusOfPantItem(index);
            });
        }
    }
    public void ModifyStatusOfPantItem(int index)
    {
        if (SaveLoadController.Instance.pantOwner.Contains(index))
        {
            if (SaveLoadController.Instance.currentPant != index)
            {
                ShowEquipButton();
            }
            else
            {
                ShowUnequipButton();
            }
        }
        else
        {
            ShowBuyButton();
            priceText.text = WeaponShopController.Instance.pricePant[index].ToString();
        }
    }
    public void MuscleButton()
    {
        ModifyStatusOfShieldItem(0);
        if (typeSkin == 3)
        {
            WeaponShopController.Instance.BackToNormalSkin();
        }
        ClearButtonItemHighlight();
        ClearButtonIconHighlight();
        typeSkin = 2;

        // icon dang mac thu
        buttonsDisplay[0].transform.GetChild(0).GetComponent<Image>().enabled = true;
        // doi mau icon mu, quan, khien, skin
        iconButton[2].color = Color.yellow;


        WeaponShopController.Instance.ResetPartOfSkin();
        WeaponShopController.Instance.ChooseShieldToHold(0);
        DeleteListenerOnClick(currentDisplay);
        ChangeButtonDisplay(2);
        priceText.text = WeaponShopController.Instance.priceShield[0].ToString();
        for (int i = 0; i < 2; i++)
        {
            buttonsDisplay[i].GetComponentInChildren<Image>().sprite = shiledImage[i].sprite;
        }
        for (int i = 0; i < 2; i++)
        {
            int index = i;
            buttonsDisplay[i].onClick.AddListener(() =>
            {
                ClearButtonItemHighlight();
                currentItemIndex = index;
                buttonsDisplay[index].transform.GetChild(0).GetComponent<Image>().enabled = true;
                WeaponShopController.Instance.ChooseShieldToHold(index);

                ModifyStatusOfShieldItem(index);
            });
        }
    }
    public void ModifyStatusOfShieldItem(int index)
    {
        if (SaveLoadController.Instance.shieldOwner.Contains(index))
        {
            if (SaveLoadController.Instance.currentShield != index)
            {
                ShowEquipButton();
            }
            else
            {
                ShowUnequipButton();
            }
        }
        else
        {
            ShowBuyButton();
            priceText.text = WeaponShopController.Instance.priceShield[index].ToString();
        }
    }
    public void SkinButton()
    {
        ModifyStatusOfSkinItem(0);
        ClearButtonItemHighlight();
        ClearButtonIconHighlight();
        typeSkin = 3;

        // icon dang mac thu
        buttonsDisplay[0].transform.GetChild(0).GetComponent<Image>().enabled = true;
        // doi mau icon mu, quan, khien, skin
        iconButton[3].color = Color.yellow;

        WeaponShopController.Instance.ChooseSkin(0);
        DeleteListenerOnClick(currentDisplay);
        ChangeButtonDisplay(5);
        priceText.text = WeaponShopController.Instance.priceSkin[0].ToString();
        for (int i = 0; i < 5; i++)
        {
            buttonsDisplay[i].GetComponentInChildren<Image>().sprite = skinImage[i].sprite;
        }
        for (int i = 0; i < 5; i++)
        {
            int index = i;
            buttonsDisplay[i].onClick.AddListener(() =>
            {
                ClearButtonItemHighlight();
                currentItemIndex = index;
                buttonsDisplay[index].transform.GetChild(0).GetComponent<Image>().enabled = true;
                WeaponShopController.Instance.ChooseSkin(index);

                ModifyStatusOfSkinItem(index);
            });
        }
    }
    public void ModifyStatusOfSkinItem(int index)
    {
        if (SaveLoadController.Instance.skinOwner.Contains(index))
        {
            if (SaveLoadController.Instance.currentSkin != index)
            {
                ShowEquipButton();
            }
            else
            {
                ShowUnequipButton();
            }
        }
        else
        {
            ShowBuyButton();
            priceText.text = WeaponShopController.Instance.priceSkin[index].ToString();
        }
    }

    public void ChangeButtonDisplay(int numItems) 
    {
        if (currentDisplay == 0)
        {

            for (int i = 0; i < numItems; i++)
            {
                buttonsDisplay[i].gameObject.SetActive(true);
                currentDisplay++;
            }
        }
        else
        {
            if (currentDisplay < numItems)
            {
                for (int i = currentDisplay; i < numItems; i++)
                {
                    buttonsDisplay[i].gameObject.SetActive(true);
                    currentDisplay++;
                }
            }
            else if (currentDisplay > numItems)
            {
                for (int i = currentDisplay - 1; i > numItems - 1; i--)
                {
                    buttonsDisplay[i].gameObject.SetActive(false);
                    currentDisplay--;
                }
            }
        }
    }
    public void DeleteListenerOnClick(int numButtons)
    {
        for (int i = 0; i < numButtons; i++)
        {
            buttonsDisplay[i].onClick.RemoveAllListeners();
        }
    }
    public void ClearButtonIconHighlight()
    {
        for (int i = 0; i < iconButton.Count; i++)
        {
            iconButton[i].color = Color.white;
        }
    }
    public void ClearButtonItemHighlight()
    {
        for (int i = 0; i < 20; i++)
        {
            buttonsDisplay[i].transform.GetChild(0).GetComponent<Image>().enabled = false;
        }
    }

    public void BuyButton()
    {
        switch (typeSkin)
        {
            case 0:
                if (SaveLoadController.Instance.gold >= WeaponShopController.Instance.priceHat[currentItemIndex])
                {
                    SaveLoadController.Instance.gold -= WeaponShopController.Instance.priceHat[currentItemIndex];
                    SaveLoadController.Instance.hatOwner.Add(currentItemIndex);
                    ShowEquipButton();
                    SaveLoadController.Instance.SaveData(UIManager.Instance.player);
                }
                break;
            case 1:
                if (SaveLoadController.Instance.gold >= WeaponShopController.Instance.pricePant[currentItemIndex])
                {
                    SaveLoadController.Instance.gold -= WeaponShopController.Instance.pricePant[currentItemIndex];
                    SaveLoadController.Instance.pantOwner.Add(currentItemIndex);
                    ShowEquipButton();
                    SaveLoadController.Instance.SaveData(UIManager.Instance.player);
                }
                break;
            case 2:
                if (SaveLoadController.Instance.gold >= WeaponShopController.Instance.priceShield[currentItemIndex])
                {
                    SaveLoadController.Instance.gold -= WeaponShopController.Instance.priceShield[currentItemIndex];
                    SaveLoadController.Instance.shieldOwner.Add(currentItemIndex);
                    ShowEquipButton();
                    SaveLoadController.Instance.SaveData(UIManager.Instance.player);
                }
                break;
            case 3:
                if (SaveLoadController.Instance.gold >= WeaponShopController.Instance.priceSkin[currentItemIndex])
                {
                    SaveLoadController.Instance.gold -= WeaponShopController.Instance.priceSkin[currentItemIndex];
                    SaveLoadController.Instance.skinOwner.Add(currentItemIndex);
                    ShowEquipButton();
                    SaveLoadController.Instance.SaveData(UIManager.Instance.player);
                }
                break;
            default:
                break;
        }
    }
    public void EquipButton()
    {
        switch (typeSkin)
        {
            case 0:
                SaveLoadController.Instance.currentHat = currentItemIndex;
                ShowUnequipButton();
                SaveLoadController.Instance.currentSkin = -1;
                SaveLoadController.Instance.SaveData(UIManager.Instance.player);
                break;
            case 1:
                SaveLoadController.Instance.currentPant = currentItemIndex;
                ShowUnequipButton();
                SaveLoadController.Instance.currentSkin = -1;
                SaveLoadController.Instance.SaveData(UIManager.Instance.player);
                break;
            case 2:
                SaveLoadController.Instance.currentShield = currentItemIndex;
                ShowUnequipButton();
                SaveLoadController.Instance.currentSkin = -1;
                SaveLoadController.Instance.SaveData(UIManager.Instance.player);
                break;
            case 3:
                SaveLoadController.Instance.currentSkin = currentItemIndex;
                ShowUnequipButton();
                SaveLoadController.Instance.SaveData(UIManager.Instance.player);
                break;
            default:
                break;
        }
    }
    public void UnequipButton()
    {
        switch (typeSkin)
        {
            case 0:
                SaveLoadController.Instance.currentHat = -1;
                ShowEquipButton();
                SaveLoadController.Instance.SaveData(UIManager.Instance.player);
                break;
            case 1:
                SaveLoadController.Instance.currentPant = -1;
                ShowEquipButton();
                SaveLoadController.Instance.SaveData(UIManager.Instance.player);
                break;
            case 2:
                SaveLoadController.Instance.currentShield = -1;
                ShowEquipButton();
                SaveLoadController.Instance.SaveData(UIManager.Instance.player);
                break;
            case 3:
                SaveLoadController.Instance.currentSkin = -1;
                ShowEquipButton();
                SaveLoadController.Instance.SaveData(UIManager.Instance.player);
                break;
            default:
                break;
        }
    }
    public void ShowBuyButton()
    {
        buyButton.SetActive(true);
        equipButton.SetActive(false);
        unequipButton.SetActive(false);
    }
    public void ShowEquipButton()
    {
        buyButton.SetActive(false);
        equipButton.SetActive(true);
        unequipButton.SetActive(false);
    }
    public void ShowUnequipButton()
    {
        buyButton.SetActive(false);
        equipButton.SetActive(false);
        unequipButton.SetActive(true);
    }
}
