using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform player; // O transform do personagem que a c�mera deve seguir

    private void FixedUpdate()
    {
        transform.position = Vector2.Lerp(transform.position, player.position, 0.1f);

    }
}