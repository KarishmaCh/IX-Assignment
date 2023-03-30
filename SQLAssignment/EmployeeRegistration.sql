CREATE  database edu
use edu

CREATE TABLE Employee (
    employee_id INT IDENTITY(1,1) PRIMARY KEY,
    first_name NVARCHAR(10) NOT NULL CHECK (first_name <> ''),
    last_name NVARCHAR(10) NOT NULL CHECK (last_name <> ''),
    email NVARCHAR(100) NOT NULL UNIQUE CHECK (email LIKE '%_@%.%'),
    mobile_number NVARCHAR(10) NOT NULL CHECK (mobile_number LIKE '[0-9]%' AND mobile_number NOT LIKE '%[^0-9]%'),
    gender NVARCHAR(10) NOT NULL CHECK (gender IN ('Male', 'Female', 'Other')),
    age INT NOT NULL CHECK (age >= 18 AND age <= 99),
    date_of_birth DATE NOT NULL CHECK (date_of_birth <= DATEADD(YEAR, -18, GETDATE())),
    created_by INT NOT NULL,
    created_date DATETIME NOT NULL,
    updated_by INT,
    updated_date DATETIME,
    is_active BIT NOT NULL
);

-- Insert data into Employee table
INSERT INTO Employee (first_name, last_name, email, mobile_number, gender, age, date_of_birth, created_by, created_date, is_active)
VALUES
('John', 'Doe', 'johndoe@example.com', '9876543210', 'Male', 25, '1996-01-01', 1, GETDATE(), 1),
('Jane', 'Doe', 'janedoe@example.com', '1234567890', 'Female', 27, '1994-01-01', 2, GETDATE(), 1),
('Bob', 'Smith', 'bobsmith@example.com', '5678901234', 'Other', 30, '1991-01-01', 3, GETDATE(), 0);



CREATE TABLE Course (
  course_id INT PRIMARY KEY IDENTITY(1,1),
  course_name VARCHAR(255) NOT NULL,

  duration INT NOT NULL
);


CREATE TABLE Employee_Course (
    employee_id INT NOT NULL,
    course_id INT NOT NULL,
  
    created_by INT NOT NULL,
    created_date DATETIME DEFAULT CURRENT_TIMESTAMP NOT NULL,
    updated_by INT,
    updated_date DATETIME DEFAULT CURRENT_TIMESTAMP  NOT NULL,
    is_active BIT NOT NULL,
    PRIMARY KEY (employee_id, course_id),
    FOREIGN KEY (employee_id) REFERENCES Employee(employee_id),
   FOREIGN KEY (course_id) REFERENCES Course(course_id),
  
);
CREATE TABLE Education (
    education_id INT PRIMARY KEY  IDENTITY(1,1),
    employee_id INT NOT NULL,
    course_id INT NOT NULL,
    created_by INT NOT NULL,
    created_date DATETIME DEFAULT CURRENT_TIMESTAMP NOT NULL,
    updated_by INT,
    updated_date DATETIME DEFAULT CURRENT_TIMESTAMP  NOT NULL,
    is_active BIT NOT NULL,
    FOREIGN KEY (employee_id) REFERENCES Employee(employee_id),
  FOREIGN KEY (course_id) REFERENCES Course(course_id)
);



-- Insert data into Course table
INSERT INTO Course (course_name, duration)
VALUES
('mca', 3),
('be', 3),
('Mba', 2);
select * from Course

-- Insert data into Employee_Course table
INSERT INTO Employee_Course (employee_id, course_id, created_by, updated_by, is_active)
VALUES
(1, 1, 1, NULL, 1),
(2, 2, 2, NULL, 1),
(3, 1, 3, NULL, 0);

-- Insert data into Education table
INSERT INTO Education (employee_id, course_id, created_by, created_date, is_active)
VALUES
(1, 2, 1, GETDATE(), 1),
(2, 3, 2, GETDATE(), 1),
(3, 1, 3, GETDATE(), 0);

SELECT * FROM Education;

SELECT * FROM Course;
SELECT * FROM Employee_Course;

SELECT * FROM Employee_Course;


