using UnityEngine;

namespace _Project.Scripts
{
    [RequireComponent(typeof(Animator))]
    public class HandsAnimation : MonoBehaviour
    {
        [SerializeField] private PlayerMovement _movement;
        [SerializeField] private ItemDragAndDrop _itemDragAndDrop;
        [SerializeField] private KeyCode _fuckKey;
        
        private Animator _animator;
        private static readonly int _isWalkingHash = Animator.StringToHash("IsWalking");
        private static readonly int _isGrabbingHash = Animator.StringToHash("IsGrabbing");
        private static readonly int _isFuckHash = Animator.StringToHash("IsFuck");

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            _animator.SetBool(_isWalkingHash, _movement.IsWalking);
            _animator.SetBool(_isGrabbingHash, _itemDragAndDrop.IsDragging);
            _animator.SetBool(_isFuckHash, Input.GetKey(_fuckKey) && !_itemDragAndDrop.IsDragging);
        }
    }
}