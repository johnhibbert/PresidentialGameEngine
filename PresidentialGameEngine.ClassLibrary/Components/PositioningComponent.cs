using PresidentialGameEngine.ClassLibrary.Interfaces;

namespace PresidentialGameEngine.ClassLibrary.Components
{
    public class PositioningComponent<SubjectEnum> : IPositioningComponent<SubjectEnum>
        where SubjectEnum : Enum
    {
        private List<SubjectEnum> SubjectOrder { get; set; }

        public SubjectEnum[] GetSubjectOrder => [.. SubjectOrder];

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

        public void MoveSubjectUp(SubjectEnum subjectEnum) 
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
