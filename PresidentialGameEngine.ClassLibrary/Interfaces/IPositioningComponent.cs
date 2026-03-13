using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresidentialGameEngine.ClassLibrary.Interfaces
{
    public interface IPositioningComponent<TSubject>
        where TSubject : Enum
    {
        public TSubject[] GetSubjectOrder { get; }

        public void SetSubjectOrder(IEnumerable<TSubject> orderedSubjectValues);

        public void MoveSubjectUp(TSubject subjectEnum);

    }
}
