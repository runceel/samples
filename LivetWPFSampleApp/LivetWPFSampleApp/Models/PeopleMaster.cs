using Codeplex.Reactive;
using Livet;
using Livet.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reactive.Linq;
using System.Collections.Specialized;

namespace LivetWPFSampleApp.Models
{
    public class PeopleMaster : NotificationObject
    {
        private readonly PeopleRepository repository = new PeopleRepository();

        public ObservableCollection<Person> People { get; private set; }


        #region InputPerson変更通知プロパティ
        private Person _InputPerson = new Person();

        public Person InputPerson
        {
            get
            { return _InputPerson; }
            set
            { 
                if (_InputPerson == value)
                    return;
                _InputPerson = value;
                RaisePropertyChanged("InputPerson");
            }
        }
        #endregion


        public PeopleMaster(IObservable<object> interaction)
        {
            interaction.OfType<CollectionChanged<Person>>()
                .Where(x => x.Action == NotifyCollectionChangedAction.Replace)
                .Subscribe(x =>
                {
                    var target = this.People.First(y => y.ID == x.Value.ID);
                    target.Name = x.Value.Name;
                    target.Age = x.Value.Age;
                });

            this.People = new ObservableCollection<Person>();
        }

        public void Load()
        {
            this.People.Clear();
            var results = this.repository.Load();
            foreach (var i in results)
            {
                this.People.Add(i);
            }
        }

        public void Delete(long id)
        {
            this.repository.Delete(id);
            this.People.Remove(this.People.Single(x => x.ID == id));
        }

        public void AddPerson()
        {
            this.repository.Insert(this.InputPerson);
            this.People.Add(this.InputPerson);
            this.InputPerson = new Person();
        }
    }
}
