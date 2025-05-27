using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresidentialGameEngine.ClassLibrary.Interfaces
{
    public interface IPositioningComponent<SubjectEnum>
        where SubjectEnum : Enum
    {
        public SubjectEnum[] GetSubjectOrder { get; }

        public void SetSubjectOrder(IEnumerable<SubjectEnum> orderedSubjectValues);

        public void MoveSubjectUp(SubjectEnum subjectEnum);

    }
}
