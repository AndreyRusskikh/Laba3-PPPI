namespace Laba3
{
    public class Listener
    {
        private List<ListEntry> entry;
        public void WorkersChanged(object source, WorkersChangedEventArgs<string> e)
        {
            if (entry == null)
            {
                entry = new List<ListEntry>();
            }
            ListEntry listEntry = new ListEntry(e.CollectionName, e.update, e.ChangePropertiesName, e.ChangedKey);
            entry.Add(listEntry);
        }

        public override string ToString()
        {
            string AllEl = "";
            if (entry != null)
            {
                foreach (ListEntry e in entry)
                    AllEl += $"{e.ToString()}\n";
                return AllEl;
            }
            return "Список пуст";
        }


    }
    public class ListEntry
    {
        public string CollectionName { get; set; }
        public Update typeOfProperty { get; set; }
        public string PropertyName { get; set; }

        public string KeyChangeEl { get; set; }
        public ListEntry(string CollName, Update update, string propName, string KeyEl)
        {
            CollectionName = CollName;
            typeOfProperty = update;
            PropertyName = propName;
            KeyChangeEl = KeyEl;
        }

        public override string ToString()
        {
            string result = $"Collection Name: {CollectionName}\nUpdate: {typeOfProperty}\nChangePropertiesName: {PropertyName}\nKeyChaneElement:  {KeyChangeEl}\n\n";

            return result;
        }
    }
}
