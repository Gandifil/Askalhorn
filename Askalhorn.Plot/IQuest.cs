namespace Askalhorn.Plot
{
    public interface IQuest
    {
        QuestState State { get; }
        string Name { get; }
        string Description { get; }
        //IReadOnlyCollection<IQuestStep> Steps { get; }
    }
}