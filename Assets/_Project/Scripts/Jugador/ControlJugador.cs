using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D),typeof(Animator))]
public class ControlJugador : MonoBehaviour
{
    [Header("Configuración")] 
    [SerializeField] private float velocidadMovimiento = 86;

    [Header("Teclas")]
    [SerializeField] private KeyCode teclaArriba = KeyCode.W;
    [SerializeField] private KeyCode teclaIzquierda = KeyCode.A;
    [SerializeField] private KeyCode teclaAbajo = KeyCode.S;
    [SerializeField] private KeyCode teclaDerecha = KeyCode.D;
    [SerializeField] private KeyCode teclaUsar = KeyCode.E;

    private Rigidbody2D _rigidbody;
    private CircleCollider2D _collider;
    private Animator _animator;

    private int Arriba => ValorDeTecla(teclaArriba);
    private int Izquierda => ValorDeTecla(teclaIzquierda);
    private int Abajo => ValorDeTecla(teclaAbajo);
    private int Derecha => ValorDeTecla(teclaDerecha);

    /// <summary>
    /// Dada una tecla, devuelve 1 si está siendo presionada, de lo contrario 0.
    /// Para esto se utiliza la condición ternaria: (booleano ? valor_si_verdadero : valor_si_falso)
    /// </summary>
    /// <param name="tecla">La tecla sobre la que queremos saber si está siendo presionada.</param>
    /// <returns>1 si está siendo presionada, de lo contrario 0.</returns>
    private int ValorDeTecla(KeyCode tecla) => Input.GetKey(tecla) ? 1 : 0;
    
    /// <summary>
    /// Devuelve un vector indicando la dirección a mover.
    /// El primer argumento es el eje X, las direcciones izquierda y derecha. El movimiento hacia la izquierda siempre es negativo.
    /// El segundo argumento es el eje Y, las direcciones arriba y abajo. El movimiento hacia abajo siempre es negativo.
    /// Se normaliza el resultado de dirección para que la velocidad no sea mayor cuando se mueva en diagonal por ejemplo.
    /// </summary>
    private Vector2 DireccionAMover => new Vector2(-Izquierda + Derecha, Arriba - Abajo).normalized;

    // Esta función se ejecuta cada vez que se modifica un parámetro en el inspector.
    private void OnValidate()
    {
        
    }
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<CircleCollider2D>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        // Se evita que el usuario pueda moverse mientras mantenga presionada la tecla de usar.
        if(!Input.GetKey(teclaUsar))
            AplicarMovimiento();
        
        if(Input.GetKeyDown(teclaUsar))
            Usar();
    }

    /// <summary>
    /// Aplica fuerzas al personaje para dirigirlo usando las teclas de movimiento.
    /// </summary>
    private void AplicarMovimiento()
    {
        _rigidbody.velocity += DireccionAMover * velocidadMovimiento * Time.deltaTime;
        
        // Animar en base a la dirección de movimiento
        _animator.SetFloat("x", DireccionAMover.x);
        _animator.SetFloat("y", DireccionAMover.y);
    }

    /// <summary>
    /// Hace uso de algún objeto con el que se pueda interactuar.
    /// </summary>
    private void Usar()
    {
        Collider2D[] castHit = Physics2D.OverlapCircleAll(transform.position, _collider.radius);

        foreach (Collider2D collider in castHit)
        {
            if (collider.TryGetComponent(out IUsable usable))
                usable.Usar();
        }
    }
}
