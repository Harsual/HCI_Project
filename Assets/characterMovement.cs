using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class characterMovement : MonoBehaviour
{
    public CharacterController controller;
    public int health = 100;
    public GameController gameController;

    public Image damageIndicator;


    private int attackInterval = 240;
    public float speed = 6f;
    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal,0f,vertical).normalized;

        if(direction.magnitude >= 0.1f)
        {
            controller.Move(direction*speed*Time.deltaTime);
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("getting hit");
            //TakeDamage(20);

            
        }
            
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") && Time.frameCount%attackInterval == 0)
        {
            TakeDamage(20);
        }
        

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Heal();
        }
    }


    public void TakeDamage(int damage)
    {
        health -= damage;
        Color damageAlpha = damageIndicator.color;

        if (damageAlpha.a < 0.5f)
        {
            damageAlpha.a += .1f;
            damageIndicator.color = damageAlpha;
        }
       

        if (health <= 0)
        {
            Invoke(nameof(DestroyEnemy), .5f);

        }
    }

    private void DestroyEnemy()
    {
        gameController.GameOver();
        //Destroy(gameObject);
        //Debug.Log("Dead!!!!");
    }

    private void Heal()
    {
        health = 100;
        Color damageAlpha = damageIndicator.color;

        damageAlpha.a = 0;
        damageIndicator.color = damageAlpha;
        //Destroy(gameObject);
        //Debug.Log("Dead!!!!");
    }
}

