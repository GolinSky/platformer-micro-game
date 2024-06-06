using LightWeightFramework.Components.Repository;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Mario.Repository
{
    public class AddressableRepository: IRepository
    {
        public TSource Load<TSource>(string key) where TSource : Object
        {
            return Addressables.LoadAssetAsync<TSource>(key).WaitForCompletion();
        }

        public TComponent LoadComponent<TComponent>(string key) where TComponent : Component
        {
            return Addressables.LoadAssetAsync<GameObject>(key).WaitForCompletion().GetComponent<TComponent>();
        }
    }
}