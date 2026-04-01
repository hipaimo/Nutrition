using UnityEngine;

public class BodyTransformation : MonoBehaviour
{
    public SkinnedMeshRenderer bodyMesh;

    private int bellyIndex = -1;
    private int hipsIndex = -1;
    private int armMuscleIndex = -1;
    private int legMuscleIndex = -1;
    private int faceBloatIndex = -1;

    [Range(0, 100)] public float fatLevel = 0;
    [Range(0, 100)] public float muscleLevel = 0;
    [Range(0, 100)] public float waterRetentionLevel = 0;

    // NOUVEAU : Multiplicateur de durée (1, 2 ou 3 mois)
    public float durationMultiplier = 1f;

    void Start()
    {
        if (bodyMesh == null)
            bodyMesh = GetComponentInChildren<SkinnedMeshRenderer>();

        if (bodyMesh != null && bodyMesh.sharedMesh != null)
        {
            bellyIndex = bodyMesh.sharedMesh.GetBlendShapeIndex("Belly_Fat");
            hipsIndex = bodyMesh.sharedMesh.GetBlendShapeIndex("Hips_Fat");
            armMuscleIndex = bodyMesh.sharedMesh.GetBlendShapeIndex("Arm_Muscle");
            legMuscleIndex = bodyMesh.sharedMesh.GetBlendShapeIndex("Leg_Muscle");
            faceBloatIndex = bodyMesh.sharedMesh.GetBlendShapeIndex("Face_Bloat");
        }
    }

    void Update()
    {
        ApplyTransformations();
    }

    void ApplyTransformations()
    {
        // Applique le multiplicateur de durée aux transformations
        float finalFat = fatLevel * durationMultiplier;
        float finalMuscle = muscleLevel * durationMultiplier;
        float finalWater = waterRetentionLevel * durationMultiplier;

        // Graisse (ventre + hanches)
        if (bellyIndex >= 0)
            bodyMesh.SetBlendShapeWeight(bellyIndex, Mathf.Clamp(finalFat, 0, 100));
        if (hipsIndex >= 0)
            bodyMesh.SetBlendShapeWeight(hipsIndex, Mathf.Clamp(finalFat * 0.7f, 0, 100));

        // Muscle (bras + jambes)
        if (armMuscleIndex >= 0)
            bodyMesh.SetBlendShapeWeight(armMuscleIndex, Mathf.Clamp(finalMuscle, 0, 100));
        if (legMuscleIndex >= 0)
            bodyMesh.SetBlendShapeWeight(legMuscleIndex, Mathf.Clamp(finalMuscle, 0, 100));

        // Rétention d'eau (visage)
        if (faceBloatIndex >= 0)
            bodyMesh.SetBlendShapeWeight(faceBloatIndex, Mathf.Clamp(finalWater, 0, 100));
    }

    // Fonction publique pour l'UI
    public void SetFoodType(string foodType)
    {
        switch (foodType.ToLower())
        {
            case "sucre":
                fatLevel = 60;
                waterRetentionLevel = 40;
                muscleLevel = 0;
                break;
            case "gras":
                fatLevel = 80;
                waterRetentionLevel = 20;
                muscleLevel = 0;
                break;
            case "proteines":
                muscleLevel = 70;
                fatLevel = 10;
                waterRetentionLevel = 0;
                break;
            case "sale":
                waterRetentionLevel = 90;
                fatLevel = 20;
                muscleLevel = 0;
                break;
            case "equilibre":
                fatLevel = 20;
                muscleLevel = 30;
                waterRetentionLevel = 10;
                break;
        }
    }

    // NOUVELLE FONCTION : Pour mettre à jour la durée depuis l'UI
    public void SetDuration(float months)
    {
        // 1 mois = 0.33, 2 mois = 0.66, 3 mois = 1.0
        durationMultiplier = months / 3f;
        Debug.Log("Durée mise à jour : " + months + " mois (Multiplicateur : " + durationMultiplier + ")");
    }
}