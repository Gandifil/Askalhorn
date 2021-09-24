using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Askalhorn.Common;
using Newtonsoft.Json;

namespace Askalhorn.Plot
{
    public sealed class Quest: IQuest
    {
        string IQuest.Name => _quest.Name.ToString();

        string IQuest.Description
        {
            get
            {
                var builder = new StringBuilder();
                builder.AppendLine(_quest.Description.ToString());

                var step = _questStep;
                var stack = new Stack<string>();
                do
                {
                    stack.Push(step.Description.ToString());
                    step = _quest.Steps.Find(x => x.Name == step.NameOfLastStep);
                } while (step is not null);
                
                while(stack.Any())
                    builder.AppendLine(stack.Pop());

                return builder.ToString();
            }
        }

        //IReadOnlyCollection<IQuestStep> IQuest.Steps => _quest.Steps;

        public QuestState State { get; private set; }

        private QuestStep _questStep;
        private string _currentStep;

        public string CurrentStep
        {
            get => _currentStep;
            set
            {
                var step = _quest.Steps.Find(x => x.Name == value);

                if (step is null)
                    throw new ArgumentOutOfRangeException(nameof(value), "Failed to find step");
                
                if (step.NameOfLastStep != _currentStep)
                    throw new InvalidOperationException( "Next.NameOfLastStep must be equals a current.");
                
                _currentStep = value;
                _questStep = step;
            }
        }

        public readonly string ContentName;
        private readonly QuestFile _quest;

        [JsonConstructor]
        public Quest(string contentName, string step, QuestState state)
        {
            ContentName = contentName;
            State = state;
            CurrentStep = step;
            
            var filePath = $"quests/{ContentName}";
            _quest = Storage.Content.Load<QuestFile>(filePath);
        }

        public Quest(string contentName, string step = null)
        {
            ContentName = contentName;
            State = QuestState.InProgress;
            
            var filePath = $"quests/{ContentName}";
            _quest = Storage.Content.Load<QuestFile>(filePath);
            
            if (step is null)
            {
                _questStep = _quest.Steps.Find(x => string.IsNullOrEmpty(x.NameOfLastStep));
                _currentStep = _questStep.Name;
            }
            else
                CurrentStep = step;
        }

        public void Complete(string stepName = null)
        {
            State = QuestState.Completed;

            if (string.IsNullOrEmpty(stepName))
            {
                var noFinishStep = _quest.Steps.Select(x => x.NameOfLastStep).ToList();
                var step = _quest.Steps.Find(x => noFinishStep.Find(y => y == x.Name) is null);
                
                _currentStep = step.Name;
                _questStep = step;
            }
            else
                CurrentStep = stepName;
        }

        public void Fail()
        {
            State = QuestState.Failed;
        }

        private bool Equals(Quest other)
        {
            return ContentName == other.ContentName;
        }

        public bool Equals(IQuest? other)
        {
            var anotherQuest = other as Quest;
            if (anotherQuest is null)
                return false;
            else
                return Equals(anotherQuest);
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is Quest other && Equals(other);
        }

        public override int GetHashCode()
        {
            return (ContentName != null ? ContentName.GetHashCode() : 0);
        }
    }
}