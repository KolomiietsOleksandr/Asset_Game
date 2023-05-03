using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class HumanFight : MonoBehaviour
{
    public bool PlayerStep = true;
    public bool EnemyStep = false;
    public bool CriticalBoolPl = false;
    public bool CriticalBoolEn = false;

    GameObject Player;
    GameObject Enemy;
    Animator AnimatorPlayer;
    Animator AnimatorEnemy;

    private Vector3 PlPos;
    private Vector3 EnPos;

    public float speed;
    public float progressMove;

    public int IsMove = 0;

    public void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Enemy = GameObject.FindGameObjectWithTag("Enemy");

        AnimatorPlayer = Player.GetComponent<Animator>();
        AnimatorEnemy = Enemy.GetComponent<Animator>();
    }

    public void PlayerAttack()
    {
        PlayerStat PlayerStat = Player.GetComponent<PlayerStat>();
        EnemyStat EnemyStat = Enemy.GetComponent<EnemyStat>();
        if (PlayerStep == true) 
        {
            CriticalBoolPl = false;
            CriticalBoolEn= false;
            PlPos = Player.transform.position;
            EnPos = Enemy.transform.position;
            IsMove = 1;
            if (PlayerStat.CriticalChance >= Random.Range(0, 100)) { CriticalBoolPl = true; }
            if (EnemyStat.CriticalChance >= Random.Range(0, 100)) { CriticalBoolEn = true; }
        }
    }

    public void Attacks()
    {
        PlayerStat PlayerStat = Player.GetComponent<PlayerStat>();
        EnemyStat EnemyStat = Enemy.GetComponent<EnemyStat>();
        IsAttacked IsAttackedPL = Player.GetComponent<IsAttacked>();
        IsAttacked IsAttackedEn = Enemy.GetComponent<IsAttacked>();

        if (PlayerStep == true)
        {
            if (CriticalBoolPl == true)
            {
                AnimatorPlayer.SetInteger("CriticalAttack", 2);
                if (IsAttackedPL.AttackCheker == 1)
                {
                    EnemyStat.health -= (PlayerStat.damage * PlayerStat.CriticalMultiplier - EnemyStat.armor);
                    AnimatorPlayer.SetInteger("CriticalAttack", 1);
                    IsMove = 2;
                    IsAttackedPL.AttackReset();
                }
            }

            if (CriticalBoolPl == false)
            {
                AnimatorPlayer.SetInteger("Attack", 2);
                if (IsAttackedPL.AttackCheker == 1)
                {
                    EnemyStat.health -= (PlayerStat.damage - EnemyStat.armor);
                    AnimatorPlayer.SetInteger("Attack", 1);
                    IsMove = 2;
                    IsAttackedPL.AttackReset();
                }
            }
        }

        if (EnemyStep == true)
        {
            if (CriticalBoolEn == true)
            {
                AnimatorEnemy.SetInteger("CriticalAttack", 2);
                if (IsAttackedEn.AttackCheker == 1)
                {
                    PlayerStat.health -= (EnemyStat.damage * EnemyStat.CriticalMultiplier - PlayerStat.armor);
                    AnimatorEnemy.SetInteger("CriticalAttack", 1);
                    IsMove = 2;
                    IsAttackedEn.AttackReset();
                }
            }
            if (CriticalBoolEn == false)
            {
                AnimatorEnemy.SetInteger("Attack", 2);

                if (IsAttackedEn.AttackCheker == 1)
                {
                    PlayerStat.health -= (EnemyStat.damage - PlayerStat.armor);
                    AnimatorEnemy.SetInteger("Attack", 1);
                    IsMove = 2;
                    IsAttackedEn.AttackReset();
                }
            }
        }
    }

    public void Die() 
    {
        PlayerStat PlayerStat = Player.GetComponent<PlayerStat>();
        EnemyStat EnemyStat = Enemy.GetComponent<EnemyStat>();

        if (EnemyStat.health <= 0) { AnimatorEnemy.SetBool("Die", true); IsMove = 4; }

        if (PlayerStat.health <= 0) { AnimatorPlayer.SetBool("Die", true); IsMove = 4; }
    }
    

    void FixedUpdate()
    {   
        if (PlayerStep == true && IsMove != 4)
        {
            if (IsMove == 1)
            {
                AnimatorPlayer.SetBool("Walk", true);
                Player.transform.position = Vector3.Lerp(PlPos, EnPos, progressMove);
                progressMove += speed;    
            }
            if (progressMove >= 1 && IsMove == 1)
            {               
                AnimatorPlayer.SetBool("Walk", false);
                Attacks();

            }
            if (IsMove == 2)
            {
                AnimatorPlayer.SetBool("Walk",true);
                Player.transform.position = Vector3.Lerp(PlPos, EnPos, progressMove);
                progressMove -= speed;
            }
            if (progressMove <= 0 && IsMove == 2) { AnimatorPlayer.SetBool("Walk", false); PlayerStep = false; EnemyStep = true; IsMove = 1; Die(); }
        }

        if (EnemyStep == true && IsMove != 4)
        {
            if (IsMove == 1)
            {
                AnimatorEnemy.SetBool("Walk", true);
                Enemy.transform.position = Vector3.Lerp(EnPos, PlPos, progressMove);
                progressMove += speed;
            }
            if (progressMove >= 1 && IsMove == 1)
            {
                AnimatorEnemy.SetBool("Walk", false);
                Attacks();
            }

            if (IsMove == 2)
            {
                AnimatorEnemy.SetBool("Walk", true);
                Enemy.transform.position = Vector3.Lerp(EnPos, PlPos, progressMove);
                progressMove -= speed;
            }
            if (progressMove <= 0 && IsMove == 2) { AnimatorEnemy.SetBool("Walk", false); PlayerStep = true; EnemyStep = false; IsMove = 0; Die(); }
        }
    }
}