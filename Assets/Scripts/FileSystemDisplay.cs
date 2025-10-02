using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(FileSystem))]
public class FileSystemDisplay : MonoBehaviour
{
    [SerializeField] private VerticalLayoutGroup _content;
    [SerializeField] private Button _folderPrefab;
    [SerializeField] private Button _filePrefab;
    
    private FileSystem _fileSystem;
    private InputHandler _object;
    private UnityEngine.UI.Outline _selectedButtonOutline;
    
    private void Awake()
    {
        _fileSystem = GetComponent<FileSystem>();
    }

    private void OnEndInputEnded(string input)
    {
        _object.InputEnded -= OnEndInputEnded;
        _object.InputCanceled -= OnInputCanceled;

        _object.GetComponentInChildren<TMP_InputField>().gameObject.SetActive(false);
        
        if(_object.TryGetComponent<FolderView>(out _))
            _fileSystem.AddFolder(input);
        else if(_object.TryGetComponent<FileView>(out _))
            _fileSystem.AddFile(input);
        
        DisplayFolder(_fileSystem.CurrentFolder);
    }
    
    private void OnInputCanceled()
    {
        _object.InputEnded -= OnEndInputEnded;
        _object.InputCanceled -= OnInputCanceled;

        Destroy(_object.gameObject);
    }

    private void DisplayFolder(Folder folder)
    {
        for (int i = 0; i < _content.transform.childCount; i++)
            Destroy(_content.transform.GetChild(i).gameObject);

        foreach (var file in folder.Folders)
        {
            var newObject = Instantiate(_folderPrefab, _content.transform, true);
            newObject.GetComponent<FolderView>().SetFolder(file);
            
            newObject.GetComponent<InputHandler>().Clicked += SetSelected;
            newObject.GetComponent<InputHandler>().DoubleClicked += EnterFolder;
        }

        foreach (var file in folder.Files)
        {
            var newObject = Instantiate(_filePrefab, _content.transform, true);
            newObject.GetComponent<FileView>().SetFile(file);
            
            newObject.GetComponent<InputHandler>().Clicked += SetSelected;
        }
    }
    
    public void DisplayCreateFile(InputHandler prefab)
    {
        _object = Instantiate(prefab, _content.transform);
        _object.GetComponentInChildren<TMP_InputField>(true).gameObject.SetActive(true);
        
        _object.InputEnded += OnEndInputEnded;
        _object.InputCanceled += OnInputCanceled;
    }

    public void EnterFolder(FolderView folderView)
    {
        _fileSystem.ChangeCurrentFolder(folderView.Folder);
        
        DisplayFolder(_fileSystem.CurrentFolder);
    }
    
    public void SetSelected(UnityEngine.UI.Outline outline)
    {
        print("Обводка!");
        
        if (_selectedButtonOutline)
            _selectedButtonOutline.enabled = false;

        _selectedButtonOutline = outline;
        _selectedButtonOutline.enabled = true;
    }

    public void DeleteSelected()
    {
        if (_selectedButtonOutline)
        {
            _fileSystem.RemoveFolder(_selectedButtonOutline.gameObject.GetComponent<FolderView>().Folder);
            Destroy(_selectedButtonOutline.gameObject);
        }
    }

    public void Unfold()
    {
        if(_fileSystem.HistoryCount == 0) return;
        
        _fileSystem.Unfold();
        DisplayFolder(_fileSystem.CurrentFolder);
    }
}
