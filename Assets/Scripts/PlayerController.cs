using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 3f; // Velocidad del fantasma
    private Rigidbody2D rb;      // Referencia al Rigidbody
    private Vector2 movement;    // Dirección de movimiento

    void Start()
    {
        // Obtenemos el Rigidbody de Luma
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Capturamos input (WASD o flechas)
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        // Movemos a Luma aplicando física
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}