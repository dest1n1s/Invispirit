// <copyright file="HealthBar.cs" company="ECYSL">
// Copyright (c) ECYSL. All rights reserved.
// </copyright>

using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// The health bar object.
/// </summary>
public class HealthBar
{
    private const float MaxHp = 100;
    private const float EffectBarDecreasePreUpdate = 0.005f;

    private readonly Image effect;
    private readonly Image blood;

    /// <summary>
    /// Initializes a new instance of the <see cref="HealthBar"/> class.
    /// </summary>
    /// <param name="barPrefab">the prefab from which the <see cref="GameObject"/> should be instantiated from.</param>
    /// <param name="playerName">the name of the player as the title of the health bar.</param>
    /// <param name="parentTransform">the parent parentTransform.</param>
    /// <param name="position">the position of the health bar.</param>
    public HealthBar(GameObject barPrefab, string playerName, Transform parentTransform, Vector3 position)
    {
        this.BarObject = Object.Instantiate(barPrefab);
        this.Hp = MaxHp;
        this.effect = this.BarObject.transform.Find("Effect").GetComponent<Image>();
        this.blood = this.BarObject.transform.Find("Blood").GetComponent<Image>();

        // Set the player's name as the title of the bar
        this.BarObject.transform.Find("Text").GetComponent<Text>().text = playerName;

        this.BarObject.transform.SetParent(parentTransform);
        this.BarObject.transform.localPosition = position;
    }

    /// <summary>
    /// Gets the GameObject of the health bar.
    /// </summary>
    public GameObject BarObject { get; }

    /// <summary>
    /// Gets or sets the HP to be displayed in the health bar.
    /// </summary>
    public float Hp { get; set; }

    /// <summary>
    /// Updates the visual effect of the health bar.
    /// </summary>
    public void Update()
    {
        this.blood.fillAmount = this.Hp / MaxHp;
        if (this.effect.fillAmount > this.blood.fillAmount)
        {
            // Gradually decrease the effect bar
            this.effect.fillAmount -= EffectBarDecreasePreUpdate;
        }
        else
        {
            this.effect.fillAmount = this.blood.fillAmount;
        }
    }
}