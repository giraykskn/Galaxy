using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp_script : MonoBehaviour
{

    [SerializeField]
    private float _speed = 3.0f;
    [SerializeField]
    private int _powerId;
    [SerializeField]
    private float _speedPower = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if(transform.position.y < -7f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            Player_script player = other.transform.GetComponent<Player_script>();
            if(player != null)
            {
                if(_powerId == 0)
                {
                    player.ActivateTripleShot();
                }
                else if(_powerId == 1)
                {
                    player.SetSpeed(_speedPower);
                }
                else
                {
                    player.ActivateShield();
                }
                
            }
            Destroy(this.gameObject);
        }
    }
}
