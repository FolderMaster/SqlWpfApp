﻿namespace ViewModel.Interfaces.Services.Document
{
    public interface ISelection
    {
        public bool? Bold { get; set; }

        public bool? Italic { get; set; }

        public double? FontSize { get; set; }

        public object? FontFamily { get; set; }

        public object? Alignment { get; set; }

        public IRange Range { get; }

        public void InsertList(object markerStyle);

        public void InsertTable(int columnsCount, int rowsCount);

        public void InsertImage(object data);

        public void Select(IRange range);
    }
}
