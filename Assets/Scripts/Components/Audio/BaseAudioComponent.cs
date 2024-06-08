using LightWeightFramework.Model;
using Mario.Components.Base;
using Mario.Services;
using UnityEngine;
using Zenject;

namespace Mario.Components.Audio
{
    public abstract class BaseAudioComponent<TAudioModel>: Component<TAudioModel>, IAudioObserver, IInitializable, ILateDisposable
        where TAudioModel : BaseAudioModel
    {
        protected readonly IAudioService audioService;

        protected BaseAudioComponent(IModel rootModel, IAudioService audioService) : base(rootModel)
        {
            this.audioService = audioService;
        }

        public void Initialize()
        {
            audioService.AddSoundObserver(this);
            Update(audioService.SoundSettings);
            OnInit();
        }
        
        public void LateDispose()
        {
            audioService.RemoveSoundObserver(this);    
            OnRelease();
        }

        public void Update(IAudioSettings state)
        {
            Model.ApplySettings(state);
        }
        
        protected void PlayClip(AudioClip clip)
        {
            Model.PlayOneShot(clip);
        }
        
        protected virtual void OnInit(){}
        protected virtual void OnRelease(){}
    }
}