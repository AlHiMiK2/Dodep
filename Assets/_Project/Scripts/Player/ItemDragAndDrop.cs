using System.Collections;
using _Project.Scripts.Interfaces;
using UnityEngine;

public class ItemDragAndDrop : MonoBehaviour
{
    [Header("Drag")]
    [SerializeField] private float _distance;
    [SerializeField] private float _startDragDuration;
    [SerializeField] private Transform _joint;
    [SerializeField] private PlayerMovement _playerMovement;
    [Header("Drop")] 
    [SerializeField] private LayerMask _dropLayerMask;
    [Header("Raycast")]
    [SerializeField] private KeyCode _grabKey;
    [SerializeField] private float _raycastDistance;
    [SerializeField] private LayerMask _layerMask;

    private Rigidbody _item;
    private Collider _itemCollider;
    private int _itemLayer;
    private Vector3 _dragOffset;
    
    public bool IsDragging { get; private set; }
    
    private void Update()
    {
        if (Input.GetKey(_grabKey) && _item == null)
        {
            Drag();
        }
        else if (!Input.GetKey(_grabKey))
        {
            Drop();
        }
    }

    private void Drag()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        Physics.Raycast(ray, out RaycastHit hitInfo, _raycastDistance, _layerMask, QueryTriggerInteraction.Ignore);
        
        if (hitInfo.rigidbody && hitInfo.rigidbody.TryGetComponent(out IDraggable draggable))
        {
            _item = hitInfo.rigidbody;
            _itemCollider = hitInfo.collider;
            _dragOffset = _joint.InverseTransformDirection(_item.position - hitInfo.point);
            _item.isKinematic = true;
            _itemLayer = _item.gameObject.layer;
            _item.gameObject.layer = _joint.gameObject.layer;
            _item.transform.parent = _joint;
            StartCoroutine(DragCoroutine());
        }
        
        IsDragging = true;
    }

    private IEnumerator DragCoroutine()
    {
        if (_item == null) yield break;
        float time = Time.time;
        float speed = Vector3.Distance(_dragOffset, _item.transform.localPosition) / _startDragDuration;

        while (_item && time + _startDragDuration > Time.time)
        {
            _item.transform.localPosition = Vector3.MoveTowards(_item.transform.localPosition, _dragOffset, speed * Time.deltaTime);
            yield return null;
        }
    }

    private void Drop()
    {
        if (_item)
        {
            if (Physics.CheckBox(_itemCollider.bounds.center, _itemCollider.bounds.extents * 0.5f, _item.transform.rotation, _dropLayerMask, QueryTriggerInteraction.Ignore) == false)
            {
                _item.gameObject.layer = _itemLayer;
                _item.isKinematic = false;
                _item.transform.parent = null;
                _item.linearVelocity = _playerMovement.Velocity;
                _item = null;
                IsDragging = false;
            }
        }
        else
        {
            IsDragging = false;
        }
    }
}
