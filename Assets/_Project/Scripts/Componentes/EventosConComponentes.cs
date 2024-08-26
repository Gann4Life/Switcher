using System;
using System.Collections;
using System.Collections.Generic;
using Componentes;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

/// <summary>
/// Lee el estado de un componente y ejecuta eventos en base a aquellos estados.
/// </summary>
public class EventosConComponentes : MonoBehaviour
{
    [SerializeField] private ComponenteBinario componenteAVerificar;
    [Header("Eventos")]
    [SerializeField] private UnityEvent onEncenderEvento;
    [SerializeField] private UnityEvent onApagarEvento;
    [SerializeField] private UnityEvent<bool> onPresionarEvento;
    
    private void OnEnable()
    {
        componenteAVerificar.OnEncender += ComponenteAVerificarOnOnEncender;
        componenteAVerificar.OnApagar += ComponenteAVerificarOnOnApagar;
        componenteAVerificar.OnPresionar += ComponenteAVerificarOnOnPresionar;
    }

    private void OnDisable()
    {
        componenteAVerificar.OnEncender -= ComponenteAVerificarOnOnEncender;
        componenteAVerificar.OnApagar -= ComponenteAVerificarOnOnApagar;
        componenteAVerificar.OnPresionar -= ComponenteAVerificarOnOnPresionar;
    }

    private void ComponenteAVerificarOnOnEncender(object sender, EventArgs e) => onEncenderEvento.Invoke();
    private void ComponenteAVerificarOnOnApagar(object sender, EventArgs e) => onApagarEvento.Invoke();
    private void ComponenteAVerificarOnOnPresionar(object sender, EventArgs e) => onPresionarEvento.Invoke(componenteAVerificar.Encendido);

    private void OnDrawGizmosSelected()
    {
        if (!componenteAVerificar) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, componenteAVerificar.transform.position);
    }
}
