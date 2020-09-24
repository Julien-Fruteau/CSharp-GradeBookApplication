using Xunit;
using GradeBook.GradeBooks;

namespace GradeBookTests
{
    public class TestRankedGradeBooks
    {
        public RankedGradeBook SetUp()
        {
            var names = new string[]
            {
                "toto", "titi", "grosminet", "riri", "fifi", "loulou"
            };
            var grades = new double[] { 50.0, 30.0, 60.0, 40.0, 80.0, 70.0 };
            RankedGradeBook book = new RankedGradeBook("class");
            for (int i = 0; i < names.Length; i++)
            {
                book.AddStudent(new GradeBook.Student(
                    names[i],
                    GradeBook.Enums.StudentType.Standard,
                    GradeBook.Enums.EnrollmentType.Campus
                ));
                book.AddGrade(names[i], grades[i]);
            }

            return book;
        }

        [Fact]
        public void TestGetLetterGrade()
        {
        //Given
            RankedGradeBook book = SetUp();
        //When
            var expected = new double[] { 80.0, 70.0, 60.0, 50.0, 40.0, 30.0 };
        //Then
            Assert.Equal(expected, book.GetDescendingSortedGrades());
        }

        [Fact]
        public void TestGetDescendingSortedGradesRankPosition()
        {
            //Given
            RankedGradeBook book = SetUp();
        //When
        //Then
            Assert.Equal('F', book.GetLetterGrade(20.0));
            Assert.Equal('F', book.GetLetterGrade(30.0));
            Assert.Equal('F', book.GetLetterGrade(39.9));
            Assert.Equal('D', book.GetLetterGrade(50.0));
            Assert.Equal('D', book.GetLetterGrade(50.1));
            Assert.Equal('C', book.GetLetterGrade(69.0));
            Assert.Equal('B', book.GetLetterGrade(75.0));
            Assert.Equal('A', book.GetLetterGrade(80.0));
            Assert.Equal('A', book.GetLetterGrade(90.0));
        }
    }
}