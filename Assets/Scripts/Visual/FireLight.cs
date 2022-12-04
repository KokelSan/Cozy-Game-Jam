using UnityEngine;
using UnityEngine.Assertions;

namespace Cozy
{
    [RequireComponent(typeof(Light))]
    public class FireLight : MonoBehaviour
    {
        [Header("Light Intensity Perturbation")]
        [Tooltip("Frequency of the perturbation in Hertz. (Hz)")]
        [SerializeField] private float _frequency;
        [SerializeField] private float _magnitude;

        private Light _light;
        private float _baseIntensity;

        void Start()
        {
            _light = GetComponent<Light>();
            Assert.IsNotNull(_light);

            _baseIntensity = _light.intensity;
        }

        void Update()
        {
            _light.intensity = _baseIntensity + GetPerturbation(_magnitude, _frequency, Time.time);
        }

        private float GetPerturbation(float magnitude, float frequency, float time)
        {
            return Mathf.Sin(2 * Mathf.PI * frequency * time) * magnitude;
        }
    }

}

