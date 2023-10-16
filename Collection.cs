using System;
using System.Collections.Generic;
using System.ComponentModel;

// Клас для елементу колекції
public class CollectionItem : INotifyPropertyChanged
{
    private string name;

    public string Name
    {
        get { return name; }
        set
        {
            if (name != value)
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

// Клас для колекції
public class MyCollection<T> where T : CollectionItem
{
    private List<T> collection = new List<T>();

    public event EventHandler<CollectionChangedEventArgs> CollectionChanged;

    // Додаємо елемент до колекції
    public void AddItem(T item)
    {
        collection.Add(item);
        item.PropertyChanged += Item_PropertyChanged;
        OnCollectionChanged(new CollectionChangedEventArgs("ItemAdded", collection.Count - 1));
    }

    // Замінюємо елемент в колекції
    public void ReplaceItem(int index, T newItem)
    {
        if (index >= 0 && index < collection.Count)
        {
            collection[index].PropertyChanged -= Item_PropertyChanged;
            collection[index] = newItem;
            collection[index].PropertyChanged += Item_PropertyChanged;
            OnCollectionChanged(new CollectionChangedEventArgs("ItemReplaced", index));
        }
    }

    // Видаляємо елемент з колекції
    public void RemoveItem(int index)
    {
        if (index >= 0 && index < collection.Count)
        {
            collection[index].PropertyChanged -= Item_PropertyChanged;
            collection.RemoveAt(index);
            OnCollectionChanged(new CollectionChangedEventArgs("ItemRemoved", index));
        }
    }

    // Обробник події PropertyChanged для елементів колекції
    private void Item_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == "Name")
        {
            var item = (T)sender;
            int index = collection.IndexOf(item);
            OnCollectionChanged(new CollectionChangedEventArgs("ItemPropertyChanged", index));
        }
    }

    // Метод для сповіщення про зміни в колекції
    protected virtual void OnCollectionChanged(CollectionChangedEventArgs e)
    {
        CollectionChanged?.Invoke(this, e);
    }
}

// Аргументи для подій зміни колекції
public class CollectionChangedEventArgs : EventArgs
{
    public string ChangeType { get; }
    public int Index { get; }

    public CollectionChangedEventArgs(string changeType, int index)
    {
        ChangeType = changeType;
        Index = index;
    }
}


