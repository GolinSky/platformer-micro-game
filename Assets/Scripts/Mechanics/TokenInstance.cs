using Mario.Components.Health;
using Mario.Services;
using Platformer.Gameplay;
using UnityEngine;
using Zenject;
using static Platformer.Core.Simulation;


namespace Platformer.Mechanics
{
    /// <summary>
    /// This class contains the data required for implementing token collection mechanics.
    /// It does not perform animation of the token, this is handled in a batch by the 
    /// TokenController in the scene.
    /// </summary>
    [RequireComponent(typeof(Collider2D))]
    public class TokenInstance : MonoBehaviour
    {
        public AudioClip tokenCollectAudio;
        [Tooltip("If true, animation will start at a random position in the sequence.")]
        public bool randomAnimationStartTime = false;
        [Tooltip("List of frames that make up the animation.")]
        public Sprite[] idleAnimation, collectedAnimation;

        internal Sprite[] sprites = new Sprite[0];

        internal SpriteRenderer _renderer;

        //unique index which is assigned by the TokenController in a scene.
        internal int tokenIndex = -1;
        internal TokenController controller;
        //active frame in animation, updated by the controller.
        internal int frame = 0;
        internal bool collected = false;

        [Inject]
        private IAudioService AudioService { get; }
        
        private void Awake()
        {
            _renderer = GetComponent<SpriteRenderer>();
            if (randomAnimationStartTime)
                frame = Random.Range(0, sprites.Length);
            sprites = idleAnimation;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            IHealthViewComponent healthViewComponent = other.GetComponent<IHealthViewComponent>();
            if (healthViewComponent is { IsDead: false, IsPlayer: true })
            {
                OnPlayerEnter();
            }
        }

        private void OnPlayerEnter()
        {
            if (collected) return;
            frame = 0;
            sprites = collectedAnimation;
            collected = true;

            
            AudioService.PlayClipAtPoint(tokenCollectAudio, transform.position);

        }
    }
}