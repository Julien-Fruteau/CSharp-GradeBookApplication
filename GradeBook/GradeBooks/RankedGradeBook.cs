using System;
using GradeBook.Enums;
using System.Linq;

namespace GradeBook.GradeBooks
{
    public class RankedGradeBook: BaseGradeBook
    {
        public RankedGradeBook(string name, bool isWeighted): base(name, isWeighted)
        {
            Type = GradeBookType.Ranked;
        }

        public override char GetLetterGrade(double averageGrade)
        {
            if (Students.Count < 5)
                throw new InvalidOperationException();

            char rankPosition = 'A';
            int twentyPercent = Students.Count / 5;
            var descendingSortedGrades = GetDescendingSortedGrades();

            for (int i = 0; i < Students.Count; i += twentyPercent)
            {
                if (averageGrade.CompareTo(descendingSortedGrades[i]) >= 0)
                    break;
                rankPosition = DropLetterGrade(rankPosition);
            }
            return rankPosition;
        }


        public double[] GetDescendingSortedGrades()
        {
            return Students.OrderByDescending(x => x.AverageGrade).Select(x => x.AverageGrade).ToArray<double>();
        }

        public char DropLetterGrade(char rankPosition)
        {
            switch (rankPosition)
            {
                case 'A':
                    return 'B';
                case 'B':
                    return 'C';
                case 'C':
                    return 'D';
                default:
                    return 'F';
            }
        }

        public override void CalculateStatistics()
        {
            if (Students.Count < 5)
            {
                Console.WriteLine("Ranked grading requires at least 5 students with grades in order to properly calculate a student's overall grade.");
                return;
            }
            base.CalculateStatistics();
        }

        public override void CalculateStudentStatistics(string name)
        {
            if (Students.Count < 5)
            {
                Console.WriteLine("Ranked grading requires at least 5 students with grades in order to properly calculate a student's overall grade.");
                return;
            }
            base.CalculateStudentStatistics(name);
        }
    }
}