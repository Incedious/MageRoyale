using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour {

    public float speed = 3.0f;
    public float turnSpeed = 50.0f;
    public float health = 100.0f;

    public Animator anim;
    public GameObject spell;
    public GameObject spellSpawn;
    private Camera cam;
    private GameObject tmpSpell;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        cam = GetComponentInChildren<Camera>();
        //spellSpawn = GameObject.Find("Player(Clone)/MagicSpawn");
    }

    public void TakeDamage(float damage)
    {
        Debug.Log("Player hit for " + damage);
        health -= damage;
        if (health <= 0.0f)
        {
            Debug.Log("Dead.");
            Destroy(this.gameObject);
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (!isLocalPlayer)
        {
            cam.enabled = false;
        }

        if (isLocalPlayer)
        {
            cam.enabled = true;
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                speed *= 2.0f;
            } 
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                speed /= 2.0f;
            }

            if (Input.GetKey(KeyCode.W))
            {
                transform.Translate(new Vector3(0.0f, 0.0f, speed * Time.deltaTime));
                anim.SetBool("walking", true);
            } else if (Input.GetKey(KeyCode.S))
            {
                transform.Translate(new Vector3(0.0f, 0.0f, -speed * Time.deltaTime));
                anim.SetBool("walking", true);
            }
            else
            {
                anim.SetBool("walking", false);
            }

            if (Input.GetKey(KeyCode.D))
            {
                transform.Rotate(new Vector3(0.0f, turnSpeed * Time.deltaTime, 0.0f));
            }

            if (Input.GetKey(KeyCode.A))
            {
                transform.Rotate(new Vector3(0.0f, -turnSpeed * Time.deltaTime, 0.0f));
            }

            if (Input.GetMouseButtonDown(1))
            {
                tmpSpell = Instantiate(spell,spellSpawn.transform);
                tmpSpell.transform.parent = null;
                NetworkServer.Spawn(tmpSpell);
            }
        }
    }
}
