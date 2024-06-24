using System;
using UnityEngine;

namespace Componentes
{
    public class ComponenteGenerador : ComponenteBinario
    {
        [SerializeField] private ComponenteBinario[] componentes;
        [SerializeField] private ComponenteBinario interruptor;

        protected override void EstadoEncendido()
        {
            base.EstadoEncendido();
            HabilitarMultiplesCompnentes(interruptor.Encendido);
        }

        protected override void EstadoApagado()
        {
            base.EstadoApagado();
            HabilitarMultiplesCompnentes(interruptor.Encendido);
        }

        private void HabilitarMultiplesCompnentes(bool value)
        {
            foreach (ComponenteBinario componente in componentes)
                componente.Encender(value);
        }

        private void OnEnable()
        {
            interruptor.OnEncender += InterruptorOnOnEncender;
            interruptor.OnApagar += InterruptorOnOnApagar;
        }
        
        private void OnDisable()
        {
            interruptor.OnEncender -= InterruptorOnOnEncender;
            interruptor.OnApagar -= InterruptorOnOnApagar;
        }

        private void InterruptorOnOnApagar(object sender, EventArgs e) => Encender(interruptor.Encendido);

        private void InterruptorOnOnEncender(object sender, EventArgs e) => Encender(interruptor.Encendido);

        private void OnDrawGizmosSelected()
        {
            if (!interruptor) return;
            Gizmos.color = Color.magenta;
            Gizmos.DrawLine(transform.position, interruptor.transform.position);

            if (componentes.Length == 0) return;
            Gizmos.color = Color.white;
            foreach (ComponenteBinario componente in componentes)
                Gizmos.DrawLine(transform.position, componente.transform.position);
        }
    }
}