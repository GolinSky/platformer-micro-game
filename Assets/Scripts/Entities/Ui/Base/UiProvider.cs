using UnityEngine;

namespace Mario.Entities.Ui.Base
{
    public interface IUiProvider
    {
        Transform Root { get; }
    }
    public class UiProvider : MonoBehaviour, IUiProvider
    {
        [field: SerializeField] public Transform Root { get; private set; }
    }
}