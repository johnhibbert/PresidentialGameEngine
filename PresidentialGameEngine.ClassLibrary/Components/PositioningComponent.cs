using PresidentialGameEngine.ClassLibrary.Interfaces;

namespace PresidentialGameEngine.ClassLibrary.Components
{
    public class PositioningComponent<SubjectEnum> : IPositioningComponent<SubjectEnum>
        where SubjectEnum : Enum
    {
        private List<SubjectEnum> SubjectOrder { get; set; }

        public PositioningComponent()
        {
            SubjectOrder = [];

            var subjectValues = Enum.GetValues(typeof(SubjectEnum)).OfType<SubjectEnum>().ToList();
            var valueOfNone = (SubjectEnum)Enum.ToObject(typeof(SubjectEnum), 0);
            subjectValues.Remove(valueOfNone);

            SubjectOrder = subjectValues;
        }

        public void SetSubjectOrder(IEnumerable<SubjectEnum> orderedSubjectValues) 
        {
            SubjectOrder = orderedSubjectValues.ToList();
        }

        public void MoveSubjectUp(SubjectEnum subjectEnum) 
        {
            var itemIndex = SubjectOrder.IndexOf(subjectEnum);
            if (itemIndex > 0)
            {
                var item = SubjectOrder[itemIndex];
                SubjectOrder.Remove(item);
                SubjectOrder.Insert(itemIndex - 1, item);
            }
        }
    }
}
