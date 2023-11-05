using UnityEngine;

[CreateAssetMenu]
public class TestNpcConditionCallback : ScriptableObject
{
    public bool Test(string id)
    {
        Debug.Log("Test callback: " + id);
        return true;
    }
}
