using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Managers;
using UnityEngine;

namespace Walls
{
    public class WallWithObstacle : WallBase
    { 
        [SerializeField] private List<Obstacle> _obtacles = new List<Obstacle>();
        private Stack<Obstacle> _shuffledObtacles = new Stack<Obstacle>();
        private Stack<Obstacle> _activeObtacles = new Stack<Obstacle>();

        [Header("Particle Settings")]
        [SerializeField] private ParticleSystem _hitParticle;

        private void Start()
        {
            if (_obtacles.Count <= 0) return;
            StartCoroutine(ActivateObtacles(0.1f));
        }

        public void HitTheWall()
        {
            if (_obtacles.Count <= 0) return;
            _hitParticle?.Play();
            EventBus.OnHitTheWallEvent();
            StartCoroutine(ActivateObtacles(0.5f));
        }

        private IEnumerator ActivateObtacles(float delay)
        {
            StartCoroutine(DeactivateObtacles());
            ShuffleObtacles();
            
            yield return new WaitForSeconds(delay);
            
            for (int i = 0; i < 4; i++)
            {
                Obstacle obstacle = _shuffledObtacles.Pop();
                obstacle.Activate();
                _activeObtacles.Push(obstacle);
                yield return new WaitForSeconds(0.1f);
            }
        }
        
        private IEnumerator DeactivateObtacles()
        {
            while (_activeObtacles.Count > 0)
            {
                Obstacle obstacle = _activeObtacles.Pop();
                obstacle.Deactivate();
                yield return new WaitForSeconds(0.1f);
            }
        }
        
        private void ShuffleObtacles()
        {
            Obstacle[] array = _obtacles.ToArray();

            var rnd = new System.Random();
            var shuffledArray = array.OrderBy(item => rnd.Next()).ToArray();

            _shuffledObtacles.Clear(); 

            foreach (var obstacle in shuffledArray)
            {
                _shuffledObtacles.Push(obstacle);
            }
        }
    }
}