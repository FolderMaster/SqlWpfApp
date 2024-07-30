using Microsoft.EntityFrameworkCore;

using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.ObjectModel;
using System.ComponentModel;

using Model.Independent;

namespace Model.Dependent
{
    /// <summary>
    /// Класс режимом обучения с названием и названием режима оценивания.
    /// </summary>
    [Table("StudyForms")]
    [PrimaryKey(nameof(Name))]
    public class StudyForm
    {
        /// <summary>
        /// Генератор идентификаторов.
        /// </summary>
        private static IdGenerator _idGenerator = new(1);

        /// <summary>
        /// Возвращает и задаёт все режимы обучения.
        /// </summary>
        public static ObservableCollection<StudyForm> StudyForms { get; set; } = new();

        /// <summary>
        /// Возвращает и задаёт название.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Возвращает и задаёт название режима оценивания.
        /// </summary>
        public string GradeModeName { get; set; }

        /// <summary>
        /// Возвращает и задаёт связанный режим оценивания.
        /// </summary>
        [Browsable(false)]
        public virtual GradeMode GradeMode { get; set; }

        /// <summary>
        /// Возвращает и задаёт связанные дисциплины.
        /// </summary>
        [Browsable(false)]
        public virtual ObservableCollection<Discipline> Disciplines { get; set; }

        /// <summary>
        /// Создаёт экземпляр класса <see cref="StudyForm"/> по умолчанию.
        /// </summary>
        public StudyForm() => ValuesGenerator.GenerateValues(_idGenerator, () =>
            StudyForms.FirstOrDefault((d) => d.Name == Name) != null, (id) =>
            Name = nameof(Name) + "_" + id, () => StudyForms.Add(this));
    }
}