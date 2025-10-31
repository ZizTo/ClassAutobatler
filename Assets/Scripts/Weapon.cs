using UnityEngine;
using System.Collections.Generic;

public class Weapon
{
    public enum TYPE
    {
        RUB,
        DROB,
        KOL
    }
    public TYPE type;
    public string weaponName;
    public int damage;

    public Weapon(TYPE newType, string newName, int newDamage)
    {
        type = newType;
        weaponName = newName;
        damage = newDamage;
    }

    public static Dictionary<TYPE, string> typeToString = new Dictionary<TYPE, string>()
    {
        [TYPE.DROB] = "��������",
        [TYPE.RUB] = "�������",
        [TYPE.KOL] = "�������",
    };

    public string weaponToText()
    {
        return ($"������ {weaponName}\n" +
            $"���� ������ {damage}\n" +
            $"��� ������ {typeToString[type]}\n");
    }
}
