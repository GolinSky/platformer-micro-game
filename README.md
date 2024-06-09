
# Platform test project

Project is created for Technical Assignment

Project using [zenject](https://github.com/modesttree/Zenject) to create MVC base architectures
## Demo


[Release build](https://github.com/GolinSky/platformer-micro-game/releases/tag/Release-1.0)

## Documentation

Project use next custom package:

[com.script-utils.dotween](https://github.com/GolinSky/ScriptUtilities.git?path=/Assets/ScriptUtils/Dotween) - Tween extensions

[com.light-weight-framework](https://github.com/GolinSky/LightWeightFramework.git?path=/Assets/LightWeightFramework) - MVC-based solution 

[com.script-utils.editor-serialization](https://github.com/GolinSky/ScriptUtilities.git?path=/Assets/ScriptUtils/EditorSerialization) - Dictionary serialization solution for using dictionaries in editor

[com.script-utils.math](https://github.com/GolinSky/ScriptUtilities.git?path=/Assets/ScriptUtils/Math) - Math extension 

[com.script-utils.time](https://github.com/GolinSky/ScriptUtilities.git?path=/Assets/ScriptUtils/Time) - Timer solutions
 
## Code

## Entry point


ProjectInstaller is main DIC container which stores all main services of application

```c#
   public class ProjectInstaller: MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .BindInterfacesNonLazy<AddressableRepository>()
                .BindInterfaces<SceneService>()
                .BindInterfaces<SaveDataService>();

            IRepository repository = Container.Resolve<IRepository>();
            Container.BindModel<SceneModel>(repository);
        }
    }
```

### Base systems


SceneService loading scenes in whole Project

```c#
    public interface ISceneService : IService
    {
        void LoadScene(SceneType sceneType);
    }
    
    public class SceneService: Service, ISceneService, ILoadingCommand
    {
        private readonly ISceneModelObserver _sceneModelObserver;

        public SceneService(ISceneModelObserver sceneModelObserver)
        {
            _sceneModelObserver = sceneModelObserver;
            Application.backgroundLoadingPriority = ThreadPriority.High;// need to be to load async stuff faster
        }

        public void LoadScene(SceneType sceneType)
        {
            SceneManager.LoadSceneAsync(_sceneModelObserver.GetSceneName(sceneType));
        }

        public void LoadCoreScene()
        {
            LoadScene(SceneType.Core);
        }
    }
```

SaveDataService saves/get all data, which is implemented by ISerializedDto interface

```c#
 public interface ISaveDataService: IService
    {
        void Save<TSerializedDto>(TSerializedDto serializedDto) where TSerializedDto : ISerializedDto;
        TSerializedDto Get<TSerializedDto>(string id) where TSerializedDto : ISerializedDto;
        bool HasKey(string saveDataKey);
    }
    
    public class SaveDataService: Service, ISaveDataService
    {
        public void Save<TSerializedDto>(TSerializedDto serializedDto) where TSerializedDto : ISerializedDto
        {
            string json = JsonConvert.SerializeObject(serializedDto);
            PlayerPrefs.SetString(serializedDto.Id, json);
            PlayerPrefs.Save();
        }

        public TSerializedDto Get<TSerializedDto>(string id)where TSerializedDto : ISerializedDto
        {
            string json = PlayerPrefs.GetString(id);
            TSerializedDto serializedDto = JsonConvert.DeserializeObject<TSerializedDto>(json);
            return serializedDto;
        }

        public bool HasKey(string saveDataKey)
        {
            return PlayerPrefs.HasKey(saveDataKey);
        }
    }
```
### Scene installers


CoreInstaller is core scene installer 

```c#
public class CoreInstaller : MonoInstaller
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private PositionProvider positionProvider;
    
    [Inject] private IRepository Repository { get; }

    public override void InstallBindings()
    {
        Container
            .BindFactory<Vector3, PlayerView, PlayerFacade>()
            .FromSubContainerResolve()
            .ByNewContextPrefab<PlayerInstaller>(Repository.LoadComponent<PlayerInstaller>(nameof(PlayerInstaller)));

        Container
            .BindInterfacesNonLazy<CoreService>()
            .BindInterfaces<InputService>()
            .BindInterfaces<TokenService>()
            .BindInterfaces<AudioService>();


        Container
            .BindInstance(spawnPoint)
            .WithId(TransformInjectKeys.SpawnPoint)
            .AsSingle();

        Container.BindEntity(positionProvider);
        
    }
```


UiInstaller is scene installer, which work in every scene to provide work for ui system

```c#
public class UiInstaller: Installer
    {
        private readonly IRepository repository;
        private readonly UiType uiType;
        private readonly IUiProvider uiProvider;


        public UiInstaller(IRepository repository, UiType uiType, UiProvider uiProvider)
        {
            this.repository = repository;
            this.uiType = uiType;
            this.uiProvider = uiProvider;
        }
        
        public override void InstallBindings()
        {
            //Container.BindEntity(uiType);
            Container.BindEntity(uiProvider);
            
            Container
                .BindInterfacesAndSelfTo<Ui>()
                .FromComponentInNewPrefab(repository.LoadComponent<Ui>($"{uiType}{nameof(Ui)}"))
                .UnderTransform(uiProvider.Root)
                .AsSingle();

        }
    }
```

### Dynamic/Static view injection 

BaseViewInstaller is abstraction for injection MVC entity dynamicaly(via facade) or statically(putting installer with view in scene)

```c#
 public abstract class BaseViewInstaller<TController, TModel> : MonoInstaller
        where TController : Controller<TModel>
        where TModel : Model
    {
        [SerializeField] protected bool bindViewComponents;
        [SerializeField] protected bool bindMonoComponent;

        protected View View { get; set; }
        protected IRepository Repository { get; private set; }

        protected virtual string ModelPath { get; } = typeof(TModel).Name;


        [Inject]
        public void Constructor(IRepository repository)
        {
            Repository = repository;
        }

        public sealed override void InstallBindings()
        {
            OnBeforeInjection();
            BindParameters();
            BindModel();
            BindController();
            BindComponents();
            BindView();
            ResolveView();
            BindMonoComponents();
            BindViewComponents();
        }

        protected virtual void BindViewComponents()
        {
            if (bindViewComponents)
            {
                Container.Install<ViewComponentsInstaller>(new object[] { View });
            }
        }

        protected virtual void BindMonoComponents()
        {
            if (bindMonoComponent)
            {
                Container.Install<MonoComponentInstaller>(new object[] { View.Transform });
            }
        }

        protected virtual void BindController()
        {
            Container.BindInterfaces<TController>();
        }

        protected virtual void BindModel()
        {
            Container.BindModel<TModel>(Repository, ModelPath, OnModelCreated);
        }

        protected virtual void OnModelCreated() {}
        protected virtual void BindParameters() {}
        protected virtual void BindComponents() {}
        protected virtual void OnBeforeInjection(){}
        protected abstract void BindView();
        protected abstract void ResolveView();
    }
```


## Usage/Examples

### MVC 

#### Model
Storing and working with data

```c#
using System.Collections.Generic;
using UnityEngine;

namespace LightWeightFramework.Model
{
    public abstract class Model:ScriptableObject, IModel
    {
        [SerializeField] protected List<Model> models;
   
        protected virtual void Awake()
        {
            foreach (Model innerModel in models)
            {
                AddModel(innerModel);
            }
        }
        public List<IModel> CurrentModels { get; } = new List<IModel>();
        
        public TModelObserver GetModelObserver<TModelObserver>() where TModelObserver : IModelObserver
        {
            if (this is TModelObserver modelObserver) return modelObserver;
            
            return GetModelInternal<TModelObserver>();
        }

        public TModelObserver GetModel<TModelObserver>() where TModelObserver : IModel
        {
            return GetModelInternal<TModelObserver>();
        }

        private TModelObserver GetModelInternal<TModelObserver>() 
        {
            foreach (var model in CurrentModels)
            {
                if (model is TModelObserver modelObserver)
                {
                    return modelObserver;
                }
            }

            return default;
        }

        private Model AddModel(Model model)
        {
            var instancedModel = Instantiate(model);
            CurrentModels.Add(instancedModel);
            return instancedModel;
        }
        
        protected void AddInnerModels(params InnerModel[] model) 
        {
            foreach (InnerModel innerModel in model)
            {
                innerModel.Init();
                CurrentModels.Add(innerModel);
            }
        }
 
    }
}
```

#### Controller

Storing business logic
All logic must be kept here

```c#
using LightWeightFramework.Model;


namespace LightWeightFramework.Controller
{
    public abstract class Controller : IController
    {
        public virtual string Id => GetType().Name;
        public abstract IModel GetModel();
    }

    public abstract class Controller<TModel> : Controller, IController
        where TModel : IModel
    {
        protected TModel Model { get; }

        protected Controller(TModel model)
        {
            Model = model;
        }

        public override IModel GetModel()
        {
            return Model;
        }
    }
}
```


#### View

Represent object. Must have only unity component logic parts(rigidbody, image, etc..)
Contract with IModelObserver, which only provides data from model 

```c#
   
using LightWeightFramework.Command;
using LightWeightFramework.Components;
using LightWeightFramework.Model;
using Zenject;

namespace Mario.Entities.Base
{
    public abstract class View<TModel, TCommand>:View<TModel>
        where TModel : IModelObserver
        where TCommand : ICommand
    {
        [Inject]
        protected TCommand Command { get; }
    }

    public abstract class View<TModel> : View, IInitializable, ILateDisposable
        where TModel : IModelObserver
    {
        [Inject] protected new TModel Model { get; private set; }

        public void Initialize()
        {
            Init(Model);
            OnInitialize();
        }

        public void LateDispose()
        {
            Release();
            OnDispose();
        }

        protected abstract void OnInitialize();
        protected abstract void OnDispose();
    }
}
```
