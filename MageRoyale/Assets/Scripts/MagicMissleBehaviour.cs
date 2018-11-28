using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicMissleBehaviour : MonoBehaviour {

    public float damage = 10.0f;


    void Start()
    {
        var tm = GetComponentInChildren<RFX4_TransformMotion>(true);
        if (tm != null) tm.CollisionEnter += Tm_CollisionEnter;
    }

    private void Tm_CollisionEnter(object sender, RFX4_TransformMotion.RFX4_CollisionInfo e)
    {
        Debug.Log(e.Hit.transform.name); //will print collided object name to the console.
        if(e.Hit.transform.tag == "Player")
        {
            e.Hit.transform.GetComponent<PlayerControler>().TakeDamage(damage);
        }

        Destroy(this.gameObject);
    }
}
