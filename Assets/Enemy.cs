using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // faz a vereficação se fez a colisao com o gameObject com a Tag Player
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if (player != null) // se o player for diferente de null, executa o if
            {
                player.TakeDamege(50); // chama a funcao inserindo valo de 50 como parametro
            }
        }
    }
}