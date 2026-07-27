using UnityEngine;

[CreateAssetMenu(fileName = "New Customer", menuName = "Customer/Customer Data")]
public class CustomerData : ScriptableObject
{
    public string customerName;

    public Sprite portrait;

    public DialogueData dialogue;
}