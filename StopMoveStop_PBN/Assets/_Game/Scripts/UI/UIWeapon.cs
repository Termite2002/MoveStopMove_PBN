using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIWeapon : UICanvas
{
    private Player player;


    public Text weaponName;
    public Text weaponPrice;
    public Text coinText;

    public GameObject ownedImage, equipButton, unequipButton, buyButton;

    private void OnEnable()
    {
        ChangeAnim("OpenWeapon");
        coinText.text = SaveLoadController.Instance.gold.ToString();
    }
    private void Start()
    {
        player = FindObjectOfType<Player>();
        CheckIfWeaponOwned();
        weaponName.text = WeaponShopController.Instance.namesWeaponDisplay[0];
        weaponPrice.text = WeaponShopController.Instance.pricesWeaponDisplay[0].ToString();
    }
    public void CloseButton()
    {
        ChangeAnim("CloseWeapon");
        UIManager.Instance.OpenUI<UIMainMenu>();
        Close(0.5f);
        WeaponShopController.Instance.ResetWhenClickCloseButton();

        ChangeNameAndPriceOfWeaponDisplay();
    }
    public void NextRightButton()
    {
        WeaponShopController.Instance.NextRightButton();
        CheckIfWeaponOwned();
        ChangeNameAndPriceOfWeaponDisplay();
    }
    public void NextLeftButton()
    {
        WeaponShopController.Instance.NextLeftButton();
        CheckIfWeaponOwned();
        ChangeNameAndPriceOfWeaponDisplay();
    }
    public void ChangeNameAndPriceOfWeaponDisplay()
    {
        weaponName.text = WeaponShopController.Instance.namesWeaponDisplay[WeaponShopController.Instance.currentWeaponDisplay];
        weaponPrice.text = WeaponShopController.Instance.pricesWeaponDisplay[WeaponShopController.Instance.currentWeaponDisplay].ToString();
    }
    public void BuyButton()
    {
        if (SaveLoadController.Instance.gold >= WeaponShopController.Instance.pricesWeaponDisplay[WeaponShopController.Instance.currentWeaponDisplay])
        {
            SaveLoadController.Instance.gold -= WeaponShopController.Instance.pricesWeaponDisplay[WeaponShopController.Instance.currentWeaponDisplay];
            coinText.text = SaveLoadController.Instance.gold.ToString();
            ownedImage.SetActive(true);
            buyButton.SetActive(false);

            SaveLoadController.Instance.weaponOwner.Add(WeaponShopController.Instance.currentWeaponDisplay);

            // save
            SaveLoadController.Instance.SaveData(player);
        }
    }
    public void EquipButton()
    {
        if (SaveLoadController.Instance.weaponOwner.Contains(WeaponShopController.Instance.currentWeaponDisplay))
        {
            UIManager.Instance.player.currentWeapon = (WeaponType)System.Enum.Parse(typeof(WeaponType), WeaponShopController.Instance.namesWeaponDisplay[WeaponShopController.Instance.currentWeaponDisplay]);
            UIManager.Instance.player.RenderWeaponToHold();

            equipButton.SetActive(false);
            unequipButton.SetActive(true);

            // save
            SaveLoadController.Instance.SaveData(player);
        }
    }



    public void CheckIfWeaponOwned()
    {
        // So huu vu khi chua
        if (SaveLoadController.Instance.weaponOwner.Contains(WeaponShopController.Instance.currentWeaponDisplay))
        {
            ownedImage.SetActive(true);
            buyButton.SetActive(false);
        }
        else
        {
            ownedImage.SetActive(false);
            buyButton.SetActive(true);
        }

        // Trang bi vu khi chua
        if (UIManager.Instance.player.currentWeapon.ToString() == WeaponShopController.Instance.namesWeaponDisplay[WeaponShopController.Instance.currentWeaponDisplay])
        {
            equipButton.SetActive(false);
            unequipButton.SetActive(true);

            ownedImage.SetActive(true);
            buyButton.SetActive(false);
        }
        else
        {
            equipButton.SetActive(true);
            unequipButton.SetActive(false);
        }
    }
}
