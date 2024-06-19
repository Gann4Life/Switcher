using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Item : MonoBehaviour
{
    [Header("Sonido")]
    [SerializeField] private AudioClip sfxPickup;
    [SerializeField] private AudioClip sfxDrop;
    
    protected ControlJugador _jugador;
    protected Vector2 _lastPosition;

    private bool _habilitado = true;
    
    public void Usar(ControlJugador usuario)
    {
        _jugador = usuario;
        AudioSource.PlayClipAtPoint(sfxPickup, transform.position);
    }

    public void DejarDeUsar(ControlJugador usuario)
    {
        
    }

    public void Soltar()
    {
        _lastPosition = _jugador.transform.position;
        _jugador = null;
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
        
    }
    
    private void Start()
    {
        _lastPosition = transform.position;
    }

    private void Update()
    {
        if (_jugador)
            transform.position = Vector2.Lerp(transform.position, (Vector2)_jugador.transform.position + new Vector2(0.5f, 0.5f), Time.deltaTime*10);
        else
            HoveringEffect();
    }

    private void HoveringEffect()
    {
        if (!_habilitado) return;
        
        float oscillationValue = Mathf.Sin(Time.time) * 0.25f;
        Vector2 targetPosition = new Vector2(_lastPosition.x, _lastPosition.y + oscillationValue);
        transform.position = Vector2.Lerp(transform.position, targetPosition, Time.deltaTime * 10);
    }
}
