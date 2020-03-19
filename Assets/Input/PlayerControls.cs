// GENERATED AUTOMATICALLY FROM 'Assets/Input/PlayerControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""Arena"",
            ""id"": ""29be948c-6828-45ce-843b-0737f5763288"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""308afad3-8a88-45d4-a0a8-8ddc3bfdd179"",
                    ""expectedControlType"": ""Stick"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Stare"",
                    ""type"": ""Button"",
                    ""id"": ""570984ef-e491-4c56-af42-20f309132a68"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""9ce4bcac-d10f-45d9-9e05-b8b1b9b6de02"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""0df49039-2507-4f5b-85df-880d797738cc"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Stare"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5d2c7b09-9678-4e6c-a782-b1aa1babf63c"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamePads"",
                    ""action"": ""Stare"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9d54e927-76f0-496c-b9ec-29620b8c4502"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8057f03a-530b-45b5-9342-fa346feff857"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamePads"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""GamePads"",
            ""bindingGroup"": ""GamePads"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Arena
        m_Arena = asset.FindActionMap("Arena", throwIfNotFound: true);
        m_Arena_Movement = m_Arena.FindAction("Movement", throwIfNotFound: true);
        m_Arena_Stare = m_Arena.FindAction("Stare", throwIfNotFound: true);
        m_Arena_Jump = m_Arena.FindAction("Jump", throwIfNotFound: true);
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

    // Arena
    private readonly InputActionMap m_Arena;
    private IArenaActions m_ArenaActionsCallbackInterface;
    private readonly InputAction m_Arena_Movement;
    private readonly InputAction m_Arena_Stare;
    private readonly InputAction m_Arena_Jump;
    public struct ArenaActions
    {
        private @PlayerControls m_Wrapper;
        public ArenaActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_Arena_Movement;
        public InputAction @Stare => m_Wrapper.m_Arena_Stare;
        public InputAction @Jump => m_Wrapper.m_Arena_Jump;
        public InputActionMap Get() { return m_Wrapper.m_Arena; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ArenaActions set) { return set.Get(); }
        public void SetCallbacks(IArenaActions instance)
        {
            if (m_Wrapper.m_ArenaActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_ArenaActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_ArenaActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_ArenaActionsCallbackInterface.OnMovement;
                @Stare.started -= m_Wrapper.m_ArenaActionsCallbackInterface.OnStare;
                @Stare.performed -= m_Wrapper.m_ArenaActionsCallbackInterface.OnStare;
                @Stare.canceled -= m_Wrapper.m_ArenaActionsCallbackInterface.OnStare;
                @Jump.started -= m_Wrapper.m_ArenaActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_ArenaActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_ArenaActionsCallbackInterface.OnJump;
            }
            m_Wrapper.m_ArenaActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Stare.started += instance.OnStare;
                @Stare.performed += instance.OnStare;
                @Stare.canceled += instance.OnStare;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
            }
        }
    }
    public ArenaActions @Arena => new ArenaActions(this);
    private int m_GamePadsSchemeIndex = -1;
    public InputControlScheme GamePadsScheme
    {
        get
        {
            if (m_GamePadsSchemeIndex == -1) m_GamePadsSchemeIndex = asset.FindControlSchemeIndex("GamePads");
            return asset.controlSchemes[m_GamePadsSchemeIndex];
        }
    }
    public interface IArenaActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnStare(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
    }
}
