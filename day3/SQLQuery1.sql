DROP TABLE Adress;
DROP TABLE Student;

CREATE TABLE Adress
(
	Adress_id int constraint adress_pk primary key,
	Street varchar(20) not null,
	Number int not null,
	City varchar(20) not null,
	Zipcode varchar(10) not null
);

CREATE TABLE Student
( 
	Student_id int constraint student_pk primary key,
	First_name varchar(20) not null,
	Last_name varchar(20) not null,
	Adress_id int not null constraint student_fk_adress references Adress(Adress_id)
);



INSERT INTO Adress VALUES(1, 'nesto', 2, 'nesto', '3562');
INSERT INTO Student VALUES(2, 'marko', 'markic', 1);


INSERT INTO Adress VALUES(3, 'nesto', 3, 'sezam', '3562');
INSERT INTO Student VALUES(4, 'martina', 'bla', 3);

SELECT * FROM Student;
SELECT * FROM Adress;