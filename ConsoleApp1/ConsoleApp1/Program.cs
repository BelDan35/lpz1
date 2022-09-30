using System.Diagnostics;

class Person
{
    private string name;
    private string secondname;
    private DateTime dateOfBirth;

    public Person(string name, string secondname, System.DateTime birthday)
    {
        this.name = name;
        this.secondname = secondname;
        this.dateOfBirth = birthday;
    }

    public Person()
    {
        this.name = "";
        this.secondname = "";
        this.dateOfBirth = new DateTime();
    }

    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    public string SecondName
    {
        get { return secondname; }
        set { secondname = value; }
    }

    public DateTime DateOfBirth
    {
        get { return dateOfBirth; }
        set { dateOfBirth = value; }
    }

    public int YearOfDateOfBirth
    {
        get { return dateOfBirth.Year; }
        set { dateOfBirth = new DateTime(value, dateOfBirth.Month, dateOfBirth.Day); }
    }

    public override string ToString()
    {
        return dateOfBirth.Date.ToString() + " " + secondname + " " + name;
    }

    public string ToShortString()
    {
        return name + " " + secondname;
    }
}

enum Education { Specialist, Bachelor, SecondEducation }

class Exam
{
    public string Discipline { get; set; }
    public int Rate { get; set; }
    public DateTime DateOfExam { get; set; }

    public Exam(string discipline, int rate, DateTime dateOfExam)
    {
        this.Discipline = discipline;
        this.Rate = rate;
        this.DateOfExam = dateOfExam;
    }
    public Exam()
    {
        this.Discipline = "";
        this.Rate = 0;
        this.DateOfExam = DateTime.Now;
    }
    public override string ToString()
    {
        return Discipline + " " + Rate + " " + DateOfExam;
    }
}

class Student
{
    private Person person;
    private Education education;
    private int group;
    private readonly List<Exam> exams = new List<Exam>();

    public Student(Person person, Education education, int group)
    {
        this.person = person;
        this.education = education;
        this.group = group;
    }

    public Student()
    {
        this.person = new Person();
        this.education = Education.Bachelor;
        this.group = 204;
    }

    public Person Person
    {
        get { return person; }
        set { person = value; }
    }

    public Education Education
    {
        get { return education; }
        set { education = value; }
    }

    public int Group
    {
        get { return group; }
        set { group = value; }
    }

    public Exam[] Exams
    {
        get { return exams.ToArray(); }

    }

    public double AvgRate
    {
        get { return exams.Count > 0 ? exams.Average(e => e.Rate) : 0; }
    }

    public bool this[Education index]
    {
        get
        {
            return this.Education == index;
        }
    }

    public void AddExams(params Exam[] exams)
    {
        this.exams.AddRange(exams);
    }

    public override string ToString()
    {
        return string.Format("{0} {1} {2} {3}", person, education, group, string.Join(", ", exams));
    }

    public string ToShortString()
    {
        return string.Format("{0} {1} {2} {3:0.00}", person, education, group, AvgRate);
    }
}

internal class Program
{
    private static void Main(string[] args)
    {
        var student = new Student(new Person("Вася", "Пупкин", new DateTime(2004, 12, 09)), Education.Bachelor, 89);
        Console.WriteLine(student.ToShortString());

        Console.WriteLine(student[Education.Bachelor]);
        Console.WriteLine(student[Education.Specialist]);
        Console.WriteLine(student[Education.SecondEducation]);

        student.Person = new Person("Иван", "Иванов", new DateTime(2021, 03, 08));
        student.Group = 205;
        student.Education = Education.SecondEducation;

        Console.WriteLine(student);

        student.AddExams(new Exam("Ино", 5, new DateTime(2004, 08, 11)), new Exam("C#", 5, new DateTime(2004, 09, 11)));

        Console.WriteLine(student);

        var linearArray = new Exam[1000000];
        var rectArray = new Exam[1000, 1000];
        var jaggedArray = new Exam[1000][];

        for (int i = 0; i < jaggedArray.Length; i++)
            jaggedArray[i] = new Exam[1000];

        var a = Stopwatch.StartNew();

        for (int i = 0; i < 1000000; i++)
            linearArray[i] = null;


        a.Stop();
        Console.WriteLine(a.Elapsed);

        a = Stopwatch.StartNew();

        for (int i = 0; i < 1000; i++)
            for (int j = 0; j < 1000; j++)
                rectArray[i, j] = null;

        a.Stop();
        Console.WriteLine(a.Elapsed);


        a = Stopwatch.StartNew();

        for (int i = 0; i < 1000; i++)
            for (int j = 0; j < 1000; j++)
                jaggedArray[i][j] = null;

        a.Stop();
        Console.WriteLine(a.Elapsed);

        Console.ReadKey();
    }
}