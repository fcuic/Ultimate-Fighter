using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed;
    public bool grounded;
    public float jumpForce = 750f;


    private Rigidbody2D rbd;
    private Animator anim;
    

    //delaying to see death animation
    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        UIManager.instance.OnGameOver();
    }

    // Start is called before the first frame update
    void Start()
    {
        rbd = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        anim.SetFloat("Speed", Mathf.Abs(x));
        anim.SetBool("Grounded", grounded);
        if (x > 0)
        {
            transform.localScale = Vector2.one;
        }
        else if (x<0) 
        {
            transform.localScale = new Vector2(-1, 1);
        }
        rbd.velocity = new Vector2(x * walkSpeed, rbd.velocity.y);

        if (Input.GetKeyDown(KeyCode.W)) 
        {
            Jump();
        }
    }
    public void Jump() 
    {
        if (grounded) 
        {
            rbd.AddForce(Vector2.up * jumpForce);
        }
    }
    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.gameObject.tag == "Coin")
        {
            CoinManager.instance.updateCoin();
            UIManager.instance.OnCoinPickup();
            Destroy(target.gameObject);
        }
        else if (target.gameObject.tag=="Spike") 
        {  
            anim.SetTrigger("Death");
            StartCoroutine(ExecuteAfterTime(0.3f));
        }
        else if (target.gameObject.name == "Door")
        {          
            UIManager.instance.OnLevelComplete();

        }
    }
}
