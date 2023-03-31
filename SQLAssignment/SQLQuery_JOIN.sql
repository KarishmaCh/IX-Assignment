create database books

use books


CREATE TABLE authors (
  id INT NOT NULL,
  first_name VARCHAR(255) NOT NULL,
  last_name VARCHAR(255) NOT NULL,
  PRIMARY KEY (id)
);

INSERT INTO authors (id, first_name, last_name)
VALUES (11, 'Ellen', 'Writer'),
       (12, 'Olga', 'Brain'),
       (13, 'Jack', 'Smart'),
       (14, 'Donald', 'Brain'),
       (15, 'Yao', 'Dou');

CREATE TABLE editors (
  id INT NOT NULL,
  first_name VARCHAR(255) NOT NULL,
  last_name VARCHAR(255) NOT NULL,
  PRIMARY KEY (id)
);

INSERT INTO editors (id, first_name, last_name)
VALUES (21, 'Daniel', 'Brown'),
       (22, 'Mark', 'Johnson'),
       (23, 'Maria', 'Jones'),
       (24, 'Cathrine', 'Roberts'),
       (25, 'Sebastian', 'Wright'),
	   (26, 'Barbara', 'Jones'),
       (27, 'Matthew', 'Smith');
       

CREATE TABLE translators (
  id INT NOT NULL,
  first_name VARCHAR(255) NOT NULL,
  last_name VARCHAR(255) NOT NULL,
  PRIMARY KEY (id)
);



INSERT INTO translators (id, first_name, last_name)
VALUES (31, 'Ira', 'Davies'),
       (32, 'Ling', 'Weng'),
       (33, 'Kristian', 'Green'),
       (34, 'Roman', 'Edwards');

CREATE TABLE books (
  id INT NOT NULL,
  title VARCHAR(255) NOT NULL,
  type VARCHAR(255) NOT NULL,
  author_id INT NOT NULL,
  editor_id INT,
  translator_id INT,
  PRIMARY KEY (id),
  FOREIGN KEY (author_id) REFERENCES authors(id),
  FOREIGN KEY (editor_id) REFERENCES editors(id),
  FOREIGN KEY (translator_id) REFERENCES translators(id)
);


INSERT INTO books (id, title, type, author_id, editor_id, translator_id)
VALUES (1, 'Time to Grow Up!', 'original', 11, 21,Null),
       (2, 'Your Trip', 'translated', 15, 22, 32),
       (3, 'Lovely Love', 'original', 14, 24, NULL),
       (4, 'Dream Your Life', 'original', 11, 24, NULL),
       (5, 'Oranges', 'translated', 12, 25, 31),
       (6, 'Your Happy Life', 'translated', 15, 22, 33),
       (7, 'Applied AI', 'translated', 13, 23, 34),
       (8, 'My Last Book', 'original', 11, Null, NULL);

	   select *from books


	   
--1.  let’s say that we want to display information about each book’s author and translator (i.e., their last names). We also want to keep the basic information about each book (i.e., id, title, and type).
	    SELECT books.id, books.title, books.type, authors.last_name AS author_last_name, translators.last_name AS translator_last_name
       FROM books
       LEFT JOIN authors ON books.author_id = authors.id
       LEFT JOIN translators ON books.translator_id = translators.id;

	   --2 We want to show the basic book information (i.e., ID and title) along with the last names of the corresponding editors
	  SELECT books.id, books.title, editors.last_name
      FROM books
      JOIN editors ON books.editor_id = editors.id;
---3  List all the Editors with their book titles by their last names
     SELECT editors.last_name AS author_last_name, books.title
      FROM books
      Right JOIN editors ON editors.id = books.editor_id
      ORDER BY editors.last_name;
    --4. List of books their Authors, Editors, Translators by book type

	SELECT 
  b.title AS book_title,
  a.first_name AS author_first_name,
  a.last_name AS author_last_name,
  e.first_name AS editor_first_name,
  e.last_name AS editor_last_name,
  t.first_name AS translator_first_name,
  t.last_name AS translator_last_name,
  b.type AS book_type
FROM books AS b
LEFT JOIN authors AS a ON b.author_id = a.id
LEFT JOIN editors AS e ON b.editor_id = e.id
LEFT JOIN translators AS t ON b.translator_id = t.id
ORDER BY b.type, b.title;