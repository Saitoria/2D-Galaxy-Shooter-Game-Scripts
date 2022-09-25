using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private int _powerupSpeed = 3;
    // Start is called before the first frame update
    [SerializeField]
    private int _powerupID;
    [SerializeField]
    private AudioClip _clip;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down* _powerupSpeed* Time.deltaTime);
        if(transform.position.y <= -7)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.transform.tag == "player")
        {
            Player player = other.transform.GetComponent<Player>();
            AudioSource.PlayClipAtPoint(_clip, transform.position);
            if(player!=null)
            {
                switch(_powerupID)
                {
                    case 0:
                        player.TrippleShotActive();
                        break;
                    case 1:
                        player.SpeedPowerupActive();
                        break;
                    case 2:
                        player.ShieldPowerupActive();
                        break;
                    default:
                        Debug.Log("Unassigned powerup");
                        break;
                }
            }
            Destroy(this.gameObject); 
        }
    }   
}
