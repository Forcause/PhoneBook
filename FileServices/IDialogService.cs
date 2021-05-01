namespace practice6WPF
{
    public interface IDialogService
    {
        void ShowMessage(string message);
        string FilePath { get; set; }
        int FilterIndex { get; set; }
        bool OpenFileDialog();
        bool SaveFileDialog();
    }
}