using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public enum NodeState { Running, Success, Failure }

public abstract class Node
{
    public Node Parent { get; set; }
    protected NodeState state;
    protected List<Node> children = new();

    Dictionary<string, object> data = new Dictionary<string, object>();

    public void SetData(string key, object value)
    {
        data[key] = value;
    }

    public object GetData(string key)
    {
        if (data.TryGetValue(key, out object value))
            return value;
        if (Parent != null)
            return Parent.GetData(key);
        return null;
    }

    public bool TryRemoveData(string key)
    {
        if (data.Remove(key))
            return true;
        if (Parent != null)
            return Parent.TryRemoveData(key);
        return false;
    }

    public abstract NodeState Evaluate();

    public Node()
    {
        Parent = null;
        state = NodeState.Running;
    }

    public Node(List<Node> pChildren)
    {
        Parent = null;
        state = NodeState.Running;
        foreach (Node child in pChildren)
            Attach(child);
    }

    protected void Attach(Node n)
    {
        children.Add(n);
        n.Parent = this;
    }
}

public class Sequence : Node
{
    public Sequence(List<Node> n) : base(n) { }

    public override NodeState Evaluate()
    {
        foreach (Node child in children)
        {
            state = child.Evaluate();
            if (state != NodeState.Success)
            {
                return state;
            }
        }
        state = NodeState.Success;
        return NodeState.Success;
    }
}

public class Selector : Node
{
    public Selector(List<Node> n) : base(n)
    {
        if (n.Count == 0)
            throw new ArgumentException();
    }

    public override NodeState Evaluate()
    {
        foreach (Node child in children)
        {
            state = child.Evaluate();
            if (state != NodeState.Failure)
            {
                return state;
            }
        }
        state = NodeState.Failure;
        return NodeState.Failure;
    }
}

public class Inverter : Node
{
    public Inverter(List<Node> n) : base(n)
    {
        if (n.Count != 1)
            throw new ArgumentException();
    }

    public override NodeState Evaluate()
    {
        NodeState childState = children[0].Evaluate();
        if (childState == NodeState.Success)
            state = NodeState.Failure;
        else if (childState == NodeState.Failure)
            state = NodeState.Success;
        else
            state = NodeState.Running;
        return state;
    }
}

public class Tautologie : Node
{
    public Tautologie() : base() { }
    public override NodeState Evaluate()
    {
        Node root = Parent;
        while (root.Parent != null)
            root = root.Parent;
        root.SetData("prof", "Phil");


        state = NodeState.Success;
        return state;
    }
}

public class IsWithinRange : Node
{
    Transform target;
    Transform self;
    float detectionRange;

    public IsWithinRange(Transform target, Transform self, float detectionRange) : base()
    {
        this.target = target;
        this.self = self;
        this.detectionRange = detectionRange;
    }
    public override NodeState Evaluate()
    {
        state = NodeState.Failure;
        if (Vector3.Distance(target.position, self.position) <= detectionRange)
            state = NodeState.Success;
        return state;
    }
}

public class GoToTarget : Node
{
    Transform target;
    NavMeshAgent agent;
    float stoppingDistance;

    public GoToTarget(Transform target, NavMeshAgent agent, float stoppingDistance) : base()
    {
        this.target = target;
        this.agent = agent;
        this.stoppingDistance = stoppingDistance;
    }
    public override NodeState Evaluate()
    {
        if (agent.SetDestination(target.position))
            state = NodeState.Running;
        else
            state = NodeState.Failure;
        //if the distance between target and agent is inferior to the stopping distance
        if (Vector3.Distance(agent.transform.position, target.position) <= stoppingDistance)
            state = NodeState.Success;
        return state;
    }
}

public class PatrolTask : Node
{
    Transform[] destinations;
    NavMeshAgent agent;
    int currentDestination = 0;

    float waitTime = 0;
    float elapsedTime = 0;
    bool isWaiting = false;

    public PatrolTask(NavMeshAgent agent, Transform[] destinations, float waitTime) : base()
    {
        this.agent = agent;
        this.destinations = destinations;
        this.waitTime = waitTime;
    }
    public override NodeState Evaluate()
    {
        state = NodeState.Running;
        if (isWaiting)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= waitTime)
            {
                isWaiting = false;
                elapsedTime = 0;
                currentDestination = (currentDestination + 1) % destinations.Length;
            }
        }
        else
        {
            if (!agent.SetDestination(destinations[currentDestination].position))
                state = NodeState.Failure;
            if (Vector3.Distance(agent.transform.position, destinations[currentDestination].position) <= agent.stoppingDistance)
                isWaiting = true;
        }
        return state;
    }
}