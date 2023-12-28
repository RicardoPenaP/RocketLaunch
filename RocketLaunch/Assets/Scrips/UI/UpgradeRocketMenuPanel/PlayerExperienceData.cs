
public class PlayerExperienceData
{
    public float currentExperience;
    public float targetExperience;
    public float maxExperience;
    public float normalizedCurrentExperience;
    public float normalizedTargetExperience;

    public PlayerExperienceData(float currentExperience, float targetExperience, float maxExperience)
    {
        this.currentExperience = currentExperience;
        this.targetExperience = targetExperience;
        this.maxExperience = maxExperience;
        normalizedCurrentExperience = currentExperience / maxExperience;
        normalizedTargetExperience = targetExperience / maxExperience;
    }
}
