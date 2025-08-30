using _Project.Scripts.Interfaces;
using UnityEngine;

public class Item : MonoBehaviour, IDraggable
{
    [SerializeField] private int _price;

    public int Price => _price;
}
