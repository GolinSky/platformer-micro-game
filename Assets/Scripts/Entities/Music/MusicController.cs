using LightWeightFramework.Controller;
using Mario.Services;
using Zenject;

namespace Mario.Entities.Music
{
    public class MusicController : Controller<MusicModel>, IInitializable, ILateDisposable, IAudioObserver
    {
        private readonly IAudioService audioService;

        public MusicController(MusicModel model, IAudioService audioService) : base(model)
        {
            this.audioService = audioService;
        }

        public void Initialize()
        {
            audioService.AddMusicObserver(this);
            Update(audioService.MusicSettings);
            Model.PlayClip(Model.MusicClip);
        }

        public void LateDispose()
        {
            audioService.RemoveMusicObserver(this);
        }

        public void Update(IAudioSettings state)
        {
            Model.ApplySettings(state);
        }
    }
}