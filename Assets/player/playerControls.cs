//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/playerControls.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @PlayerControls: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""playerControls"",
    ""maps"": [
        {
            ""name"": ""Player_map"",
            ""id"": ""e39eb87c-da27-4c16-a59f-e8d8dcf5e9e8"",
            ""actions"": [
                {
                    ""name"": ""movement"",
                    ""type"": ""Value"",
                    ""id"": ""70cbf64c-4169-448d-bd92-5268956554b3"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""928acfe4-220a-4fc4-a283-4d6b129dc0c4"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""72c6df3f-cb46-4b61-aa6d-7f2c68c06f45"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""ebd0f1ef-8ef6-404e-bf31-4aee97d323c3"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""f0d4cabd-c7ef-4123-80d8-9801f46b1e00"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""f3292929-e4bf-490e-88dc-460d3b6d73a4"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Player_map
        m_Player_map = asset.FindActionMap("Player_map", throwIfNotFound: true);
        m_Player_map_movement = m_Player_map.FindAction("movement", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Player_map
    private readonly InputActionMap m_Player_map;
    private List<IPlayer_mapActions> m_Player_mapActionsCallbackInterfaces = new List<IPlayer_mapActions>();
    private readonly InputAction m_Player_map_movement;
    public struct Player_mapActions
    {
        private @PlayerControls m_Wrapper;
        public Player_mapActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @movement => m_Wrapper.m_Player_map_movement;
        public InputActionMap Get() { return m_Wrapper.m_Player_map; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(Player_mapActions set) { return set.Get(); }
        public void AddCallbacks(IPlayer_mapActions instance)
        {
            if (instance == null || m_Wrapper.m_Player_mapActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_Player_mapActionsCallbackInterfaces.Add(instance);
            @movement.started += instance.OnMovement;
            @movement.performed += instance.OnMovement;
            @movement.canceled += instance.OnMovement;
        }

        private void UnregisterCallbacks(IPlayer_mapActions instance)
        {
            @movement.started -= instance.OnMovement;
            @movement.performed -= instance.OnMovement;
            @movement.canceled -= instance.OnMovement;
        }

        public void RemoveCallbacks(IPlayer_mapActions instance)
        {
            if (m_Wrapper.m_Player_mapActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IPlayer_mapActions instance)
        {
            foreach (var item in m_Wrapper.m_Player_mapActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_Player_mapActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public Player_mapActions @Player_map => new Player_mapActions(this);
    public interface IPlayer_mapActions
    {
        void OnMovement(InputAction.CallbackContext context);
    }
}
