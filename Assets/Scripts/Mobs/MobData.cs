using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "MyAssets/New Mob")]
public class MobData : ScriptableObject
{
    public int ID;
    public string displayName;
    [TextArea(4, 4)]
    public Sprite icon;
    public GameObject prefab;
    public string name;
    public string rarity;
    public string group;
    public int hp;
    public int maxhp;
    public int armor;
    public int magicArmor;
    public int minDmg;
    public int maxDmg;
    public int speed;
    public bool tameable;
    public int tameDificult;
    public string owner;
    public bool legend;
    public GameObject[] loot;
}
