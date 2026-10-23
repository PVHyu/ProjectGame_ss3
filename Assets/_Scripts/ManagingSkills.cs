using System.Collections.Generic;
using UnityEngine;

public class ManagingSkills : BaseMonoBehaviour
{
    [SerializeField] private List<BaseAttack> skills = new();

    protected override void Reset()
    {
        LoadSkills();
    }

    protected virtual void LoadSkills()
    {
        skills.Clear(); 

        BaseAttack[] foundSkills =
            GetComponentsInChildren<BaseAttack>(true);

        skills.AddRange(foundSkills);  

        Debug.Log($"Loaded {skills.Count} skills", gameObject);
    }

    public BaseAttack GetSkill(string skillName)
    {
        return skills.Find(skill => skill.name == skillName);
    }
}