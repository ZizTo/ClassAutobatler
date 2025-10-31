using UnityEngine;
using TMPro;

public class App : MonoBehaviour
{
    public Entity player = new Entity();
    public Entity enemy = new Entity();

    public GameObject playerSprite, enemySprite;

    [SerializeField] GameObject LevelUpScreen, ProceedScreen, RestartScreen, ChangeWeaponScreen;
    [SerializeField] TMP_Text voinLevel, razboinikLevel, varvorLevel, weaponOne, weaponTwo;
    public TMP_Text RestartName;

    Weapon Mech = new Weapon(Weapon.TYPE.RUB, "���", 3);
    Weapon Kinzhal = new Weapon(Weapon.TYPE.KOL, "������", 2);
    Weapon Dubina = new Weapon(Weapon.TYPE.DROB, "������", 3);
    Weapon Topor = new Weapon(Weapon.TYPE.RUB, "�����", 4);
    Weapon Kopio = new Weapon(Weapon.TYPE.KOL, "�����", 3);
    Weapon LegMech = new Weapon(Weapon.TYPE.RUB, "����������� ���", 10);

    public void StartNew()
    {
        RestartScreen.SetActive(false);
        player = new Entity();
        enemy = new Entity();
        ShowLevelUpScreen();
    }

    public void ShowChangeWeaponScreen()
    {
        ChangeWeaponScreen.SetActive(true);
        weaponOne.text = player.weapon.weaponToText();
        weaponTwo.text = enemy.winWeapon.weaponToText();
    }

    public void KeepWeapon()
    {
        ChangeWeaponScreen.SetActive(false);
        if (player.level < 3) ShowLevelUpScreen();
        else ShowProceedScreen();
    }

    public void ChangeWeapon()
    {
        ChangeWeaponScreen.SetActive(false);
        player.weapon = enemy.winWeapon;
        if (player.level < 3) ShowLevelUpScreen();
        else ShowProceedScreen();
    }

    public void ShowLevelUpScreen()
    {
        LevelUpScreen.SetActive(true);
        foreach (var classLevel in player.classesLevels)
        {
            switch(classLevel.Key)
            {
                case Entity.CLASS.VOIN:
                    voinLevel.text = $"�������: {classLevel.Value}";
                    break;
                case Entity.CLASS.RAZBOINIK:
                    razboinikLevel.text = $"�������: {classLevel.Value}";
                    break;
                case Entity.CLASS.VARAVAR:
                    varvorLevel.text = $"�������: {classLevel.Value}";
                    break;
                default:
                    break;
            }
        }
    }

    public void ShowProceedScreen()
    {
        ProceedScreen.SetActive(true);
    }

    public void ShowRestartScreen()
    {
        RestartScreen.SetActive(true);
    }

    public void ChooseVoin()
    {
        if (player.level == 0) { player.SpawnPlayer(Entity.CLASS.VOIN, Mech); }
        else { player.LevelUp(Entity.CLASS.VOIN); }
        SpawnEnemyAndStartFight();
    }

    public void ChooseRazb()
    {
        if (player.level == 0) { player.SpawnPlayer(Entity.CLASS.RAZBOINIK, Kinzhal); }
        else { player.LevelUp(Entity.CLASS.RAZBOINIK); }
        SpawnEnemyAndStartFight();
    }

    public void ChooseVarvar()
    {
        if (player.level == 0) { player.SpawnPlayer(Entity.CLASS.VARAVAR, Dubina); }
        else { player.LevelUp(Entity.CLASS.VARAVAR); }
        SpawnEnemyAndStartFight();
    }

    void SpawnEnemyAndStartFight()
    {
        LevelUpScreen.SetActive(false);
        ProceedScreen.SetActive(false);
        enemy = new Entity();
        switch (Random.Range(0, 6))
        {
            case 0:
                enemy.SpawnEnemy("������", 5, new Weapon(Weapon.TYPE.KOL, "������ �������", 2),
                    1, 1, 1, Entity.SPOSOBNOST.NOTHING, Kinzhal);
                break;
            case 1:
                enemy.SpawnEnemy("������", 10, new Weapon(Weapon.TYPE.KOL, "������ �������", 2),
                    2, 2, 1, Entity.SPOSOBNOST.UYAZ_K_DROB, Dubina);
                break;
            case 2:
                enemy.SpawnEnemy("�����", 8, new Weapon(Weapon.TYPE.KOL, "������ ������", 1),
                    3, 1, 2, Entity.SPOSOBNOST.NEUYAZ_K_RUB, Kopio);
                break;
            case 3:
                enemy.SpawnEnemy("�������", 6, new Weapon(Weapon.TYPE.KOL, "������ ��������", 3),
                    1, 3, 1, Entity.SPOSOBNOST.SKRIT_ATTACK, Mech);
                break;
            case 4:
                enemy.SpawnEnemy("�����", 10, new Weapon(Weapon.TYPE.KOL, "������ ������", 1),
                    3, 1, 3, Entity.SPOSOBNOST.KAMENNAYA_KOZHA, Topor);
                break;
            case 5:
                enemy.SpawnEnemy("������", 20, new Weapon(Weapon.TYPE.KOL, "������ �������", 4),
                    3, 3, 3, Entity.SPOSOBNOST.OGON, LegMech);
                break;
            default:
                break;
        };
        playerSprite.SetActive(true);
        enemySprite.SetActive(true);
        GetComponent<Fight>().StartFight(player, enemy);
    }
}
