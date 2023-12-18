using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum NodeState { Running, Success, Failure}
public abstract class Node
{
    protected NodeState state { get; set; }
    public Node parent = null;
    protected List<Node> children = new();

    Dictionary<string, object> data = new Dictionary<string, object>();

    public void SetData(string key, object value)
    {
        data[key] = value;
    }

    public object GetData(string key)
    {
        if(data.TryGetValue(key, out object value))
            return value;
        if(parent != null)
            return parent.GetData(key);

        return null;
    }

    public bool RemoveData(string key)
    {
        if (data.Remove(key))
        {
            return true;
        }
        if (parent != null)
            return parent.RemoveData(key);

        return false;
    }



    public Node()
    {
        state = NodeState.Running;
        parent = null;
    }

    public Node(List<Node> pChildren)
    {
        state = NodeState.Running;
        parent = null;
        foreach (Node child in pChildren)
            Attach(child);
    }

    protected void Attach(Node n)
    {
        children.Add(n);
        n.parent = this;
    }
    public abstract NodeState Evaluate();
}

public class Selector : Node
{
    public Selector(List<Node> n) : base(n) {}

    public override NodeState Evaluate()
    {
        foreach(Node n in children)
        {
            NodeState localstate = n.Evaluate();
            switch (localstate)
            {
                case NodeState.Failure:
                    continue;
                case NodeState.Success :
                case NodeState.Running :
                    state = localstate;
                    return state;
            }
        }
        state = NodeState.Failure;
        return state;
    }
}

public class Sequence : Node
{
    public Sequence(List<Node> n) : base(n) { }

    public override NodeState Evaluate()
    {
        foreach (Node n in children)
        {
            NodeState localstate = n.Evaluate();
            switch (localstate)
            {
                case NodeState.Success:
                    continue;
                case NodeState.Failure:
                case NodeState.Running:
                    state = localstate;
                    return state;
            }
        }
        state = NodeState.Success;
        return state;
    }
}

public class Inverter : Node
{   
    public Inverter(Node n) : base()
    {
        Attach(n);
    }

    public override NodeState Evaluate()
    {
        switch (children[0].Evaluate())
        {
            case NodeState.Success:
                state = NodeState.Failure;
                break;
            case NodeState.Failure:
                state = NodeState.Success;
                break;
            case NodeState.Running:
                state = NodeState.Running;
                break;
        }
        return state;
    }
}

public class XOR : Node
{
    public XOR(List<Node> n) : base(n) { }

    public override NodeState Evaluate()
    {
        int nbSuccess = 0;
        state = NodeState.Failure;
        foreach (Node n in children)
        {
            switch (n.Evaluate())
            {
                case NodeState.Success:
                    nbSuccess++;
                    state = NodeState.Success;
                    break;
                case NodeState.Running:
                    state = NodeState.Running;
                    return state;
            }
            if (nbSuccess > 1)
            {
                state = NodeState.Failure;
                break;
            }
        }
        return state;
    }
}

public class Steal : Node
{
    public override NodeState Evaluate()
    {
        Debug.Log($"Le voleur a volé {((Transform)GetData("currentTarget")).gameObject.name}");
        state = NodeState.Running;
        return state;
    }
}

public class PatrolTask : Node
{
    Transform[] destinations;
    int destinationIndex = 0;

    NavMeshAgent agent;
    float waitTime;
    float elapsedTime = 0;
    bool isWaiting = false;

    public PatrolTask(Transform[] destinations, float waitTime, NavMeshAgent agent)
    {
        this.destinations = destinations;
        this.waitTime = waitTime;
        this.agent = agent;
    }

    public override NodeState Evaluate()
    {
        if(isWaiting)
        {
            elapsedTime += Time.deltaTime;
            if(elapsedTime >= waitTime)
            {
                isWaiting = false;
            }
        }
        else
        {
            if (Vector3.Distance(agent.transform.position, destinations[destinationIndex].position) < agent.stoppingDistance)
            {
                destinationIndex = (destinationIndex + 1) % destinations.Length;
                isWaiting = true;
                elapsedTime = 0;
            }
            else
            {
                agent.destination = destinations[destinationIndex].position;
            }
        }

        state = NodeState.Running;
        return state;
    }
}

public class GoToTarget : Node
{
    
    NavMeshAgent agent;

    public GoToTarget(NavMeshAgent agent)
    {
        this.agent = agent;
    }

    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData("currentTarget");
        agent.destination = target.position;
        if(agent.remainingDistance <= agent.stoppingDistance)
        {
            state = NodeState.Success;
            return state;
        }
        state = NodeState.Running;
        return state;
    }
}

public class IsWithinRange : Node
{
    Transform target;
    Transform self;
    float detectionRange;

    public IsWithinRange(Transform target, Transform self, float detectionRange)
    {
        this.target = target;
        this.self = self;
        this.detectionRange = detectionRange;
    }

    public override NodeState Evaluate()
    {
        state = NodeState.Failure;
        if (Vector3.Distance(self.position, target.position) <= detectionRange)
        {
            state = NodeState.Success;
            parent.parent.SetData("currentTarget", target);
        }
        return state;
    }
}