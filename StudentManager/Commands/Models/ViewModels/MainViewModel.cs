using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using StudentManager.Commands;
using StudentManager.Models;

namespace StudentManager.ViewModels;

public sealed class MainViewModel : INotifyPropertyChanged
{
    private string _studentName = string.Empty;
    private string _studentGroup = string.Empty;
    private string _gradeText = string.Empty;
    private string _studentEmail = string.Empty;
    private Student? _selectedStudent;
    private string _statusMessage = string.Empty;

    public ObservableCollection<Student> Students { get; } = new();

    public RelayCommand AddStudentCommand { get; }
    public RelayCommand RemoveStudentCommand { get; }

    public event PropertyChangedEventHandler? PropertyChanged;

    public MainViewModel()
    {
        AddStudentCommand = new RelayCommand(
            _ => AddStudent(),
            _ => CanAddStudent());

        RemoveStudentCommand = new RelayCommand(
            _ => RemoveSelectedStudent(),
            _ => SelectedStudent is not null);

        Students.Add(new Student
        {
            Name = "Alice Brown",
            Group = "CS-21",
            Grade = 91,
            Email = "alice@univ.chernivtsi.ua"
        });

        Students.Add(new Student
        {
            Name = "Bob Green",
            Group = "CS-22",
            Grade = 78,
            Email = "bob@univ.chernivtsi.ua"
        });

        UpdateStatus();
    }

    public string StudentName
    {
        get => _studentName;
        set
        {
            if (_studentName == value) return;
            _studentName = value;
            OnPropertyChanged();
        }
    }

    public string StudentGroup
    {
        get => _studentGroup;
        set
        {
            if (_studentGroup == value) return;
            _studentGroup = value;
            OnPropertyChanged();
        }
    }

    public string GradeText
    {
        get => _gradeText;
        set
        {
            if (_gradeText == value) return;
            _gradeText = value;
            OnPropertyChanged();
        }
    }

    public string StudentEmail
    {
        get => _studentEmail;
        set
        {
            if (_studentEmail == value) return;
            _studentEmail = value;
            OnPropertyChanged();
        }
    }

    public Student? SelectedStudent
    {
        get => _selectedStudent;
        set
        {
            if (_selectedStudent == value) return;
            _selectedStudent = value;
            OnPropertyChanged();
        }
    }

    public string StatusMessage
    {
        get => _statusMessage;
        private set
        {
            if (_statusMessage == value) return;
            _statusMessage = value;
            OnPropertyChanged();
        }
    }

    private bool CanAddStudent()
    {
        return !string.IsNullOrWhiteSpace(StudentName)
               && !string.IsNullOrWhiteSpace(StudentGroup)
               && int.TryParse(GradeText, out int grade)
               && grade >= 0
               && grade <= 100
               && !string.IsNullOrWhiteSpace(StudentEmail);
    }

    private void AddStudent()
    {
        if (!int.TryParse(GradeText, out int grade)) return;

        Students.Add(new Student
        {
            Name = StudentName.Trim(),
            Group = StudentGroup.Trim(),
            Grade = grade,
            Email = StudentEmail.Trim()
        });

        StudentName = string.Empty;
        StudentGroup = string.Empty;
        GradeText = string.Empty;
        StudentEmail = string.Empty;

        UpdateStatus();
    }

    private void RemoveSelectedStudent()
    {
        if (SelectedStudent is null) return;

        Students.Remove(SelectedStudent);
        SelectedStudent = null;
        UpdateStatus();
    }

    private void UpdateStatus()
    {
        StatusMessage = $"Students: {Students.Count}";
    }

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}