using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{

    public float speed = 5.0f;
    
    private Rigidbody2D rb;

    [Header("Movimentação")]
    public float x;

    public float jumpForce = 16;
    public bool isGrounded;
    public float groundCheckRadius = 2;
    public Transform footPosition;
    public LayerMask whatIsGround;

    [Header("Animação")]
    public Animator animator;
    
    private void Start() {
        
        rb  = GetComponent<Rigidbody2D>();
        animator.SetBool("running", false);
    }

    private void Update() {
        
        x = Input.GetAxisRaw("Horizontal");
        if(Input.GetKeyDown(KeyCode.Space)){
            Jump();
        }
        
    }

    private void FixedUpdate(){
        
        checkGround();
        Move();
    }
    void Move(){
        rb.velocity = new Vector2(x * speed * Time.fixedDeltaTime, rb.velocity.y);
        if(rb.velocity.x != 0){
            animator.SetBool("running", true);
        }else{
            animator.SetBool("running", false);
        }
    }
    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(footPosition.position, groundCheckRadius);
    }

    void checkGround(){
        isGrounded = Physics2D.OverlapCircle(footPosition.position, groundCheckRadius, whatIsGround);
    }

    void Jump(){
        //fazer com que o personagem pule
        if(isGrounded){
            rb.AddForce(new Vector2(0, 1 * jumpForce * Time.fixedDeltaTime),ForceMode2D.Impulse);
        }else{
            return;
        }
        
    }

    

}
