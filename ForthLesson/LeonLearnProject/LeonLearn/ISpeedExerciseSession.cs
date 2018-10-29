using System;

namespace LeonLearn
{
    public interface ISpeedExerciseSession
    {
        Lesson CreateLesson();
        bool[] EndLesson(Lesson lesson, bool[] answers);
    }
}