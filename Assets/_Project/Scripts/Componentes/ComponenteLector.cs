using System;
using UnityEngine;

namespace Componentes
{
    public class ComponenteLector : ComponenteBinario
    {
        [SerializeField] private Item itemAValidar;
        
        [Header("Sonido")]
        [SerializeField] private AudioClip sfxValido;
        [SerializeField] private AudioClip sfxError;

        public void ValidarTarjeta(Item tarjeta)
        {
            if (tarjeta != itemAValidar)
            {
                AudioSource.PlayClipAtPoint(sfxError, transform.position);
                return;
            }
            AudioSource.PlayClipAtPoint(sfxValido, transform.position);
            Encender(true);
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawLine(transform.position, itemAValidar.transform.position);
        }
    }
}