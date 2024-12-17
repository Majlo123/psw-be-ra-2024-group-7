using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;

namespace Explorer.Tours.Core.Domain;

public class Quiz : Entity
{
    public int TourId { get; init; }
    public string Title { get; private set; }
    public List<QuizQuestion> Questions { get; private set; } = new();

    // Podrazumevani konstruktor potreban za EF Core
    private Quiz()
    {
        // Ostaviti inicijalizaciju praznu za EF Core
    }

    // Konstruktor za poslovnu logiku
    public Quiz(int tourId, string title, List<QuizQuestion> questions)
    {
        if (string.IsNullOrWhiteSpace(title)) throw new ArgumentNullException(nameof(title));
        if (questions == null || questions.Count == 0) throw new ArgumentNullException(nameof(questions));

        TourId = tourId;
        Title = title;
        Questions = questions;
    }

    public void AddQuestion(QuizQuestion question)
    {
        if (question == null) throw new ArgumentNullException(nameof(question));
        Questions.Add(question);
    }

    public bool Valid()
    {
        return !string.IsNullOrWhiteSpace(Title) && Questions != null && Questions.Count > 0;
    }
}
