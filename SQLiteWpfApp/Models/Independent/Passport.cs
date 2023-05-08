﻿using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace SQLiteWpfApp.Models.Independent
{
    [Table("Passports")]
    [PrimaryKey(nameof(SerialNumber))]
    public class Passport : INotifyPropertyChanged
    {
        private static IdGenerator _idGenerator = new(0);

        private long _serialNumber = 0;

        private string _name = "";

        private string _permanentResidenceAddress = "";

        private Sex _sex = Sex.Male;

        private byte[] _scan = null;

        public static ObservableCollection<Passport> Passports { get; set; } = new();

        public long SerialNumber
        {
            get => _serialNumber;
            set
            {
                if(value != SerialNumber)
                {
                    _serialNumber = value;
                    InvokePropertyChanged(nameof(SerialNumber));
                }
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                if(value != Name)
                {
                    _name = value;
                    InvokePropertyChanged(nameof(Name));
                }
            }
        }

        public string PermanentResidenceAddress
        {
            get => _permanentResidenceAddress;
            set
            {
                if(value != PermanentResidenceAddress)
                {
                    _permanentResidenceAddress = value;
                    InvokePropertyChanged(nameof(PermanentResidenceAddress));
                }
            }
        }

        public Sex Sex
        {
            get => _sex;
            set
            {
                if(value != Sex)
                {
                    _sex = value;
                    InvokePropertyChanged(nameof(Sex));
                }
            }
        }

        public byte[] Scan
        {
            get => _scan;
            set
            {
                if (value != Scan)
                {
                    _scan = value;
                    InvokePropertyChanged(nameof(Scan));
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public Passport() => ValuesGenerator.GenerateValues(_idGenerator, () =>
            Passports.FirstOrDefault((p) => p.Name == Name) != null, (id) => {
                SerialNumber = id;
                Name = nameof(Name) + "_" + id;
                PermanentResidenceAddress = nameof(PermanentResidenceAddress) + "_" + id;
            }, () => Passports.Add(this));

        private void InvokePropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}