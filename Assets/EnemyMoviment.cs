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
    public float jumpDetectionDistance = 1.0f; //distancia de detecção para fazer o pulo;
    public float antecipationTime = 0.7f; // tempo para antecipar o pulo
    private Rigidbody2D rb;
    private bool isAntecipatingJump = false;


    //animação do inimigo

    public Animator animator;
    


    private void Start()
    {
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
            animator.SetBool("isRunnig", false);

            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, jumpDetectionDistance);

            if (!isAntecipatingJump && hit.collider != null && hit.collider.CompareTag("ground"))
            {
                StartCoroutine(AnticipateJump());

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
