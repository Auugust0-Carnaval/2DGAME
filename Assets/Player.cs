using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float speed; // valor é daclarado no unitycine 
    public float direction; // setado com valor de input e vetores do RigidBody
    public Rigidbody2D rig;
    public float jumper; // valor declarado no Unitycine
    public float maxVelocityY = 10;
    private bool canJump = true;
    public int contJump = 0;
    private SpriteRenderer spriteRenderer;
    public Animator animator;

    //dashing
    public float dashSpeed;
    public float dashDuration;
    private bool isDashing = false; // valor de dash começa com falso | sem ter feito algum dashing
    private float dashEndTime;
    private float dashCooldownEndTime;


    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        animator = GetComponent<Animator>();

    }

    void Update()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, 0f); //não deixa que o personagem tenha uma rotação nos eixos x,y,z | Quartenion = metodo de referencia a rotação
        direction = Input.GetAxis("Horizontal"); // seta na variavel inputs do teclado como = a-d ou sentinhas direcionais

        //condicao do dash

        if (Input.GetKeyDown(KeyCode.Q) && !isDashing && Time.time >= dashCooldownEndTime) // input da tecla "Q" ativa o dashing
        {
            isDashing = true; // ativa o dashing
            dashEndTime = Time.time + dashDuration;
            dashCooldownEndTime = Time.time + 0.8f;
        }






        if (Input.GetAxis("Horizontal") != 0)
        {
            //esta correndo
            animator.SetBool("isRunning", true);



        }
        else
        {
            //esta parado
            animator.SetBool("isRunning", false);

        }


        if (canJump && Input.GetKeyDown(KeyCode.Space))
        {
            rig.AddForce(Vector2.up * jumper, ForceMode2D.Impulse);
            canJump = false;

            Debug.Log(contJump++);
        }

        // Limita a velocidade vertical
        if (rig.velocity.y > maxVelocityY)
        {
            rig.velocity = new Vector2(rig.velocity.x, maxVelocityY);
        }

        if (direction > 0) // verefica a direção do movimento e vira o jogador
        {
            spriteRenderer.flipX = false; //vira para direita

        }
        else if (direction < 0)
        {
            spriteRenderer.flipX = true; //vira para esquerda

        }
    }

    void OnCollisionEnter2D(Collision2D collision) // metodo de colisao, adicionando uma tag do GameObject como parametro
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            canJump = true; // Permite o pulo novamente ao tocar no chão
        }
    }

    void FixedUpdate()
    {
        if (isDashing)
        {
            if (Time.time < dashEndTime)
            {
                rig.velocity = new Vector2(direction * dashSpeed, rig.velocity.y);

            }
            else
            {
                isDashing = false;
            }
        }
        else
        {
            rig.velocity = new Vector2(direction * speed, rig.velocity.y);
        }

        if (Time.time >= dashCooldownEndTime)
        {
            //cooldown do dash terminou, permita o proximo dash/

            //posso adioconar logico como efeitos sonoros ou animaçõe visuais
        }
    }
}
