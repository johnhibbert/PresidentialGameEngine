using PresidentialGameEngine.ClassLibrary.Interfaces;

namespace PresidentialGameEngine.ClassLibrary.Components
{
    public class PositioningComponent<TSubject> : IPositioningComponent<TSubject>
        where TSubject : Enum
    {
        private List<TSubject> SubjectOrder { get; set; }

        public TSubject[] GetSubjectOrder => [.. SubjectOrder];

        public PositioningComponent()
        {
            SubjectOrder = [];

            var subjectValues = Enum.GetValues(typeof(TSubject)).OfType<TSubject>().ToList();
            var valueOfNone = (TSubject)Enum.ToObject(typeof(TSubject), 0);
            subjectValues.Remove(valueOfNone);

            SubjectOrder = subjectValues;
        }

        public void SetSubjectOrder(IEnumerable<TSubject> orderedSubjectValues) 
        {
            if(orderedSubjectValues.Count() != SubjectOrder.Count) 
            {
                throw new ArgumentException($"New order count must match existing subject count. {SubjectOrder.Count}");
            }
            if(SubjectOrder.Except(orderedSubjectValues).Any()) 
            {
                var diff = SubjectOrder.Except(orderedSubjectValues);
                throw new ArgumentException($"New order contains element not found in current order. {string.Join(',', diff)}");
            }

            SubjectOrder = orderedSubjectValues.ToList();
        }

        public void MoveSubjectUp(TSubject subjectEnum) 
        {
            if (SubjectOrder.Contains(subjectEnum) == false)
            {
                throw new ArgumentException($"Subject not found in current order. ({subjectEnum})");
            }

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
