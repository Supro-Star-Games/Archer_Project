using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameplayMenuUI : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI _maxHP;
	[SerializeField] private TextMeshProUGUI _arrowSpeed;
	[SerializeField] private TextMeshProUGUI _attackSpeed;
	[SerializeField] private TextMeshProUGUI _physicsDamage;
	[SerializeField] private TextMeshProUGUI _fireDamage;
	[SerializeField] private TextMeshProUGUI _iceDamage;
	[SerializeField] private TextMeshProUGUI _posionDamage;
	[SerializeField] private TextMeshProUGUI _electricDamage;

	[SerializeField] private Button _closeButton;
	[SerializeField] private Button _mainMenuButton;
	[SerializeField] private PerkItemUI _perkItem;
	[SerializeField] private Transform _learnedPerksContent;
	[SerializeField] private TextMeshProUGUI _description;
	private Archer _archer;
	private List<PerkItemUI> _perkItems = new List<PerkItemUI>();

	private void Awake()
	{
		_archer = FindObjectOfType<Archer>();
		_archer.GetStatistics += InitializeStats;
		gameObject.SetActive(false);
		_closeButton.onClick.AddListener(Close);
		_mainMenuButton.onClick.AddListener(GameManager.LoadMenu);
		_mainMenuButton.onClick.AddListener(GameManager.PauseGame);
	}

	public void Show()
	{
		gameObject.SetActive(true);
	}

	public void Close()
	{
		gameObject.SetActive(false);
	//	GameManager.PauseGame();
	}

	private void OnDestroy()
	{
		_archer.GetStatistics -= InitializeStats;
	}

	public void InitializeStats(Dictionary<string, float> _stats)
	{
		_maxHP.text = _stats["MaxHP"].ToString();
		_arrowSpeed.text = _stats["ArrowSpeed"].ToString();
		_attackSpeed.text = _stats["AtkSpeed"].ToString();
		_physicsDamage.text = _stats["phDamage"].ToString();
		_fireDamage.text = _stats["fDamage"].ToString();
		_iceDamage.text = _stats["iDamage"].ToString();
		_posionDamage.text = _stats["pDamage"].ToString();
		_electricDamage.text = _stats["eDamage"].ToString();
	}

	private void OnEnable()
	{
	//	GameManager.PauseGame();
		SetupLearnedPerks();
	}

	private void OnDisable()
	{
		foreach (var item in _perkItems)
		{
			Destroy(item.gameObject);
		}

		_perkItems.Clear();
	}

	public void SetupLearnedPerks()
	{
		foreach (var perk in _archer.GetLernedPerks())
		{
			PerkItemUI _newItem = Instantiate(_perkItem, _learnedPerksContent);
			_newItem.SetPerkData(perk.PerkName, perk.PerkDescription, perk.PerkIcon, perk.PerkLVL);
			_perkItems.Add(_newItem);
		}
	}

	public void SetDescription(string _text)
	{
		_description.text = _text;
	}
}