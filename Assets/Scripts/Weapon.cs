using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Collidable
{
    // Damage struct
    public int[] damagePoint = {1,3,5,10,15,25,30};
    public float[] pushForce = {2.0f,2.1f,2.2f,2.5f,2.7f,3f,3.5f};

    //upgrade
    public int weaponLevel = 0;
    private SpriteRenderer spriteRenderer;

    //Swing
    private Animator anim;
    private float cooldown = 0.5f;
    private float lastSwing;

    private void Awake(){
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void Start(){
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    protected override void Update(){
        base.Update();
        if(Input.GetKeyDown(KeyCode.Space)){
            if(Time.time - lastSwing > cooldown){
                lastSwing = Time.time;
                Swing();
            }
        }
    }

    protected override void OnCollide(Collider2D coll){
        if(coll.tag == "Fighter"){
            if(coll.name == "barbarian_0")
                return;

            //create a new damage object then we'l send it to the fighter we've hit
            Damage dmg = new Damage
            {
                damageAmount = damagePoint[weaponLevel],
                origin = transform.position,
                pushForce = pushForce[weaponLevel]
            };

            coll.SendMessage("ReceiveDamage", dmg);
            Debug.Log(coll.name);
            
        }
    }

    private void Swing(){
        anim.SetTrigger("Swing");
        
    }

    public void UpgradeWeapon(){
        weaponLevel++;
        spriteRenderer.sprite = GameManager.instance.weaponSprites[weaponLevel];

    }

    public void SetWeaponLevel(int level){
        weaponLevel++;
        spriteRenderer.sprite = GameManager.instance.weaponSprites[weaponLevel];
    }
}
