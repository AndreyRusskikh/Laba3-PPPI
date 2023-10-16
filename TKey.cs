using System.Data;

namespace Laba3
{
    public delegate TKey KeySelector<TKey>(StationWorker sw);

    public class WorkersChangedEventArgs<TKey> : EventArgs
    {
        public string CollectionName { get;  set; }
        public Update update { get;  set; }
        public string ChangePropertiesName { get; set; }
        public TKey ChangedKey { get; set; }

        public WorkersChangedEventArgs(string collectionName, Update updated , string changePropName, TKey changedKey)
        {
            CollectionName = collectionName;
            update = updated;
            ChangePropertiesName = changePropName;
            ChangedKey = changedKey;
        }
        public override string ToString()
        {
            string result = $"Collection Name: {CollectionName}\nUpdate: {update}\nChangePropertiesName: {ChangePropertiesName}\nChangedKey:  {ChangedKey}";
            
            return result;
        }
    }

    public class StationWorkerCollection<TKey>
    {
        public string CollectionName { get; set; }

        private Dictionary<TKey, StationWorker>? workerDictionary;
        private KeySelector<TKey> keySelector;
        public event WorkersChangedHandler<TKey> WorkersChanged;

        internal Dictionary<TKey, StationWorker>? WorkerDictionary
        {
            get { return workerDictionary; }
            set { workerDictionary = value; }
        }
       
        public void ChangeRank(TKey key, Rank newRank)
        {
            if (workerDictionary.ContainsKey(key))
            {
                workerDictionary[key].Categoria = newRank;
                OnWorkersChanged(new WorkersChangedEventArgs<TKey>(CollectionName, Update.Property, "workerDictionary.Categoria", key));

            }
            else 
            {
                Console.WriteLine("Такого элемента нет в словаре");
            }
        }
        public void ChangeSpecialization(TKey key, string newSpecializacia)
        {
            if (workerDictionary.ContainsKey(key))
            {
                workerDictionary[key].Specialiazacia = newSpecializacia;
                OnWorkersChanged(new WorkersChangedEventArgs<TKey>(CollectionName, Update.Property, "workerDictionary.Specialiazacia", key));

            }
            else
            {
                Console.WriteLine("Такого элемента нет в словаре");
            }
        }
        public void ChangeStage(TKey key, int newStage)
        {
            if (workerDictionary.ContainsKey(key))
            {
                workerDictionary[key].Stage = newStage;
                OnWorkersChanged(new WorkersChangedEventArgs<TKey>(CollectionName, Update.Property, "workerDictionary.Stage", key));

            }
            else
            {
                Console.WriteLine("Такого элемента нет в словаре");
            }
        }
        public int MaxStage
        {
            get
            {

                if (workerDictionary.Any())
                {
                    return workerDictionary.Values.Max(worker => worker.Stage);
                }

                return -1;
            }
        }
        public IEnumerable<IGrouping<Rank, KeyValuePair<TKey, StationWorker>>> GroupedByRank
        {
            get
            {
                return workerDictionary.GroupBy(pair => pair.Value.Categoria);
            }
        }

        public IEnumerable<KeyValuePair<TKey, StationWorker>> RankForm(Rank value)
        {
            return workerDictionary.Where(pair => pair.Value.Categoria == value);
        }

        public List<StationWorker> GetWorkersBySpecialization(string specialization)
        {
            return workerDictionary.Values.Where(worker => worker.Specialiazacia == specialization).ToList();
        }


        public StationWorkerCollection(KeySelector<TKey> keySelector)
        {
            this.keySelector = keySelector;
            workerDictionary = new Dictionary<TKey, StationWorker>();
            CollectionName = "StationWorkerCollection";
        }

        public void AddDefaults()
        {
            if (workerDictionary == null)
            {
                workerDictionary = new Dictionary<TKey, StationWorker>();
            }
            for (int i = 0; i < 10; i++)
            {
                StationWorker stationWorker = new StationWorker();
                TKey key = keySelector(stationWorker);
                workerDictionary[key] = stationWorker;
                OnWorkersChanged(new WorkersChangedEventArgs<TKey>(CollectionName, Update.Add, "workerDictionary", key));

            }
        }

        public void AddWorker(params StationWorker[] workers)

        {
            if (workerDictionary == null)
            {
                workerDictionary = new Dictionary<TKey, StationWorker>();
            }
            foreach (var worker in workers)
            {
                TKey key = keySelector(worker);
                workerDictionary[key] = worker;

                OnWorkersChanged(new WorkersChangedEventArgs<TKey>(CollectionName, Update.Add, "workerDictionary", key));
            }
        }

        public bool Replace(StationWorker swOld, StationWorker swNew)
        {
            TKey oldKey = keySelector(swOld);
            TKey newKey = keySelector(swNew);

            if (workerDictionary.ContainsKey(oldKey))
            {
                workerDictionary.Remove(oldKey);
                workerDictionary[newKey] = swNew;

                OnWorkersChanged(new WorkersChangedEventArgs<TKey>(CollectionName, Update.Replace, "workerDictionary", newKey));

                return true;
            }
            else
            {
                return false;
            }
        }
        protected virtual void OnWorkersChanged(WorkersChangedEventArgs<TKey> e)
        {
            WorkersChanged?.Invoke(this, e);
        }
        public override string ToString()
        {
            string result = $"Collection Name: {CollectionName}\n";
            foreach (var kvp in workerDictionary)
            {
                result += kvp.Key + ": " + kvp.Value.ToString() + "\n";
            }
            return result;
        }

        public string ToShortString()
        {
            string result = $"Collection Name: {CollectionName}\n";
            foreach (var kvp in workerDictionary)
            {
                result += kvp.Key + ": " + kvp.Value.ToShortString() + "\n";
            }
            return result;
        }

        
    }
}
