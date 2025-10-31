using UnityEngine;
using TMPro;

public class Fight : MonoBehaviour
{
    Entity player;
    Entity enemy;

    [SerializeField] GameObject playerArrow, enemyArrow;
    [SerializeField] TMP_Text playerDamage, enemyDamage;

    public bool fightGo = false;
    float timer = 3f;
    bool playersTurn;
    int hod = 0;

    public void StartFight(Entity nplayer, Entity nenemy)
    {
        player = nplayer;
        enemy = nenemy;
        hod = 0;

        playersTurn = player.speed >= enemy.speed;
        ChangeArrow();

        timer = 5f;
        fightGo = true;
    }

    void ChangeArrow()
    {
        if (playersTurn)
        {
            playerArrow.SetActive(true);
            enemyArrow.SetActive(false);
        }
        else
        {
            playerArrow.SetActive(false);
            enemyArrow.SetActive(true);
        }
    }
    
    void Update()
    {
        if (fightGo) timer -= Time.deltaTime;

        if (playerDamage.alpha > 0) playerDamage.alpha -= Time.deltaTime;
        if (enemyDamage.alpha > 0) enemyDamage.alpha -= Time.deltaTime;
        if (timer <= 0)
        {
            timer = 3f;
            ProceedMove();
        }
    }

    void ProceedMove()
    {
        if (playersTurn)
        {
            int dmg = player.Attack(enemy, hod/2 + 1);
            if (dmg == -1) enemyDamage.text = "мимо";
            else enemyDamage.text = $"{dmg}";
            enemyDamage.alpha = 1f;
        }
        else
        {
            int dmg = enemy.Attack(player, hod / 2 + 1);
            if (dmg == -1) playerDamage.text = "мимо";
            else playerDamage.text = $"{dmg}";
            playerDamage.alpha = 1f;
        }
        playersTurn = !playersTurn;
        ChangeArrow();
        hod++;
        if (enemy.hp <= 0)
        {
            FightEnd(true);
        }
        if (player.hp <= 0)
        {
            FightEnd(false);
        }
    }
    
    void FightEnd(bool playerWin)
    {
        fightGo = false;
        if (playerWin)
        {
            GetComponent<App>().enemySprite.SetActive(false);
            if (player.level < 5)
                GetComponent<App>().ShowChangeWeaponScreen();
            else {
                GetComponent<App>().RestartName.text = "Вы выиграли!";
                GetComponent<App>().ShowRestartScreen();
            }
        }
        else {
            GetComponent<App>().playerSprite.SetActive(false);
            GetComponent<App>().RestartName.text = "Вы проиграли!";
            GetComponent<App>().ShowRestartScreen(); 
        }
    }
}
