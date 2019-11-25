using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float groundSPD = 5;

    public bool OnGround;
    public int DoubleJumps = 1;
    public int MaxDoubleJumps = 1;

    public LayerMask groundLayers;

    private SpriteRenderer mySpriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //Is the player grounded
        OnGround = Physics2D.OverlapArea(new Vector2(transform.position.x - 0.3f, transform.position.y - 0.3f),
            new Vector2(transform.position.x + 0.3f, transform.position.y - 0.31f), groundLayers);

       
        
        //When the button is pressed, a jump is activated
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (OnGround == true)
            {
                GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
                DoubleJumps = MaxDoubleJumps;
            }
            else if (DoubleJumps > 0)
            {
                GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
                DoubleJumps -= DoubleJumps;
            }
            else
            {

            }

        }

        //When the D key is pressed the player faces right
        if (Input.GetKeyDown(KeyCode.D))
        {
            mySpriteRenderer.flipX = false;
        }

        //When the D key is pressed the player faces left
        if (Input.GetKeyDown(KeyCode.A))
        {
            mySpriteRenderer.flipX = true;
        }
    }

    void FixedUpdate()
    {
        //When the button is held the player moves to the right
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector2.right * groundSPD * Time.deltaTime);
        }


        //When the D key is pressed the player faces left
        //When the button is held the player moves to the left
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector2.left * groundSPD * Time.deltaTime);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
            SceneManager.LoadScene(3);

        }

        
    }
   
}
