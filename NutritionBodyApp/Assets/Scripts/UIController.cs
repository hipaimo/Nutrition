using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    public BodyTransformation bodyTransform;

    public Button btnSucre;
    public Button btnGras;
    public Button btnProteines;
    public Slider sliderDuree;
    public TextMeshProUGUI labelDuree;

    void Start()
    {
        // Lier les boutons
        btnSucre.onClick.AddListener(() => OnFoodSelected("sucre"));
        btnGras.onClick.AddListener(() => OnFoodSelected("gras"));
        btnProteines.onClick.AddListener(() => OnFoodSelected("proteines"));

        // Lier le slider
        sliderDuree.onValueChanged.AddListener(OnDurationChanged);

        // Initialiser le label
        OnDurationChanged(sliderDuree.value);
    }

    void OnFoodSelected(string foodType)
    {
        if (bodyTransform != null)
        {
            bodyTransform.SetFoodType(foodType);
            Debug.Log("Aliment sélectionné : " + foodType);
        }
    }

    void OnDurationChanged(float value)
    {
        int months = Mathf.RoundToInt(value);
        labelDuree.text = "Durée : " + months + " mois";

        // NOUVEAU : Transmet la durée au script de transformation
        if (bodyTransform != null)
        {
            bodyTransform.SetDuration(months);
        }
    }
}