using TMPro;
using UnityEngine;

public class FolderView : MonoBehaviour
{
    [SerializeField] private TMP_Text _name;

    public Folder Folder { get; private set; }

    private void ChangeName(string newName)
    {
        _name.text = newName;
    }
    
    public void SetFolder(Folder folder)
    {
        ChangeName(folder.Name);
        Folder = folder;
    }
}
