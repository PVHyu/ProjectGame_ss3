using System.Collections.Generic;
using UnityEngine;

public class PlayerSkills : MonoBehaviour
{
    [SerializeField] private List<Skill> skills = new();

    protected virtual void Reset()
    {
        LoadSkills();
    }

    protected virtual void LoadSkills()
    {
        skills.Clear(); 

        Skill[] foundSkills =
            GetComponentsInChildren<Skill>(true);

        skills.AddRange(foundSkills);  

        Debug.Log($"Loaded {skills.Count} skills", gameObject);
    }

    public Skill GetSkill(string skillName)
    {
        return skills.Find(skill => skill.name == skillName);
    }
}