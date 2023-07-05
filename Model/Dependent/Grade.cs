using Microsoft.EntityFrameworkCore;

using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.ObjectModel;

using Model.Independent;

namespace Model.Dependent
{
    /// <summary>
    /// Класс оценки с названием, обозначением, названием режима оценивания и коэффициентом.
    /// </summary>
    [Table("Grades")]
    [PrimaryKey(nameof(Name))]
    public class Grade
    {
        /// <summary>
        /// Генератор идентификаторов.
        /// </summary>
        private static IdGenerator _idGenerator = new(1);

        /// <summary>
        /// Возвращает и задаёт все оценки.
        /// </summary>
        public static ObservableCollection<Grade> Grades { get; set; } = new();

        /// <summary>
        /// Возвращает и задаёт название.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Возвращает и задаёт обозначение.
        /// </summary>
        public string Symbol { get; set; }

        /// <summary>
        /// Возвращает и задаёт название режима оценивания.
        /// </summary>
        public string GradeModeName { get; set; }

        /// <summary>
        /// Возвращает и задаёт связанный режим оценивания.
        /// </summary>
        public virtual GradeMode GradeMode { get; set; }

        /// <summary>
        /// Возвращает и задаёт коэффициент.
        /// </summary>
        public int Coefficient { get; set; } = 2;

        /// <summary>
        /// Возвращает и задаёт связанные ведомости оценивания.
        /// </summary>
        public virtual ObservableCollection<GradeStatement> GradeStatements { get; set; }

        /// <summary>
        /// Создаёт экземпляр класса <see cref="Grade"/> по умолчанию.
        /// </summary>
        public Grade() => ValuesGenerator.GenerateValues(_idGenerator, () =>
            Grades.FirstOrDefault((d) => d.Name == Name) != null, (id) => {
                Name = nameof(Name) + "_" + id;
                Symbol = nameof(Symbol) + "_" + id;
            }, () => Grades.Add(this));
    }
}