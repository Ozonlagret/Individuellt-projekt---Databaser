using Individuellt_projekt___Databaser.Models2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Individuellt_projekt___Databaser
{
    internal class QueryCollection
    {
        public string getGradesQuery = @"SELECT
                                        s.first_name + ' ' + s.surname AS 'Namn',
                                        g.grade AS 'Betyg',
                                        CONVERT(varchar, g.date_assigned, 23) AS 'Datum',
                                        t.first_name + ' ' + t.surname AS 'Betygsatt av',
                                        c.course_name AS 'Kurs'
                                    FROM Grades g
                                    JOIN Students s ON g.student_id = s.student_id
                                    JOIN Staff t ON g.staff_id = t.staff_id
                                    JOIN Courses c ON g.course_id = c.course_id
                                    WHERE s.student_id = @StudentId
                                    ORDER BY g.date_assigned DESC;";

        


        public string insertIntoStaffQuery = @"INSERT INTO Staff (staff_id, first_name, surname," +                            
                                      "position_id, hire_date, department_id)" +
                                      "VALUES (@StaffId, @FirstName, @Surname, @PositionId," +
                                      "@HireDate, @DepartmentId)";

       

        public string totalSalaryQuery = @"
                                    SELECT d.department_name, SUM(s.monthly_salary) AS 'lön'
                                    FROM Staff s
                                    JOIN Departments d ON s.department_id = d.department_id
                                    GROUP BY d.department_name;";

        

        public string averageSalaryQuery = @"
                                    SELECT d.department_name, AVG(s.monthly_salary) AS 'lön'
                                    FROM Staff s
                                    JOIN Departments d ON s.department_id = d.department_id
                                    GROUP BY d.department_name;";

       

        public string getEmployeesQuery = @"
                                    SELECT
                                       s.first_name + ' ' + s.surname AS Namn,
                                       p.position_name AS Befattning,
                                       CONVERT(varchar, s.hire_date, 23) AS Anställningsdatum
                                    FROM Staff s 
                                    Join Positions p ON s.position_id = p.position_id
                                    ORDER BY s.hire_date";



        public string insertGradeTransactionQuery = @"
                                    INSERT INTO Grades (grade_id, student_id, course_id, grade, date_assigned, staff_id) VALUES (@GradeId, @StudentId, @CourseId, @Grade, @Date, @StaffId);";
    }
}
