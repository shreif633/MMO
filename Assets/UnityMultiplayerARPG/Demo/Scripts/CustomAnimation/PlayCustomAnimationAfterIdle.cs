using UnityEngine;

namespace MultiplayerARPG
{
    [RequireComponent(typeof(BaseGameEntity))]
    public class PlayCustomAnimationAfterIdle : MonoBehaviour
    {
        public float delayAfterIdle = 3f;
        public int animationId = 0;

        private BaseGameEntity _entity;
        private float _countDown = 0f;
        private bool _played = false;

        private void Start()
        {
            _entity = GetComponent<BaseGameEntity>();
            _countDown = delayAfterIdle;
        }

        private void Update()
        {
            if (!_entity.IsServer)
                return;

            if (_entity.MovementState.HasDirectionMovement())
            {
                _countDown = delayAfterIdle;
                _played = false;
                return;
            }

            _countDown -= Time.deltaTime;
            if (_countDown <= 0)
            {
                if (!_played)
                {
                    _entity.CallAllPlayCustomAnimation(animationId);
                    _countDown = (_entity.Model as ICustomAnimationModel).GetCustomAnimationClip(animationId).length;
                    _played = true;
                }
                else
                {
                    _countDown = delayAfterIdle;
                    _played = false;
                }
            }
        }
    }
}