CREATE TABLE EmployeePhotos (
    photo_id INT PRIMARY KEY IDENTITY(1,1),
    employee_id INT NOT NULL,
    FOREIGN KEY (employee_id) REFERENCES Employee(employee_id),
    profile_photo VARBINARY(MAX) NOT NULL,
    created_by INT NOT NULL,
    created_date DATETIME DEFAULT CURRENT_TIMESTAMP NOT NULL,
    updated_by INT,
    updated_date DATETIME DEFAULT CURRENT_TIMESTAMP NOT NULL,
    is_active BIT NOT NULL
);
INSERT INTO EmployeePhotos (employee_id, profile_photo, created_by, updated_by, is_active)
VALUES (1, 0x89504E470D0A1A0, 123, NULL, 1);
INSERT INTO EmployeePhotos (employee_id, profile_photo, created_by, updated_by, is_active)
VALUES (3, 0x89504E470D0A1A0, 23, 2, 1);
SELECT * FROM EmployeePhotos;


CREATE TABLE countries (
    country_id INT PRIMARY KEY IDENTITY(1,1),
    country_name NVARCHAR(50) NOT NULL UNIQUE,
    created_by INT NOT NULL,
    created_date DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_by INT,
    updated_date DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ,
    is_active BIT NOT NULL
);

INSERT INTO countries (country_name, created_by, is_active) 
VALUES ('USA', 1, 1);
INSERT INTO countries (country_name, created_by, is_active) 
VALUES ('India', 1, 1);

CREATE TABLE master_states (
    state_id INT PRIMARY KEY IDENTITY(1,1),
    state_name NVARCHAR(50) NOT NULL UNIQUE,
    country_id INT NOT NULL,
    created_by INT NOT NULL,
    created_date DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_by INT,
    updated_date DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ,
    is_active BIT NOT NULL
    FOREIGN KEY (country_id) REFERENCES countries(country_id) ON DELETE CASCADE
);

INSERT INTO master_states (state_name, country_id, created_by, is_active) 
VALUES ('California', 1, 1, 1);
INSERT INTO master_states (state_name, country_id, created_by, is_active) 
VALUES ('MH', 2, 1, 1);
INSERT INTO master_states (state_name, country_id, created_by, is_active) 
VALUES ('Panjab', 2, 1, 1);

CREATE TABLE city (
    city_id INT PRIMARY KEY  IDENTITY(1,1),
    city_name NVARCHAR(50) NOT NULL UNIQUE,
    state_id INT NOT NULL,
   
	created_by INT NOT NULL,
    created_date DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_by INT,
    updated_date DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ,
    is_active BIT NOT NULL
	 FOREIGN KEY (state_id) REFERENCES master_states (state_id) ON DELETE CASCADE
);

INSERT INTO city (city_name, state_id, created_by, is_active) 
VALUES ('Los Angeles', 1, 1, 1);

INSERT INTO city (city_name, state_id, created_by, is_active) 
VALUES ('pune', 3, 1, 1);

INSERT INTO city (city_name, state_id, created_by, is_active) 
VALUES ('p', 3 ,1, 1);

CREATE TABLE Address (
    address_id INT PRIMARY KEY IDENTITY(1,1),
    employee_id INT NOT NULL,
    street VARCHAR(255) NOT NULL,
    city_id INT NOT NULL,
    state_id INT NOT NULL,
    county_id INT NOT NULL,
    zipcode INT NOT NULL,
    is_permanent BIT NOT NULL DEFAULT 0,
    communication_address_id INT,
   created_by INT NOT NULL,
    created_date DATETIME DEFAULT CURRENT_TIMESTAMP NOT NULL,
    updated_by INT,
    updated_date DATETIME DEFAULT CURRENT_TIMESTAMP NOT NULL,
    is_active BIT NOT NULL
    FOREIGN KEY (employee_id) REFERENCES Employee (employee_id) ON DELETE CASCADE,
    FOREIGN KEY (city_id) REFERENCES city(city_id),
    FOREIGN KEY (state_id) REFERENCES master_states(state_id),
    FOREIGN KEY (county_id) REFERENCES countries(country_id),
    FOREIGN KEY (communication_address_id) REFERENCES Address(address_id)
);


INSERT INTO Address (employee_id, street, city_id, state_id, county_id, zipcode, is_permanent, communication_address_id, created_by, is_active) 
VALUES (1, '123 Main St', 1, 1, 1, 12345, 1, NULL, 1, 1);

INSERT INTO Address (employee_id, street, city_id, state_id, county_id, zipcode, is_permanent, communication_address_id, created_by, is_active) 
VALUES (1, 'katraj', 4, 3, 2, 12345, 0, NULL, 1, 1);

INSERT INTO Address (employee_id, street, city_id, state_id, county_id, zipcode, is_permanent, communication_address_id, created_by, is_active) 
VALUES (3, 'katraj', 4, 3, 2, 12345, 1, null, 1, 1);


SELECT * FROM Address;

SELECT * FROM master_states;

SELECT * FROM city;


