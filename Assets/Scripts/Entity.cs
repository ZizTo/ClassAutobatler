using UnityEngine;
using System.Collections.Generic;

public class Entity
{
    public enum CLASS
    {
        VOIN,
        RAZBOINIK,
        VARAVAR,
        ENEMY
    }

    public enum SPOSOBNOST
    {
        SKRIT_ATTACK,
        YAD,
        PRORIV,
        SHIELD,
        YAROST,
        KAMENNAYA_KOZHA,
        UYAZ_K_DROB,
        NEUYAZ_K_RUB,
        OGON,
        NOTHING
    }

    static Dictionary<SPOSOBNOST, string> spToString = new Dictionary<SPOSOBNOST, string>()
    {
        [SPOSOBNOST.SKRIT_ATTACK] = "Скрытая атака",
        [SPOSOBNOST.YAD] = "Яд",
        [SPOSOBNOST.PRORIV] = "Прорыв",
        [SPOSOBNOST.SHIELD] = "Щит",
        [SPOSOBNOST.YAROST] = "Ярость",
        [SPOSOBNOST.KAMENNAYA_KOZHA] = "Каменная кожа",
        [SPOSOBNOST.UYAZ_K_DROB] = "Уязвимость к дроб.",
        [SPOSOBNOST.NEUYAZ_K_RUB] = "Неуязвимость к руб.",
        [SPOSOBNOST.OGON] = "Огненное дыхание",
        [SPOSOBNOST.NOTHING] = "Ничего",
    };

    public int hp = 0;
    public string eName = "Игрок";
    public int damage = 0;
    public int speed = 0;
    public int endurance = 0;
    public Weapon weapon = new Weapon(Weapon.TYPE.KOL, "---", 0);
    public int level = 0;
    public Weapon winWeapon = new Weapon(Weapon.TYPE.KOL, "---", 0);

    List<SPOSOBNOST> sposobnosti = new List<SPOSOBNOST>();

    public void SpawnPlayer(CLASS newClass, Weapon newWeapon)
    {
        damage = Random.Range(1, 4);
        speed = Random.Range(1, 4);
        endurance = Random.Range(1, 4);
        LevelUp(newClass);
        weapon = newWeapon;
    }

    public void SpawnEnemy(string neName, int nhp, Weapon nenemyWeapon, int ndamage, int nspeed, int nendurance, SPOSOBNOST nspos, Weapon nwin)
    {
        eName = neName;
        hp = nhp;
        weapon = nenemyWeapon;
        damage = ndamage;
        speed = nspeed;
        endurance = nendurance;
        if (nspos != SPOSOBNOST.NOTHING)
            sposobnosti.Add(nspos);
        winWeapon = nwin;
    }

    public int Attack(Entity enemy, int hod)
    {
        int calculatedDamage = damage + weapon.damage;
        Debug.Log($"Hod: {hod}");
        int randooom = Random.Range(1, speed + enemy.speed + 1);
        Debug.Log($"rand: {randooom}");
        if (randooom <= enemy.speed)
        {
            return -1;
        }
        
        foreach (var sp in sposobnosti)
        {
            switch (sp)
            {
                case SPOSOBNOST.SKRIT_ATTACK:
                    calculatedDamage += (speed > enemy.speed) ? 1 : 0;
                    break;
                case SPOSOBNOST.YAD:
                    calculatedDamage += (hod - 1);
                    break;
                case SPOSOBNOST.PRORIV:
                    calculatedDamage += (hod == 1) ? weapon.damage : 0;
                    break;
                case SPOSOBNOST.YAROST:
                    calculatedDamage += (hod <= 3) ? 2 : -1;
                    break;
                case SPOSOBNOST.OGON:
                    calculatedDamage += (hod % 3 == 0) ? 3 : 0;
                    break;
                default:
                    break;
            }
        }
        foreach (var sp in enemy.sposobnosti)
        {
            switch (sp)
            {
                case SPOSOBNOST.SHIELD:
                    calculatedDamage -= (enemy.damage > damage) ? 3 : 0;
                    break;
                case SPOSOBNOST.KAMENNAYA_KOZHA:
                    calculatedDamage -= enemy.endurance;
                    break;
                case SPOSOBNOST.UYAZ_K_DROB:
                    calculatedDamage += (weapon.type == Weapon.TYPE.DROB) ? weapon.damage : 0;
                    break;
                case SPOSOBNOST.NEUYAZ_K_RUB:
                    calculatedDamage -= (weapon.type == Weapon.TYPE.RUB) ? weapon.damage : 0;
                    break;
            }
        }
        
        enemy.hp -= Mathf.Max(0, calculatedDamage);
        return Mathf.Max(0, calculatedDamage);
    }

    public Dictionary<CLASS, int> classesLevels = new Dictionary<CLASS, int>()
    {
        [CLASS.RAZBOINIK] = 0,
        [CLASS.VARAVAR] = 0,
        [CLASS.VOIN] = 0,
    };

    public void LevelUp(CLASS newclass)
    {
        hp = endurance;
        level++;
        if (level <= 3)
        {
            classesLevels[newclass] = classesLevels[newclass] + 1;
            int clvl = classesLevels[newclass];
            switch (newclass)
            {
                case CLASS.RAZBOINIK:
                    Debug.Log($"RAZB: {clvl}");
                    if (clvl == 1) sposobnosti.Add(SPOSOBNOST.SKRIT_ATTACK);
                    else if (clvl == 2) speed++;
                    else if (clvl == 3) sposobnosti.Add(SPOSOBNOST.YAD);
                    break;
                case CLASS.VOIN:
                    Debug.Log($"VOIN: {clvl}");
                    if (clvl == 1) sposobnosti.Add(SPOSOBNOST.PRORIV);
                    else if (clvl == 2) sposobnosti.Add(SPOSOBNOST.SHIELD);
                    else if (clvl == 3) damage++;
                    break;
                case CLASS.VARAVAR:
                    Debug.Log($"VARV: {clvl}");
                    if (clvl == 1) sposobnosti.Add(SPOSOBNOST.YAROST);
                    else if (clvl == 2) sposobnosti.Add(SPOSOBNOST.KAMENNAYA_KOZHA);
                    else if (clvl == 3) endurance++;
                    break;
                default:
                    break;
            }
        }
        foreach(var classLevel in classesLevels)
        {
            hp += classLevel.Value * (classLevel.Key == CLASS.RAZBOINIK ? 4 : classLevel.Key == CLASS.VOIN ? 5 : 6);
        }
    }

    public string getTextInfo()
    {
        string answ = $"{eName}\n" +
            $"Здоровье {hp}\n" +
            $"Урон {damage}\n" +
            $"Ловкость {speed}\n" +
            $"Выносливость {endurance}\n" + 
            weapon.weaponToText() +
            $"Способности:\n";
        if (sposobnosti.Count == 0) answ += "Отсутствуют";
        else
        {
            foreach(var spos in sposobnosti)
            {
                answ += $"{spToString[spos]}\n";
            }
        }
        return answ;
    }
}