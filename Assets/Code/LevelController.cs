using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    Player player;
    [SerializeField] List<GameObject> Listenemies;
    List<GameObject> enemies = new List<GameObject>();
    [SerializeField] int Distance;
    [SerializeField] Animator panel;
    [SerializeField] Image barr;
    [SerializeField] GameObject DiedUI,ShoopUI,WinUI;
    bool playerTurn;
    Enemy enemi;
    
    private void Start()
    {
        player = FindObjectOfType<Player>();
        player.turn.AddListener(changeTurn);
        createEnemies();
        NextBattle();
        player.Getdamage.AddListener(UpdateBarr);
        player.Died.AddListener(died);
        DiedUI.SetActive(false);
    }
    private void UpdateBarr()
    {
        barr.fillAmount = player.getDiference();
    }
    void createEnemies()
    {
        int amount = Random.Range(9, 16);
        float x = Distance;
        enemies.Add(Instantiate(Listenemies[0], new Vector2(x, 0), transform.rotation));
        x += Distance;
        for (int i = 0; i < amount; i++){
            int enemi = Random.Range(0, Listenemies.Count);
            enemies.Add(Instantiate(Listenemies[enemi],new Vector2(x,0),transform.rotation));
            x += Distance;
        }
      
    }
    void died()
    {
        DiedUI.SetActive(true);
    }
    public void changeTurn()
    {
        StartCoroutine(wait());
    }
    IEnumerator wait()
    {
        playerTurn = !playerTurn;
        if (!playerTurn)
        {
            panel.Play("EndPanel");
        }
        yield return new WaitForSeconds(1.6f);
        if (playerTurn)
        {
            player.onAttack = true;
            panel.Play("StartPanel");
        }
        else
        {
            enemi.Turn();
        }
    }
    void damagePlayer()
    {
        player.GetDamage(enemi.damage);
    }
    void OpenShoop()
    {
        ShoopUI.SetActive(true);
    }
   public void NextBattle()
    {
        ShoopUI.SetActive(false);
        playerTurn = false;
        changeTurn();
        player.FinishFight();
        if (enemies.Count <= 0)
        {
            int wins =PlayerPrefs.GetInt("Wins");
            wins++;
            PlayerPrefs.SetInt("Wins", wins);
            WinUI.SetActive(true);
            return;
        } 
        player.AssignBattle(enemies[0].transform);
        enemies[0].GetComponent<Enemy>().died.AddListener(OpenShoop);
        enemies[0].GetComponent<Enemy>().turn.AddListener(changeTurn);
        enemies[0].GetComponent<Enemy>().damagePlayer.AddListener(damagePlayer);
        enemi = enemies[0].GetComponent<Enemy>();
        enemies.RemoveAt(0);  
    }
}
