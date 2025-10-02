using TMPro;
using UnityEngine;

public class FileView : MonoBehaviour
{
    [SerializeField] private TMP_Text _name;

    public File File { get; private set; }
    
    private void ChangeName(string fileName)
    {
        _name.text = fileName;
    }
    
    public void SetFile(File file)
    {
        ChangeName(file.Name);
        File = file;
    }
}