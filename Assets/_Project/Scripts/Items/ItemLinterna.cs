using Componentes;

namespace _Project.Scripts.Items
{
    public class ItemLinterna : Item
    {
        private ComponenteLampara _lampara;

        public override void Utilizar(Inventario inventario)
        {
            base.Utilizar(inventario);
            _lampara.Encender(!_lampara.Encendido);
        }
    }
}