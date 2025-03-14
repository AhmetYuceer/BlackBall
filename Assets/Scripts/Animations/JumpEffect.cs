using UnityEngine;

namespace Animations
{
    public class JumpEffect : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _jumpParticleEffect;

        public void PlayParticle()
        {
            _jumpParticleEffect.Play();
        }

        public void DestroyParticle()
        {
            Destroy(transform.root.gameObject);
        }
    }
}
