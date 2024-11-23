using System;
using UnityEngine;

namespace Minigames.CrazyControls
{
    public class CrazyCondicion : MonoBehaviour
    {
        private Collider2D _collider2D;
        private SpriteRenderer _renderer;

        public enum type
        {
            Stun,
            Confuse
        }

        private void Start()
        {
            _renderer = GetComponent<SpriteRenderer>();
            _collider2D = GetComponent<Collider2D>();
        }

        public type currentType;

        private float _countDown;
    
        private void Update()
        {
            if (_countDown < 0)
            {
                _renderer.color=Color.white;
                _collider2D.enabled = true;
            }

            _countDown -= Time.deltaTime;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            Debug.Log("collide");
            if (other.gameObject.TryGetComponent<PlayerStateController>(out var player))
            {
                switch (currentType)
                {
                    case type.Stun:
                        player.ChangeState(player.stunState);
                        break;
                    case type.Confuse:
                        player.ChangeState(player.confuseState);
                        break;
                }
                _renderer.color = Color.gray;
                _collider2D.enabled = false;
                _countDown = 5;
            }

            
        }
    }
}

