using System.Collections.Generic;
using UnityEngine;

public class PlayerSkills : MonoBehaviour
{
    [SerializeField] private List<PlayerSkill> skills = new();

    protected virtual void Reset()
    {
        LoadSkills();
    }

    protected virtual void LoadSkills()
    {
        skills.Clear(); 

        PlayerSkill[] foundSkills =
            GetComponentsInChildren<PlayerSkill>(true);

        skills.AddRange(foundSkills);  

        Debug.Log($"Loaded {skills.Count} skills", gameObject);
    }

    public PlayerSkill GetSkill(string skillName)
    {
        return skills.Find(skill => skill.name == skillName);
    }
}