using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Item : MonoBehaviour
{
    public event Action<Item> OnUtilizar;

    [Header("Configuración")] 
    [SerializeField] private Vector2 desplazamiento = new Vector2(0.5f, 0.5f);
    [SerializeField] private bool usarRotacionDelJugador = false;
    
    [Header("Sonido")] 
    [SerializeField] private AudioClip sfxPickup;
    [SerializeField] private AudioClip sfxDrop;

    public ControlJugador Jugador { get; private set; }
    protected Vector2 _lastPosition;

    private bool _habilitado = true;

    public bool EstaSiendoUsado() => Jugador;

    public void EstablecerPosicion(Vector2 posicion) => _lastPosition = posicion;
    
    public void Usar(ControlJugador usuario)
    {
        Jugador = usuario;
        AudioSource.PlayClipAtPoint(sfxPickup, transform.position);
    }

    public void DejarDeUsar(ControlJugador usuario)
    {
        
    }

    public void Soltar()
    {
        //_lastPosition = Jugador.transform.position;
        EstablecerPosicion(Jugador.transform.position);
        Jugador = null;
        AudioSource.PlayClipAtPoint(sfxDrop, transform.position);
        transform.DOJump(_lastPosition, 1, 1, 0.5f);
    }

    public void Deshabilitar()
    {
        _habilitado = false;
        if (TryGetComponent(out SpriteRenderer spriteRenderer))
            spriteRenderer.color = new Color(1, 1, 1, 0.2f);
        if (TryGetComponent(out Collider2D collider))
            collider.enabled = false;
    }

    public void Habilitar()
    {
        _habilitado = true;
        if (TryGetComponent(out SpriteRenderer spriteRenderer))
            spriteRenderer.color = new Color(1, 1, 1, 1);
        if (TryGetComponent(out Collider2D collider))
            collider.enabled = true;
    }

    public virtual void Utilizar(Inventario inventario)
    {
        OnUtilizar?.Invoke(this);
    }
    
    private void Start()
    {
        EstablecerPosicion(transform.position);
        // _lastPosition = transform.position;
    }

    private void Update()
    {
        if (EstaSiendoUsado())
        {
            transform.position = Vector2.Lerp(transform.position,
                (Vector2)Jugador.transform.position + desplazamiento, Time.deltaTime * 10);
            UsarRotacionDelJugador();
        }
        else HoveringEffect();
    }

    private void UsarRotacionDelJugador()
    {
        if (usarRotacionDelJugador && Jugador.DireccionAMover != Vector2.zero)
            transform.up = Vector2.Lerp(transform.up, Jugador.DireccionAMover, Time.deltaTime * 10);
    }

    private void HoveringEffect()
    {
        if (!_habilitado) return;
        
        float oscillationValue = Mathf.Sin(Time.time) * 0.25f;
        Vector2 targetPosition = new Vector2(_lastPosition.x, _lastPosition.y + oscillationValue);
        transform.position = Vector2.Lerp(transform.position, targetPosition, Time.deltaTime * 10);
    }
}
