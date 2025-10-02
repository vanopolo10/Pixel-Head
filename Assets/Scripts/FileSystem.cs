using System.Collections.Generic;
using UnityEngine;

public class FileSystem : MonoBehaviour
{
    private Folder _mainFolder = new("Main");

    private Stack<Folder> _history = new();
    public Folder CurrentFolder { get; private set; }
    public int HistoryCount => _history.Count;

    private void Start()
    {
        CurrentFolder = _mainFolder;
    }

    public void RemoveFolder(Folder folder)
    {
        CurrentFolder.RemoveFolder(folder);
    }
    
    public void AddFolder(string folderName)
    {
        CurrentFolder.AddFolder(new Folder(folderName));
    }
    
    public void AddFile(string fileName)
    {
        CurrentFolder.AddFile(new File(fileName));
    }
    
    public void ChangeCurrentFolder(Folder folder)
    {
        _history.Push(CurrentFolder);

        CurrentFolder = folder;
    }

    public void Unfold()
    {
        CurrentFolder = _history.Pop();
    }
}
