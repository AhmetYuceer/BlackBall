using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;

namespace Walls
{
    public class WallWithObstacle : WallBase
    { 
        [SerializeField] private List<Obstacle> _obtacles = new List<Obstacle>();
        private Stack<Obstacle> _shuffledObstacles = new Stack<Obstacle>();
        private Stack<Obstacle> _activeObstacles = new Stack<Obstacle>();

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
            
            foreach (var obstacle in _obtacles)
            {
                obstacle.ColliderTrigger(true);
            }
            
            _hitParticle?.Play();
            EventBus.OnHitTheWallEvent();
            StartCoroutine(ActivateObtacles(0.5f));
        }

        private IEnumerator ActivateObtacles(float delay)
        {
            yield return StartCoroutine(DeactivateObtacles());
            ShuffleObstacles();

            yield return new WaitForSeconds(delay);

            int count = Random.Range(4, 7);

            for (int i = 0; i < count; i++)
            {
                if (_shuffledObstacles.Count == 0) break;

                Obstacle obstacle = _shuffledObstacles.Pop();
                obstacle.Activate();
                _activeObstacles.Push(obstacle);
                yield return new WaitForSeconds(0.1f);
            }

            foreach (var obstacle in _obtacles)
            {
                obstacle.ColliderTrigger(false);
            }
        }
        
        private IEnumerator DeactivateObtacles()
        {
            while (_activeObstacles.Count > 0)
            {
                Obstacle obstacle = _activeObstacles.Pop();
                obstacle.Deactivate();
                yield return new WaitForSeconds(0.1f);
            }
        }
        
        private void ShuffleObstacles()
        {
            Obstacle[] array = _obtacles.ToArray();

            var rnd = new System.Random();

            for (int i = array.Length - 1; i > 0; i--)
            {
                int j = rnd.Next(i + 1);
                (array[i], array[j]) = (array[j], array[i]);
            }

            _shuffledObstacles.Clear();

            foreach (var obstacle in array)
            {
                _shuffledObstacles.Push(obstacle);
            }
        }
    }
}