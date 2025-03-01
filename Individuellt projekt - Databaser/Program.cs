
using Individuellt_projekt___Databaser.Models2;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Data;
using System.Security.Cryptography;
using System.Threading.Channels;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Individuellt_projekt___Databaser
{
    internal class Program
    {
        static void AlignText(string text, int column)
        {
            Console.SetCursorPosition(column, Console.CursorTop);
            Console.Write(text);
        }
        static void Main(string[] args)
        {
            QueryCollection queries = new QueryCollection();
            var connectionString = @"Data Source=OMEGALUL;Initial Catalog=SchoolDB;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                

                using (var context = new SchoolDbContext())
                {
                    while (true)
                    {
                        connection.Open();
                        Console.Clear();
                        int choice = 0;

                        Console.WriteLine("1. Antal lärare i tjänst " +
                            "\n\n2. Elever" +
                            "\n\n3. Aktiva kurser" +
                            "\n\n4. Personal" +
                            "\n\n5. Lönestatistik" +
                            "\n\n6. Sök elev" +
                            "\n\n7. Sätt betyg");

                        choice = InputManagement.SecureInput(connection);
                        switch (choice)
                        {
                            case 1:
                                int teacherPositionId = context.Positions
                                      .Where(p => p.PositionName == "Teacher")
                                      .Select(p => p.PositionId)
                                      .FirstOrDefault(); // Fetches the PositionId for teachers

                                var teacherCounts = context.Departments
                                      .Select(d => new
                                      {
                                          d.DepartmentId,
                                          d.DepartmentName,
                                          TeacherCount = context.Staff
                                              .Count(s => s.DepartmentId == d.DepartmentId && s.PositionId == teacherPositionId)
                                      })
                                      .ToList();

                                foreach (var dept in teacherCounts)
                                {
                                    Console.WriteLine($"{dept.DepartmentName}: {dept.TeacherCount}");
                                }
                                InputManagement.ReturnToMenu();
                                break;



                            case 2:
                                var students = context.Students
                                    .Select(s => new
                                    {
                                        s.FirstName,
                                        s.Surname,
                                        s.Class.ClassName,
                                        s.BirthDate
                                    })
                                    .ToList();

                                Console.WriteLine($"{"Förnamn",-15} {"Efternamn",-15} {"Klass",-10} {"Födelsedatum",-15}");
                                Console.WriteLine(new string('-', 56));

                                foreach (var student in students)
                                {
                                    Console.WriteLine($"{student.FirstName,-15} {student.Surname,-15} " +
                                        $"{student.ClassName,-10} {student.BirthDate,-15}");
                                }


                                // ADO
                                Console.Write("Ange elev att ta fram betyg för (nr): ");
                                int numberInput = InputManagement.UserInput(1, context.Students.Count());

                                // query from QueryCollection
                                using (SqlCommand command = new SqlCommand(queries.getGradesQuery, connection))
                                {
                                    command.Parameters.AddWithValue("@StudentId", numberInput);
                                    using (SqlDataReader reader = command.ExecuteReader())
                                    {
                                        Console.Clear();
                                        if (reader.Read())
                                        {
                                            Console.WriteLine($"{reader["Namn"]}");
                                            Console.WriteLine(new string('-', 65));
                                            Console.WriteLine($"{"Kurs", -25} {"Betyg",-10} {"Betygsatt av",-15} {"Datum",-15}");
                                            Console.WriteLine(new string('-', 65));
                                            Console.WriteLine($"{reader["Kurs"],-25} {reader["Betyg"],-10} {reader["Betygsatt av"],-15} {reader["Datum"],-15}");
                                            while (reader.Read())
                                            {
                                                Console.WriteLine($"{reader["Kurs"],-25} {reader["Betyg"],-10} {reader["Betygsatt av"], -15} {reader["Datum"], -15}");
                                            }
                                        }
                                    }
                                }
                                InputManagement.ReturnToMenu();
                                break;



                            case 3:
                                var activeCourses = context.Courses
                                    .Where(c => c.StartDate.Date < DateTime.Today && c.EndDate > DateTime.Today)
                                    .Select(c => new
                                    {
                                        c.CourseName,
                                        c.StartDate,
                                        c.EndDate
                                    })
                                    .ToList();

                                Console.WriteLine($"{"Kurs",-25} {"Startdatum",-15} {"Slutdatum",-15}");
                                Console.WriteLine(new string('-', 53));

                                foreach (var course in activeCourses)
                                {
                                    string startDate = course.StartDate.ToString("yyyy-MM-dd");
                                    string endDate = course.EndDate.ToString("yyyy-MM-dd");
                                    Console.WriteLine($"{course.CourseName,-25} {startDate,-15} " +
                                        $"{endDate,-15}");
                                }

                                InputManagement.ReturnToMenu();
                                break;



                            case 4:
                                using (SqlCommand command = new SqlCommand(queries.getEmployeesQuery, connection))
                                using (SqlDataReader reader = command.ExecuteReader())
                                {
                                    Console.WriteLine($"" +
                                        $"{"Namn", -25} " +
                                        $"{"Befattning", -20} " +
                                        $"{"Anställningsdatum", -15}");

                                    Console.WriteLine(new string('-', 65));

                                    while (reader.Read())
                                    {
                                        Console.WriteLine($"" +
                                            $"{reader["Namn"], -25} " +
                                            $"{reader["Befattning"],-20} " +
                                            $"{reader["Anställningsdatum"],-15}");
                                    }
                                }

                                string firstName;
                                string surname;
                                int positionId;
                                int departmentId;
                                DateTime hireDate;
                                Console.SetCursorPosition(68, 1);
                                Console.WriteLine("1. Lägg till ny personal \n");
                                var userChoice = Console.ReadKey(true).Key;
                                if (userChoice == ConsoleKey.D1)
                                {
                                    AlignText("Ange förnamn:", 68);
                                    Console.SetCursorPosition(82, Console.CursorTop);
                                    firstName = Console.ReadLine();

                                    
                                    AlignText("Ange efternamn:",  68);
                                    Console.SetCursorPosition(84, Console.CursorTop);
                                    surname = Console.ReadLine();

                                    
                                    AlignText("Ange befattning:\n", 68);
                                    AlignText("1. Rektor\n", 68);
                                    AlignText("2. Vice Principal (dummy data genererat av chatgpt)\n", 68);
                                    AlignText("3. Administratör\n", 68);
                                    AlignText("4. Kurator\n", 68);
                                    AlignText("5. Sekreterare\n", 68);
                                    AlignText("6. Vaktmästare\n", 68);
                                    AlignText("7. IT-support\n", 68);
                                    AlignText("8. Bibliotekarie\n", 68);
                                    AlignText("9. Säkerhet\n", 68);
                                    AlignText("10. sjuksköterska\n", 68);
                                    AlignText("11. Lärare\n", 68);
                                    Console.SetCursorPosition(85, 5);

                                    
                                    while (true)
                                    {
                                        if (int.TryParse(Console.ReadLine(), out positionId))
                                        {
                                            if (positionId >= 1 && positionId <= context.Positions.Count())
                                            {
                                                break;
                                            }
                                        }
                                        Console.SetCursorPosition(68, Console.CursorTop + 12);
                                        Console.WriteLine("Ogiltig input");
                                        Console.SetCursorPosition(85, 5);

                                    }

                                    Console.SetCursorPosition(68, 18);
                                    AlignText("Ange avdelning:\n", 68);
                                    AlignText("1. CIA\n", 68);
                                    AlignText("2. FBI\n", 68);
                                    AlignText("3. KKB\n", 68);
                                    Console.SetCursorPosition(84, 18);

                                    
                                    while (true)
                                    {
                                        if (int.TryParse(Console.ReadLine(), out departmentId)) 
                                        {
                                            if (departmentId >= 1 && departmentId <= context.Departments.Count())
                                            {
                                                break;
                                            }
                                        }
                                        Console.SetCursorPosition(68, 22);
                                        Console.WriteLine("Ogiltig input");
                                        Console.SetCursorPosition(84, 18);
                                    }

                                    hireDate = DateTime.Now;
                                    int staffId = context.Staff.Count() + 1;

                                    using (SqlCommand command = new SqlCommand(queries.insertIntoStaffQuery, connection))
                                    {
                                        command.Parameters.AddWithValue("@StaffId", staffId);
                                        command.Parameters.AddWithValue("@FirstName", firstName);
                                        command.Parameters.AddWithValue("@Surname", surname);
                                        command.Parameters.AddWithValue("@PositionId", positionId);
                                        command.Parameters.AddWithValue("@HireDate", hireDate);
                                        command.Parameters.AddWithValue("@DepartmentId", departmentId);

                                        var rowsAffected = command.ExecuteNonQuery();
                                        Console.WriteLine(rowsAffected);
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Ogiltig input");
                                    InputManagement.ReturnToMenu();
                                }


                                    InputManagement.ReturnToMenu();
                                break;
                            case 5:
                                using (SqlCommand command = new SqlCommand(queries.totalSalaryQuery, connection))
                                using (SqlDataReader reader = command.ExecuteReader())
                                {
                                    Console.WriteLine($"{"Avdelning",-25} {"Total löneutbetalning",-20}");
                                    Console.WriteLine(new string('-', 48));

                                    while (reader.Read())
                                    {
                                        Console.WriteLine($"{reader["department_name"],-25} {reader["lön"],-20}");
                                    }
                                }

                                using (SqlCommand command = new SqlCommand(queries.averageSalaryQuery, connection))
                                using (SqlDataReader reader = command.ExecuteReader())
                                {
                                    Console.WriteLine($"\n\n{"Avdelning",-25} {"Genomsnittslön per anställd",-20}");
                                    Console.WriteLine(new string('-', 54));

                                    while (reader.Read())
                                    {
                                        Console.WriteLine($"{reader["department_name"],-25} {reader["lön"],-20}");
                                    }
                                }

                                InputManagement.ReturnToMenu();
                                break;
                            case 6:
                                Console.Write("Elev-ID: ");
                                int studentId;
                                while (true)
                                {
                                    if (int.TryParse(Console.ReadLine(), out studentId))
                                    {
                                        if (studentId >= 1 && studentId <= context.Students.Count()) 
                                        break;
                                    }
                                    Console.WriteLine("Ogiltig input");
                                }

                                using (SqlCommand command = new SqlCommand("GetStudent", connection))
                                {
                                    command.CommandType = CommandType.StoredProcedure;
                                    command.Parameters.Add(new SqlParameter("@StudentID", studentId));
                                  

                                    using (SqlDataReader reader = command.ExecuteReader())
                                    {
                                        while (reader.Read())
                                        {
                                            string birthDate = Convert.ToDateTime(reader["birth_date"]).ToShortDateString();
                                            Console.WriteLine($"{reader["first_name"],-10} {reader["surname"],-10}" +
                                                $"{birthDate,-15} {reader["class_name"]}", -10);
                                        }
                                    }
                                }
                                InputManagement.ReturnToMenu();
                                break;
                            case 7:
                                int gradeId = 0;
                                var gradeIds = context.Grades
                                    .Select(g => g.GradeId)
                                    .ToList();
                                while (gradeIds.Contains(gradeId))
                                {
                                    gradeId++;

                                }

                                Console.WriteLine("Elev-ID: ");
                                int studentNumber = InputManagement.UserInput(1, context.Students.Count());

                                Console.WriteLine("Kurs-ID: ");
                                int courseId = InputManagement.UserInput(1, context.Courses.Count());

                                Console.WriteLine("Lärare-Id: ");
                                int personelId = InputManagement.UserInput(11, 1000);

                                Console.WriteLine("Ange betyg (A-F): ");
                                string? input = Console.ReadLine();
                                char grade = char.ToUpper(input[0]);

                                DateTime signageDate = DateTime.Today;

                                using (SqlTransaction transaction = connection.BeginTransaction())
                                {
                                    try
                                    {
                                        using (SqlCommand command = new SqlCommand(queries.insertGradeTransactionQuery, connection, transaction))
                                        {
                                            command.Parameters.AddWithValue("@GradeId", gradeId);
                                            command.Parameters.AddWithValue("@StudentId", studentNumber);
                                            command.Parameters.AddWithValue("@CourseId", courseId);
                                            command.Parameters.AddWithValue("@Grade", grade);
                                            command.Parameters.AddWithValue("@StaffId", personelId);
                                            command.Parameters.AddWithValue("@Date", signageDate);
                                            
                                            int rowsAffected = command.ExecuteNonQuery();
                                            if (rowsAffected > 0)
                                            {
                                                transaction.Commit();
                                                Console.WriteLine("Betygstillgivning lyckad");
                                            }
                                            else
                                            {
                                                Console.WriteLine("Inget hände ¯\\_(ツ)_/¯");
                                                transaction.Rollback();
                                            }
                                        }
                                    }
                                    catch (Exception e)
                                    {
                                        transaction.Rollback();
                                        Console.WriteLine("Betygstillgivning misslyckad");
                                    }
                                }
                                
                                InputManagement.ReturnToMenu();
                                break;
                            default:
                                break;
                        }
                    connection.Close();
                    }
        }
    }
}
    }
}
