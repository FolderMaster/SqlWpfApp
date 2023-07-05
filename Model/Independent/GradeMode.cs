using Microsoft.EntityFrameworkCore;

using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

using Model.Dependent;

namespace Model.Independent
{
    /// <summary>
    /// Класс режима оценивания с названием.
    /// </summary>
    [Table("GradeModes")]
    [PrimaryKey(nameof(Name))]
    public class GradeMode
    {
        /// <summary>
        /// Генератор идентификаторов.
        /// </summary>
        private static IdGenerator _idGenerator = new(1);

        /// <summary>
        /// Возвращает и задаёт все режимы оценивания.
        /// </summary>
        public static ObservableCollection<GradeMode> GradeModes { get; set; } = new();

        /// <summary>
        /// Возвращает и задаёт название.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Возвращает и задаёт связанные оценки.
        /// </summary>
        public virtual ObservableCollection<Grade> Grades { get; set; }

        /// <summary>
        /// Возвращает и задаёт связанные режимы обучения.
        /// </summary>
        public virtual ObservableCollection<StudyForm> StudyForms { get; set; }

        /// <summary>
        /// Создаёт экземпляр класса <see cref="GradeMode"/> по умолчанию.
        /// </summary>
        public GradeMode() => ValuesGenerator.GenerateValues(_idGenerator, () =>
            GradeModes.FirstOrDefault((g) => g.Name == Name) != null, (id) =>
            Name = nameof(Name) + "_" + id, () => GradeModes.Add(this));
    }
}