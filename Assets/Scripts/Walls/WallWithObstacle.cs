using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Managers;
using UnityEngine;

namespace Walls
{
    public class WallWithObstacle : WallBase
    { 
        [SerializeField] private List<GameObject> _obtacles = new List<GameObject>();
        private Stack<GameObject> _shuffledObtacles = new Stack<GameObject>();
        private Stack<GameObject> _activeObtacles = new Stack<GameObject>();

        [Header("Particle Settings")]
        [SerializeField] private ParticleSystem _hitParticle;
        
        private void Start()
        {
            if (_obtacles.Count <= 0) return;
            StartCoroutine(ActivateObtacles(0.1f));
        }

        private void DeactivateObtacles()
        {
            while (_activeObtacles.Count > 0)
            {
                _activeObtacles.Pop().SetActive(false);
            }
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
            DeactivateObtacles();
            ShuffleObtacles();

            yield return new WaitForSeconds(delay);
        
            for (int i = 0; i < 4; i++)
            {
                GameObject obstacle = _shuffledObtacles.Pop();
                obstacle.SetActive(true);
                _activeObtacles.Push(obstacle);
            }
        }
    
        private void ShuffleObtacles()
        {
            GameObject[] array = _obtacles.ToArray();

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