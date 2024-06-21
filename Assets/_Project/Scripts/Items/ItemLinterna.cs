using System;
using Componentes;
using UnityEngine;

namespace _Project.Scripts.Items
{
    [RequireComponent(typeof(ComponenteLampara))]
    public class ItemLinterna : Item
    {
        private ComponenteLampara _lampara;

        private void Awake()
        {
            _lampara = GetComponent<ComponenteLampara>();
        }

        public override void Utilizar(Inventario inventario)
        {
            base.Utilizar(inventario);
            _lampara.Encender(!_lampara.Encendido);
        }
    }
}