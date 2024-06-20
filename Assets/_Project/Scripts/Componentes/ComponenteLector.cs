using _Project.Scripts.Items;
using UnityEngine;

namespace Componentes
{
    public class ComponenteLector : ComponenteBinario
    {
        [Tooltip("Preferiblemente, que sea un item con sprite de tarjeta.")]
        [SerializeField] private Item itemAValidar;
        
        [Header("Sonido")]
        [SerializeField] private AudioClip sfxValido;
        [SerializeField] private AudioClip sfxError;

        private Item _itemIntroducido;

        public void ValidarTarjeta(Item tarjeta)
        {
            bool esValido = tarjeta == itemAValidar;
            AudioSource.PlayClipAtPoint(esValido ? sfxValido : sfxError, transform.position);
            tarjeta.Jugador.Inventario.AccionSoltarItem();
            Encender(esValido);
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Item tarjeta))
            {
                _itemIntroducido = tarjeta;
                _itemIntroducido.OnUtilizar += ValidarTarjeta;
            }

        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent(out Item tarjeta))
            {
                if (tarjeta != _itemIntroducido) return;
                _itemIntroducido.OnUtilizar -= ValidarTarjeta;
                _itemIntroducido = null;
            }
        }

        private void OnDrawGizmos()
        {
            if (!itemAValidar) return;
            Gizmos.DrawLine(transform.position, itemAValidar.transform.position);
        }
    }
}