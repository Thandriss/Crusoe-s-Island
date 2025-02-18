using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : Interactable
{
    // Start is called before the first frame update
    private float health;
    void Start()
    {
        health = GetComponent<PlayerHealth>().cHealthMax;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void Interact()
    {
        float addHealth = Random.Range(5, 10);
        //health += addHealth;
        Debug.Log("Interact with" + gameObject.name);
    }
}
