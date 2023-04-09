using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Constant;

public class WeaponShopController : Singleton<WeaponShopController>
{
    public Transform displayPoint;
    public Material skin, pant;

    [HideInInspector]
    public string[] namesWeaponDisplay = { WEAPON_AXE, WEAPON_BOOMERANG, WEAPON_SWORD };
    public int[] pricesWeaponDisplay = { PRICE_AXE, PRICE_BOOMERANG, PRICE_SWORD };
    public int[] priceHat = { 100, 200, 300, 400, 500, 600, 700 };
    public int[] pricePant = { 100, 200, 300, 400, 500, 600, 700, 800, 900 };
    public int[] priceShield = { 100, 200 };
    public int[] priceSkin = { 100, 200, 300, 400, 500 };
    public int currentWeaponDisplay;

    [SerializeField] private List<GameObject> weaponsPrefab = new List<GameObject>();
    [SerializeField] private List<GameObject> weaponsDisplay = new List<GameObject>();

    [SerializeField] private List<GameObject> hatToWear = new List<GameObject>();
    [SerializeField] private List<Material> pantToWear = new List<Material>();
    [SerializeField] private List<GameObject> shieldToHold = new List<GameObject>();

    // Skin
    [Header("Skin")]
    [SerializeField] private List<Material> skinWear = new List<Material>();
    [SerializeField] private List<GameObject> skinDevil = new List<GameObject>();
    [SerializeField] private List<GameObject> skinAngel = new List<GameObject>();
    [SerializeField] private List<GameObject> skinWitch = new List<GameObject>();
    [SerializeField] private List<GameObject> skinDeadpool = new List<GameObject>();
    [SerializeField] private List<GameObject> skinThor = new List<GameObject>();
    private Dictionary<int, List<GameObject>> partOfSkin = new Dictionary<int, List<GameObject>>();

    public SkinnedMeshRenderer pantSkin;
    public SkinnedMeshRenderer bodySkin;

    private void Awake()
    {
        // Skin setup
        partOfSkin.Add(0, skinDevil);
        partOfSkin.Add(1, skinAngel);
        partOfSkin.Add(2, skinWitch);
        partOfSkin.Add(3, skinDeadpool);
        partOfSkin.Add(4, skinThor);
    }
    void Start()
    {
        // Weapon view
        for (int i = 0; i < weaponsPrefab.Count; i++)
        {
            weaponsDisplay.Add(Instantiate(weaponsPrefab[i], displayPoint));
            weaponsDisplay[i].transform.localPosition = Vector3.zero;
            weaponsDisplay[i].transform.localRotation = Quaternion.Euler(0, 0, 90);
            weaponsDisplay[i].SetActive(false);
        }
        weaponsDisplay[0].SetActive(true);
        currentWeaponDisplay = 0;


    }

    // Update is called once per frame
    void Update()
    {
        weaponsDisplay[currentWeaponDisplay].transform.localEulerAngles += new Vector3(0, -1, 0);
    }

    public void NextRightButton()
    {
        if (currentWeaponDisplay == weaponsDisplay.Count - 1)
        {
            weaponsDisplay[currentWeaponDisplay].SetActive(false);
            currentWeaponDisplay = 0;
        }
        else
        {
            weaponsDisplay[currentWeaponDisplay].SetActive(false);
            currentWeaponDisplay++;
        }
        weaponsDisplay[currentWeaponDisplay].SetActive(true);
    }
    public void NextLeftButton()
    {
        if (currentWeaponDisplay == 0)
        {
            weaponsDisplay[currentWeaponDisplay].SetActive(false);
            currentWeaponDisplay = weaponsDisplay.Count - 1;
        }
        else
        {
            weaponsDisplay[currentWeaponDisplay].SetActive(false);
            currentWeaponDisplay--;
        }
        weaponsDisplay[currentWeaponDisplay].SetActive(true);
    }
    public void ResetWhenClickCloseButton()
    {
        weaponsDisplay[currentWeaponDisplay].SetActive(false);
        currentWeaponDisplay = 0;
        weaponsDisplay[currentWeaponDisplay].SetActive(true);
    }
    public void ChooseHatToWear(int index)
    {
        for (int i = 0; i < hatToWear.Count; i++)
        {
            hatToWear[i].SetActive(false);
        }
        hatToWear[index].SetActive(true);
    }
    public void ChoosePantToWear(int index)
    {
        pantSkin.material = pantToWear[index];
    }
    public void ChooseShieldToHold(int index)
    {
        for (int i = 0; i < shieldToHold.Count; i++)
        {
            shieldToHold[i].SetActive(false);
        }
        shieldToHold[index].SetActive(true);
    }
    public void ChooseSkin(int index)
    {
        // Delete previous part of skin
        ResetPartOfSkin();

        RemoveAllClothesToWearSkin();
        bodySkin.material = skinWear[index];
        // parts of body
        for (int i = 0; i < partOfSkin[index].Count; i++)
        {
            partOfSkin[index][i].SetActive(true);
        }
    }
    public void ResetPartOfSkin()
    {
        foreach (KeyValuePair<int, List<GameObject>> item in partOfSkin)
        {
            for (int i = 0; i < item.Value.Count; i++)
            {
                item.Value[i].SetActive(false);
            }
        }
    }
    public void BackToNormalSkin()
    {
        bodySkin.material = skin;
        pantSkin.material = pant;
    }
    public void RemoveAllClothesToWearSkin()
    {
        // refresh skin pant material
        RefreshSkinPantToDefault();


        // refresh hat and shield
        RefreshHatToDefault();
        RefreshShieldToDefault();
    }
    public void RefreshSkinPantToDefault()
    {
        if (pantSkin.materials.Length > 0)
        {
            Material[] materials = pantSkin.materials;
            List<Material> materialsList = new List<Material>(pantSkin.materials);
            materialsList.RemoveAt(0);
            pantSkin.materials = materialsList.ToArray();
        }
    }
    public void RefreshHatToDefault()
    {
        for (int i = 0; i < hatToWear.Count; i++)
        {
            hatToWear[i].SetActive(false);
        }
    }
    public void RefreshShieldToDefault()
    {
        for (int i = 0; i < shieldToHold.Count; i++)
        {
            shieldToHold[i].SetActive(false);
        }
    }
}
