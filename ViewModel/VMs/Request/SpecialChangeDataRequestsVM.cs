using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using System;

using ViewModel.Enums;
using ViewModel.Interfaces;
using ViewModel.Interfaces.Services;
using ViewModel.Interfaces.Services.Messages;
using ViewModel.VMs.Request.ParameterRequest;
using ViewModel.VMs.Request.ParameterRequest.SpecialChangeData;

namespace ViewModel.VMs.Request
{
    /// <summary>
    /// Класс представления модели для выполнения специализированных запросов изменения данных с
    /// специализированным запросом изменения данных, коллекцией представлений модели для
    /// параметров и командой выполнения.
    /// </summary>
    public partial class SpecialChangeDataRequestsVM : RequestsVM
    {
        /// <summary>
        /// Специализированный запрос изменения данных.
        /// </summary>
        [ObservableProperty]
        private SpecialChangeDataRequest request =
            SpecialChangeDataRequest.SetNullStudentDeductings;

        public SetNullStudentPassingsRequestVM SetNullStudentPassingsRequestVM { get; } = new();

        public SetNullStudentDeductingsRequestVM SetNullStudentDeductingsRequestVM { get; } =
            new();

        public SetStudentPassingsByLastGradesRequestVM SetStudentPassingsByLastGradesRequestVM
        { get; } = new();

        public SetStudentPassingsWithoutGradesRequestVM SetStudentPassingsWithoutGradesRequestVM
        { get; } = new();

        public SetStudentDeductingsRequestVM SetStudentDeductingsRequestVM { get; } = new();

        public SetStudentScholarshipsByGradesRequestVM SetStudentScholarshipsByGradesRequestVM
        { get; } = new();

        public SetStudentScholarshipsByPassingsRequestVM SetStudentScholarshipsByPassingsRequestVM
        { get; } = new();

        /// <summary>
        /// Возвращает и задаёт команду выполнения.
        /// </summary>
        public RelayCommand ExecuteCommand { get; private set; }

        /// <summary>
        /// Создаёт экземпляр класса <see cref="SpecialChangeDataRequestsVM"/>.
        /// </summary>
        /// <param name="dbContextBuilder">Создатель контекста базы данных.</param>
        /// <param name="resourceService">Сервис ресурсов.</param>
        /// <param name="messageService">Сервис сообщений.</param>
        public SpecialChangeDataRequestsVM(ISession dbContextBuilder,
            IResourceService resourceService, IMessageService messageService) :
            base(dbContextBuilder, resourceService, messageService) =>
            ExecuteCommand = new RelayCommand(() =>
            {
                ParameterRequestVM requestVM = Request switch
                {
                    SpecialChangeDataRequest.SetNullStudentPassings =>
                        SetNullStudentPassingsRequestVM,
                    SpecialChangeDataRequest.SetNullStudentDeductings =>
                        SetNullStudentDeductingsRequestVM,    
                    SpecialChangeDataRequest.SetStudentPassingsByLastGrades =>
                        SetStudentPassingsByLastGradesRequestVM,
                    SpecialChangeDataRequest.SetStudentPassingsWithoutGrades =>
                        SetStudentPassingsWithoutGradesRequestVM,
                    SpecialChangeDataRequest.SetStudentDeductings =>
                        SetStudentDeductingsRequestVM,
                    SpecialChangeDataRequest.SetStudentScholarshipsByGrades =>
                        SetStudentScholarshipsByGradesRequestVM,
                    SpecialChangeDataRequest.SetStudentScholarshipsByPassings =>
                        SetStudentScholarshipsByPassingsRequestVM,
                    _ => throw new NotImplementedException()
                };
                var command = $"{requestVM.GetRequest()}; SELECT * FROM {requestVM.Table}";
                ExecuteCommand(command, requestVM.GetParameters());
            });
    }
}