using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    //propriedades do Player

    public int maxHealth = 100; // essa sera a vida maxima do player
    public int currentHealth; // vidal atual do Player


    public float speed; // valor é daclarado no unitycine 
    public float direction; // setado com valor de input e vetores do RigidBody
    public Rigidbody2D rig;
    public float jumper; // valor declarado no Unitycine
    public float maxVelocityY = 10;
    private bool canJump = true;
    public int contJump = 0;
    private SpriteRenderer spriteRenderer;
    public Animator animator;

    //Dashing
    public float dashSpeed;
    public float dashDuration;
    private bool isDashing = false; // valor de dash começa com falso | sem ter feito algum dashing
    private float dashEndTime;
    private float dashCooldownEndTime;

    //exibir vida
    public TextMeshProUGUI healthText; // refeencia ao componente Text


    void Start()
    {
        currentHealth = maxHealth; // setando valor inicial da vida do jogador (100)
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        healthText = GameObject.Find("text").GetComponent<TextMeshProUGUI>(); // busca o gameObject pelo Find() e seta na variavel

    }

    void UpdateHealthText()
    {
        healthText.text = currentHealth.ToString(); // atualiza o texto com a vaiavel
    }

    void Update()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, 0f); //não deixa que o personagem tenha uma rotação nos eixos x,y,z | Quartenion = metodo de referencia a rotação
        direction = Input.GetAxis("Horizontal"); // seta na variavel inputs do teclado como = a-d ou sentinhas direcionais

        //condicao do dash

        if (Input.GetKeyDown(KeyCode.Q) && !isDashing && Time.time >= dashCooldownEndTime) // input da tecla "Q" ativa o dashing
        {
            animator.SetBool("isDashing", true);
            isDashing = true; // ativa o dashing
            dashEndTime = Time.time + dashDuration; // duarcao do dashing
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

        Debug.Log(currentHealth);
    }

    void OnCollisionEnter2D(Collision2D collision) // metodo de colisao, adicionando uma tag do GameObject como parametro
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            //animator.SetBool("")
            canJump = true; // Permite o pulo novamente ao tocar no chão (ground)
        }
    }

    void FixedUpdate() // funcao de execucao de intevalos reculares (0.02s)- segundos
    {
        if (isDashing)
        {

            if (Time.time < dashEndTime)
            {
                rig.velocity = new Vector2(direction * dashSpeed, rig.velocity.y);

            }
            else
            {
                animator.SetBool("isDashing", false); // desliga a animação do dashing
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

    //funcao para tomar dano
    public void TakeDamege(int damageAmout)
    {
        currentHealth = currentHealth - damageAmout; //subritai o dano autual coma vida atual do player
        UpdateHealthText(); //chama o metodo e atualiza  variavel a cada dano tomado

        if(currentHealth <= 0) // se o  hp do personagem for menor ou igaul a 0
        {
            Die(); // persnagem morre

             // colocarei a animaçaõ em breve

        }
    }

    void Die()
    {


    }
}
