using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Serialization;

namespace Componentes
{
    public class ComponenteLampara : ComponenteBinario
    {
        [Header("Config")] 
        [SerializeField] private bool detectarAlPresionar = true;
        [SerializeField] private bool detectarAlEncender = true;
        [SerializeField] private bool detectarAlApagar = true;
        
        [Header("Conexión")]
        [SerializeField] private ComponenteBinario interruptor;

        private Light2D _light;
        
        private void OnEnable()
        {
            interruptor.OnEncender += OnEncender;
            interruptor.OnApagar += OnApagar;
            interruptor.OnPresionar += OnPresionar;
        }

        private void OnDisable()
        {
            interruptor.OnEncender -= OnEncender;
            interruptor.OnApagar -= OnApagar;
            interruptor.OnPresionar -= OnPresionar;
        }

        protected override void EstadoEncendido()
        {
            base.EstadoEncendido();
            _light.enabled = Encendido;
        }

        protected override void EstadoApagado()
        {
            base.EstadoApagado();
            _light.enabled = Encendido;
        }

        private void OnEncender(object sender, EventArgs e)
        {
            if (!detectarAlEncender) return;
            Encender(interruptor.Encendido);
        }

        private void OnApagar(object sender, EventArgs e)
        {
            if (!detectarAlApagar) return;
            Encender(interruptor.Encendido);
        }

        private void OnPresionar(object sender, EventArgs e)
        {
            if (!detectarAlPresionar) return;
            Encender(interruptor.Encendido);
        }

        private void Awake()
        {
            _light = GetComponentInChildren<Light2D>();
        }

        protected override void Start()
        {
            base.Start();
            Encender(interruptor.Encendido);
            Debug.Log("FOR GODS SAKE!!!, I MUST BE " + interruptor.Encendido);
        }
    }
}