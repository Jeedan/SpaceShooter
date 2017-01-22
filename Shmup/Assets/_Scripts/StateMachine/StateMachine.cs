using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    [SerializeField]
    public Stack<IState> statesStack = new Stack<IState>();
    public Dictionary<string, IState> mStates = new Dictionary<string, IState>();

    private IState currState;

    // Use this for initialization
    public StateMachine()
    {
    }

    public IState currentState()
    {
        if (statesStack.Count > 0)
        {
            currState = statesStack.Peek();
            return currState;
        }

        return null;
    }

    public void OnUpdate()
    {
        currentState().OnUpdate();
    }

    public void ChangeState(string name)
    {
        IState newState = mStates[name];
        if (newState != null && newState != currentState()) // if something is wrong check currentState()
        {
            PushState(name);
        }
        else
        {
            Debug.Log(currentState().ToString() + " " + name);
            Debug.Log("dict count: " + mStates.Count);
            Debug.LogError("State does not exist, or we are already in the state");
        }
    }

    public void PushState(string name)
    {
        IState state = mStates[name];
        IState prevState = currentState();

        if (state != null && state != currentState())
        {
            if (statesStack.Count > 0)
            {
                //statesStack.Pop().OnExit();
                prevState.OnExit();
            }

            statesStack.Push(state);
            currentState().OnEnter();
        }
    }

    public void PopState(string key)
    {
        Debug.Log("Before dict count: " + mStates.Count);
        IState popState = Pop();
        popState.OnExit();
        currentState().OnEnter();
        mStates.Remove(key);
        Debug.Log("After dict count: " + mStates.Count);
    }

    private IState Pop()
    {
        if (statesStack.Count > 0)
            return statesStack.Pop();
        else return null;
    }

    public void AddState(string name, IState state)
    {
        if (!mStates.ContainsKey(name) && state != null)
        {
            mStates.Add(name, state);
        }
    }
}
