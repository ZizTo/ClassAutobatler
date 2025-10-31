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
        [TYPE.DROB] = "дробящий",
        [TYPE.RUB] = "рубящий",
        [TYPE.KOL] = "колющий",
    };

    public string weaponToText()
    {
        return ($"Оружие {weaponName}\n" +
            $"Урон оружия {damage}\n" +
            $"Тип оружия {typeToString[type]}\n");
    }
}
