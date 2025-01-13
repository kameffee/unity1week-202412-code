using System;
using System.Collections.Generic;

namespace Unity1week.Scenes
{
    public class SceneArgsContainer
    {
        private readonly Dictionary<Type, object> _sceneDataMap = new();

        public void Set<T>(T sceneData) where T : class
        {
            _sceneDataMap[typeof(T)] = sceneData;
        }

        public T Get<T>() where T : ISceneArgs
        {
            return (T)_sceneDataMap[typeof(T)];
        }

        public bool TryGet<T>(out T sceneData) where T : ISceneArgs
        {
            if (_sceneDataMap.TryGetValue(typeof(T), out var value))
            {
                sceneData = (T)value;
                return true;
            }

            sceneData = default;
            return false;
        }

        public bool TryPop<T>(out T sceneData) where T : ISceneArgs
        {
            if (_sceneDataMap.TryGetValue(typeof(T), out var value))
            {
                sceneData = (T)value;
                _sceneDataMap.Remove(typeof(T));
                return true;
            }

            sceneData = default;
            return false;
        }

        public void Remove<T>() where T : class
        {
            _sceneDataMap.Remove(typeof(T));
        }
    }
}