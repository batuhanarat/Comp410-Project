using UnityEngine;

namespace Runtime.Commands.Level
{
    public class OnLevelLoaderCommand
    {
        private Transform _levelHolder;

        internal OnLevelLoaderCommand(Transform levelHolder)
        {
            _levelHolder = levelHolder;
        }

        internal void Execute()
        {
            Object.Instantiate(Resources.Load<GameObject>($"Prefabs/LevelPrefabs/level"), _levelHolder,
                true);
            
        }
    }
}