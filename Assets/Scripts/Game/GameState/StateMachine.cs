using System.Collections.Generic;
using UnityEngine;

namespace GameState
{
    public class StateMachine<T> : MonoBehaviour
    {
        private readonly Dictionary<T, State<T>> _stateList = new Dictionary<T, State<T>>();

        private State<T> CurrentState { get; set; }

        protected void GoToState(T nextStateId)
        {
            if (!_stateList.ContainsKey(nextStateId))
            {
                Debug.LogErrorFormat("error: not exist state: {0}", nextStateId);
                return;
            }

            CurrentState?.CleanUp();

            CurrentState = _stateList[nextStateId];
            CurrentState.SetUp();
        }

        protected void AddState(State<T> state)
        {
            var stateId = state.Id;
            if (_stateList.ContainsKey(stateId))
            {
                Debug.LogErrorFormat("error: already exist state: {0}", stateId);
                return;
            }

            _stateList.Add(stateId, state);
        }

        protected void Update()
        {
            CurrentState?.Update();
        }

        public bool IsStateSame(T stateId)
        {
            return 
                CurrentState != null &&
                CurrentState.Id.Equals(stateId);
        }
    }
}
