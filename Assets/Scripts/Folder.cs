using System;
using System.Collections.Generic;

public class Folder : File
{
    private List<Folder> _folders = new();
    private List<File> _files = new();

    public IReadOnlyList<Folder> Folders => _folders;
    public IReadOnlyList<File> Files => _files;

    public Folder(string name) : base(name)
    {
        
    }
    
    public void RemoveFolder(Folder folder)
    {
        _folders.Remove(folder);
    }
    
    public void AddFolder(Folder folder)
    {
        _folders.Add(folder);
    }
    
    public void AddFile(File file)
    {
        _files.Add(file);
    }
}
