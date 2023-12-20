using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using System;

using ViewModel.Enums;
using ViewModel.Interfaces.DataBase;
using ViewModel.Interfaces.Services;
using ViewModel.Interfaces.Services.Messages;
using ViewModel.VMs.Request.ParameterRequest;
using ViewModel.VMs.Request.ParameterRequest.SpecialViewData;

namespace ViewModel.VMs.Request
{
    /// <summary>
    /// Класс представления модели для выполнения специализированных запросов просмотра данных с
    /// специализированным запросом просмотра данных, коллекцией представлений модели для
    /// параметров и командой выполнения.
    /// </summary>
    public partial class SpecialViewDataRequestsVM : RequestsVM
    {
        /// <summary>
        /// Специализированный запрос просмотра данных.
        /// </summary>
        [ObservableProperty]
        private SpecialViewDataRequest _request =
            SpecialViewDataRequest.AverageDisciplineLastGrades;

        public AverageDepartmentGroupLastGradesViewRequestVM
            AverageDepartmentGroupLastGradesViewRequestVM
        { get; } = new();

        public AverageDisciplineLastGradesViewRequestVM
            AverageDisciplineLastGradesViewRequestVM
        { get; } = new();

        public DeductibleDepartmentStudentsViewRequestVM
            DeductibleDepartmentStudentsViewRequestVM
        { get; } = new();

        public DepartmentScholarshipCountsViewRequestVM
            DepartmentScholarshipCountsViewRequestVM
        { get; } = new();

        public PassedDepartmentStudentDisciplinesViewRequestVM
            PassedDepartmentStudentDisciplinesViewRequestVM
        { get; } = new();

        public PassingDisciplineCountsViewRequestVM
            PassingDisciplineCountsViewRequestVM
        { get; } = new();

        /// <summary>
        /// Возвращает и задаёт команду выполнения.
        /// </summary>
        public RelayCommand ExecuteCommand { get; private set; }

        /// <summary>
        /// Создаёт экземпляр класса <see cref="SpecialViewDataRequestsVM"/>.
        /// </summary>
        /// <param name="dbContextBuilder">Создатель контекста базы данных.</param>
        /// <param name="resourceService">Сервис ресурсов.</param>
        /// <param name="messageService">Сервис сообщений.</param>
        public SpecialViewDataRequestsVM(IDbContextBuilder dbContextBuilder,
            IResourceService resourceService, IMessageService messageService) :
            base(dbContextBuilder, resourceService, messageService) =>
            ExecuteCommand = new RelayCommand(() =>
            {
                ParameterRequestVM requestVM = Request switch
                {
                    SpecialViewDataRequest.AverageDepartmentGroupLastGrades =>
                        AverageDepartmentGroupLastGradesViewRequestVM,
                    SpecialViewDataRequest.AverageDisciplineLastGrades =>
                        AverageDisciplineLastGradesViewRequestVM,
                    SpecialViewDataRequest.DeductibleDepartmentStudents =>
                        DeductibleDepartmentStudentsViewRequestVM,
                    SpecialViewDataRequest.DepartmentScholarshipCounts =>
                        DepartmentScholarshipCountsViewRequestVM,
                    SpecialViewDataRequest.PassedDepartmentStudentDisciplines =>
                        PassedDepartmentStudentDisciplinesViewRequestVM,
                    SpecialViewDataRequest.PassingDisciplineCounts =>
                        PassingDisciplineCountsViewRequestVM,
                    _ => throw new NotImplementedException()
                };
                ExecuteCommand(requestVM.GetRequest(), requestVM.GetParameters());
            });
    }
}