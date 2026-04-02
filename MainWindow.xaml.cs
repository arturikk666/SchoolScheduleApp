using _111.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Windows;

namespace _111
{
    public partial class MainWindow : Window
    {
        private readonly AppDbContext _context;

        public MainWindow()
        {
            InitializeComponent();

            try
            {
                _context = new AppDbContext();

                // Проверка подключения к базе данных
                bool canConnect = _context.Database.CanConnect();

                if (canConnect)
                {
                    // Загружаем учителей при старте
                    LoadTeachers();
                }
                else
                {
                    MessageBox.Show("Не удалось подключиться к базе данных", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при инициализации: {ex.Message}", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadTeachers()
        {
            try
            {
                var teachers = _context.Teachers.ToList();
                TeachersGrid.ItemsSource = teachers;
                TitleText.Text = "Учителя";
                CountText.Text = $"Всего записей: {teachers.Count}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки учителей: {ex.Message}", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadClasses()
        {
            try
            {
                var classes = _context.Classes.ToList();
                ClassesGrid.ItemsSource = classes;
                TitleText.Text = "Классы";
                CountText.Text = $"Всего записей: {classes.Count}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки классов: {ex.Message}", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadSchedule()
        {
            try
            {
                var schedule = _context.Schedule
                    .Include(s => s.Class)
                    .Include(s => s.Teacher)
                    .Select(s => new
                    {
                        s.Id,
                        ClassName = s.Class.ClassName,
                        s.Subject,
                        TeacherName = s.Teacher.FullName,
                        DayOfWeek = s.DayOfWeek,
                        s.LessonNumber
                    })
                    .ToList();

                ScheduleGrid.ItemsSource = schedule;
                TitleText.Text = "Расписание";
                CountText.Text = $"Всего записей: {schedule.Count}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки расписания: {ex.Message}", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadAbsences()
        {
            try
            {
                var absences = _context.Absences
                    .Include(a => a.Teacher)
                    .Select(a => new
                    {
                        a.Id,
                        TeacherName = a.Teacher.FullName,
                        a.AbsenceDate,
                        a.Reason
                    })
                    .ToList();

                AbsencesGrid.ItemsSource = absences;
                TitleText.Text = "Отсутствия";
                CountText.Text = $"Всего записей: {absences.Count}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки отсутствий: {ex.Message}", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadReplacements()
        {
            try
            {
                var replacements = _context.Replacements
                    .Include(r => r.Absence).ThenInclude(a => a.Teacher)
                    .Include(r => r.SubstituteTeacher)
                    .Select(r => new
                    {
                        r.Id,
                        AbsentTeacher = r.Absence.Teacher.FullName,
                        SubstituteTeacher = r.SubstituteTeacher.FullName,
                        r.Status
                    })
                    .ToList();

                ReplacementsGrid.ItemsSource = replacements;
                TitleText.Text = "Замены";
                CountText.Text = $"Всего записей: {replacements.Count}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки замен: {ex.Message}", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void HideAllGrids()
        {
            TeachersGrid.Visibility = Visibility.Collapsed;
            ClassesGrid.Visibility = Visibility.Collapsed;
            ScheduleGrid.Visibility = Visibility.Collapsed;
            AbsencesGrid.Visibility = Visibility.Collapsed;
            ReplacementsGrid.Visibility = Visibility.Collapsed;
        }

        private void BtnTeachers_Click(object sender, RoutedEventArgs e)
        {
            HideAllGrids();
            TeachersGrid.Visibility = Visibility.Visible;
            LoadTeachers();
        }

        private void BtnClasses_Click(object sender, RoutedEventArgs e)
        {
            HideAllGrids();
            ClassesGrid.Visibility = Visibility.Visible;
            LoadClasses();
        }

        private void BtnSchedule_Click(object sender, RoutedEventArgs e)
        {
            HideAllGrids();
            ScheduleGrid.Visibility = Visibility.Visible;
            LoadSchedule();
        }

        private void BtnAbsences_Click(object sender, RoutedEventArgs e)
        {
            HideAllGrids();
            AbsencesGrid.Visibility = Visibility.Visible;
            LoadAbsences();
        }

        private void BtnReplacements_Click(object sender, RoutedEventArgs e)
        {
            HideAllGrids();
            ReplacementsGrid.Visibility = Visibility.Visible;
            LoadReplacements();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Функция добавления будет реализована позже", "Информация",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Функция редактирования будет реализована позже", "Информация",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Функция удаления будет реализована позже", "Информация",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}