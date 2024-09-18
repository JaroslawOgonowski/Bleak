using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Rarity
{
    Common,
    Uncommon,
    Rare,
    Epic,
    Legendary,
    Mythic,
    Unique
}

public enum MobGroup
{
    Animals,
    Beasts,
    Humanoids,
    Undead,
    Elementals,
    Demons,
    Boss,
    Constructs,
    Giants,
    Aberrations,
    Plants,
    Reptiles,
    Insects,
    Celestials
}

[CreateAssetMenu(menuName = "MyAssets/New Mob")]
public class MobData : ScriptableObject
{
    public int ID;
    public string displayName;
    [TextArea(4, 4)]
    public Sprite icon;
    public GameObject prefab;
    public string name;
    public Rarity rarity;
    public MobGroup group;
    public int hp;
    public int maxhp;
    public int armor;
    public int magicArmor;
    public int minDmg;
    public int maxDmg;
    public int speed;
    public bool tameable;
    public int tameDificult;
    public GameObject[] loot;
}
