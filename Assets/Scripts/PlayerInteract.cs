using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public MoneyText moneyText;
    [SerializeField] private GameObject WinScreen;
    [SerializeField] private GameObject LoseScreen;
    [SerializeField ]private int maxHealth = 3;
    private int health = 0;
    [SerializeField]private int money = 0;

    [SerializeField] private float invincibleTime = 1;
    private float invincibleTimer = 0;
    private bool invincible = false;

    private SpriteRenderer sprite;
    [SerializeField] private Color hurtColor;
    [SerializeField] private Color startColor;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        sprite = GetComponentInChildren<SpriteRenderer>();
        sprite.color = startColor;
    }

    // Update is called once per frame
    private void FixedUpdate() {
        if(invincibleTimer >0){
            invincibleTimer -= Time.fixedDeltaTime;
            if(invincibleTimer <= 0){
                invincible = false;
                sprite.color = startColor;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col) {
        GameObject other = col.gameObject;
        if(other.tag == "Damage"){
            Damage(other);
        }
        else if(other.tag == "Coin"){
            CollectCoin(other);
        }
        else if(other.tag == "EndLevel"){
            WinLevel();
        }
    }

    public void Damage(GameObject obj){
        if(invincible){
            return;
        }
        health -= obj.GetComponent<harmful>().GetDamage();
        if(health <= 0){
            Die();
        }
        invincibleTimer = invincibleTime;
        sprite.color = hurtColor;
    }

    public void CollectCoin(GameObject obj){
        money+=obj.GetComponent<Money>().GetValue();
        moneyText.UpdatePoints(money);
        Destroy(obj);
    }

    public void WinLevel(){
        Time.timeScale = 0;
        WinScreen.transform.SetParent(null);
        WinScreen.SetActive(true);
    }

    public void Die(){
        LoseScreen.transform.SetParent(null);
        LoseScreen.SetActive(true);
        Destroy(gameObject);
    }
}
