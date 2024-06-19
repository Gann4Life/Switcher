using System;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
[RequireComponent(typeof(ControlJugador))]
public class Inventario
{
    public event Action<ControlJugador> OnPickup;
    public event Action<ControlJugador> OnDrop;

    public float tiempoRequeridoParaSoltar = 1;
    
    private ControlJugador _usuario;
    private Item _item;
    private Slider _UISliderReleaseIndicator;
    
    public float ReleaseTime { get; private set; }
    private bool IsHoldingUseKey => Input.GetKey(_usuario.teclaUsar);

    public Inventario(ControlJugador usuario)
    {
        _usuario = usuario;
        _UISliderReleaseIndicator = _usuario.GetComponentInChildren<Slider>();
    }
    
    public void HandleInventory()
    {
        _UISliderReleaseIndicator.gameObject.SetActive(_item && IsHoldingUseKey);
        if (IsHoldingUseKey) // Count time while is pressing the key
        {
            _UISliderReleaseIndicator.value = ReleaseTime;
            ReleaseTime += Time.deltaTime;
        }
        else if (ReleaseTime != 0) // Once it's released, reset the time.
            ReleaseTime = 0;
        
        if (ReleaseTime > tiempoRequeridoParaSoltar)
            _item?.Utilizar(this);
        
        if(Input.GetKeyDown(_usuario.teclaUsar))
            AccionTomarItem();
    }

    public void AccionTomarItem()
    {
        Item nuevoItem = ObtenerItem();
        // No hacer nada si el item es el mismo que ya tenemos o intenta reemplazarlo por la misma nada.
        if (nuevoItem == _item || !nuevoItem) return;
        
        SoltarItem();
        _item = nuevoItem;
        _item?.Usar(_usuario);
        OnPickup?.Invoke(_usuario);
    }
    
    public void AccionSoltarItem()
    {
        _UISliderReleaseIndicator.value = ReleaseTime;
        SoltarItem();
        _item = null;
        OnDrop?.Invoke(_usuario);
    }

    private void SoltarItem() => _item?.Soltar();
    
    private Item ObtenerItem()
    {
        Item item = null;
        Collider2D[] castHit = Physics2D.OverlapCircleAll(_usuario.transform.position, _usuario.Collider.radius);
        foreach (Collider2D collider in castHit)
        {
            if (collider.TryGetComponent(out Item objetoUsable))
            {
                if (objetoUsable == _item) continue;
                item = objetoUsable;
            }
        }

        return item;
    }
}