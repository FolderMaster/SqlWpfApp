using System;

namespace ViewModel.VMs.Request
{
    public class StudentsUpdateParametersVM : ParametersVM
    {
        public long ID { get; set; }

        public int RecordBookNumber { get; set; }

        public bool IsDeductible { get; set; }

        public string GroupNumber { get; set; }

        public int GroupFormationYear { get; set; } = DateTime.Now.Year;

        public string ScholarshipName { get; set; }

        public StudentsUpdateParametersVM() : base([]) { }
    }
}
