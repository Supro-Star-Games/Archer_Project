using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyedPerksUI : MonoBehaviour
{
    [SerializeField] private Transform _content;
    [SerializeField] private PerkItemUI _perkItem;
    private Archer _archer;

    private void Awake()
    {
        _archer = FindObjectOfType<Archer>();
    }

    private void OnEnable()
    {
        _archer.PerksIsApplyed += ShowPerks;
        _archer.ArrowShoted += ClearPerks;
    }

    private void OnDisable()
    {
        _archer.PerksIsApplyed -= ShowPerks;
        _archer.ArrowShoted -= ClearPerks;
    }

    private List<PerkItemUI> _createdItems = new List<PerkItemUI>();

    public void ShowPerks(List<Perk> perks)
    {
        foreach (var perk in perks)
        {
            PerkItemUI _newItem = Instantiate(_perkItem, _content);
            _newItem.SetPerkData(perk.PerkName,perk.PerkDescription,perk.PerkIcon,perk.PerkLVL);
            _createdItems.Add(_newItem);
        }
    }

    public void ClearPerks()
    {
        foreach (var item in _createdItems)
        {
            Destroy(item.gameObject);
        }
        
        _createdItems.Clear();
    }
}
