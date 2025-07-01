using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;


class DiamondUI : MonoBehaviour
{
    [SerializeField] private TMP_Text diamondText;
    [SerializeField] private GameObject notEnoughDiamond;

    private void Start()
    {
    }

    public void UpdateDiamondUI(int diamond)
    {
        diamondText.text = diamond.ToString();
    }

    public void ShowNotEnoughDiamond()
    {
        notEnoughDiamond.SetActive(true);
    }

}

