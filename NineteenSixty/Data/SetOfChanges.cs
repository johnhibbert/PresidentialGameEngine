using NineteenSixty.Enums;
using PresidentialGameEngine.ClassLibrary.Data;

namespace NineteenSixty.Data;

public record SetOfChanges
{
    //I don't think it's necessary to have a generic version of this, since the types will depend greatly

    public List<SupportChange<Player, Issue>> IssueChanges { get; internal set; } = [];
    public List<SupportChange<Player, State>> StateChanges { get; internal set; } = [];
    public List<SupportChange<Player, Region>> EndorsementChanges { get; internal set; } = [];
    public List<SupportChange<Player, Region>> MediaSupportChanges { get; internal set; } = [];

    //not internal set because we are not adding to a list, but providing a list.
    //This should probably change.
    public List<Issue> NewIssuesOrder { get; set; } = [];
}
