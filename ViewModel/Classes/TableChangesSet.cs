namespace ViewModel.Classes
{
    public class TableChangesSet
    {
        private int _addedCount;

        private int _removedCount;

        private int _modifiedCount;

        public int AddedCount
        {
            get => _addedCount;
            set => _addedCount = value;
        }

        public int RemovedCount
        {
            get => _removedCount;
            set => _removedCount = value;
        }

        public int ModifiedCount
        {
            get => _modifiedCount;
            set => _modifiedCount = value; 
        }

        public int TotalCount => _addedCount + _removedCount + _modifiedCount;

        public TableChangesSet() { }

        public TableChangesSet(int addedCount, int removedCount, int modifiedCount)
        {
            AddedCount = addedCount;
            RemovedCount = removedCount;
            ModifiedCount = modifiedCount;
        }
    }
}
