using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GameAreas
{
    public sealed class GameArea
    {
        public Vector2 Size { get; }

        private readonly Transform _leftBorder;
        private readonly Transform _rightBorder;
        private readonly Transform _topBorder;
        private readonly Transform _downBorder;

        private readonly Camera _camera;
        private readonly Vector2 _halfSize;

        private List<Transform> _borders;

        public GameArea(Camera camera, Transform leftBorder,Transform rightBorder, 
            Transform topBorder, Transform downBorder)
        {
            _camera = camera;

            _leftBorder = leftBorder;
            _rightBorder = rightBorder;
            _topBorder = topBorder;
            _downBorder = downBorder;
            
            _halfSize = new Vector2(_camera.orthographicSize * _camera.aspect, _camera.orthographicSize);
            Size = _halfSize * 2;
        }

        private void InitBorders()
        {
            _borders = new List<Transform>
            {
                _leftBorder,
                _rightBorder,
                _topBorder,
                _downBorder
            };
        }

        public Vector2 GetRandomStartPosition()
        {
            InitBorders();
            
            var randomValue = Random.Range(0, _borders.Count - 1);
            var startPosition = GetPosition(_borders[randomValue]);

            _borders.Remove(_borders[randomValue]);
            
            return startPosition;
        }

        public Vector2 GetRandomEndPosition()
        {
            var randomValue = Random.Range(0, _borders.Count - 1);
            var endPosition = GetPosition(_borders[randomValue]);

            return endPosition;
        }

        private Vector2 GetPosition(Transform transform)
        {
            if (transform == _leftBorder || transform == _rightBorder)
            {
                var newPosition = GetBorderYPosition(transform);
                return newPosition;
            }

            if (transform == _topBorder || transform == _downBorder)
            {
                var newPosition = GetBorderXPosition(transform);
                return newPosition;
            }

            throw new NotImplementedException("Все сломалось");
        }

        private Vector2 GetBorderXPosition(Transform transform)
        {
            var randomX = Random.Range(-_halfSize.x , _halfSize.x);

            var newPosition = new Vector2(randomX, transform.position.y);
            
            return newPosition;
        }

        private Vector2 GetBorderYPosition(Transform transform)
        {
            var randomY = Random.Range(-_halfSize.y , _halfSize.y);

            var newPosition = new Vector2(transform.position.x, randomY);

            return newPosition;
        }

        public void PlaceObject(Transform transform, Vector2 startPosition)
        {
            transform.position = startPosition;
        }
    }
}