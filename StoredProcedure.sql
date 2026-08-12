USE employeedb;

SHOW PROCEDURE STATUS
WHERE Db = 'employeedb';

DELIMITER //

CREATE PROCEDURE GetAllEmployees()
Begin
        SELECT * FROM Employees;
End //
DELIMITER ; 



DELIMITER //

CREATE PROCEDURE AddEmployees(IN p_EmployeeId integer ,IN p_Empcode varchar(100) ,IN p_Empfname varchar(100), IN p_Emplname varchar(100), 
IN p_Empemail varchar(100), IN p_Empmobile int,IN p_DOB Date,IN p_DepartmentId int ,IN p_Salary decimal,
IN p_JoiningDate Date,IN p_IsActive boolean)
Begin
    INSERT INTO Employees(
        EmployeeId,Empcode,Empfname,Emplname,Empemail,Empmobile,DOB,DepartmentId,Salary,JoiningDate,IsActive
    )
    VALUES
    (
        p_EmployeeId,p_Empcode,p_Empfname,p_Emplname,p_Empemail,p_Empmobile,p_DOB,
        p_DepartmentId,p_Salary,p_JoiningDate,p_IsActive
    );
END //
DELIMITER ;



DELIMITER //
    CREATE PROCEDURE UpdateEmployees(IN p_EmployeeId int,IN p_Empcode varchar(100) ,IN p_Empfname varchar(100), IN p_Emplname varchar(100), 
IN p_Empemail varchar(100), IN p_Empmobile int,IN p_DOB Date,IN p_DepartmentId int ,IN p_Salary decimal,
IN p_JoiningDate Date,IN p_IsActive boolean)
    Begin
        UPDATE Employees
        SET 
       Empcode = p_Empcode,
       Empfname= p_Empfname,
       Emplname= p_Emplname,
       Empemail = p_Empemail,
       Empmobile = p_Empmobile,
       DOB= p_DOB,
       DepartmentId =p_DepartmentId,
       Salary = p_Salary,
       JoiningDate = p_JoiningDate,
       IsActive = p_IsActive
       WHERE EmployeeId = p_EmployeeId;

    END //
DELIMITER ;



DELIMITER //
CREATE PROCEDURE DeleteEmployee(IN p_EmployeeId int)
Begin
    Delete from Employees 
    WHERE EmployeeId= p_EmployeeId;

End //
DELIMITER ;


DELIMITER //

CREATE PROCEDURE Search(
    IN p_EmployeeId INT,
    IN p_Empfname VARCHAR(100),
    IN p_DepartmentId INT,
    IN p_Emplname VARCHAR(100)
)
BEGIN
    SELECT *
    FROM Employees
    WHERE (p_EmployeeId IS NULL OR EmployeeId = p_EmployeeId)
      AND (p_Empfname IS NULL OR Empfname LIKE CONCAT('%', p_Empfname, '%'))
      AND (p_Emplname IS NULL OR Emplname LIKE CONCAT('%', p_Emplname, '%'))
      AND (p_DepartmentId IS NULL OR DepartmentId = p_DepartmentId);
END //

DELIMITER ;

DELIMITER //
CREATE PROCEDURE GetEmployeeDashboard()
BEGIN
SELECT
 (SELECT COUNT(*)) AS TotalEmployees FROM Employees;

 (SELECT COUNT(*)) AS ActiveEmployees FROM Employees WHERE IsActive=1;

 (SELECT COUNT(*)) AS InactiveEmployees FROM Employees WHERE IsActive=0;

 (SELECT AVG(Salary)) AS averagesalary FROM Employees;

 (SELECT COUNT(*)) AS TotalDepartments FROM Departments;

 (SELECT COUNT(*)) AS EmployeesJoinedThisMonth
FROM Employees
WHERE MONTH(JoiningDate) = MONTH(CURDATE())
  AND YEAR(JoiningDate) = YEAR(CURDATE());

END //  

DELIMITER ;



DROP PROCEDURE IF EXISTS GetEmployeesDashboard;
