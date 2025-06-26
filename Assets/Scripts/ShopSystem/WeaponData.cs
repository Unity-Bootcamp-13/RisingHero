// SkillData.cs ¿¹½Ã
using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Weapon/Create New Weapon")]
public class WeaponData : ScriptableObject
{
    public string name;
    public int weaponId;
    public Sprite icon;
    public string description;
}
