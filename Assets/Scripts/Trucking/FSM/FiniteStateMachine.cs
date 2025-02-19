using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Trucking.FSM
{
	public class FiniteStateMachine<T>
	{
		private T Owner;
		private FSMState<T> CurrentState;
		private FSMState<T> PreviousState;

		private List<FSMState<T>> fSMStates = new List<FSMState<T>>();

		public void Awake()
		{
			CurrentState = null;
			PreviousState = null;
		}

		public void Configure(T owner, FSMState<T> InitialState)
		{
			Owner = owner;
			ChangeState(InitialState);
		}

		public void Update()
		{
			if (CurrentState != null)
				CurrentState.Execute(Owner);
		}

		public void ChangeState(FSMState<T> NewState)
		{

			if (NewState == null)
			{
				return;
			}

			PreviousState = CurrentState;
			CurrentState = NewState;

			Debug.Log($"ChangeState : {CurrentState} =====> {NewState}");

			if (PreviousState != null)
				PreviousState.Exit(Owner);

			fSMStates.Clear();
			fSMStates.Add(CurrentState);

			if (CurrentState != null)
				CurrentState.Enter(Owner);
		}

		public void RevertToPreviousState()
		{
			if (PreviousState != null)
				ChangeState(PreviousState);
		}

		public FSMState<T> GetCurrentState()
		{
			return CurrentState;
		}

		public FSMState<T> GetPreviousState()
		{
			if (fSMStates.Count < 2)
			{
				return PreviousState;
			}

			return fSMStates[fSMStates.Count - 2];
		}

		public void PushState(FSMState<T> NewState)
		{
			if (NewState == null || NewState == CurrentState)
			{
				return;
			}

			Debug.Log($"PushState : {CurrentState} =====> {NewState}");


			fSMStates.Add(NewState);

			if (CurrentState != null)
			{
				CurrentState.Push(Owner);
			}

			CurrentState = NewState;

			if (CurrentState != null)
			{
				CurrentState.Enter(Owner);
			}
		}

		public void PopState(bool doEnter = true)
		{
			FSMState<T> last = fSMStates.Last();

			if (last == null)
			{
				return;
			}

//	    Debug.Log($"PopState : {last} =====> {fSMStates[fSMStates.Count - 2]}");


			if (last != null)
			{
				last.Exit(Owner);
				fSMStates.Remove(last);
			}

			CurrentState = fSMStates.Last();
			CurrentState?.Pop(Owner);
			
			if (doEnter)
			{
				//CurrentState?.Enter(Owner);
			}
		}

	};

}