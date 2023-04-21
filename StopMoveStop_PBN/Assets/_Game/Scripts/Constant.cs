using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constant 
{
    // Anim object
    public const string ANIM_IDLE = "Idle";
    public const string ANIM_DEATH = "Dead";
    public const string ANIM_RUN = "Run";
    public const string ANIM_ATTACK = "Attack";
    public const string ANIM_DANCE = "Dance";
    // Anim UI
    public const string ANIM_CLOSE_MENU = "CloseMenu";
    public const string ANIM_CLOSE_WEAPON = "CloseWeapon";
    public const string ANIM_CLOSE_SHOP = "CloseShop";
    public const string ANIM_CLOSE_SETTING = "CloseSetting";

    public const string ANIM_OPEN_MENU = "OpenMenu";
    public const string ANIM_OPEN_WEAPON = "OpenWeapon";
    public const string ANIM_OPEN_SHOP = "OpenShop";
    public const string ANIM_OPEN_SETTING = "OpenSetting";

    public const string WEAPON_AXE = "Axe";
    public const string WEAPON_BOOMERANG = "Boomerang";
    public const string WEAPON_SWORD = "Sword";
    public const string WEAPON_Z = "Z";
    public const string WEAPON_CANDY = "Candy";
    public const string WEAPON_HAMMER = "Hammer";
    public const string WEAPON_UZI = "Uzi";

    public const string GAME_BOT = "Bot";
    public const string GAME_PLAYER = "Player";

    public const string _MUSICVOLUME = "MusicVolume";
    public const string _SEVOLUME = "SEVolume";

    public const string HEADPOINT = "HeadPoint";

    [Header("Weapon Price")]
    public const int PRICE_AXE = 50 ;
    public const int PRICE_BOOMERANG = 70 ;
    public const int PRICE_SWORD = 100 ;
    public const int PRICE_Z = 150;
    public const int PRICE_CANDY = 200;
    public const int PRICE_HAMMER = 300;
    public const int PRICE_UZI = 500;

    public string[] shieldName = { "Khien 1", "Khien 2" };
    public string[] hatName = { "Arrow", "Ear", "Flower", "Hair", "Hat Cap", "Hat" };
}
