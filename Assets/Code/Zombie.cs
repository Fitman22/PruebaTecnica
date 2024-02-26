using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class Zombie : Enemy
{
  
    public override void GetDamage(int damage)
    {
        Health -= damage;
        GameObject p = Instantiate(points, transform.position, transform.rotation);
        p.GetComponentInChildren<TextMeshProUGUI>().text = "-" + damage;
        Destroy(p, 2);
        if (Health <= 0) Died();
    }

    public override void Died()
    {
        GetComponent<Animator>().Play("Died");
        died.Invoke();
        OnDied = true;
        ShoopController.instance.addMoney(reward);
        Destroy(gameObject, 10);
    }
    public override void FinishAttack()
    {
        damagePlayer.Invoke();
    }
    public override void Turn()
    {
        if (OnDied) return;
        int option = Random.Range(0,2);
        if (option == 0 || (Health+heal)>MaxHealth) { GetComponent<Animator>().Play("Attack"); }
        else { Heal(); }
        
        turn.Invoke();
    }
    public override void Heal()
    {
        GameObject p = Instantiate(points, transform.position, transform.rotation);
        p.GetComponentInChildren<TextMeshProUGUI>().text = "+" + heal;
        p.GetComponentInChildren<TextMeshProUGUI>().color = Color.green;
        Destroy(p, 2);
        Health += heal;
        if (Health >= MaxHealth) Health = MaxHealth;
    }
}
