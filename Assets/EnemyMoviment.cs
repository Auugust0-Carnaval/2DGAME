using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class EnemyMoviment : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform target; // Referência ao transform do PlayerOne
    public float speed = 5.0f; // Velocidade de movimento do inimigo
    public float jumpForce = 7.0f; // força de pulo de enimigo
    public float jumpDetectionDistance = 10.5f; //distancia de detecção para fazer o pulo;
    public float antecipationTime = 1.7f; // tempo para antecipar o pulo
    private Rigidbody2D rb;
    private bool isAntecipatingJump = false;
    private SpriteRenderer spriteRenderer;


    //animação do inimigo
    public Animator animator;

    private Player playerScript; //refeencia a classe Player
    


    private void Start()
    {
        playerScript = FindObjectOfType<Player>(); 
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>(); // pega dados incluidas no componente de "Animator" e seta na variavel
    }

    private void Update()
    {

        transform.rotation = Quaternion.Euler(0f, 0f, 0f); // evita que o enemigo rotacione

        if (target != null)
        {

            // Calcula a direção para o player
            Vector2 direction = (target.position - transform.position).normalized;

            //faz a vereficação do jogador em relação ao inimigo
            if(direction.x < 0) // vetor x = horizontal
            {
                spriteRenderer.flipX = true; //inverte sprite do inimigo para esqueda
            }
            else
            {
                spriteRenderer.flipX = false; // inverte sprite do inimigo para direita
            }

            //RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, jumpDetectionDistance);
            ////Debug.DrawRay(transform.position, direction * jumpDetectionDistance, Color.red);
            //Debug.DrawRay(transform.position, direction * jumpDetectionDistance, Color.red);

            //if(!isAntecipatingJump && hit.collider != null && hit.collider.CompareTag("Player"))
            //{

            //    Debug.Log("Detect box obetacule");
            //    StartCoroutine(AnticipateJump());

            //    if (playerScript != null)
            //    {
            //        playerScript.TakeDamege(50); // quantidade de dado que o player recebe
            //        var currentLife = playerScript.currentHealth.ToString();
            //        Debug.Log(currentLife);
            //    }

            //}


            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, jumpDetectionDistance);

            if (!isAntecipatingJump && hit.collider != null)
            {
                if (hit.collider.CompareTag("Player"))
                {
                    Debug.Log("Enemy detected player");
                    StartCoroutine(AnticipateJump());

                    if (playerScript != null)
                    {
                        playerScript.TakeDamege(50);
                        var currentLife = playerScript.currentHealth.ToString();
                        Debug.Log(currentLife);
                    }
                }
            }


            // Move o inimigo na direção do player
            transform.Translate(direction * speed * Time.deltaTime);
            animator.SetBool("isRunning", true);
        }
    }

    IEnumerator AnticipateJump()
    {
        isAntecipatingJump = true;
        yield return new WaitForSeconds(antecipationTime);

        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        isAntecipatingJump = false;
    }
}
