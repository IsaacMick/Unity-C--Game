using UnityEngine;
using UnityEngine.UI;

public class HealthCanvasScript : MonoBehaviour
{
    [SerializeField]
    private RectTransform healthBarRec;
    [SerializeField]
    private Text healthText;

    private void Start()
    {
        if (healthBarRec == null)
        {
            Debug.LogError("There is no health bar shared to script.");
        }

        if (healthText == null)
        {
            Debug.LogError("Health does not have a reference to \"Text\"");
        }

    }

    public void SetHealth(int maxHealth, int currentHealth)
    {
        float averageOfHealth = (float)currentHealth / maxHealth;

        float yScale = healthBarRec.localScale.y;
        float zScale = healthBarRec.localScale.z;
        healthBarRec.localScale = new Vector3(averageOfHealth, yScale, zScale);

        healthText.text = currentHealth.ToString();
    }
}
