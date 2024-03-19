using System;
using UnityEngine;

public class ContainerCounterVisual : MonoBehaviour
{
    private static readonly int OpenClose = Animator.StringToHash("OpenClose");
    [SerializeField] private ContainerCounter containerCounter;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        containerCounter.OnPlayerGrabbedObject += ContainerCounterOnPlayerGrabbedObject;
    }

    private void ContainerCounterOnPlayerGrabbedObject(object sender, EventArgs e)
    {
        _animator.SetTrigger(OpenClose);
    }
}