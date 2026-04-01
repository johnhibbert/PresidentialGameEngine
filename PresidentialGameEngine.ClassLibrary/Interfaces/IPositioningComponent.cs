namespace PresidentialGameEngine.ClassLibrary.Interfaces
{
    /// <summary>
    /// IPositioningComponent represents where some subject has a changeable order of priority.
    /// For 1960, this would cover the issues changing positions.
    /// </summary>
    /// <typeparam name="TSubject">The subject enumeration</typeparam>
    public interface IPositioningComponent<TSubject>
        where TSubject : Enum
    {
        public TSubject[] GetSubjectOrder { get; }

        public void SetSubjectOrder(IEnumerable<TSubject> orderedSubjectValues);

        public void MoveSubjectUp(TSubject subjectEnum);

    }
}
