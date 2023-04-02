using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShopController : MonoBehaviour
{
    public Transform displayPoint;
    [SerializeField] private List<GameObject> weaponsDisplay = new List<GameObject>();

    void Start()
    {
        foreach (WeaponType weapon in System.Enum.GetValues(typeof(WeaponType)))
        {
            weaponsDisplay.Add(ObjectPoolPro.Instance.GetFromPool(weapon.ToString()));
        }
        weaponsDisplay[0].transform.position = displayPoint.position;
        weaponsDisplay[0].gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
