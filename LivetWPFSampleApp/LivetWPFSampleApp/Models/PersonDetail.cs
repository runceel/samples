using Codeplex.Reactive;
using Livet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace LivetWPFSampleApp.Models
{
    public class PersonDetail : NotificationObject
    {
        private readonly PeopleRepository repository = new PeopleRepository();

        private ISubject<object> interaction;


        #region EditTarget変更通知プロパティ
        private Person _EditTarget;

        public Person EditTarget
        {
            get
            { return _EditTarget; }
            private set
            { 
                if (_EditTarget == value)
                    return;
                _EditTarget = value;
                RaisePropertyChanged("EditTarget");
            }
        }
        #endregion


        public PersonDetail(ISubject<object> interaction)
        {
            this.interaction = interaction;
        }

        public void Update()
        {
            this.repository.Update(this.EditTarget);
            this.interaction.OnNext(
                CollectionChanged<Person>.Replace(-1, this.EditTarget));
        }

        public void SetEditTarget(long id)
        {
            this.EditTarget = this.repository.Find(id);
        }
    }
}
