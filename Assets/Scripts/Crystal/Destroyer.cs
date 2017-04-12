using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
public class Destroyer : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, transform.localScale);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject);
    }
}
